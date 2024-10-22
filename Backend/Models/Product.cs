using EStore.Models.Enums;
using NuGet.Packaging;

namespace EStore.Models
{
    public class Product
    {
        public Product()
        {
            
        }
        public Product(ProductDTO dto)
        {
            this.Title = dto.Title;
            this.Description = dto.Description;
            this.Price = dto.Price;
            this.Type = Enum.Parse<ProductType>(dto.Type);
            this.Colors = dto.Colors.Select(x=> new ProductColors() { Color = Enum.Parse<ProductColor>(x), ProductId = dto.Id}).ToList();
            this.Urls = dto.Urls.Select(x=> new ProductUrls() { Url = x,  ProductId = dto.Id}).ToList();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ProductType Type { get; set; }
        public bool IsHidden { get; set; } 
        public decimal Price { get; set; }

        public ICollection<ProductColors> Colors { get; set; } = new List<ProductColors>();
        public ICollection<ProductUrls> Urls { get; set; } = new List<ProductUrls>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); 


        public void UpdateFromDTO(ProductDTO dto)
        {
            Description = dto.Description;
            Title = dto.Title;
            Type = Enum.Parse<ProductType>(dto.Type);
            Colors = dto.Colors.Select(x => new ProductColors() { Color = Enum.Parse<ProductColor>(x), ProductId = dto.Id }).ToList();
        }
    }
}