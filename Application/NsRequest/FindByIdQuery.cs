using Application.Common.Messaging;
using Application.Common.Repositories;
using MediatR;

namespace Application.NsRequest
{
    public class FindByIdQuery : BaseRequest<Result>
    {
        public FindByIdQuery(string requestId)
        {
            RequestId = requestId;
        }

        public string RequestId { get; set; }


        public class HandleFindByIdQuery : IRequestHandler<FindByIdQuery, Result>
        {
            private readonly IRequestRepository _requestRepository;
            private readonly IRequestInputRepository _requestInputRepository;
            private readonly IRequestTypeRepository _requestTypeRepository;
            private readonly ISubRequestTypeRepository _subRequestTypeRepository;

            public HandleFindByIdQuery(IRequestRepository requestRepository, IRequestInputRepository requestInputRepository, IRequestTypeRepository requestTypeRepository, ISubRequestTypeRepository subRequestTypeRepository)
            {
                _requestRepository = requestRepository;
                _requestInputRepository = requestInputRepository;
                _requestTypeRepository = requestTypeRepository;
                _subRequestTypeRepository = subRequestTypeRepository;
            }
            public async Task<Result> Handle(FindByIdQuery request, CancellationToken cancellationToken)
            {
                var nsRequest = await _requestRepository.FindById(request.RequestId);
                nsRequest.requestInputs = await _requestInputRepository.FindByRequestId(nsRequest.Id);
                nsRequest.requestType = await _requestTypeRepository.FindById(nsRequest.RequestTypeId);
                nsRequest.subRequestTypes = await _subRequestTypeRepository.FindByRequestId(nsRequest.Id);
                return Result.Success(nsRequest);
            }
        }


    }
}
