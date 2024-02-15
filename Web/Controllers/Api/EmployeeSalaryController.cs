using Application.Employees;
using Application.Salaries;
using Domain.EmployeeContext;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSalaryController : BaseApiController
    {
        // GET: api/<EmployeeSalaryController>
        [HttpGet]
        public async Task<IActionResult>  Get()
        {
            var query = new FindEmployeeSalaryQuery();
            var result = await Mediator.Send(query);
            var employees = result.GetValue<List<EmployeeSalary>>();
            return Ok(employees);
        }

        // GET api/<EmployeeSalaryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "True";
        }

        // POST api/<EmployeeSalaryController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEmployeeSalaryCommand command)
        {

            if (!ModelState.IsValid)
                return BadRequest(command);
            var result = await Mediator.Send(command);
            var empSalary = result.GetValue<EmployeeSalary>();
            return Ok(result);
        }

        // PUT api/<EmployeeSalaryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeSalaryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
