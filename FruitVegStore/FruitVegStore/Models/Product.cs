using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FruitVegStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        [Range(0.01, 1000)]
        public decimal PricePerKg { get; set; }

        [Range(0, 10000)]
        public double QuantityInStock { get; set; }

        public DateTime ExpirationDate { get; set; }

        [Required]
        public bool IsAvailable { get; set; }
       
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        
        public Category? Category { get; set; }
      
        public ICollection<Order>? Orders { get; set; }
    }
}
