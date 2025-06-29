namespace ECommerce.OrderService.Application.DTOs
{
    public record OrderItemDto(int ProductId, int Quantity, decimal UnitPrice);
}
