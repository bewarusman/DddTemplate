using Application.Common.Exceptions;
using Application.Common.Messaging;
using Application.Common.Repositories;

using MediatR;
using Domain.EmployeeContext;

namespace Application.Employees;

// Command to create an employee's salary
public class CreateEmployeeSalaryCommand : BaseRequest<Result>
{
    // Properties for the command
    public string EmployeeId { get; set; }
    public string SalaryId { get; set; }
    public decimal Amount { get; set; }
    public decimal Bonus { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    //public decimal Total { get; set; }

    // Handler for the command
    public class Handler : IRequestHandler<CreateEmployeeSalaryCommand, Result>
    {
        private readonly IEmployeeSalaryRepository _employeeSalaryRepository;

        //
        // Constructor to inject dependencies
        //
        public Handler(IEmployeeSalaryRepository employeeSalaryRepository)
        {
            _employeeSalaryRepository = employeeSalaryRepository;
        }
        //
        // Method to handle the command asynchronously
        //
        public async Task<Result> Handle(CreateEmployeeSalaryCommand request, CancellationToken cancellationToken)
        {
            //
            // Create a new EmployeeSalary object with provided data
            //
            var employeeSal = new EmployeeSalary(request.EmployeeId, request.SalaryId, request.Amount, request.Bonus, request.Year, request.Month);

            //
            // Call repository method to create the salary record
            //
            var affectedRows = await _employeeSalaryRepository.Create(employeeSal);

            //
            // Check if the salary record was created successfully
            //
            if (affectedRows != 1)
            {
                //
                // Throw an exception if creation was not successful
                //
                throw new CommandException("The employee salary could not be created!");
            }

            //
            // If creation was successful, return a success result
            //
            var message = $"The new salary for an employee is created";
            return Result.Success(employeeSal, message);
        }
    }
}
