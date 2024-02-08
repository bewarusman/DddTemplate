using Application.Common.Services;
using Domain.UserContext;

namespace Application.Common.Repositories;

public interface IUserRepository : ISingletonService
{
    Task<User> FindOne(string username);
}