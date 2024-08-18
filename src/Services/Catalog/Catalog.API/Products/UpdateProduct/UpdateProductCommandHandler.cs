
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImagePath, decimal Price)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Handler UpdateProductCommandHandler has been called for product with id {command.Id}");
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is null)
            {
                throw new NotFoundException("Product not found!");
            }

            product.Name = command.Name;
            product.Description = command.Description;
            product.Category = command.Category;
            product.ImagePath = command.ImagePath;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync();

            return new UpdateProductResult(true);
        }
    }
}
