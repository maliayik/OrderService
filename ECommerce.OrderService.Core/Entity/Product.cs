using System.ComponentModel.DataAnnotations;

namespace ECommerce.OrderService.Core.Entity
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}