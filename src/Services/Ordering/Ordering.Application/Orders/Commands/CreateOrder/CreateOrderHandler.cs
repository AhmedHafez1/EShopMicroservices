using BuildingBlocks.CQRS;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IAppDbContext context) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = CreateOrder(command.OrderDto);

            context.Orders.Add(order);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Value);
        }

        public Order CreateOrder(OrderDto orderDto)
        {
            var order = Order.Create(
                id: OrderId.Of(orderDto.Id),
                customerId: CustomerId.Of(orderDto.CustomerId),
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.AddressLine,
                orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode),
                billingAddress: Address.Of(orderDto.BillingAddress.FirstName, orderDto.BillingAddress.LastName, orderDto.BillingAddress.EmailAddress, orderDto.BillingAddress.AddressLine,
                orderDto.BillingAddress.Country, orderDto.BillingAddress.State, orderDto.BillingAddress.ZipCode),
                payment: Payment.Of(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.CVV, orderDto.Payment.PaymentMethod)
                );

            foreach (var item in orderDto.OrderItems)
            {
                order.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
            }

            return order;
        }
    }
}
