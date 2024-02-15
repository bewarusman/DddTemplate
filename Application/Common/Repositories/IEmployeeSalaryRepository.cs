using Application.Common.Services;
using Domain.EmployeeContext;

namespace Application.Common.Repositories;

public interface IEmployeeSalaryRepository : ISingletonService
{
    Task<int> Create(EmployeeSalary employeeSalary);
    Task<IList<EmployeeSalary>> Find();
    Task<EmployeeSalary> FindOne(string id);
    Task<int> Update(EmployeeSalary employeeSalary);
   
}
