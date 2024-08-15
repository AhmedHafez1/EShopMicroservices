using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, List<string> Category, string ImagePath, decimal Price) : IRequest<CreateProductResult>;
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint
    {
    }
}
