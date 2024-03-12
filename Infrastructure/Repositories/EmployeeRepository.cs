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

        //........
        //--All employees along with their salary without duplicate
        //..........

//        SELECT e.id, e.firstname, e.lastname, e.email, e.phonenumber, s.amount
//        FROM Employee e
//LEFT JOIN(
//    SELECT Employee_Id, MAX(amount) AS amount
//    FROM Salary
//    GROUP BY Employee_Id
//) s ON e.ID = s.Employee_Id
//ORDER BY e.FirstName;

        //var sql = "SELECT DISTINCT e.id, e.firstname, e.lastname, e.email, e.phonenumber,s.amount FROM Employee e LEFT JOIN Salary s ON e.ID = s.Employee_Id order by e.FirstName;";
        var sql = "SELECT e.id, e.firstname, e.lastname, e.email, e.phonenumber, s.amount FROM Employee e LEFT JOIN (SELECT Employee_Id, MAX(amount) AS amount  FROM Salary    GROUP BY Employee_Id ) s ON e.ID = s.Employee_Id ORDER BY e.FirstName";

        return _dbService.Query<Employee>(sql);
    }

    public Task<Employee> FindOne(string id)
    {
        //var sql = "select * from employee where Id=:Id";
        //var sql = "SELECT DISTINCT e.id, e.firstname, e.lastname, e.email, e.phonenumber, s.amount FROM Employee e LEFT JOIN Salary s ON e.ID = s.Employee_Id";

        //var sql = "SELECT e.id, e.firstname, e.lastname, e.email, e.phonenumber, s.amount FROM Employee e  LEFT JOIN Salary s ON e.ID = s.Employee_Id WHERE e.id =:EmployeeId AND ISLast=1";
        //var sql = "SELECT e.id, e.firstname, e.lastname, e.email, e.phonenumber, s.amount FROM Employee e LEFT JOIN Salary s ON e.ID = s.Employee_Id WHERE e.id =:Id AND ISLast = 1";
        //var sql = "SELECT e.id, e.firstname, e.lastname, e.email, e.phonenumber, (SELECT MAX(amount) FROM Salary WHERE Employee_Id = e.id) AS Amount FROM Employee e WHERE e.id =:Id";
        var sql = "SELECT e.ID AS ID, e.FIRSTNAME AS FIRSTNAME, e.LASTNAME AS LASTNAME,    e.EMAIL AS EMAIL,  e.PHONENUMBER AS PHONENUMBER,    es.EMPLOYEE_ID AS EMPLOYEE_ID,  es.SALARY_ID AS SALARY_ID,  es.AMOUNT AS AMOUNT, es.BONUS AS BONUS, es.YEAR AS YEAR, es.MONTH AS MONTH, es.TOTAL AS TOTAL FROM employee e INNER JOIN employee_salaries es ON e.ID= es.employee_id WHERE e.ID =:Id";

        return _dbService.QuerySingle<Employee>(sql, new { Id = id });
    }
    public Task<IList<Employee>> FindMany(string id)
    {
        //var sql = "select * from employee where Id=:Id";
        //var sql = "SELECT DISTINCT e.id, e.firstname, e.lastname, e.email, e.phonenumber, s.amount FROM Employee e LEFT JOIN Salary s ON e.ID = s.Employee_Id";

        //var sql = "SELECT e.id, e.firstname, e.lastname, e.email, e.phonenumber, s.amount FROM Employee e  LEFT JOIN Salary s ON e.ID = s.Employee_Id WHERE e.id =:EmployeeId AND ISLast=1";
        //var sql = "SELECT e.id, e.firstname, e.lastname, e.email, e.phonenumber, s.amount FROM Employee e LEFT JOIN Salary s ON e.ID = s.Employee_Id WHERE e.id =:Id AND ISLast = 1";
        //var sql = "SELECT e.id, e.firstname, e.lastname, e.email, e.phonenumber, (SELECT MAX(amount) FROM Salary WHERE Employee_Id = e.id) AS Amount FROM Employee e WHERE e.id =:Id";
        //var sql = "SELECT e.ID AS ID, e.FIRSTNAME AS FIRSTNAME, e.LASTNAME AS LASTNAME,    e.EMAIL AS EMAIL,  e.PHONENUMBER AS PHONENUMBER,    es.EMPLOYEE_ID AS EMPLOYEE_ID,  es.SALARY_ID AS SALARY_ID,  es.AMOUNT AS AMOUNT, es.BONUS AS BONUS, es.YEAR AS YEAR, es.MONTH AS MONTH, es.TOTAL AS TOTAL FROM employee e INNER JOIN employee_salaries es ON e.ID= es.employee_id WHERE e.ID =:Id";
        var sql = "SELECT e.ID AS ID, e.FIRSTNAME AS FIRSTNAME, e.LASTNAME AS LASTNAME,    e.EMAIL AS EMAIL,  e.PHONENUMBER AS PHONENUMBER,    es.EMPLOYEE_ID AS EMPLOYEE_ID,  es.SALARY_ID AS SALARY_ID,  es.AMOUNT AS AMOUNT, es.BONUS AS BONUS, es.YEAR AS YEAR, es.MONTH AS MONTH, es.TOTAL AS TOTAL FROM employee e LEFT JOIN employee_salaries es ON e.ID= es.employee_id WHERE e.ID =:Id ORDER BY es.YEAR desc, es.MONTH desc";

        return _dbService.Query<Employee>(sql, new { Id = id });
    }

    public Task<int> Update(Employee employee)
    {
        var sql = "update employee set FirstName=:FirstName ,LastName=:LastName ,Email=:Email ,PhoneNumber=:PhoneNumber where Id=:Id";
        return _dbService.Execute(sql, employee);
    }
}
