using ECommerce.OrderService.Application.DTOs;
using ECommerce.OrderService.Core.Entity;

namespace ECommerce.OrderService.Application.Extensions;

public static class OrderMappingExtensions
{
    public static Order ToEntity(this OrderDto dto)
    {
        return new Order
        {
            Id = dto.Id,
            UserId = dto.UserId,
            DateCreated = dto.DateCreated,
            OrderStatus = dto.OrderStatus,
            OrderItems = dto.OrderItems.Select(oi => new OrderItem
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList()
        };
    }

    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto(
            order.Id,
            order.UserId,
            order.DateCreated,
            order.OrderStatus,
            order.OrderItems.Select(oi =>
                new OrderItemDto(
                    oi.ProductId,
                    oi.Quantity,
                    oi.UnitPrice
                )).ToList()
        );
    }
}