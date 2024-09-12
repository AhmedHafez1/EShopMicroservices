using Ordering.Domain.Abstractions;

namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<Guid>
    {
        public OrderItem(decimal price, int quantity, Guid productId, Guid orderId)
        {
            Price = price;
            Quantity = quantity;
            ProductId = productId;
            OrderId = orderId;
        }

        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid OrderId { get; private set; }
    }
}