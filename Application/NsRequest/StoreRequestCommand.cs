using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Messaging;
using Application.Common.Repositories;
using Domain.Entities;
using MediatR;
namespace Application.NsRequest
{
    public class StoreRequestCommand : BaseRequest<Result>
    {
        public string requestTypeId { get; set; }
        public List<string> subRequestTypes { get; set; }
        public string requestInput { get; set; }

        public class StoreRequestCommandHandler : IRequestHandler<StoreRequestCommand, Result>
        {
            private readonly IValidationApiService _validationApiService;
            private readonly IRequestService _requestService;
            private readonly IRequestTypeRepository _requestTypeRepository;

            public StoreRequestCommandHandler(IValidationApiService validationApiService, IRequestService requestService, IRequestTypeRepository requestTypeRepository)
            {
                _validationApiService = validationApiService;
                _requestService = requestService;
                _requestTypeRepository = requestTypeRepository;
            }
            public async Task<Result> Handle(StoreRequestCommand request, CancellationToken cancellationToken)
            {


                if (request.subRequestTypes.Count() < 1 || request.requestInput.Length < 5)
                {
                    throw new InvalidFormException("Kindly fill all required fields");
                }

                var requestType = await _requestTypeRepository.FindById(request.requestTypeId);


                var response = await _validationApiService.ValidateMultiple2(request.requestInput.Replace(" ", ""), requestType.InputType);

                if (response.Item3 == null || response.Item3.Count() == 0)
                    throw new InvalidFormException("No Valid Input Was Found !");

                var nsRequest = new Requests(request.requestTypeId, "alan", RequestStatus.CREATED);
                IList<RequestInput> inputs = new List<RequestInput>();
                // valid number
                foreach (string number in response.Item3)
                {
                    var input = new RequestInput(nsRequest.Id, number, false, false, false, false);
                    inputs.Add(input);
                }
                // vip numbers
                if (response.Item4 != null && response.Item4.Count() > 0)
                    foreach (string number in response?.Item4)
                    {
                        var input = inputs.First(o => o.input == number);
                        input.vip = true;

                    }
                // golden number
                if (response.Item5 != null && response.Item5.Count() > 0)
                    foreach (string number in response?.Item5)
                    {
                        var input = inputs.First(o => o.input == number);
                        input.golden = true;
                    }



                foreach (string subRequest in request.subRequestTypes)
                {
                    nsRequest.requestWithSubRequest.Add(new RequestWithSubRequest(nsRequest.Id, subRequest));
                }

                nsRequest.requestInputs = inputs;

                nsRequest.subRequestTypeIds = request.subRequestTypes;


                //return subRequests;

                await _requestService.Save(nsRequest);
                return Result.Success<Requests>(nsRequest, "Successfully created request");
            }

        }
    }
}
