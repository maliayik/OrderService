using ECommerce.OrderService.Core.Entity;

namespace ECommerce.OrderService.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task<List<Order>> GetByUserIdAsync(int userId);
        Task AddAsync(Order order);
        void Delete(int id);
    }
}
