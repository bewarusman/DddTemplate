using Application.Common.Interfaces;
using Application.Common.Repositories;
using Domain.EmployeeContext;


namespace Infrastructure.Repositories;

public class EmployeeSalaryRepository : IEmployeeSalaryRepository
{

    private readonly IDbService _dbService;



    public EmployeeSalaryRepository(IDbService dbService)
    {
        _dbService = dbService;
    }

    public Task<int> Create(EmployeeSalary employeeSalary)
    {
        var sql = "INSERT INTO employee(ID,FIRSTNAME,LASTNAME,EMAIL,PHONENUMBER) values(:Id,:FirstName,:LastName,:Email,:PhoneNumber)";
        return _dbService.Execute(sql, employeeSalary);

    }




    public Task<IList<EmployeeSalary>> Find()
    {

        var sql = "SELECT * FROM employee_salaries";
        return _dbService.Query<EmployeeSalary>(sql);
    }

    public Task<EmployeeSalary> FindOne(string id)
    {
        var sql = "select * from employee_salaries where Id=:Id";
        return _dbService.QuerySingle<EmployeeSalary>(sql, new { Id = id });
    }

    public Task<int> Update(EmployeeSalary employeeSalary)
    {
        var sql = "update employee set FirstName=:FirstName ,LastName=:LastName ,Email=:Email ,PhoneNumber=:PhoneNumber where Id=:Id";
        return _dbService.Execute(sql, employeeSalary);
    }
}
