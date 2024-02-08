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
        var sql = "INSERT INTO employee(ID,FIRSTNAME,LASTNAME,EMAIL,PHONENUMBER) values(:Id,:FirstName,:LastName,:Email,:PhoneNumber)";
        return _dbService.Execute(sql, employee);
    }


    public Task<IList<Employee>> Find()
    {

        var sql = "SELECT * FROM employee";
        return _dbService.Query<Employee>(sql);
    }

    public Task<Employee> FindOne(string id)
    {
        var sql = "select * from employee where Id=:Id";
        return _dbService.QuerySingle<Employee>(sql, new { Id = id });
    }

    public Task<int> Update(Employee employee)
    {
        var sql = "update employee set FirstName=:FirstName ,LastName=:LastName ,Email=:Email ,PhoneNumber=:PhoneNumber where Id=:Id";
        return _dbService.Execute(sql, employee);
    }
}
