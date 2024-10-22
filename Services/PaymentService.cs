using EStore.Controllers.ResponseModels;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using EStore.Controllers.RequestModels;
using System.Security.Cryptography;
using System.Net;
using static System.Net.WebRequestMethods;
using EStore.Data;
using EStore.Models.Enums;
using EStore.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Immutable;
using System.Collections.Frozen;
using EStore.Models;
using System.Threading.Tasks;
using MailKit.Search;
using Estore.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EStore.Services
{
    public interface IPaymentService
    {
        Task<IntentionResponse> CreateIntention(PaymentRequest request, decimal totalPrice);
        bool IsHmacCorrect(TransactionProcessedCallBackResponse response, string hmac);
        string CreateLink(IntentionResponse response);
        Task<decimal> ValidateColors_PricesInPaymentRequest(PaymentRequest request);
        Task<Order?> ProcessTransactionCallback(TransactionData data);
        Task<bool> SendEmailAsync(Order dbOrder);
    }

    public class PaymentService : IPaymentService
    {
        private readonly string _publicKey;
        private readonly string _secretKey;
        private readonly string _walletsIntegrationId;
        private readonly string _cardsIntegrationId;
        private readonly string _profileHMACSecret;
        private readonly string _paymentLink;
        private readonly AppDbContext _context;
        private readonly IEmailingService _emailingService;


        public PaymentService(IConfiguration configuration, AppDbContext context, IEmailingService emailingService)
        {
            _publicKey = configuration.GetSection("Authentication")["Paymob:PublicKey"] ?? throw new Exception();
            _secretKey = configuration.GetSection("Authentication")["Paymob:SecretKey"] ?? throw new Exception();
            _cardsIntegrationId = configuration.GetSection("Authentication")["Paymob:CardsIntegrationId"] ?? throw new Exception();
            _walletsIntegrationId = configuration.GetSection("Authentication")["Paymob:WalletsIntegrationId"] ?? throw new Exception();
            _profileHMACSecret = configuration.GetSection("Authentication")["Paymob:HMAC"] ?? throw new Exception();
            _paymentLink = $"https://accept.paymob.com/unifiedcheckout/?publicKey={_publicKey}&clientSecret=";
            _context = context;
            _emailingService = emailingService;

        }



        public async Task<IntentionResponse> CreateIntention(PaymentRequest request, decimal totalPrice)
        {
            var paymentMethod = Enum.Parse<PaymentMethod>(request.BillingData.PaymentMethod.ToLower());

            var order = new Order()
            {
                Status = "Created",
                CreatedAt = DateTime.Now,
                OrderItems = request.Items.Select(x => new OrderItem(x)).ToList(),
                PaymentMethod = paymentMethod,
                RemainingCash = paymentMethod == PaymentMethod.cash ? totalPrice / 2m : 0m,
                TotalPrice = paymentMethod == PaymentMethod.cash ? totalPrice / 2m : totalPrice
            };

 
            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();

            if (paymentMethod == PaymentMethod.cash)
            {
                totalPrice = totalPrice / 2m;

                request.Items.ForEach(x =>
                {
                    x.Price = x.Price / 2m;
                });
            }

            var requestData = new
            {
                amount = totalPrice * 100m,
                currency = "EGP",
                payment_methods = new[] { paymentMethod.ToString() },
                items = request.Items.Select(x => new
                {
                    name = x.Title,
                    amount = x.Price * 100m, // for a single Item (with no quantity)
                    quantity = x.Quantity,

                }),
                billing_data = request.BillingData,
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", _secretKey);

                try
                {
                    var response = await client.PostAsJsonAsync("https://accept.paymob.com/v1/intention/", requestData);
                    response.EnsureSuccessStatusCode(); // Throw if not a success code.

                    var responseData = await response.Content.ReadAsStringAsync();

                    return await response.Content.ReadFromJsonAsync<IntentionResponse>()
                                                        ?? throw new HttpRequestException("Response is null");
                }
                catch (HttpRequestException)
                {
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                    throw;
                }
            }

        }

        public string CreateLink(IntentionResponse response)
        {
            return _paymentLink + response.client_secret;
        }


        public bool IsHmacCorrect(TransactionProcessedCallBackResponse response, string hmac)
        {
            var hmacResponseData = MakeHmacString(response);
            var calculatedHmac = CalculateHMAC(hmacResponseData);
            return calculatedHmac == hmac;
        }


        private string CalculateHMAC(string data)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(_profileHMACSecret)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        private string MakeHmacString(TransactionProcessedCallBackResponse response)
        {
            var transaction = response.Obj;
            return $"{transaction.AmountCents}" +
                $"{transaction.CreatedAt.ToString("yyyy-MM-ddTHH:mm:ss.ffffff")}" +
                $"{transaction.Currency}" +
                $"{transaction.ErrorOccurred}" +
                $"{transaction.HasParentTransaction}{transaction.TransactionId}{transaction.IntegrationId}" +
                $"{transaction.Is3dSecure}" +
                $"{transaction.IsAuth}" +
                $"{transaction.IsCapture}" +
                $"{transaction.IsRefunded}" +
                $"{transaction.IsStandAlonePayment}" +
                $"{transaction.IsVoided}" +
                $"{transaction.Order.Id}" +
                $"{transaction.Owner}{transaction.Pending}" +
                $"{transaction.SourceData.Pan}{transaction.SourceData.SubType}{transaction.SourceData.Type}" +
                $"{transaction.Success}";
        }

        public async Task<decimal> ValidateColors_PricesInPaymentRequest(PaymentRequest request)
        {
            var items = request.Items;

            var ids = items.Select(x => x.ProductId);

            var products = await (from product in _context.Products.Include(x => x.Colors)
                                  where ids.Contains(product.Id)
                                  select new { product.Id, product.Price, product.Colors })
                           .AsNoTracking()
                           .ToDictionaryAsync(x => x.Id, x => new { x.Price, x.Colors });

            decimal totalPrice = 0;

            foreach (var item in items)
            {
                if (item.Price != products[item.ProductId].Price)
                {
                    throw new ArgumentException("Price is invalid!");
                }

                if(!products[item.ProductId].Colors.Select(x => x.Color).Contains(Enum.Parse<ProductColor>(item.Color)))
                {
                    throw new ArgumentException("Price is invalid!");
                }
                totalPrice += item.Price * item.Quantity;
            }

            var payMethod = Enum.Parse<PaymentMethod>(request.BillingData.PaymentMethod.ToLower());

            return totalPrice;
        }

        public async Task<Order?> ProcessTransactionCallback(TransactionData data)
        {

            if (!int.TryParse(data.SpecialReference, out int orderId) )
                return null;

            var dbOrder = await _context.Orders
                .Include(x=> x.OrderItems)
                .ThenInclude(x=> x.Product)
                .FirstOrDefaultAsync(x => x.Id == orderId);
            
            if (dbOrder == null)
                return null;

            dbOrder.Transaction = new PaymobTransaction(data);
            dbOrder.Client = new Client(data.Order.ShippingData);

            await _context.SaveChangesAsync();

            return dbOrder;
        }

        public async Task<bool> SendEmailAsync(Order dbOrder)
        {
            if (dbOrder == null)
                return false;

            string body = string.Empty;

            body += $"{dbOrder.Client}\n";
            body += $"PaymentMethod: {dbOrder.PaymentMethod}, TotalPrice: {dbOrder.TotalPrice}, RemainingCash: {dbOrder.RemainingCash}\n\n";

            string orderStr = string.Empty;
            foreach (var item in dbOrder.OrderItems)
            {
                orderStr += item.ToString();
            }
            body += orderStr + "\n";

            await _emailingService.SendEmailAsync(new EmailRequest()
            {
                RecipientName = $"عبد الرحمن خالد محمد",
                Subject = "A New Order Has been Successfully Purchased",
                To = "bodi.khaled24@gmail.com",
                Body = body               
            });

            return true;
        }

    }
}


// https://accept.paymob.com/unifiedcheckout/?publicKey=egy_pk_test_RsbXPHclwbLwyfZWeiICf6Rgr24hzQwm&clientSecret=

// https://accept.paymobsolutions.com/api/acceptance/post_pay // default redirect


/*  public async Task<TokenResponse?> AuthenticatePaymob()
        {
            var request = new
            {
                api_key = _apiKey
            };

            var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/auth/tokens", request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TokenResponse>();
        }

        public async Task<OrderResponse?> RegisterOrder(string token, PaymentRequest request)
        {
            var orderRequest = new
            {
                auth_token = token,
                delivery_needed = "false",
                amount_cents = request.Amount * 100,
                currency = "EGP",
                items = new object[] { request.Cart }  // Add items if needed
            };

            var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/ecommerce/orders", orderRequest);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<OrderResponse>();
        }

        public async Task<PaymentKeyResponse?> GetPaymentKey(string token, string orderId, PaymentRequest request)
        {
            var paymentKeyRequest = new
            {
                auth_token = token,
                amount_cents = request.Amount * 100,
                expiration = 3600,
                order_id = orderId,
                billing_data = new
                {
                    email = request.Email,
                    first_name = request.FirstName,
                    last_name = request.LastName,
                    phone_number = request.PhoneNumber,
                },
                currency = "EGP",
                integration_id = _integrationId
            };

            var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/acceptance/payment_keys", paymentKeyRequest);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PaymentKeyResponse>();
        }


        public Uri PaymentUriPage(string paymentKey)
        {
            var iframURL = $"https://accept.paymob.com/api/acceptance/iframes/863458?payment_token={paymentKey}";

            return new Uri(iframURL);
        }

        public async Task<RefundResponse?> RefundTransaction(string token, RefundRequest request)
        {
            var refundRequest = new
            {
                auth_token = token,
                transaction_id = request.TransactionId,
                amount_cents = request.AmountCents,
                refund_reason = request.RefundReason
            };

            var response = await _httpClient.PostAsJsonAsync("https://accept.paymob.com/api/acceptance/void_refund/refund", refundRequest);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RefundResponse>();
        }*/