using FruitVegStore.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FruitVegStore.DTOs
{
    public class OrderDto
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }



        [Range(0.1, 1000)]
        public double QuantityOrdered { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required, MaxLength(50)]
        public string CustomerName { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Address { get; set; } = null!;
    }
}
