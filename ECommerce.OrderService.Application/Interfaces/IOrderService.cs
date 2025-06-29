using ECommerce.OrderService.Application.DTOs;
using ECommerce.OrderService.Core.Entity;

namespace ECommerce.OrderService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task<List<OrderDto>> GetOrdersByUserIdAsync(int userId);
        Task<OrderDto> GetOrderByIdAsync(int id);
        void DeleteOrder(int id);
    }
}
