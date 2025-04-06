using Application.Common.Messaging;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Web.Common;

public class BaseController : Controller
{
    private IMediator mediator;
    private IAuthenticationService authenticationService;

    protected IMediator Mediator
        => this.mediator ??= this.HttpContext
            .RequestServices
            .GetService<IMediator>();

    protected IAuthenticationService AuthenticationService
        => this.authenticationService ??= this.HttpContext
            .RequestServices
            .GetService<IAuthenticationService>();


    protected IActionResult ReturnIfModelStateIsNotValid(string redirectAction, object redirectParams)
    {
        TempData["ERROR_MSG"] = string.Join(" | ", ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage));
        return RedirectToAction(redirectAction, redirectParams);
    }

    protected IActionResult ReturnView<TModel>(
        string action,
        Result result)
        where TModel : class, new()
    {
        if (result.Succeeded)
        {
            TempData["SUCCESS_MSG"] = result.SuccessMessage;
            var model = result.GetValue<TModel>();
            return View(action, model);
        }
        else
        {
            TempData["ERROR_MSG"] = result.ErrorMessage;
            return View(action);
        }
    }

    protected IActionResult ReturnViewElseRedirect<TModel>(
        string action,
        Result result,
        string redirectAction,
        object redirectParams = null)
        where TModel : class, new()
    {
        if (result.Succeeded)
        {
            if (result.SuccessMessage != "SUCCESS")
                TempData["SUCCESS_MSG"] = result.SuccessMessage;
            var model = result.GetValue<TModel>();
            if (model == null)
            {
                TempData["ERROR_MSG"] = "An error is occured! No information is found to display!";
                return RedirectToAction(redirectAction, redirectParams);
            }
            return View(action, model);
        }
        else
        {
            TempData["ERROR_MSG"] = result.ErrorMessage;
            return RedirectToAction(redirectAction, redirectParams);
        }
    }

    protected IActionResult ReturnRedirect(
        string successAction,
        string errorAction,
        Result result,
        object successRouteValues = null,
        object errorRouteValues = null)
    {
        if (result.Succeeded)
        {
            TempData["SUCCESS_MSG"] = result.SuccessMessage;
            return RedirectToAction(successAction, successRouteValues);
        }
        else
        {
            TempData["ERROR_MSG"] = result.ErrorMessage;
            return RedirectToAction(errorAction, errorRouteValues);
        }
    }

}