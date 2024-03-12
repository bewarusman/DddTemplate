using Application.Common.Exceptions;
using Application.Common.Messaging;
using Application.Common.Repositories;

using MediatR;
using Domain.EmployeeContext;
using System.ComponentModel.DataAnnotations;

namespace Application.Employees;

public class CreateEmployeeCommand : BaseRequest<Result>
{
    [Required(ErrorMessage = "The first name is required!")]
    public string FirstName { get; set; }

    public string LastName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }

    public class Handler : IRequestHandler<CreateEmployeeCommand, Result>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public Handler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Result> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
      
                var employee = new Employee(request.FirstName, request.LastName, request.Email, request.PhoneNumber);
                var affectedRows = await _employeeRepository.Create(employee);

                if (affectedRows != 1)
                    throw new CommandException("The employee could not be created!");

                var message = $"The new employee '{employee.FirstName}' is created";
                return Result.Success(employee, message);
          
        }
    }
}