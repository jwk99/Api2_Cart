using Api2_Cart.Data;
using Api2_Cart.DTOs;
using Api2_Cart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api2_Cart.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly CartContext _cartContext;
        public ProductsController(CartContext cartContext)
        {
            _cartContext = cartContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _cartContext.Products
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Price
                }).ToListAsync();
            return Ok(products);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };
            _cartContext.Products.Add(product);
            await _cartContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(GetAll),
                new { id = product.Id },
                product);
        }
    }
}
