using Application.Common.Services;
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
    private readonly IServerFileManager _serverFileManager;

    public NSRequestController(ILogger<HomeController> logger, IMediator mediator, IServerFileManager serverFileManager)
    {
        _logger = logger;
        _mediator = mediator;
        _serverFileManager = serverFileManager;
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

    public IActionResult DealerCreate()
    {
        return View(new StoreRequestCommand());
    }

    [HttpPost]
    public async Task<IActionResult> Store([FromForm] StoreRequestCommand command)
    {
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
    public async Task<IActionResult> Validate([FromRoute] string inputs)
    {
        var query = new ValidationQuery();
        query.RequestInput = inputs;
        var result = await _mediator.Send(query);
        return Ok(result);
    }


    [HttpPost("Download/{id}")]
    public async Task<IActionResult> Download(string fileId)
    {
        var file = await _serverFileManager.Download(fileId);

        return File(file.Filebytes, "application/octet-stream", file.Filename);
    }

    [HttpGet("OpenFile/{id}")]
    public async Task<IActionResult> OpenFile(string Id)
    {

        var file = await _serverFileManager.Download(Id);

        var stream = new MemoryStream(file.Filebytes);

        Response.Headers.Add("Content-Disposition", "inline; filename=" + file.Filename);

        return new FileStreamResult(stream, file.IsPdf ? "application/pdf" : "application/octet-stream");
    }


}