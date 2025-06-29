using System.ComponentModel.DataAnnotations;

namespace ECommerce.OrderService.Core.Entity
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
