using Application.Common.Interfaces;
using Application.Common.Repositories;
using Domain.EmployeeContext;
using System.Collections.Generic;


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

        var sql = "INSERT INTO Employee_Salaries (Employee_id, Salary_id, Amount, Bonus, Year, Month, Total) VALUES(:EmployeeId,:SalaryId,:Amount,:Bonus,:Year,:Month,:Total)";
        return _dbService.Execute(sql, employeeSalary);

    }




    public Task<IList<EmployeeSalary>> Find()
    {
        var sql = "SELECT * FROM employee_salaries ";
        return _dbService.Query<EmployeeSalary>(sql);
    }

    public Task<IList<EmployeeSalary>> FindMany(string id)
    {
        //var sql = "select * from employee_salaries where Id=:Id";
        var sql = "SELECT Salary.*, Employee_Salaries.Month FROM Salary INNER JOIN Employee_Salaries ON Salary.id = Employee_Salaries.Salary_id INNER JOIN Employee ON Employee_Salaries.Employee_id = Employee.ID WHERE Employee.ID =:id";
        return _dbService.Query<EmployeeSalary>(sql, new { Id = id });
    }

    public Task<int> Update(EmployeeSalary employeeSalary)
    {
        var sql = "update employee set FirstName=:FirstName ,LastName=:LastName ,Email=:Email ,PhoneNumber=:PhoneNumber where Id=:Id";
        return _dbService.Execute(sql, employeeSalary);
    }
}
