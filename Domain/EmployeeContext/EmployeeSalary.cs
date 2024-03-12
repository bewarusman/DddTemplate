using Domain.Common.Exceptions;


namespace Domain.EmployeeContext;
public class EmployeeSalary
{
    public string EmployeeId { get; set; }
    public string SalaryId { get; set; }
    public decimal Amount { get; set; }
    public decimal Bonus { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal Total { get; set; }

    public EmployeeSalary() { }

    public EmployeeSalary(
     
        string employeeId,
        string salaryId,
        decimal amount,
        decimal bonus,
        int year,
        int month
        
    )
    {
        ValidateParameters(employeeId, salaryId, amount);

        EmployeeId = employeeId;
        SalaryId = salaryId;
        Amount = amount;
        Bonus = bonus;
        Year = year;
        Month = month;
        Total = Amount + Bonus;
        //Total = total;
    }


    public void Update(
            string employeeId,
            string salaryId,
            decimal amount,
            decimal bonus,
            int year,
            int month,
            decimal total)
    {
        ValidateParameters(employeeId, salaryId, amount);
        EmployeeId = employeeId;
        SalaryId = salaryId;
        Amount = amount;
        Bonus = bonus;
        Year = year;
        Month = month;
        Total = amount+ bonus;
    }






    private void ValidateParameters(string employeeId, string salaryId, decimal amount)
    {
        if (string.IsNullOrEmpty(employeeId))
        {
            throw new NotFoundException("Employee cannot be null or empty.");
        }

        if (string.IsNullOrEmpty(salaryId))
        {
            throw new NotFoundException("Salary cannot be null or empty.");
        }

        if (amount <= 0)
        {
            throw new InvalidFormatException("Amount must be greater than zero.");
        }

         
    }
}