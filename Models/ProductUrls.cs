namespace EStore.Models
{
    public class ProductUrls
    {
        public int ProductId { get; set; }
        public required string Url { get; set; }

        public Product Product { get; set; } = null!;
    }
}