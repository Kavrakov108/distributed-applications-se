using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FruitVegStore.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public bool Active { get; set; }

        public double? Discount { get; set; }

       
        public ICollection<Product>? Products { get; set; }
    }
}
