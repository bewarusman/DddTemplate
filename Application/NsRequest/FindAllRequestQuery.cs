using Application.Common.Messaging;
using Application.Common.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.NsRequest
{
    public class FindAllRequestQuery : BaseRequest<Result>
    {

        public class HandleFindAllRequests : IRequestHandler<FindAllRequestQuery, Result>
        {
            private readonly IRequestRepository _requestRepository;
            private readonly IRequestInputRepository _requestInputRepository;
            private readonly IRequestTypeRepository _requestTypeRepository;

            private readonly ISubRequestTypeRepository _subRequestTypeRepository;

            public HandleFindAllRequests(IRequestRepository requestRepository, IRequestInputRepository requestInputRepository, IRequestTypeRepository requestTypeRepository, ISubRequestTypeRepository subRequestTypeRepository)
            {
                _requestRepository = requestRepository;
                _requestInputRepository = requestInputRepository;
                _requestTypeRepository = requestTypeRepository;

                _subRequestTypeRepository = subRequestTypeRepository;
            }
            public async Task<Result> Handle(FindAllRequestQuery request, CancellationToken cancellationToken)
            {

                var requests = await _requestRepository.FindInStatus(RequestStatus.CREATED, RequestStatus.PROCESSING, RequestStatus.COMPLETED);

                foreach (var item in requests)
                {
                    item.requestInputs = await _requestInputRepository.FindByRequestId(item.Id);
                    item.requestType = await _requestTypeRepository.FindById(item.RequestTypeId);
                    item.subRequestTypes = await _subRequestTypeRepository.FindByRequestId(item.Id);

                }
                return Result.Success(requests);
            }
        }
    }
}
