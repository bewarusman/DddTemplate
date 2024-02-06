using Application.Common.Exceptions;

namespace Infrastructure.Common.Exceptions;

public class DbExecuteException : InfrastructureException
{
    public string Sql { get; set; }
    public object Param { get; set; }

    public DbExecuteException(string message, Exception innerException, string sql, object param)
        : base(message, innerException)
    {
        Sql = sql;
        Param = param;
    }
}