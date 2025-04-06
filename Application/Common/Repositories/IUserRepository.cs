using Application.Common.Services;
using Domain.UserContext;

namespace Application.Common.Repositories;

public interface IUserRepository : ISingletonService
{
    Task<NsAccount> FindOne(string username);
    //Task<int> Create(User salary);
}