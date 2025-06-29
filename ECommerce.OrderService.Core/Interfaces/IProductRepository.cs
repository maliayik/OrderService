using ECommerce.OrderService.Core.Entity;

namespace ECommerce.OrderService.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetByIdsAsync(IEnumerable<int> ids);

        Task<bool> IsStockAvailableAsync(int productId, int quantity);

        Task UpdateAsync(Product product);
    }
}