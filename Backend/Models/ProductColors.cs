using EStore.Models.Enums;

namespace EStore.Models
{
    public class ProductColors
    {
        public int ProductId { get; set; }
        public required ProductColor Color { get; set; }

        public Product Product { get; set; } = null!;
    }
}