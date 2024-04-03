using EmployeeAPI.Dto;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers.interfaces
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ControllerAPI:ControllerBase
    {


        [HttpGet("/all")]
        [ProducesResponseType(statusCode: 200, type: typeof(List<Employee>))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<List<Employee>>> GetAll();

        [HttpGet("/findById")]
        [ProducesResponseType(statusCode: 200, type: typeof(Employee))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Employee>> GetById(int id);

        [HttpPost("/createEmployee")]
        [ProducesResponseType(statusCode: 201, type: typeof(Employee))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        public abstract Task<ActionResult<Employee>> CreateEmployee(CreateRequest request);

        [HttpPut("/updateEmployee")]
        [ProducesResponseType(statusCode: 200, type: typeof(Employee))]
        [ProducesResponseType(statusCode: 400, type: typeof(String))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Employee>> UpdateEmployee(int id, UpdateRequest request);

        [HttpDelete("/deleteEmployee")]
        [ProducesResponseType(statusCode: 200, type: typeof(Employee))]
        [ProducesResponseType(statusCode: 404, type: typeof(String))]
        public abstract Task<ActionResult<Employee>> DeleteEmployee(int id);


    }
}
