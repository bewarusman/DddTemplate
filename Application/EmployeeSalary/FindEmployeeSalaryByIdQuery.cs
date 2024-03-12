using Application.Common.Messaging;
using Application.Common.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace Application.EmployeeSalary;

public class FindEmployeeSalaryByIdQuery : BaseRequest<Result>
{

    public string id { get; set; }
    public class Handler : IRequestHandler<FindEmployeeSalaryByIdQuery, Result>
    {
        private readonly IEmployeeSalaryRepository _employeeSalaryRepository;

        public Handler(IEmployeeSalaryRepository employeeSalaryRepository)
        {
            _employeeSalaryRepository = employeeSalaryRepository;
        }
        public async Task<Result> Handle(FindEmployeeSalaryByIdQuery request, CancellationToken cancellationToken)
        {
            var salaries = await _employeeSalaryRepository.FindMany(request.id);
            return Result.Success(salaries);
          
        }
    }




}
