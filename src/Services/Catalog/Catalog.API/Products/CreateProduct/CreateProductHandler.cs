using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, List<string> Category, string ImagePath, decimal Price) : IRequest<CreateProductResult>;
    public record CreateProductResult(Guid Id);


    public class CreateProductHandler : IRequestHandler<CreateProductRequest, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
