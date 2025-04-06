using Application.Common.Services;
using Domain.Entities;

namespace Application.Common.Repositories
{
    public interface ISubRequestTypeRepository : ISingletonService
    {
        Task<IList<SubRequestType>> FindAll();
        Task<int> Update(SubRequestType subRequestType);
        Task<SubRequestType> FindById(string id);
        Task<IList<SubRequestType>> FindByRequestId(string id);
        Task<IList<SubRequestType>> FindByIds(IList<string> subRequestTypeIds);
        Task<IList<SubRequestType>> FindRequestForSubRequest();
    }
}