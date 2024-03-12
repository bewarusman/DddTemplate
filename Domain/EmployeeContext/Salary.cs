using Domain.Common.Exceptions;

namespace Domain.EmployeeContext;

public class Salary : Entity
{
    public string EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public bool IsLast { get; set; }
    public DateTime CreatedAt { get; set; }


    public Salary() { }

    public Salary(string employeeId, decimal amount)
    {
        ValidateParameters(employeeId, amount);

        EmployeeId = employeeId;
        Amount = amount;
        IsLast = true;
    }

    public void Update(string employeeId, decimal amount)
    {
        ValidateParameters(employeeId, amount);

        EmployeeId = employeeId;
        Amount = amount;
        IsLast = true;
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