using EmployeeAPI.Exceptions;
using EmployeeAPI.Models;
using EmployeeAPI.Repository.interfaces;
using EmployeeAPI.Service.interfaces;

namespace EmployeeAPI.Service
{
    public class QueryService : IQueryService
    {

        private IRepository _repository;

        public QueryService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Employee>> GetAll()
        {
            var employees = await _repository.GetAllAsync();

            if (employees.Count() == 0)
            {
                throw new ItemsDoNotExist(Constants.Constants.ItemsDoNotExist);
            }

            return (List<Employee>)employees;
        }


        public async Task<Employee> GetById(int id)
        {
            var employees = await _repository.GetById(id);

            if (employees == null)
            {
                throw new ItemsDoNotExist(Constants.Constants.ItemDoesNotExist);
            }

            return employees;
        }


    }
}
