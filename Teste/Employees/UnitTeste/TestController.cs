using EmployeeAPI.Constants;
using EmployeeAPI.Controllers;
using EmployeeAPI.Controllers.interfaces;
using EmployeeAPI.Dto;
using EmployeeAPI.Exceptions;
using EmployeeAPI.Models;
using EmployeeAPI.Service.interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Employees.Helpers;

namespace Teste.Employees.UnitTeste
{
    public class TestController
    {

        private readonly Mock<ICommandService> _mockCommandService;
        private readonly Mock<IQueryService> _mockQueryService;
        private readonly ControllerAPI EmployeeApiController;

        public TestController()
        {
            _mockCommandService = new Mock<ICommandService>();
            _mockQueryService = new Mock<IQueryService>();

            EmployeeApiController = new ControllerEmployee(_mockQueryService.Object, _mockCommandService.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mockQueryService.Setup(repo => repo.GetAll()).ThrowsAsync(new ItemsDoNotExist(Constants.ItemsDoNotExist));

            var restult = await EmployeeApiController.GetAll();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(restult.Result);

            Assert.Equal(Constants.ItemsDoNotExist, notFoundResult.Value);
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var Employees = TestEmployeeFactory.CreateEmployees(5);
            _mockQueryService.Setup(repo => repo.GetAll()).ReturnsAsync(Employees);

            var result = await EmployeeApiController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            var allEmployees = Assert.IsType<List<Employee>>(okResult.Value);

            Assert.Equal(4, allEmployees.Count);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public async Task Create_InvalidSalary()
        {

            var createRequest = new CreateRequest
            {
                Name = "test",
                Departament = "test",
                Salary = 0
            };

            _mockCommandService.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).
                ThrowsAsync(new InvalidSalary(Constants.InvalidSalary));

            var result = await EmployeeApiController.CreateEmployee(createRequest);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);

            Assert.Equal(400, badRequest.StatusCode);
            Assert.Equal(Constants.InvalidSalary, badRequest.Value);

        }

        [Fact]
        public async Task Create_ValidData()
        {
            var createRequest = new CreateRequest
            {

                Name = "test",
                Departament = "test",
                Salary = 1000
            };

            var Employee = TestEmployeeFactory.CreateEmployee(1);
            Employee.Salary = createRequest.Salary;
            Employee.Name = createRequest.Name;
            Employee.Departament = createRequest.Departament;

            _mockCommandService.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).ReturnsAsync(Employee);

            var result = await EmployeeApiController.CreateEmployee(createRequest);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, Employee);

        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var update = new UpdateRequest
            {
                Salary = 0
            };

            _mockCommandService.Setup(repo => repo.Update(1, update)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await EmployeeApiController.UpdateEmployee(1, update);

            var ntFound = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal(ntFound.StatusCode, 404);
            Assert.Equal(Constants.ItemDoesNotExist, ntFound.Value);

        }
        [Fact]
        public async Task Update_ValidData()
        {
            var update = new UpdateRequest
            {
                Salary = 1000
            };

            var Employee = TestEmployeeFactory.CreateEmployee(1);

            _mockCommandService.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<UpdateRequest>())).ReturnsAsync(Employee);

            var result = await EmployeeApiController.UpdateEmployee(1, update);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(okResult.StatusCode, 200);
            Assert.Equal(okResult.Value, Employee);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mockCommandService.Setup(repo => repo.Delete(1)).ThrowsAsync(new ItemDoesNotExist(Constants.ItemDoesNotExist));

            var result = await EmployeeApiController.DeleteEmployee(1);

            var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);

            Assert.Equal(notFound.StatusCode, 404);
            Assert.Equal(notFound.Value, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {

            var Employee = TestEmployeeFactory.CreateEmployee(1);

            _mockCommandService.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(Employee);

            var result = await EmployeeApiController.DeleteEmployee(1);

            var okresult = Assert.IsType<OkObjectResult>(result.Result);

            Assert.Equal(200, okresult.StatusCode);
            Assert.Equal(okresult.Value, Employee);

        }
    }
}
