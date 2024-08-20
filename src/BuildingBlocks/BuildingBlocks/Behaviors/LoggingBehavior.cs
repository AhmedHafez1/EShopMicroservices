using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) :
        IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation($"[START] Handling Request {typeof(TRequest).Name} with Response {typeof(TResponse).Name} - Request Data : {request}");

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();
            var elapsedTime = timer.Elapsed;

            if (elapsedTime.Seconds > 3)
            {
                logger.LogWarning($"[PERFORMANCE] The request {typeof(TRequest).Name} took {elapsedTime.Seconds} seconds.");
            }

            logger.LogInformation($"[END] Handling Request {typeof(TRequest).Name} with Response {typeof(TResponse).Name}");

            return response;
        }
    }
}
