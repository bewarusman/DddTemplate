using Application.Common.Services;
using Domain.EmployeeContext;

namespace Application.Common.Repositories;

public interface IEmployeeSalaryRepository : ISingletonService
{
    Task<int> Create(EmployeeSalary employeeSalary);
    Task<IList<EmployeeSalary>> Find();
    Task<IList<EmployeeSalary>> FindMany(string id);
    Task<int> Update(EmployeeSalary employeeSalary);
   
}
