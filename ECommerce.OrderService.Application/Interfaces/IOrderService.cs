using ECommerce.OrderService.Application.DTOs;
using ECommerce.OrderService.Core.Entity;

namespace ECommerce.OrderService.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto);
        Task<List<OrderDto>> GetOrderByCustomerIdAsync(int customerId);
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task DeleteOrderById(int id);
    }
}
