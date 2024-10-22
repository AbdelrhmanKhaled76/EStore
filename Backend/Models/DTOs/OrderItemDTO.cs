using EStore.Models.Enums;
using System.Text.Json.Serialization;

namespace EStore.Models.DTOs
{
    public class OrderItemDTO
    {
        [JsonPropertyName("id")]
        public int ProductId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; } // in to string / 100

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        public int OrderId { get; set; }

        public override string ToString()
        {
            return $"ProdId: {ProductId}, Title: {Title}, Color:{Color}, Quantity: {Quantity} ,Price: {Price/100m}\n";
        }

    }
}
