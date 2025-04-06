using Application.Common.Interfaces;
using Application.Common.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class SubRequestTypeRepository : ISubRequestTypeRepository
    {
        private readonly IDbService _dbService;

        public SubRequestTypeRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        public Task<int> Add(SubRequestType subRequestType)
        {
            var sql = "INSERT INTO NSecurity.SubRequestTypes(Id, Name, CreatedAt, CreatedBy, IsRegistrationFormRequired, IsDatesRequired) VALUES(:Id, :Name, :CreatedAt, :CreatedBy, BOOL_TO_NUM(:IsRegistrationFormRequired), BOOL_TO_NUM(:IsDatesRequired))";
            return _dbService.Execute(sql, subRequestType);
        }

        public Task<IList<SubRequestType>> FindAll()
        {
            var sql = "SELECT * FROM NSecurity.SubRequestTypes ";
            return _dbService.Query<SubRequestType>(sql);
        }

        public Task<SubRequestType> FindById(string id)
        {
            var sql = "SELECT * FROM NSecurity.SubRequestTypes WHERE Id = :Id";
            return _dbService.QuerySingle<SubRequestType>(sql, new { id });
        }

        public Task<IList<SubRequestType>> FindByIds(IList<string> subRequestTypeIds)
        {
            var sql = "SELECT * FROM NSecurity.SubRequestTypes WHERE Id IN :SubRequestTypeIds";
            return _dbService.Query<SubRequestType>(sql, new { subRequestTypeIds });
        }

        public Task<IList<SubRequestType>> FindByRequestId(string id)
        {
            var sql = "select * from subRequestTypes where id in (select subRequestId from RequestWithSubRequest where RequestId=:Id)";
            return _dbService.Query<SubRequestType>(sql, new { id });
        }

        public Task<IList<SubRequestType>> FindRequestForSubRequest()
        {
            var sql = "SELECT * FROM NSecurity.SubRequestTypes ";
            return _dbService.Query<SubRequestType>(sql);
        }

        public Task<int> Update(SubRequestType subRequestType)
        {
            var sql = "UPDATE NSecurity.SubRequestTypes SET Name = :Name, IsRegistrationFormRequired = BOOL_TO_NUM(:IsRegistrationFormRequired), IsDatesRequired = BOOL_TO_NUM(:IsDatesRequired) WHERE Id = :Id";
            return _dbService.Execute(sql, subRequestType);
        }


    }
}