using Domain.Common.Exceptions;

namespace Application.Common.Exceptions;

public abstract class ApplicationException : DomainException
{
    protected ApplicationException(string message) : base(message)
    {
    }
}