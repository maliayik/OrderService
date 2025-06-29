using ECommerce.OrderService.Core.Entity;

namespace ECommerce.OrderService.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task<List<Order>> GetOrderByCustomerIdAsync(int customerId);
        Task AddAsync(Order order);
        Task Delete(Order order);
    }
}
