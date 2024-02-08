using Application.Common.Messaging;
using Application.Common.Repositories;
using MediatR;

namespace Application.Employees;

public class FindEmployeeByIdQuery : BaseRequest<Result>
{
    public string id { get; set; }
   


    public class Handler : IRequestHandler<FindEmployeeByIdQuery, Result>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public Handler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<Result> Handle(FindEmployeeByIdQuery request, CancellationToken cancellationToken)

        {
            var employees = await _employeeRepository.FindOne(request.id);
            return Result.Success(employees);
        }
    }




}