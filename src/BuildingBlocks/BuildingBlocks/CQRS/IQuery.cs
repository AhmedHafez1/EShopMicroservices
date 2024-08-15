using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface IQuery<T> :IRequest<T> where T : notnull;
}
