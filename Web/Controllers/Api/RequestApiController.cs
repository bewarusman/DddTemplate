using Application.NsRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers.Api;

[Route("api/request")]
[ApiController]
[Authorize]
public class RequestApiController : BaseController
{
    private readonly IMediator _mediator;
    public RequestApiController(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IActionResult> GetHistory(int start, int length, string number, string from_date, string to_date)
    {
        //var command = new FindArchiveCourtLetterHistoryQuery(start, length, number, lang, team);
        //var result = await Mediator.Send(command);
        return Ok("asdas");
    }

    [HttpPost("validate")]
    public async Task<IActionResult> Validate([FromBody] ValidationQuery model)
    {
        var result = await _mediator.Send(model);
        return Ok(result);
    }
}
