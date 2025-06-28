using System.ComponentModel.DataAnnotations;

namespace ECommerce.OrderService.Core.Entity
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<OrderItem>? Items { get; set; }
    }
}
