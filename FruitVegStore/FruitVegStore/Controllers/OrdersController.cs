using FruitVegStore.Data;
using FruitVegStore.DTOs;
using FruitVegStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FruitVegStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.Product)
                .Select(o => MapToDto(o))
                .ToListAsync();

            return Ok(orders);
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            return Ok(MapToDto(order));
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderDto dto)
        {
            var order = MapToEntity(dto);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Зареждаме навигационното свойство Product, за да върнем името на продукта
            await _context.Entry(order).Reference(o => o.Product).LoadAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, MapToDto(order));
        }

        // PUT: api/orders/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDto dto)
        {
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder == null)
                return NotFound();

            // Актуализиране на съществуващата поръчка
            existingOrder.ProductId = dto.ProductId;
            existingOrder.QuantityOrdered = dto.QuantityOrdered;
            existingOrder.OrderDate = dto.OrderDate;
            existingOrder.CustomerName = dto.CustomerName;
            existingOrder.Address = dto.Address;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/orders/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Mapping methods
        private static OrderDto MapToDto(Order order) => new OrderDto
        {
           
            ProductId = order.ProductId,
            QuantityOrdered = order.QuantityOrdered,
            OrderDate = order.OrderDate,
            CustomerName = order.CustomerName,
            Address = order.Address
            
        };

        private static Order MapToEntity(OrderDto dto) => new Order
        {
            ProductId = dto.ProductId,
            QuantityOrdered = dto.QuantityOrdered,
            OrderDate = dto.OrderDate,
            CustomerName = dto.CustomerName,
            Address = dto.Address
        };
    }
}
