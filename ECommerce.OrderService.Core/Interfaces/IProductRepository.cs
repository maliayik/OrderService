using ECommerce.OrderService.Core.Entity;

namespace ECommerce.OrderService.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<bool> IsStockAvailableAsync(int productId, int quantity);
        Task UpdateAsync(Product product); 
    }
}
