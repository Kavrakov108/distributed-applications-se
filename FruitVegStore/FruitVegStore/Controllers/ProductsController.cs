using FruitVegStore.Data;
using FruitVegStore.DTOs;
using FruitVegStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitVegStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

       
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Select(p => MapToDto(p))
                .ToListAsync();

            return Ok(products);
        }

        // GET: api/products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            return Ok(MapToDto(product));
        }

     
       [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto dto)
        {
            var product = MapToEntity(dto);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, MapToDto(product));
        }

        // PUT: api/products/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto dto)
        {
            if (id <= 0) return BadRequest();

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null) return NotFound();

            // Update fields
            existingProduct.Name = dto.Name;
            existingProduct.PricePerKg = dto.PricePerKg;
            existingProduct.QuantityInStock = dto.QuantityInStock;
            existingProduct.ExpirationDate = dto.ExpirationDate;
            existingProduct.IsAvailable = dto.IsAvailable;
            existingProduct.CategoryId = dto.CategoryId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Mapping methods
        private static ProductDto MapToDto(Product p) => new ProductDto
        {
            Name = p.Name,
            PricePerKg = p.PricePerKg,
            QuantityInStock = p.QuantityInStock,
            ExpirationDate = p.ExpirationDate,
            IsAvailable = p.IsAvailable,
            CategoryId = p.CategoryId
        };

        private static Product MapToEntity(ProductDto dto) => new Product
        {
            Name = dto.Name,
            PricePerKg = dto.PricePerKg,
            QuantityInStock = dto.QuantityInStock,
            ExpirationDate = dto.ExpirationDate,
            IsAvailable = dto.IsAvailable,
            CategoryId = dto.CategoryId
        };
    }
}
