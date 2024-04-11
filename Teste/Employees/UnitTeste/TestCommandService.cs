using EmployeeAPI.Constants;
using EmployeeAPI.Dto;
using EmployeeAPI.Exceptions;
using EmployeeAPI.Models;
using EmployeeAPI.Repository.interfaces;
using EmployeeAPI.Service;
using EmployeeAPI.Service.interfaces;
using FluentMigrator.Builders.Create;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Employees.Helpers;

namespace Teste.Employees.UnitTeste
{
    public class TestCommandService
    {
        private readonly Mock<IRepository> _mock;
        private readonly ICommandService _commandService;

        public TestCommandService()
        {
            _mock = new Mock<IRepository>();
            _commandService = new CommandService(_mock.Object);
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

            _mock.Setup(repo => repo.Create(createRequest)).ReturnsAsync((Employee)null);
            var exception = await Assert.ThrowsAsync<InvalidSalary>(() => _commandService.Create(createRequest));

            Assert.Equal(Constants.InvalidSalary, exception.Message);
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

            var Employee = TestEmployeeFactory.CreateEmployee(50);
            Employee.Salary = createRequest.Salary;

            _mock.Setup(repo => repo.Create(It.IsAny<CreateRequest>())).ReturnsAsync(Employee);

            var result = await _commandService.Create(createRequest);

            Assert.NotNull(result);
            Assert.Equal(result.Salary, createRequest.Salary);
        }

        [Fact]
        public async Task Update_ItemDoesNotExist()
        {
            var updateRequest = new UpdateRequest
            {
                Salary = 1000
            };

            _mock.Setup(repo => repo.GetById(50)).ReturnsAsync((Employee)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _commandService.Update(50, updateRequest));

            Assert.Equal(Constants.ItemDoesNotExist, exception.Message);
        }

        [Fact]
        public async Task Update_InvalidSalary()
        {
            var updateRequest = new UpdateRequest
            {
                Salary = 0
            };
            var Employee = TestEmployeeFactory.CreateEmployee(50);
            Employee.Salary = updateRequest.Salary.Value;
            _mock.Setup(repo => repo.GetById(50)).ReturnsAsync(Employee);

            var exception = await Assert.ThrowsAsync<InvalidSalary>(() => _commandService.Update(50, updateRequest));

            Assert.Equal(Constants.InvalidSalary, exception.Message);
        }

        [Fact]
        public async Task Update_ValidData()
        {
            var updateREquest = new UpdateRequest
            {
                Salary = 10000
            };

            var Employee = TestEmployeeFactory.CreateEmployee(1);
            Employee.Salary = updateREquest.Salary.Value;

            _mock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(Employee);
            _mock.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<UpdateRequest>())).ReturnsAsync(Employee);

            var result = await _commandService.Update(1, updateREquest);

            Assert.NotNull(result);
            Assert.Equal(Employee, result);

        }

        [Fact]
        public async Task Delete_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.DeleteById(It.IsAny<int>())).ReturnsAsync((Employee)null);

            var exception = await Assert.ThrowsAnyAsync<ItemDoesNotExist>(() => _commandService.Delete(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);

        }

        [Fact]
        public async Task Delete_ValidData()
        {
            Employee Employee = TestEmployeeFactory.CreateEmployee(50);

            _mock.Setup(repo => repo.GetById(It.IsAny<int>())).ReturnsAsync(Employee);

            var restul = await _commandService.Delete(50);

            Assert.NotNull(restul);
            Assert.Equal(Employee, restul);
        }

    }
}
