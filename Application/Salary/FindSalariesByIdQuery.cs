using Application.Common.Messaging;
using Application.Common.Repositories;
using MediatR;

namespace Application.Employees;

public class FindSalariesById : BaseRequest<Result>
{
    public string id { get; set; }


    public class Handler : IRequestHandler<FindSalariesById, Result>
    {
        private readonly ISalaryRepository _salaryRepository;

        public Handler(ISalaryRepository salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }


        public async Task<Result> Handle(FindSalariesById request, CancellationToken cancellationToken)
        {
            var salaries = await _salaryRepository.FindOne(request.id);
            return Result.Success(salaries);
        }
    }




}