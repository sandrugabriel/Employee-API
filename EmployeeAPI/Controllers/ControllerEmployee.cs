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
            var employee = await _repository.GetAllAsync();
            return Ok(employee);
        }

        [HttpGet("/findById")]
        public async Task<ActionResult<Employee>> GetById([FromQuery] int id)
        {
            var employee = await _repository.GetByIdAsync(id);
            return Ok(employee);
        }

        [HttpGet("/findByName/{name}")]
        public async Task<ActionResult<Employee>> GetByName([FromRoute] string name)
        {
            var employee = await _repository.GetByNameAsync(name);
            return Ok(employee);
        }

    }
}
