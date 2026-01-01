using System.ComponentModel.DataAnnotations;

namespace Api2_Cart.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, MaxLength(120)]
        public string Name { get; set; } = default!;
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
