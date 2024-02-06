using MediatR;

namespace Application.Common.Messaging;

public class BaseRequest<TResponse>
        : IRequest<TResponse>
        where TResponse : Result
{ }
