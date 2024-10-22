namespace EStore.Models.DTOs
{
    public class ProductCardDTO
    {
        public ProductCardDTO()
        {
            
        }
        public ProductCardDTO(Product pd)
        {
            Id = pd.Id;
            Url = pd.Urls.First().Url;
            Price = pd.Price;
            
        }
        public int Id { get; set; }
        public string Url { get; set; }
        
        public decimal Price { get; set; }
    }
}
