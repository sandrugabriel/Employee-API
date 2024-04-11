using EmployeeAPI.Constants;
using EmployeeAPI.Exceptions;
using EmployeeAPI.Models;
using EmployeeAPI.Repository.interfaces;
using EmployeeAPI.Service;
using EmployeeAPI.Service.interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Employees.Helpers;

namespace Teste.Employees.UnitTeste
{
    public class TestQueryService
    {
        private readonly Mock<IRepository> _mock;
        private readonly IQueryService _service;

        public TestQueryService()
        {
            _mock = new Mock<IRepository>();
            _service = new QueryService(_mock.Object);
        }

        [Fact]
        public async Task GetAll_ItemsDoNotExist()
        {
            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Employee>());

            var exception = await Assert.ThrowsAsync<ItemsDoNotExist>(() => _service.GetAll());

            Assert.Equal(exception.Message, Constants.ItemsDoNotExist);
        }

        [Fact]
        public async Task GetAll_ValidData()
        {
            var banks = TestEmployeeFactory.CreateEmployees(5);

            _mock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(banks);

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.Equal(banks, result);

        }

        [Fact]
        public async Task GetById_ItemDoesNotExist()
        {
            _mock.Setup(repo => repo.GetById(1)).ReturnsAsync((Employee)null);

            var exception = await Assert.ThrowsAsync<ItemDoesNotExist>(() => _service.GetById(1));

            Assert.Equal(exception.Message, Constants.ItemDoesNotExist);
        }

        [Fact]
        public async Task GetById_ValidData()
        {
            var bank = TestEmployeeFactory.CreateEmployee(1);
            _mock.Setup(repo => repo.GetById(1)).ReturnsAsync(bank);

            var result = await _service.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(bank, result);

        }

    }
}
