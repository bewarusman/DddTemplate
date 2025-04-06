using Application.Common.Interfaces;
using Application.Common.Repositories;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class RequestService : IRequestService
    {
        private readonly IDbService _dbService;
        private IRequestInputRepository _requestInputRepository;
        //private readonly IRequestRepository _requestRepository;

        public RequestService(IDbService dbService, IRequestInputRepository requestInputRepository)
        {
            _dbService = dbService;
            _requestInputRepository = requestInputRepository;
            //_requestRepository = requestRepository;
        }

        public async Task<int> Save(Requests request)
        {

            var commands = new List<Tuple<string, object>>();

            // first save request 
            commands.Add(new Tuple<string, object>("insert into requests(ID,REQUESTTYPEID,ASSIGNEDTO,CREATEDBY,CREATEDAT,STATUS) values(:Id,:RequestTypeId,:AssignedTo,:CreatedBy,:CreatedAt,:Status)", request));


            // second save request inputs
            commands.Add(new Tuple<string, object>("insert into requestInputs(ID,REQUESTID,INPUT,ISHIDDEN,APPROVED,VIP,AUDITED,CREATEDAT,GOLDEN) values(:Id,:requestId,:input,bool_to_num(:isHidden),bool_to_num(:approved),bool_to_num(:vip),bool_to_num(:audited),:CreatedAt,bool_to_num(:golden))", request.requestInputs));

            // third save sub requests
            commands.Add(new Tuple<string, object>("insert into requestWithSubrequest(ID,REQUESTID,SUBREQUESTID,CREATEDAT) values(:Id,:requestId,:subrequestId,:CreatedAt)", request.requestWithSubRequest));


            NsRequestFlows requestFlow = new NsRequestFlows(request.Id, RequestStatus.CREATED, "", "", "", "", FlowStatus.REQUIRED, FlowStatus.REQUIRED);
            // finally make flows in flow table
            commands.Add(new Tuple<string, object>(@"insert into REQUESTFLOWS(ID,REQUESTID,STATUS,ASSIGNEDTOSECURITY,ASSIGNEDTOARCHIVE,SECURITYMESSAGE,ARCHIVEMESSAGE,SECURITYSTATUS,ARCHIVESTATUS,CREATEDAT) 
                                                                      values(:Id,:requestId,:status,:assignedToSecurity,:assignedToArchive,:securityMessage,:archiveMessage,:securityStatus,:archiveStatus,:CreatedAt)", requestFlow));

            return await _dbService.ExecuteWithTransaction(commands);

        }
    }
}