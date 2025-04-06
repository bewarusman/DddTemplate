using Application.Common.Services;

namespace Application.Common.Repositories
{
    public interface IRequestService : ISingletonService
    {
        public Task<int> Save(Domain.Entities.Requests requests);
    }
}
