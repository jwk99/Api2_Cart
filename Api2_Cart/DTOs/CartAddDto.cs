using System.ComponentModel.DataAnnotations;

namespace Api2_Cart.DTOs
{
    public record CartAddDto([Required] int ProductId, [Range(1, int.MaxValue)] int Qty);
}
