using MediatR;

namespace Application.NsRequest
{
    public class FindHistoryQueryPaginated : IRequest<object>
    {


        public class FindHistoryQueryPaginatedHandler : IRequestHandler<FindHistoryQueryPaginated, object>
        {
            public Task<object> Handle(FindHistoryQueryPaginated request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
