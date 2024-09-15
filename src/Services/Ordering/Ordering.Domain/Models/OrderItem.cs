namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<OrderItemId>
    {
        public OrderItem(decimal price, int quantity, ProductId productId, OrderId orderId)
        {
            Id = OrderItemId.Of(Guid.NewGuid());
            Price = price;
            Quantity = quantity;
            ProductId = productId;
            OrderId = orderId;
        }

        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public ProductId ProductId { get; private set; }
        public OrderId OrderId { get; private set; }
    }
}