
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);
    public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Handler DeleteProductCommandHandler has been called with id {command.Id}");

            var product = await session.LoadAsync<Product>(command.Id);

            if (product == null)
            {
                throw new NotFoundException("Product not existing!");
            }

            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync();

            return new DeleteProductResult(true);
        }
    }
}
