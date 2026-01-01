using Api2_Cart.Models;
using Microsoft.EntityFrameworkCore;

namespace Api2_Cart.Data
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
               .HasCheckConstraint(
                   "CK_Products_Price",
                   "[Price] >= 0"
               );
            modelBuilder.Entity<Order>();
            modelBuilder.Entity<OrderItem>()
               .HasCheckConstraint(
                   "CK_OrderItems_Qty",
                   "[Qty] > 0"
               );
        }
    }
}
