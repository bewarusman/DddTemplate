using Application.Common.Interfaces;
using Application.Common.Repositories;
using Domain.EmployeeContext;

namespace Infrastructure.Repositories;

public class SalaryRepository : ISalaryRepository
{

    private readonly IDbService _dbService;

    public SalaryRepository(IDbService dbService)
    {

        _dbService = dbService;
    }

    public Task<int> Create(Salary salary)
    {
        //var sql = "INSERT INTO Salary (id, Employee_id, Amount, IsLast) VALUES(:Id, :EmployeeId, :Amount,BOOL_TO_NUM(:IsLast))";
        var sql = "INSERT INTO Salary (id, Employee_id, Amount, IsLast) VALUES(:Id, :EmployeeId, :Amount,:IsLast)";


        return _dbService.Execute(sql, salary);
    }


    public Task<IList<Salary>> Find()
    {
        var sql = "SELECT * FROM salary";
        return _dbService.Query<Salary>(sql);
    }

    public Task<Salary> FindOne(string id)
    {
        var sql = "SELECT * FROM salary where Id=:id";
        return _dbService.QuerySingle<Salary>(sql, new { Id = id });
    }

    public Task<int> Update(Salary salary)
    {
        var sql = "UPDATE Salary SET Employee_id = :EmployeeId, Amount = :Amount WHERE id = :Id";
        return _dbService.Execute(sql, salary);
    }

    
    public Task<int> Delete(Salary salary)
    {
        var sql = "DELETE FROM salary WHERE Id=:Id";
        return _dbService.Execute(sql, salary);
    }
}
