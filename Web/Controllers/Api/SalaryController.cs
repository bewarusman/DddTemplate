using Application.Employees;
using Application.Salaries;
using Domain.EmployeeContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

namespace Web.Controllers.Api;


[Route("api/[controller]")]
[ApiController]
public class SalaryController : BaseApiController
{
    // GET: SalaryController
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new FindSalariesQuery();
        var result = await Mediator.Send(query);
        var salaries = result.GetValue<List<Salary>>();
        return Ok(salaries);
    }




    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var query = new FindSalariesById { id = id };
        var result = await Mediator.Send(query);
        var salaries = result.GetValue<List<Salary>>();
        return Ok(result);
    }

    //POST api/<ValuesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSalaryCommand command)
    {

        if (!ModelState.IsValid)
            return BadRequest(command);
        var result = await Mediator.Send(command);
        var salary = result.GetValue<Salary>();
        return Ok(result);
    }

    //Not Implemented Yet
    // PUT api/<ValuesController>/5
    //[HttpPut]
    //public async Task<IActionResult> Put(UpdateSalaryCommand command)
    //{

    //    if (!ModelState.IsValid)
    //        return BadRequest(command);
    //    var result = await Mediator.Send(command);
    //    var employee = result.GetValue<Employee>();
    //    return Ok(result);
    //}

    // DELETE api/<ValuesController>/5
 
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteSalaryByIdCommand { Id = id };
        var result = await Mediator.Send(command);

        return Ok(result);
    }

}
