using ECommerce.OrderService.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.OrderService.Infrastructure
{
    public class AppDbContext(DbContextOptions options):DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
                new List<Product>
                {
                    new()
                    {
                        Id = 1,Name = "Product 1", Stock = 100, Price = 10.99m
                    },
                    new()
                    {
                        Id = 2,Name = "Product 2", Stock = 50, Price = 20.99m
                    },
                    new()
                    {
                        Id = 3,Name = "Product 3", Stock = 200, Price = 5.99m
                    }
                }
            );
        }
    }
}
