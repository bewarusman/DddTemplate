namespace Application.Common.Exceptions;

public class CommandException : ApplicationException
{
    public CommandException(string message) : base(message)
    {
    }
}
