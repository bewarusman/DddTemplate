namespace Application.Common.Exceptions
{
    internal class InvalidFormException : ApplicationException
    {
        public InvalidFormException(string message) : base(message)
        {
        }
    }
}
