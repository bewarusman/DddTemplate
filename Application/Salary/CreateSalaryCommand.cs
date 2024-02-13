using Application.Common.Exceptions;
using Application.Common.Messaging;
using Application.Common.Repositories;
using MediatR;

using Domain.EmployeeContext;




namespace Application.Salaries;

public class CreateSalaryCommand : BaseRequest<Result>
{ 
    public string EmployeeId { get; set; }
    public decimal Amount { get; set; }
    public int IsLast { get; set; }



    public class Handler : IRequestHandler<CreateSalaryCommand, Result>
    {
        private readonly ISalaryRepository _salaryRepository;

        public Handler(ISalaryRepository salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }

        public async Task<Result> Handle(CreateSalaryCommand request, CancellationToken cancellationToken)
        {

            var salary = new Salary(request.EmployeeId , request.Amount,request.IsLast);
            var affectedRows = await _salaryRepository.Create(salary);

            if (affectedRows != 1)
                throw new CommandException("The salary could not be created!");

            var message = $"A new salary is created";
            return Result.Success(salary, message);

        }
    }

}
