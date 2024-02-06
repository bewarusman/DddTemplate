using Application.Employees;
using Domain.EmployeeContext;
using Microsoft.AspNetCore.Mvc;
using Web.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseApiController
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEmployeeCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(command);

            var result = await Mediator.Send(command);
            var employee = result.GetValue<Employee>();
            return Ok(result);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
