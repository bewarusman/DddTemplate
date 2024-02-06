using Domain.Common.Exceptions;

namespace Application.Common.Exceptions;

public abstract class InfrastructureException : DomainException
{
    public Exception InnerException { get; set; }

    protected InfrastructureException(string message, Exception innerException) : base(message)
    {
        InnerException = innerException;
    }
}