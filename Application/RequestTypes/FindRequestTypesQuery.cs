using Application.Common.Repositories;
using Domain.Entities;
using MediatR;
namespace Application.RequestTypes
{
    public class FindRequestTypesQuery : IRequest<IList<RequestType>>
    {
    }

    public class FindCourtsHandler : IRequestHandler<FindRequestTypesQuery, IList<RequestType>>
    {
        private readonly IRequestTypeRepository _requestTypeRepository;

        public FindCourtsHandler(IRequestTypeRepository requestTypeRepository)
        {
            _requestTypeRepository = requestTypeRepository;
        }

        public async Task<IList<RequestType>> Handle(FindRequestTypesQuery request, CancellationToken cancellationToken)
        {

            var requestTypes = await _requestTypeRepository.FindAll();
            return requestTypes;

        }
    }
}