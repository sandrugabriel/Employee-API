using EmployeeAPI.Models;

namespace EmployeeAPI.Service.interfaces
{
    public interface IQueryService
    {
        Task<List<Employee>> GetAll();

        Task<Employee> GetById(int id);

    }
}
