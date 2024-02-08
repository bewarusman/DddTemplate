using Application.Common.Interfaces;
using Application.Common.Repositories;
using Domain.UserContext;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbService _dbService;

    public UserRepository(IDbService dbService)
    {
        _dbService = dbService;
    }

    public Task<User> FindOne(string username)
    {
        var sql = "SELECT * FROM Users WHERE Username = :Username";
        return _dbService.QuerySingle<User>(sql, new { username });
    }
}