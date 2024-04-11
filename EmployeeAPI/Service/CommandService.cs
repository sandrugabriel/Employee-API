using EmployeeAPI.Dto;
using EmployeeAPI.Exceptions;
using EmployeeAPI.Models;
using EmployeeAPI.Repository.interfaces;
using EmployeeAPI.Service.interfaces;

namespace EmployeeAPI.Service
{
    public class CommandService : ICommandService
    {

        private IRepository _repository;

        public CommandService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Employee> Create(CreateRequest request)
        {

            if (request.Salary <= 0)
            {
                throw new InvalidSalary(Constants.Constants.InvalidSalary);
            }

            var employee = await _repository.Create(request);

            return employee;
        }

        public async Task<Employee> Update(int id, UpdateRequest request)
        {

            var employee = await _repository.GetById(id);
            if (employee == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }


            if (employee.Salary <= 0)
            {
                throw new InvalidSalary(Constants.Constants.InvalidSalary);
            }
            employee = await _repository.Update(id, request);
            return employee;
        }

        public async Task<Employee> Delete(int id)
        {

            var employee = await _repository.GetById(id);
            if (employee == null)
            {
                throw new ItemDoesNotExist(Constants.Constants.ItemDoesNotExist);
            }
            await _repository.DeleteById(id);
            return employee;
        }


    }
}
