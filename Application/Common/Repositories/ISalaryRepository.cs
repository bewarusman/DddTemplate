
using Application.Common.Services;
using Domain.EmployeeContext;

namespace Application.Common.Repositories;

public interface ISalaryRepository : ISingletonService
{
    Task<int> Create(Salary salary);
    Task<IList<Salary>> Find();
    Task<Salary> FindOne(string id);
    Task<int> Update(Salary salary);
    Task<int> Delete(Salary salary);
}



