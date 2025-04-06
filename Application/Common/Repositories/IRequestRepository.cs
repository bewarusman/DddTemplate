using Application.Common.Services;
using Domain.Entities;

namespace Application.Common.Repositories
{
    public interface IRequestRepository : ISingletonService
    {
        Task<int> Create(Requests request);
        Task<IList<Requests>> FindAllPaginated();
        Task<IList<Requests>> FindInStatus(params string[] teams);
        Task<Requests> FindById(string Id);
    }
}