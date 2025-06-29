using ECommerce.OrderService.Core.Entity;
using System.Text.Json.Serialization;

namespace ECommerce.OrderService.Application.DTOs
{
    public record OrderDto(
        int Id,
        int CustomerId,
        DateTime DateCreated,
        [property: JsonConverter(typeof(JsonStringEnumConverter))]
        OrderStatus OrderStatus,
        List<OrderItemDto> OrderItems
    );
}