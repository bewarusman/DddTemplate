namespace Application.Common.Exceptions;

public class QueryException : ApplicationException
{
    public QueryException(string message) : base(message)
    {
    }
}