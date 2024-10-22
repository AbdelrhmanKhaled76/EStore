using EStore.Data;
using EStore.Models;
using EStore.Models.DTOs;
using EStore.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace EStore.Services
{
    public interface IProductService
    {
        Task<ProductDTO?> GetProduct(int id);
        Task<IEnumerable<ProductCardDTO>> GetProductTypeCards(ProductType type);
    }
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<ProductDTO?> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(x => x.Urls)
                .Include(x => x.Colors)
                .Where(x => x.IsHidden == false)
                .FirstOrDefaultAsync(x => x.Id == id);


            if (product == null)
            {
                return null;
            }

            return new ProductDTO(product);
        }

        public async Task<IEnumerable<ProductCardDTO>> GetProductTypeCards(ProductType type)
        {
            var products = await _context.Products
                .Include(x => x.Urls)
                .Include(x => x.Colors)
                .Where(x => x.IsHidden == false)
                .Where(x => x.Type == type)
                .OrderBy(x => x.Price)
                .Select(pd => new ProductCardDTO()
                {
                    Id = pd.Id,
                    Url = pd.Urls.First().Url,
                    Price = pd.Price
                })
                .ToListAsync();

            return products;
        }

    }
}
