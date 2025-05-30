using System.ComponentModel.DataAnnotations;

namespace FruitVegStore.DTOs
{
    public class CategoryDto
    {
        [Required, MaxLength(30)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
          
        [Required]
        public bool Active { get; set; }

        public double? Discount { get; set; }
    }
}
