using Application.Common.Interfaces;
using Application.Common.Repositories;
using Domain.EmployeeContext;

namespace Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbService _dbService;

    public EmployeeRepository(IDbService dbService)
    {
        _dbService = dbService;
    }

    public Task<int> Create(Employee employee)
    {
        var sql = "INSERT INTO....";
        return _dbService.Execute(sql, employee);
    }

    public Task<IList<Employee>> Find()
    {
        throw new NotImplementedException();
    }

    public Task<Employee> FindOne(string id)
    {
        throw new NotImplementedException();
    }

    public Task<int> Update(Employee employee)
    {
        throw new NotImplementedException();
    }
}
