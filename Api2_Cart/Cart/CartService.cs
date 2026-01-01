using Microsoft.Extensions.Caching.Memory;

namespace Api2_Cart.Cart
{
    public class CartService
    {
        private readonly IMemoryCache _cache;
        private const string Prefix = "cart_";
        public CartService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public List<CartItem> Get(string sessionId) => _cache.GetOrCreate(Prefix + sessionId, _ => new List<CartItem>())!;
        public void Clear(string sessionId)=>_cache.Remove(Prefix + sessionId);
    }
}
