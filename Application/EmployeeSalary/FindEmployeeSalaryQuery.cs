using Application.Common.Messaging;
using Application.Common.Repositories;
using MediatR;



// Then Will ask Kak bewar why is error when UnCommented
//
//namespace Application.EmployeeSalary;    
//

public class FindEmployeeSalaryQuery : BaseRequest<Result>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public class Handler : IRequestHandler<FindEmployeeSalaryQuery, Result>
    {
        private readonly IEmployeeSalaryRepository _employeeSalaryRepository;

        public Handler(IEmployeeSalaryRepository employeeSalaryRepository)
        {
            _employeeSalaryRepository = employeeSalaryRepository;
        }
        public async Task<Result> Handle(FindEmployeeSalaryQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeSalaryRepository.Find();
            return Result.Success(employees);
        }
    }

}