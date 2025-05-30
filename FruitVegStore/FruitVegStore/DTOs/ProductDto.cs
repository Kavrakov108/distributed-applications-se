using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FruitVegStore.DTOs
{
    public class ProductDto
    {
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
    }
}
