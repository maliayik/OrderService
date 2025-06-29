using ECommerce.OrderService.Core.Entity;

namespace ECommerce.OrderService.Application.DTOs
{
    public record OrderDto(
        int Id,
        int UserId,
        DateTime DateCreated,
        OrderStatus OrderStatus,
        List<OrderItemDto> OrderItems
    );
}
