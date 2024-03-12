using Application.Common.Repositories;
using Application.Employees;
using Application.Salaries;
using Domain.EmployeeContext;
using Domain.SamtContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers.Api;


[Route("api/[controller]")]
[ApiController]
public class CustomerFormController : BaseApiController
{

    //[HttpGet]
    //public async Task<IActionResult> Get(string from, string to)
    //{
    //    var query = new FindCustomerFormByDateQuery { From = from, To = to };
    //    var result = await Mediator.Send(query);
    //    var records = result.GetValue<List<CustomerForm>>();
    //    return Ok(records);
    //}

    //[HttpGet("findByDates")]
    //public string Get()
    //{
    //    return "asd";
    //}

 




    //[HttpGet]
    //public async Task<IActionResult> Get(string from, string to)
    //{
    //    var query = new FindCustomerFormByDateQuery { From = from, To = to };
    //    var result = await Mediator.Send(query);
    //    var records = result.GetValue<List<CustomerForm>>();

      

    //    return Ok(records);
    //}


    [HttpGet]
    public async Task<IActionResult> Get(string from, string to)
    {
        var query = new FindCustomerFormByDateQuery { From = from, To = to };
        var result = await Mediator.Send(query);
        var records = result.GetValue<List<CustomerForm>>();
        return Ok(records);
    }


}
