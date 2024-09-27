namespace Ordering.Application.Dtos
{
    public record OrderItemDto(Guid ProductId, Guid OrderId, decimal Price, int Quantity)
    {
    }
}
