using EmployeeAPI.Dto;
using EmployeeAPI.Models;
using EmployeeAPI.Repository.interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmployeeAPI.Controllers
{
    [ApiController]
    [Route("api/v1/employee")]
    public class ControllerEmployee : ControllerBase
    {

        private readonly ILogger<ControllerEmployee> _logger;

        private IRepository _repository;

        public ControllerEmployee(ILogger<ControllerEmployee> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }


        [HttpPost("/create")]
        public async Task<ActionResult<Employee>> Create([FromBody] CreateRequest request)
        {
            var employee = await _repository.Create(request);
            return Ok(employee);

        }

        [HttpPut("/update")]
        public async Task<ActionResult<Employee>> Update([FromQuery] int id, [FromBody] UpdateRequest request)
        {
            var employee = await _repository.Update(id, request);
            return Ok(employee);
        }

        [HttpDelete("/deleteById")]
        public async Task<ActionResult<Employee>> DeleteCarById([FromQuery] int id)
        {
            var employee = await _repository.DeleteById(id);
            return Ok(employee);
        }

    }
}
