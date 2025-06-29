using ECommerce.OrderService.Application.DTOs;
using ECommerce.OrderService.Core.Entity;

namespace ECommerce.OrderService.Application.Extensions;

public static class OrderMappingExtensions
{
    public static Order ToEntity(this CreateOrderDto dto, List<Product> products)
    {
        var orderItems = dto.OrderItems.Select(item =>
        {
            var product = products.FirstOrDefault(p => p.Id == item.ProductId);
            if (product == null)
                throw new Exception($"Ürün bulunamadı: ID {item.ProductId}");

            return new OrderItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            };
        }).ToList();

        return new Order
        {
            CustomerId = dto.CustomerId,
            DateCreated = DateTime.UtcNow,
            OrderStatus = OrderStatus.Pending,
            OrderItems = orderItems
        };
    }

    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto(
            order.Id,
            order.CustomerId,
            order.DateCreated,
            order.OrderStatus,
            order.OrderItems.Select(oi =>
                new OrderItemDto(
                    oi.ProductId,
                    oi.Product?.Name ?? "Bilinmiyor",
                    oi.Quantity,
                    oi.UnitPrice,
                    oi.Quantity * oi.UnitPrice
                )).ToList()
        );
    }

    public static Order ToEntity(this OrderDto dto)
    {
        return new Order
        {
            Id = dto.Id,
            CustomerId = dto.CustomerId,
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
}