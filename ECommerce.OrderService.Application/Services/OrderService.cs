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

            var missingProductIds = productIds.Except(products.Select(p => p.Id)).ToList();
            if (missingProductIds.Any())
                throw new Exception($"Sistemde bulunamayan ürün ID'leri: [{string.Join(", ", missingProductIds)}]");

            var insufficientProducts = new List<string>();
            
            foreach (var item in orderDto.OrderItems)
            {
                var product = products.First(p => p.Id == item.ProductId);

                if (product.Stock < item.Quantity)
                {
                    insufficientProducts.Add(product.Name);
                    continue;
                }
               
                product.Stock -= item.Quantity;
                await productRepository.UpdateAsync(product);
            }

            if (insufficientProducts.Any())
                throw new Exception($"Sipariş oluşturulamadı. Stok yetersiz: {string.Join(", ", insufficientProducts)}");

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