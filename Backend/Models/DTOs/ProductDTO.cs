using EStore.Models.Enums;

namespace EStore.Models
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            
        }
        public ProductDTO(Product product)
        {
            Id = product.Id;
            Title = product.Title;
            Description = product.Description;
            Type = product.Type.ToString();
            Colors = product.Colors.Select(x=> x.Color.ToString()).ToArray();
            Urls = product.Urls.Select(x=> x.Url.ToString()).ToArray();
            Price = product.Price;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }

        public ICollection<string> Colors { get; set; } = new List<string>();
        public ICollection<string> Urls { get; set; } = new List<string>();
    }
}