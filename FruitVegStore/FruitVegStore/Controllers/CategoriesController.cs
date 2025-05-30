using FruitVegStore.Data;
using FruitVegStore.DTOs;
using FruitVegStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitVegStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .Select(c => MapToReadDto(c))
                .ToListAsync();

            return Ok(categories);
        }

        // GET: api/categories/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            return Ok(MapToReadDto(category));
        }

        // POST: api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> PostCategory(CategoryDto dto)
        {
            var category = MapToEntity(dto);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, MapToReadDto(category));
        }

        // PUT: api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, CategoryDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            category.Name = dto.Name;
            category.Description = dto.Description;
            category.CreatedAt = dto.CreatedAt;
            category.Active = dto.Active;
            category.Discount = dto.Discount;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/categories/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Mapping methods
        private static CategoryDto MapToReadDto(Category category) => new CategoryDto
        {
           
            Name = category.Name,
            Description = category.Description,
            CreatedAt = category.CreatedAt,
            Active = category.Active,
            Discount = category.Discount,
           
        };

        private static Category MapToEntity(CategoryDto dto) => new Category
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = dto.CreatedAt,
            Active = dto.Active,
            Discount = dto.Discount
        };
    }
}
