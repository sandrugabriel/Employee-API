using EmployeeAPI.Models;
using System;

namespace EmployeeAPI.Repository.interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();

        Task<Employee> GetByIdAsync(int id);

        Task<Employee> GetByNameAsync(string name);
    }
}
