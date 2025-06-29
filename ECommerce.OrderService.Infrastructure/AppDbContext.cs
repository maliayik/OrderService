using ECommerce.OrderService.Core.Entity;
using Microsoft.EntityFrameworkCore;


namespace ECommerce.OrderService.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(p => p.Stock).IsRequired();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasMany(o => o.OrderItems)
                    .WithOne(oi => oi.Order)
                    .HasForeignKey(oi => oi.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.Property(oi => oi.Quantity)
                    .IsRequired();

                entity.Property(oi => oi.UnitPrice)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(oi => oi.Product)
                    .WithMany()
                    .HasForeignKey(oi => oi.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Product>().HasData(
                new List<Product>
                {
                    new()
                    {
                        Id = 1,Name = "Keyboard", Stock = 100, Price = 10.99m
                    },
                    new()
                    {
                        Id = 2,Name = "Mouse", Stock = 50, Price = 20.99m
                    },
                    new()
                    {
                        Id = 3,Name = "Headset", Stock = 200, Price = 5.99m
                    }
                }
            );

            modelBuilder.Entity<Customer>().HasData(
                new List<Customer>
                {
                    new()
                    {
                        Id = 1, Name = "Mehmet", Address = "Istanbul"
                    },
                    new()
                    {
                        Id = 2, Name = "Ahmet", Address = "Ankara"
                    },
                    new()
                    {
                        Id = 3, Name = "Fatma", Address = "Izmir"
                    }

                });


        }
    }
}
