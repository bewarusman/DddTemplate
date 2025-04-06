using Application.Common.Repositories;
using Domain.Entities;
using MediatR;

namespace Application.RequestTypes
{
    public class FindSubRequestByRequestIdQuery : IRequest<IList<SubRequestType>>
    {

        public string requestTypeId { get; set; }


        public class FindCourtsHandler : IRequestHandler<FindSubRequestByRequestIdQuery, IList<SubRequestType>>
        {
            private readonly IRequestTypeWithSubRequestTypeRepository _requestTypeWithSubRequestTypeRepository;
            private readonly ISubRequestTypeRepository _subRequestType;

            public FindCourtsHandler(IRequestTypeWithSubRequestTypeRepository requestTypeWithSubRequestTypeRepository, ISubRequestTypeRepository subRequestType)
            {
                _requestTypeWithSubRequestTypeRepository = requestTypeWithSubRequestTypeRepository;
                _subRequestType = subRequestType;
            }
            public async Task<IList<SubRequestType>> Handle(FindSubRequestByRequestIdQuery request, CancellationToken cancellationToken)
            {

                var subRequests = await _requestTypeWithSubRequestTypeRepository.FindByRequestId(request.requestTypeId);

                return subRequests;
                //throw new NotImplementedException();
            }
        }
    }
}
