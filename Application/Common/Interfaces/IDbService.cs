using Application.Common.Services;

namespace Application.Common.Interfaces;

public interface IDbService : ISingletonService
{
    Task<int> Execute(string sql, object param = null, bool procs = false);
    Task<int> ExecuteWithTransaction(IList<Tuple<string, object>> commands);
    Task<IList<T>> Query<T>(string sql, object param = null);
    Task<T> QuerySingle<T>(string sql, object param);
    Task<IList<TU>> Query<T, U, TU>(string sql, object param, Func<T, U, TU> map, string splitOn);
}