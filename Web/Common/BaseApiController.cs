using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Common;

[ApiController]
public class BaseApiController : ControllerBase
{
    private IMediator mediator;

    protected IMediator Mediator
        => this.mediator ??= this.HttpContext
            .RequestServices
            .GetService<IMediator>();
}