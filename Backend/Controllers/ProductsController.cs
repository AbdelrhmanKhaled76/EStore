using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EStore.Data;
using EStore.Models;
using EStore.Models.DTOs;
using EStore.Services;
using EStore.Models.Enums;

namespace EStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly AppDbContext _context;

        public ProductsController(IProductService service, AppDbContext context)
        {
            _productService = service;
            _context = context;
        }


        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }


        [HttpGet("MenWallets")]
        public async Task<IActionResult> GetMenWallets()
        {
            var product = await _productService.GetProductTypeCards(ProductType.MenWallet);
            
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        
        [HttpGet("WomenWallets")]
        public async Task<IActionResult> GetWomenWallets()
        {
            var product = await _productService.GetProductTypeCards(ProductType.WomenWallet);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        
        [HttpGet("Bags")]
        public async Task<IActionResult> GetBags()
        {
            var product =  await _productService.GetProductTypeCards(ProductType.Bag);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        
        [HttpGet("Accessories")]
        public async Task<IActionResult> GetAccessories()
        {
            var product =  await _productService.GetProductTypeCards(ProductType.Accessory);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        // /api/products

        /*[HttpPost]
        public async Task<IActionResult> AddRangeProduct([FromBody] List<ProductDTO> dtos)
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var orderedDtos = dtos.ToList();
                    await _context.Products.AddRangeAsync(orderedDtos.Select(x => new Product(x)));

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.InnerException?.Message);
                }

            }
        }*/

    }
}
