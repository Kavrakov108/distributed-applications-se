using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FruitVegStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
       
        public Product? Product { get; set; }

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
