using Application.Common.Interfaces;
using Application.Common.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class RequestTypeWithSubRequestTypeRepository : IRequestTypeWithSubRequestTypeRepository
    {
        private readonly IDbService _dbservice;

        public RequestTypeWithSubRequestTypeRepository(IDbService dbservice)
        {
            _dbservice = dbservice;
        }

        public Task<int> Add(RequestTypeWithSubRequestType requestTypeWithSubRequestType)
        {
            var sql = "INSERT INTO RequestTypeWithSubRequestTypes(Id, RequestTypeId, SubRequestTypeId, CreatedBy, CreatedAt) VALUES(:Id, :RequestTypeId, :SubRequestTypeId, :CreatedBy, :CreatedAt)";
            return _dbservice.Execute(sql, requestTypeWithSubRequestType);
        }

        public Task<int> AddAll(string requestTypeId, IList<RequestTypeWithSubRequestType> requestWithSubRequestTypes)
        {
            var commands = new List<Tuple<string, object>>();
            commands.Add(new Tuple<string, object>("DELETE FROM RequestTypeWithSubRequestTypes WHERE RequestTypeId = :requestTypeId", new { requestTypeId }));
            commands.Add(new Tuple<string, object>("INSERT INTO RequestTypeWithSubRequestTypes VALUES(:Id, :RequestTypeId, :SubRequestTypeId, :CreatedBy, :CreatedAt)", requestWithSubRequestTypes));
            return _dbservice.ExecuteWithTransaction(commands);
        }

        public Task<IList<RequestTypeWithSubRequestType>> FindAll()
        {
            var sql = "SELECT * FROM RequestTypeWithSubRequestTypes";
            return _dbservice.Query<RequestTypeWithSubRequestType>(sql);
        }

        public async Task<IList<SubRequestType>> FindByRequestId(string Id)
        {
            var sql = "select * from subRequestTypes where id in (SELECT SubRequestTypeId FROM RequestTypeWithSubRequestTypes where RequestTypeId=:Id)";
            return await _dbservice.Query<SubRequestType>(sql, new { Id });
        }
    }
}