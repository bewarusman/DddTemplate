using Application.Common.Services;
using Domain.EmployeeContext;

namespace Application.Common.Repositories;

public interface IEmployeeRepository : ISingletonService
{
    Task<int> Create(Employee employee);
    Task<IList<Employee>> Find();
    Task<Employee> FindOne(string id);
    Task<IList<Employee>> FindMany(string id);
    
    Task<int> Update(Employee employee);
}