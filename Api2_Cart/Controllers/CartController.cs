using Api2_Cart.Cart;
using Api2_Cart.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Api2_Cart.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }
        private string SessionId
        {
            get
            {
                if (!Request.Cookies.TryGetValue("sid", out var sid))
                {
                    sid = Guid.NewGuid().ToString();
                    Response.Cookies.Append("sid", sid, new CookieOptions
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict
                    });
                }
                return sid;
            }
        }
        [HttpGet]
        public IActionResult Get() => Ok(_cartService.Get(SessionId));
        [HttpPost("add")]
        public IActionResult Add(CartAddDto dto)
        {
            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var cart=_cartService.Get(SessionId);
            var item=cart.FirstOrDefault(p=>p.ProductId== dto.ProductId);
            if (item is null)
                cart.Add(new CartItem(dto.ProductId, dto.Qty));
            else
                cart[cart.IndexOf(item)]=item with { Qty = item.Qty + dto.Qty };
            return Ok();
        }
        [HttpPatch("item")]
        public IActionResult Update(CartAddDto dto)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var cart = _cartService.Get(SessionId);
            var item = cart.FirstOrDefault(p => p.ProductId == dto.ProductId);
            if (item is null)
                return NotFound();
            cart[cart.IndexOf(item)] = item with { Qty = dto.Qty };
            return Ok();
        }
        [HttpDelete("item/{productId:int}")]
        public IActionResult Delete(int productId)
        {
            var cart=_cartService.Get(SessionId);
            var item = cart.FirstOrDefault(p => p.ProductId == productId);
            if(item is null) return NotFound();
            cart.Remove(item);
            return NoContent();
        }


    }

}
