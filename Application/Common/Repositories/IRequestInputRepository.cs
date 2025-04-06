using Application.Common.Services;
using Domain.Entities;

namespace Application.Common.Repositories
{
    public interface IRequestInputRepository : ISingletonService
    {
        Task<int> Create(RequestInput input);
        Task<IList<RequestInput>> FindByRequestId(string requestId);
    }
}
