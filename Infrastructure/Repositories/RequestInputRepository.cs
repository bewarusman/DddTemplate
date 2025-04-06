using Application.Common.Interfaces;
using Application.Common.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class RequestInputRepository : IRequestInputRepository
    {
        private readonly IDbService _dbService;

        public RequestInputRepository(IDbService dbService)
        {
            _dbService = dbService;
        }

        public async Task<int> Create(RequestInput requestInput)
        {
            string query = @"insert into requestInputs(ID,REQUESTID,INPUT,ISHIDDEN,APPROVED,VIP,AUDITED,CREATEDAT,CREATEDON) 
            values(:Id,:RequestId,:Input,:isHidden,:approved,:vip,:audited,:createdat,:createdon)";

            return await _dbService.Execute(query, requestInput);
        }


        public async Task<IList<RequestInput>> FindByRequestId(string requestId)
        {
            string query = "select * from REQUESTINPUTS WHERE RequestId=:requestId";
            return await _dbService.Query<RequestInput>(query, new { requestId });
        }
    }
}
