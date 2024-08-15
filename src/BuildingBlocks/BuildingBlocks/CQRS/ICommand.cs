
using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommand: IRequest;
    public interface ICommand<TResponse> : IRequest<TResponse>;
}
