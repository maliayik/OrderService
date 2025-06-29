using ECommerce.OrderService.Application.DTOs;
using ECommerce.OrderService.Application.Extensions;
using ECommerce.OrderService.Application.Interfaces;
using ECommerce.OrderService.Core.Entity;
using ECommerce.OrderService.Core.Interfaces;

namespace ECommerce.OrderService.Application.Services
{
    public class OrderService(IProductRepository productRepository, IOrderRepository orderRepository) : IOrderService
    {
        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            if (orderDto is null)
                throw new ArgumentNullException(nameof(orderDto));

            foreach (var item in orderDto.OrderItems)
            {
                if (!await productRepository.IsStockAvailableAsync(item.ProductId, item.Quantity))
                    throw new Exception($"Stok yetersiz: Ürün ID {item.ProductId}");
            }

            foreach (var item in orderDto.OrderItems)
            {
                var product = await productRepository.GetByIdAsync(item.ProductId);
                if (product != null)
                { 
                    product.Stock -= item.Quantity; 
                    await productRepository.UpdateAsync(product);
                }
            }

            var order = orderDto.ToEntity();
            await orderRepository.AddAsync(order);
            return order.ToDto();
        }

        public async Task<List<OrderDto>> GetOrdersByUserIdAsync(int userId)
        {
            var orderByUserId = await orderRepository.GetByUserIdAsync(userId);
            if (!orderByUserId.Any())
            {
                throw new Exception("Kullanıcıya ait sipariş bulunmadı");
            }
            return orderByUserId.Select(o => o.ToDto()).ToList();

        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var orderById = await orderRepository.GetByIdAsync(id);
            if (orderById is null)
            {
                throw new Exception($"{id} numaralı sipariş bulunamadı");
            }
            return orderById.ToDto();
        }

        public void DeleteOrder(int id)
        {
            var order = orderRepository.GetByIdAsync(id).Result;
            if (order is null)
            {
                throw new Exception($"{id} numaralı silenecek sipariş bulunamadı");
            }
            orderRepository.Delete(id);
        }
    }
}
