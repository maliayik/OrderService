using ECommerce.OrderService.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.OrderService.Infrastructure
{
    public class AppDbContext(DbContextOptions options):DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
