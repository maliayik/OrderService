namespace ECommerce.OrderService.Application.DTOs
{
    public record CreateOrderDto(int CustomerId, List<CreateOrderItemDto> OrderItems);
    public record CreateOrderItemDto(int ProductId, int Quantity);
}