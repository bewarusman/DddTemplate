using Application.Common.Exceptions;
using Application.Common.Messaging;
using Application.Common.Repositories;
using MediatR;

namespace Application.Salaries;

public class DeleteSalaryByIdCommand : BaseRequest<Result>
{

    public string Id { get; set; }

    public class Handler : IRequestHandler<DeleteSalaryByIdCommand, Result>
    {
        private readonly ISalaryRepository _salaryRepository;

        public Handler(ISalaryRepository salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }

        public async Task<Result> Handle(DeleteSalaryByIdCommand request, CancellationToken cancellationToken)
        {
            var salary = await _salaryRepository.FindOne(request.Id);
            var affectedRows = await _salaryRepository.Delete(salary);

            if (affectedRows != 1)
                throw new CommandException("The salary could not be Deleted!");

            var message = $"The Salary Deleted Successfully";
            return Result.Success(salary, message);
        }
    }




}
