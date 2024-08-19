﻿using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, List<string> Category, string ImagePath, decimal Price) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.ImagePath).NotEmpty().WithMessage("Image is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Description is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    public class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(command);
            var errors = result.Errors.Select(e => e.ErrorMessage);

            if (errors.Any())
            {
                throw new ValidationException(errors.FirstOrDefault());
            }


            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Category = command.Category,
                ImagePath = command.ImagePath,
                Price = command.Price,
            };

            session.Store(product);
            await session.SaveChangesAsync();

            return new CreateProductResult(product.Id);
        }
    }
}
