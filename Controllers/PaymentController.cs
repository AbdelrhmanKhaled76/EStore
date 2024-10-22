using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EStore.Controllers.RequestModels;
using EStore.Controllers.ResponseModels;
using EStore.Models;
using EStore.Services;
using System.Diagnostics;
using Estore.Services;

namespace EStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // /api/payment/create-payment
        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment(PaymentRequest paymentRequest)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var totalPrice = await _paymentService.ValidateColors_PricesInPaymentRequest(paymentRequest);

                var intentionResponse = await _paymentService.CreateIntention(paymentRequest, totalPrice);

                var link = _paymentService.CreateLink(intentionResponse);
                return Ok(new {link});

            }
            catch (Exception ex)
            {

                return BadRequest(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }


        // /api/Payment/ProcessTransactionCallback

        [HttpPost("ProcessTransactionCallback")]
        public async Task<IActionResult> Process( Dictionary<string,string> res, [FromQuery]string hmac)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            TransactionProcessedCallBackResponse response = new TransactionProcessedCallBackResponse();

            if (!_paymentService.IsHmacCorrect(response, hmac))
            {
                // ??
                Console.WriteLine("HMAC NOT CORRECT!");
            }

            try
            {
                var dbOrder = await _paymentService.ProcessTransactionCallback(response.Obj);
                if (dbOrder is null)
                    return BadRequest();

                if (response.Obj.Success)
                {
                   await _paymentService.SendEmailAsync(dbOrder);
                }


                return Ok("Your Items were Purchased Successfully !");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.InnerException is null ? ex.Message : ex.InnerException.Message );
            }

           

        }


/*      [HttpPost("ProcessShippingData")]
        public async Task<IActionResult> ProcessShippingData([FromBody] ShippingData response)
        {

            Console.WriteLine("Transaction Processed");
            return Ok();

        }
*/



    }
}
