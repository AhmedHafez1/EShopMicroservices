using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto OrderDto) : ICommand<CreateOrderResult>;
public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator: AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.OrderDto.OrderName).NotEmpty().WithMessage("Order name is required!");
        RuleFor(x => x.OrderDto.CustomerId).NotEmpty().WithMessage("Customer id is reqired!");
        RuleFor(x => x.OrderDto.OrderItems).NotEmpty().WithMessage("Order items shouldn't be empty");
    }
}

