using ECommerce.OrderService.Core.Entity;
using ECommerce.OrderService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.OrderService.Infrastructure.Repositories
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetOrderByCustomerIdAsync(int customerId)
        {
            return await context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }

        public Task Delete(Order order)
        {
            context.Orders.Remove(order!);
            context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}