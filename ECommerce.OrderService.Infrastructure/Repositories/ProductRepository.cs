using ECommerce.OrderService.Core.Entity;
using ECommerce.OrderService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.OrderService.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<bool> IsStockAvailableAsync(int productId, int quantity)
        {
            var product = await context.Products.FindAsync(productId);
            return product != null && product.Stock >= quantity;
        }

        public async Task UpdateAsync(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }

    }
}
