using Application.Common.Exceptions;
using Application.Common.Messaging;
using Application.Common.Repositories;
using MediatR;

namespace Application.Employees;

public class UpdateEmployeeCommand : BaseRequest<Result>
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public class Handler : IRequestHandler<UpdateEmployeeCommand, Result>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public Handler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.FindOne(request.Id);

            if (employee == null)
                throw new QueryException("could not find record with given id");

            employee.Update(request.FirstName, request.LastName, request.Email, request.PhoneNumber);

            int affectedRows = await _employeeRepository.Update(employee);
            if (affectedRows != 1)
                throw new CommandException("The employee could not be Updated!");

            var message = $"The employee '{employee.FirstName}' is Updated";
            return Result.Success(employee, message);
        }
    }
}