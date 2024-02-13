using Domain.Common.Exceptions;

namespace Domain.EmployeeContext;

public class Salary : Entity
{
    public string EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public int IsLast { get; set; }

    public Salary() { }

    public Salary(string employeeId, decimal amount, int isLast)
    {
        ValidateParameters(employeeId, amount);

        EmployeeId = employeeId;
        Amount = amount;
        IsLast = 1;
    }

    public void Update(string employeeId, decimal amount, int isLast)
    {
        ValidateParameters(employeeId, amount);

        EmployeeId = employeeId;
        Amount = amount;
        IsLast = 1;
    }

    private void ValidateParameters(string employeeId, decimal amount)
    {
        if (string.IsNullOrEmpty(employeeId))
        {
            throw new NotFoundException("Employee cannot be null or empty.");
        }

        if (amount <= 0)
        {
            throw new InvalidFormatException("Amount must be greater than zero.");
        }
    }
}



