using Application.Common.Messaging;
using Application.Common.Repositories;
using MediatR;

public class FindCustomerFormByDateQuery : BaseRequest<Result>
{
    public string lostMsisdn { get; set; }
    public string From { get; set; }
    public string To { get; set; }


    public class Handler : IRequestHandler<FindCustomerFormByDateQuery, Result>
    {
        private readonly ICustomerFormRepository _customerFormRepository;

        public Handler(ICustomerFormRepository customerFormRepository)
        {
            _customerFormRepository = customerFormRepository;
        }
        
        public async Task<Result> Handle(FindCustomerFormByDateQuery request, CancellationToken cancellationToken)
        {
            var records = await _customerFormRepository.FindByDate(request.From, request.To);
            return Result.Success(records);
        }
    }
}