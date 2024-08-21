
namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : IQuery<StoreBasketResult>;
    public record StoreBasketResult(string UserName);
    public class StoreBasketCommandHandler : IQueryHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            return new StoreBasketResult("Najah");
        }
    }
}
