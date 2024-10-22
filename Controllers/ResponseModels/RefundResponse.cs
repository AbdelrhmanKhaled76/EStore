namespace EStore.Controllers.ResponseModels
{
    // RefundResponse.cs
    public class RefundResponse
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public int AmountCents { get; set; }
        public string CreatedAt { get; set; }
        // Add any other properties returned by the Paymob API
    }
}
