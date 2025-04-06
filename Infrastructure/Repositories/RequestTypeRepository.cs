using Application.Common.Interfaces;
using Application.Common.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class RequestTypeRepository : IRequestTypeRepository
    {
        private readonly IDbService _dbService;

        public RequestTypeRepository(IDbService dbAdapter)
        {
            _dbService = dbAdapter;
        }

        public Task<int> Add(RequestType requestType)
        {
            var sql = "INSERT INTO  RequestTypes(Id, Name, CreatedAt, CreatedBy, HasMultipleInputs, InputType) VALUES(:Id, :Name, :CreatedAt, :CreatedBy, BOOL_TO_NUM(:HasMultipleInputs), :InputType)";
            return _dbService.Execute(sql, requestType);
        }

        public Task<IList<RequestType>> FindAll()
        {
            var sql = "SELECT * FROM  RequestTypes";
            return _dbService.Query<RequestType>(sql);
        }

        public Task<RequestType> FindById(string id)
        {
            var sql = "SELECT * FROM RequestTypes WHERE Id = :Id";
            return _dbService.QuerySingle<RequestType>(sql, new { id });
        }

        public Task<IList<RequestType>> FindByIds(IList<string> ids)
        {
            var sql = "SELECT * FROM  RequestTypes WHERE Id IN :Ids";
            return _dbService.Query<RequestType>(sql, new { ids });
        }

        public Task<int> Update(RequestType requestType)
        {
            var sql = "UPDATE  RequestTypes SET Name = :Name, HasMultipleInputs = BOOL_TO_NUM(:HasMultipleInputs), InputType = :InputType WHERE Id = :Id";
            return _dbService.Execute(sql, requestType);
        }
    }
}