using System.ComponentModel.DataAnnotations;

namespace ECommerce.OrderService.Core.Entity
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public decimal Price { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
