using System.Net;
using System.Text;
using EmployeeAPI.Dto;
using EmployeeAPI.Models;
using Newtonsoft.Json;
using Teste.Employees.Helpers;
using Teste.Employees.Infrastructure;

namespace Teste.Employees.IntegrationTest;


public class IntegrationTest : IClassFixture<ApiWebApplicationFactory>
{
    
        private readonly HttpClient _client;

        public IntegrationTest(ApiWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllEmployees_EmployeesFound_ReturnsOkStatusCode_ValidResponse()
        {
            var createEmployeeRequest = TestEmployeeFactory.CreateEmployee(1);
            var content = new StringContent(JsonConvert.SerializeObject(createEmployeeRequest), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/v1/ControllerEmployee/createEmployee", content);

            var response = await _client.GetAsync("/api/v1/ControllerEmployee/all");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetEmployeeById_EmployeeFound_ReturnsOkStatusCode()
        {
            var createEmployeeRequest = new CreateRequest
            { Name = "test", Departament = "ASasdadd", Salary = 2000};
            var content = new StringContent(JsonConvert.SerializeObject(createEmployeeRequest), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/v1/ControllerEmployee/createEmployee", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Employee>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result.Name, createEmployeeRequest.Name);
        }

        [Fact]
        public async Task GetEmployeeById_EmployeeNotFound_ReturnsNotFoundStatusCode()
        {
            var response = await _client.GetAsync("/api/v1/ControllerEmployee/findById?id=9999");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode()
        {
            var request = "/api/v1/ControllerEmployee/createEmployee";
            
            var createEmployee = new CreateRequest
                { Name = "test", Departament = "ASasdadd", Salary = 2000};

            var content = new StringContent(JsonConvert.SerializeObject(createEmployee), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Employee>(responseString);

            Assert.NotNull(result);
            Assert.Equal(createEmployee.Name, result.Name);
        }

        [Fact]
        public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode()
        {
            var request = "/api/v1/ControllerEmployee/createEmployee";
            var createEmployee = new CreateRequest
                { Name = "test", Departament = "ASasdadd", Salary = 2000};

            var content = new StringContent(JsonConvert.SerializeObject(createEmployee), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Employee>(responseString);

            request = $"/api/v1/ControllerEmployee/updateEmployee?id={result.Id}";
            var updateEmployee = new UpdateRequest { Salary = 20 };
            content = new StringContent(JsonConvert.SerializeObject(updateEmployee), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Employee>(responceStringUp);


            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Salary, updateEmployee.Salary);
        }

        [Fact]
        public async Task Put_Update_InvalidEmployeeSalary_ReturnsBadRequestStatusCode()
        {
            var request = "/api/v1/ControllerEmployee/createEmployee";
            var createEmployee = new CreateRequest
                { Name = "test", Departament = "ASasdadd", Salary = 2000};

            var content = new StringContent(JsonConvert.SerializeObject(createEmployee), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Employee>(responseString);

            request = $"/api/v1/ControllerEmployee/updateEmployee?id={result.Id}";
            var updateEmployee = new UpdateRequest { Salary = -3 };
            content = new StringContent(JsonConvert.SerializeObject(updateEmployee), Encoding.UTF8, "application/json");

            response = await _client.PutAsync(request, content);
            var responceStringUp = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Employee>(responseString);


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.NotEqual(result1.Salary, updateEmployee.Salary);
        }

        [Fact]
        public async Task Put_Update_EmployeeDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerEmployee/updateEmployee";
            var updateEmployee = new UpdateRequest { Salary = 30 };
            var content = new StringContent(JsonConvert.SerializeObject(updateEmployee), Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(request, content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_Delete_EmployeeExists_ReturnsDeletedEmployee()
        {
            var request = "/api/v1/ControllerEmployee/createEmployee";
            var createEmployee = new CreateRequest
                { Name = "test", Departament = "ASasdadd", Salary = 2000};

            var content = new StringContent(JsonConvert.SerializeObject(createEmployee), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(request, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Employee>(responseString)!;

            request = $"/api/v1/ControllerEmployee/deleteEmployee?id={result.Id}";

            response = await _client.DeleteAsync(request);
            var responceString = await response.Content.ReadAsStringAsync();
            var result1 = JsonConvert.DeserializeObject<Employee>(responseString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(result1.Name, createEmployee.Name);
        }

        [Fact]
        public async Task Delete_Delete_EmployeeDoesNotExist_ReturnsNotFoundStatusCode()
        {
            var request = "/api/v1/ControllerEmployee/deleteEmployee?id=7";

            var response = await _client.DeleteAsync(request);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

}