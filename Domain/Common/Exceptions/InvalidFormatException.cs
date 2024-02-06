namespace Domain.Common.Exceptions;

public class InvalidFormatException : DomainException
{
    public InvalidFormatException(string message) : base(message)
    {
    }
}