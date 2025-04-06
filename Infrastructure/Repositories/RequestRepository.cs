using Application.Common.Interfaces;
using Application.Common.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly IDbService _dbService;

        public RequestRepository(IDbService dbService)
        {
            _dbService = dbService;
        }
        public Task<int> Create(Requests request)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Requests>> FindAllPaginated()
        {
            throw new NotImplementedException();
        }

        public Task<Requests> FindById(string Id)
        {
            var sql = "SELECT * FROM REQUESTS WHERE id=:id";
            return _dbService.QuerySingle<Requests>(sql, new { Id });
        }

        public Task<IList<Requests>> FindInStatus(string[] Status)
        {
            var sql = "SELECT * FROM REQUESTS WHERE STATUS in :Status";
            return _dbService.Query<Requests>(sql, new { Status });
        }

    }
}
