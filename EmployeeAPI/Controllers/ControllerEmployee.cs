using EmployeeAPI.Controllers.interfaces;
using EmployeeAPI.Dto;
using EmployeeAPI.Exceptions;
using EmployeeAPI.Models;
using EmployeeAPI.Repository.interfaces;
using EmployeeAPI.Service.interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmployeeAPI.Controllers
{

    public class ControllerEmployee : ControllerAPI
    {


        private IQueryService _queryService;
        private ICommandService _commandService;

        public ControllerEmployee(IQueryService queryService, ICommandService commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        public override async Task<ActionResult<List<Employee>>> GetAll()
        {
            try
            {
                var employees = await _queryService.GetAll();

                return Ok(employees);

            }
            catch (ItemsDoNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Employee>> GetById(int id)
        {

            try
            {
                var employee = await _queryService.GetById(id);
                return Ok(employee);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }

        }

        public override async Task<ActionResult<Employee>> CreateEmployee(CreateRequest request)
        {
            try
            {
                var employee = await _commandService.Create(request);
                return Ok(employee);
            }
            catch (InvalidSalary ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public override async Task<ActionResult<Employee>> UpdateEmployee(int id, UpdateRequest request)
        {
            try
            {
                var employee = await _commandService.Update(id, request);
                return Ok(employee);
            }
            catch (InvalidSalary ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }

        public override async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _commandService.Delete(id);
                return Ok(employee);
            }
            catch (ItemDoesNotExist ex)
            {
                return NotFound(ex.Message);
            }
        }




        /*private readonly ILogger<ControllerEmployee> _logger;

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
        }*/

    }
}
