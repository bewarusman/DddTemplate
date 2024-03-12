using Application.Common.Messaging;
using Application.Common.Repositories;
using MediatR;


namespace Application.Employees;

public class FindEmployeesQuery : BaseRequest<Result>
{
    public class Handler : IRequestHandler<FindEmployeesQuery, Result>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public Handler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<Result> Handle(FindEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.Find();
            return Result.Success(employees);
        }
    }

}