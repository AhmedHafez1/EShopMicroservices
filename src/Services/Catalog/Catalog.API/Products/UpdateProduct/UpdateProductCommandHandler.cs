﻿
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, List<string> Category, string ImagePath, decimal Price)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
                .Length(2, 77).WithMessage("Name length must be between 2 and 77 characters");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.ImagePath).NotEmpty().WithMessage("Image is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Description is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
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
