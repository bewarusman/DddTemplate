using Application.Common.Services;
using Domain.Entities;

namespace Application.Common.Repositories
{
    public interface IRequestTypeWithSubRequestTypeRepository : ISingletonService
    {
        Task<IList<RequestTypeWithSubRequestType>> FindAll();
        Task<IList<SubRequestType>> FindByRequestId(string Id);
        Task<int> AddAll(string requestTypeId, IList<RequestTypeWithSubRequestType> requestWithSubRequestTypes);

    }
}