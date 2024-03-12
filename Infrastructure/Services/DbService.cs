using Application.Common.Interfaces;
using Dapper;
using Infrastructure.Common.Configurations;
using Infrastructure.Common.Exceptions;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace Infrastructure.Services;

public class DbService : IDbService
{
    private readonly IConnectionStrings _connectionStrings;

    public DbService(IConnectionStrings connectionStrings)
    {
        _connectionStrings = connectionStrings;
    }

    public IDbConnection GetConnection()
    {
        var connectionString = _connectionStrings.SecurityPortal;
        try
        {
            IDbConnection connection = new OracleConnection(connectionString);
            connection.Open();
            return connection;
        }
        catch (Exception ex)
        {
            throw new DbConnectionException("Unable to connecto to DB.", ex, connectionString);
        }
    }

    public async Task<int> Execute(string sql, object param = null, bool procs = false)
    {
        using IDbConnection connection = GetConnection();
        try
        {
            int result;
            if (procs)
                result = await connection.ExecuteAsync(sql, param, commandType: System.Data.CommandType.StoredProcedure);
            else
                result = await connection.ExecuteAsync(sql, param);
            return result;

        }
        catch (Exception ex)
        {
            throw new DbExecuteException("Unable to execute query.", ex, sql, param);
        }
    }

    public async Task<int> ExecuteWithTransaction(IList<Tuple<string, object>> commands)
    {
        var affectedRows = 0;
        using IDbConnection connection = GetConnection();
        using var transaction = connection.BeginTransaction();
        foreach (var command in commands)
        {
            try
            {
                int result = await connection.ExecuteAsync(command.Item1, command.Item2, transaction: transaction);
                affectedRows += result;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new DbExecuteException(ex.Message, ex, command.Item1, command.Item2);
            }
        }
        transaction.Commit();
        return affectedRows;
    }

    public async Task<IList<T>> Query<T>(string sql, object param = null)
    {
        using IDbConnection connection = GetConnection();
        try
        {
            var result = await connection.QueryAsync<T>(sql, param);
            return result.ToList();

        }
        catch (Exception ex)
        {
            throw new DbQueryException("Unable to run query.", ex, sql, param);
        }
    }

    public async Task<T> QuerySingle<T>(string sql, object param)
    {
        using IDbConnection connection = GetConnection();
        try
        {
            var result = await connection.QueryAsync<T>(sql, param);
            return result.FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new DbQueryException("Unable to run query.", ex, sql, param);
        }
    }

    public async Task<IList<TU>> Query<T, U, TU>(string sql, object param, Func<T, U, TU> map, string splitOn)
    {
        using IDbConnection connection = GetConnection();
        try
        {
            var result = await connection.QueryAsync(sql, map, param, splitOn: splitOn);
            return result.ToList();
        }
        catch (Exception ex)
        {
            throw new DbQueryException("Unable to run query.", ex, sql, param);
        }
    }
}