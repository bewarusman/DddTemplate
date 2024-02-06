using Application.Common.Exceptions;

namespace Infrastructure.Common.Exceptions;

public class DbConnectionException : InfrastructureException
{
    public string ConnectionString { get; set; }

    public DbConnectionException(string message, Exception innerException, string connectionString)
        : base(message, innerException)
    {
        ConnectionString = connectionString;
    }
}