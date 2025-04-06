using Application.Common.Services;
using Domain.Entities;

namespace Application.Common.Repositories
{
    public interface IRequestTypeRepository : ISingletonService
    {
        Task<IList<RequestType>> FindAll();
        Task<int> Update(RequestType requestType);
        Task<RequestType> FindById(string id);
        Task<IList<RequestType>> FindByIds(IList<string> ids);
    }
}