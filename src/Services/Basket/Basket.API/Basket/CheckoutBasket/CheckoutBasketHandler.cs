
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckout) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);
    public class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasketAsync(command.BasketCheckout.UserName);

            if (basket == null)
                return new CheckoutBasketResult(false);

            var checkoutEvent = command.BasketCheckout.Adapt<BasketCheckoutEvent>();
            checkoutEvent.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(checkoutEvent, cancellationToken);

            await repository.DeleteBasketAsync(command.BasketCheckout.UserName, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }
}
