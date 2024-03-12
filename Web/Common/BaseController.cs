using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Common;

public class BaseController : Controller
{
    private IMediator mediator;

    protected IMediator Mediator
        => this.mediator ??= this.HttpContext
            .RequestServices
            .GetService<IMediator>();
}