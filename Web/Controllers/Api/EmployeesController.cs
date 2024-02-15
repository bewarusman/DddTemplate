using Application.Employees;
using Application.Salaries;
using Domain.EmployeeContext;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var query = new FindEmployeesQuery();
        var result = await Mediator.Send(query);
        var employees = result.GetValue<List<Employee>>();
        return Ok(employees);
    }

    


    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var query = new FindEmployeeByIdQuery { id = id };
        var result = await Mediator.Send(query);
        var employees = result.GetValue<List<Employee>>();
        return Ok(result);
    }

    // POST api/<ValuesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateSalaryCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(command);

        var result = await Mediator.Send(command);
        var employee = result.GetValue<Employee>();
        return Ok(employee);
    }

    // PUT api/<ValuesController>/5
    [HttpPut]
    public async Task<IActionResult> Put(UpdateEmployeeCommand command)
    {

        if (!ModelState.IsValid)
            return BadRequest(command);
        var result = await Mediator.Send(command);
        var employee = result.GetValue<Employee>();
        return Ok(result);
    }

    // DELETE api/<ValuesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}