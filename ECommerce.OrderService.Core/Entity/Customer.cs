namespace ECommerce.OrderService.Core.Entity
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
    }
}