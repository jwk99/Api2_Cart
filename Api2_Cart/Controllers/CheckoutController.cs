using Api2_Cart.Cart;
using Api2_Cart.Data;
using Api2_Cart.DTOs;
using Api2_Cart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Api2_Cart.Controllers
{
    [ApiController]
    [Route("api/checkout")]
    public class CheckoutController : ControllerBase
    {
        private readonly CartContext _cartContext;
        private readonly CartService _cartService;

        public CheckoutController(CartContext cartContext, CartService cartService)
        {
            _cartContext = cartContext;
            _cartService = cartService;
        }
        private string SessionId => Request.Cookies["sid"]!;
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutResultDto dto)
        {
            var cart=_cartService.Get(SessionId);
            if (!cart.Any())
                return Conflict("Cart is empty");
            using var tx = await _cartContext.Database.BeginTransactionAsync();
            var order = new Order();
            _cartContext.Orders.Add(order);
            await _cartContext.SaveChangesAsync();

            foreach(var c in cart)
            {
                var product = await _cartContext.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == c.ProductId);
                if(product is null)
                    return NotFound($"Product {c.ProductId} not found");
                _cartContext.OrderItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Qty = c.Qty,
                    Price = product.Price
                });
            }
            await _cartContext.SaveChangesAsync();
            await tx.CommitAsync();
            var total = await _cartContext.OrderItems
                .Where(i=>i.OrderId==order.Id)
                .SumAsync(i=>i.Qty*i.Price);
            _cartService.Clear(SessionId);
            return Created($"/api/orders/{order.Id}", new CheckoutResultDto(order.Id, total));
        }
    }
}
