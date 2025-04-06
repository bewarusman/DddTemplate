using Application.NsRequest;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Common;
using Web.Models;
namespace Web.Controllers;

[Authorize]
public class NSRequestController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMediator _mediator;

    public NSRequestController(ILogger<HomeController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var query = new FindAllRequestQuery();
        var result = await _mediator.Send(query);
        var requests = result.GetValue<List<Requests>>();

        return View(requests);
    }

    public IActionResult Create()
    {
        return View(new StoreRequestCommand());
    }

    [HttpPost]
    public async Task<IActionResult> Store([FromForm] StoreRequestCommand command)
    {
        if (!ModelState.IsValid)
        {
            return View(command);
        }

        var result = await _mediator.Send(command);

        return ReturnRedirect(
            "Index",
            "Create",
            result,
            null,
            null);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public async Task<IActionResult> History()
    {
        var query = new FindAllHistoryRequestQuery();
        var result = await _mediator.Send(query);
        var requests = result.GetValue<List<Requests>>();

        return View(requests);
    }
    public async Task<IActionResult> Display([FromRoute] string id)
    {
        var query = new FindByIdQuery(id);
        var result = await _mediator.Send(query);
        var request = result.GetValue<Requests>();
        return View(request);
    }
}