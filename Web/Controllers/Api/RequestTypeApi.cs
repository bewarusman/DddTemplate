using Application.RequestTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Api
{
    [Route("api/requestType")]
    [ApiController]
    public class RequestTypeApi : ControllerBase
    {
        private readonly IMediator _mediator;

        public RequestTypeApi(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(string requestId)
        {
            var query = new FindSubRequestByRequestIdQuery();
            query.requestTypeId = requestId;
            var result = _mediator.Send(query);

            return Ok(result);

        }

        public async Task<IActionResult> get([FromBody] string requestId)
        {


            return Ok("dsadasd");

        }
    }
}
