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
                .FirstOrDefaultAsync(o => o.Id == id);
        }


        public async Task<List<Order>> GetByUserIdAsync(int userId)
        {
            return await context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }


        public async Task AddAsync(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }


        public void Delete(int id)
        {
            var order = context.Orders.Find(id);
            if (order is not null)
            {
                context.Orders.Remove(order);
            }
        }

    }
}
