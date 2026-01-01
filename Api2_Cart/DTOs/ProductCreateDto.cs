using System.ComponentModel.DataAnnotations;

namespace Api2_Cart.DTOs
{
    public record ProductCreateDto([Required, MaxLength(120)] string Name, [Range(0, double.MaxValue)] decimal Price);
}
