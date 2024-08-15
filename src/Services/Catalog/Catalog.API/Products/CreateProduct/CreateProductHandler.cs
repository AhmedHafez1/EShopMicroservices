using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, List<string> Category, string ImagePath, decimal Price) : IRequest<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Category = command.Category,
                ImagePath = command.ImagePath,
                Price = command.Price,
            };

            // Create product in db and save changes

            return new CreateProductResult(new Guid());
        }
    }
}
