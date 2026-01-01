using System.ComponentModel.DataAnnotations;

namespace Api2_Cart.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Range(1, int.MaxValue)]
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
