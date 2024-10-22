
using EStore.Controllers.RequestModels;
using EStore.Models.DTOs;
using Org.BouncyCastle.Asn1.Cms;
using System.Text.Json.Serialization;

namespace EStore.Controllers.ResponseModels
{
    public class TransactionProcessedCallBackResponse
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("obj")]
        public TransactionData Obj { get; set; }

        [JsonPropertyName("transaction_processed_callback_responses")]
        public string? TransactionProcessedCallbackResponses { get; set; }
    }

    public class TransactionData
    {
        [JsonPropertyName("id")]
        public long TransactionId { get; set; }

        [JsonPropertyName("special_reference")]
        public string SpecialReference { get; set; }

        [JsonPropertyName("pending")]
        public bool Pending { get; set; }

        [JsonPropertyName("amount_cents")]
        public decimal AmountCents { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("has_parent_transaction")]
        public bool HasParentTransaction { get; set; }

        [JsonPropertyName("is_3d_secure")]
        public bool Is3dSecure { get; set; }

        [JsonPropertyName("is_auth")]
        public bool IsAuth { get; set; }

        [JsonPropertyName("is_standalone_payment")]
        public bool IsStandAlonePayment { get; set; }

        [JsonPropertyName("is_capture")]
        public bool IsCapture { get; set; }

        [JsonPropertyName("is_voided")]
        public bool IsVoided { get; set; }

        [JsonPropertyName("is_refunded")]
        public bool IsRefunded { get; set; }

        [JsonPropertyName("integration_id")]
        public long IntegrationId { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("source_data")]
        public CardData SourceData { get; set; }

        [JsonPropertyName("merchant_commission")]
        public decimal MerchantCommission { get; set; }

        [JsonPropertyName("is_void")]
        public bool IsVoid { get; set; }

        [JsonPropertyName("is_refund")]
        public bool IsRefund { get; set; }

        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("error_occured")]
        public bool ErrorOccurred { get; set; }

        [JsonPropertyName("is_live")]
        public bool IsLive { get; set; }

        [JsonPropertyName("source_id")]
        public long SourceId { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("order")]
        public PaymobOrder Order { get; set; }

        [JsonPropertyName("owner")]
        public long Owner { get; set; }

    }

    public class Metadata
    {
        public int OrderId { get; set; }
    }

    public class PaymobOrder
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("delivery_needed")]
        public bool DeliveryNeeded { get; set; }

        [JsonPropertyName("shipping_data")]
        public ShippingData ShippingData { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("is_return")]
        public bool IsReturn { get; set; }

        [JsonPropertyName("is_cancel")]
        public bool IsCancel { get; set; }

        [JsonPropertyName("is_returned")]
        public bool IsReturned { get; set; }

        [JsonPropertyName("is_canceled")]
        public bool IsCanceled { get; set; }
       
    }

    public class ShippingData
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonPropertyName("building")]
        public string Building { get; set; }

        [JsonPropertyName("floor")]
        public string Floor { get; set; }

        [JsonPropertyName("apartment")]
        public string Apartment { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }

        [JsonPropertyName("shipping_method")]
        public string ShippingMethod { get; set; }

        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }

        public override string ToString()
        {
            return $"FullName: {FirstName + " " + LastName}\nPhone Number: {PhoneNumber}\nEmail: {Email}\nAddress: {Street}\n";
        }
    }

    public class CardData
    {
        [JsonPropertyName("pan")]
        public string Pan { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("sub_type")]
        public string SubType { get; set; }
    }

}
