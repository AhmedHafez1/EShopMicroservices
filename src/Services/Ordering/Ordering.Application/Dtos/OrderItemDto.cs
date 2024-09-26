using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Dtos
{
    public record OrderItemDto(ProductId ProductId, OrderId OrderId, decimal Price, int Quantity)
    {
    }
}
