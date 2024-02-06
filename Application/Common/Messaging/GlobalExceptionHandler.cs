using Application.Common.Exceptions;
using Domain.Common.Exceptions;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Messaging;

public class GlobalExceptionHandler<TRequest, TResponse, TExcpetion> : IRequestExceptionHandler<TRequest, TResponse, TExcpetion>
where TRequest : BaseRequest<TResponse>
where TResponse : Result, new()
where TExcpetion : Exception
{
    private readonly ILogger _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler<TRequest, TResponse, TExcpetion>> logger)
    {
        _logger = logger;
    }

    public Task Handle(TRequest request, TExcpetion exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        var response = Result.Failed(exception.Message) as TResponse;

        if (exception is InfrastructureException)
            _logger.LogCritical(exception.InnerException,
                "Something went wrong while handling request of type! {@requestType} {@Request} {@exception} {@inner_exception}",
                typeof(TRequest),
                request,
                exception,
                exception.InnerException);
        else if (exception is DomainException)
            _logger.LogWarning(exception, "Something went wrong while handling request of type! {@requestType} {@Request}", typeof(TRequest), request);
        else
            _logger.LogError(exception, "A severe exception occured! {@requestType} {@Request}", typeof(TRequest), request);

        state.SetHandled(response);

        return Task.CompletedTask;
    }
}