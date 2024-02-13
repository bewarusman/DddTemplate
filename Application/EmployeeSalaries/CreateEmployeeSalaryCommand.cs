//using Application.Common.Exceptions;
//using Application.Common.Messaging;
//using Application.Common.Repositories;
//using MediatR;

//using Domain.EmployeeContext;

//namespace Application.Salaries;

//public class CreateEmployeeSalaryCommand : BaseRequest<Result>
//{
//    public string EmployeeId { get; set; }
//    public string SalaryId { get; set; }
//    public decimal Amount { get; set; }
//    public decimal Bonus { get; set; }
//    public int Year { get; set; }
//    public int Month { get; set; }
//    public decimal Total { get; set; }



//    public class Handler : IRequestHandler<CreateEmployeeSalaryCommand, Result>
//    {
//        private readonly IEmployeeSalaryRepository _employeeSalaryRepository;

//        public Handler(IEmployeeSalaryRepository employeeSalaryRepository)
//        {
//            _employeeSalaryRepository = employeeSalaryRepository;
//        }

//        public async Task<Result> Handle(CreateEmployeeSalaryCommand request, CancellationToken cancellationToken)
//        {

//            var empSal = new EmployeeSalary(request.EmployeeId, request.SalaryId, request.Amount, request.Bonus, request.Year, request.Month, request.Total);
//            var affectedRows = await _employeeSalaryRepository.Create(empSal);

//            if (affectedRows != 1)
//                throw new CommandException("The EmployeeSalary could not be created!");

//            var message = $"A new EmployeeSalary is created";
//            return Result.Success(empSal, message);

//        }
//    }

//}
