namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName);
            if (basket is null)
            {
                throw new NotFoundException($"No baskets found for user {userName}");
            }

            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            session.Store(basket);
            await session.SaveChangesAsync();

            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            var basket = await GetBasket(userName, cancellationToken);
            session.Delete(basket);

            return true;
        }

    }
}
