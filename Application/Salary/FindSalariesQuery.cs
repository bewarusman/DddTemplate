using Application.Common.Messaging;
using Application.Common.Repositories;
using MediatR;

namespace Application.Employees;

public class FindSalariesQuery : BaseRequest<Result>
{
    
    public string EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public bool IsLast { get; set; }


    public class Handler : IRequestHandler<FindSalariesQuery, Result>
    {
        private readonly ISalaryRepository _salaryRepository;

        public Handler(ISalaryRepository salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }
        public async Task<Result> Handle(FindSalariesQuery request, CancellationToken cancellationToken)
        {
            var salaries = await _salaryRepository.Find();
            return Result.Success(salaries);
        }
    }

}