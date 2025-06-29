using ECommerce.OrderService.Application.DTOs;
using ECommerce.OrderService.Application.Extensions;
using ECommerce.OrderService.Application.Interfaces;
using ECommerce.OrderService.Core.Interfaces;

namespace ECommerce.OrderService.Application.Services
{
    public class OrderService(IProductRepository productRepository, IOrderRepository orderRepository) : IOrderService
    {
        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            ArgumentNullException.ThrowIfNull(orderDto);

            var productIds = orderDto.OrderItems.Select(i => i.ProductId).Distinct().ToList();
            var products = await productRepository.GetByIdsAsync(productIds);

            foreach (var item in orderDto.OrderItems)
            {
                if (!await productRepository.IsStockAvailableAsync(item.ProductId, item.Quantity))
                    throw new Exception($"{products.Select(w => w.Name)}Sipariş oluşturulamadı, yetersiz stok");
            }

            foreach (var item in orderDto.OrderItems)
            {
                var product = products.FirstOrDefault(p => p.Id == item.ProductId);
                if (product != null)
                {
                    product.Stock -= item.Quantity;
                    await productRepository.UpdateAsync(product);
                }
            }

            var order = orderDto.ToEntity(products);
            await orderRepository.AddAsync(order);
            return order.ToDto();
        }

        public async Task<List<OrderDto>> GetOrderByCustomerIdAsync(int customerId)
        {
            var orderByCustomerId = await orderRepository.GetOrderByCustomerIdAsync(customerId);
            if (orderByCustomerId.Count == 0)
            {
                throw new Exception("Kullanıcıya ait sipariş bulunmadı");
            }
            return orderByCustomerId.Select(o => o.ToDto()).ToList();
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var orderById = await orderRepository.GetByIdAsync(id);
            if (orderById == null)
            {
                throw new Exception($"{id} numaralı sipariş bulunamadı");
            }
            return orderById.ToDto();
        }

        public async Task DeleteOrderById(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);
            if (order is null)
                throw new Exception("Sipariş bulunamadı");
            await orderRepository.Delete(order);
        }
    }
}