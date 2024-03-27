using EmployeeAPI.Dto;
using EmployeeAPI.Models;
using System;

namespace EmployeeAPI.Repository.interfaces
{
    public interface IRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();


        Task<Employee> Create(CreateRequest request);

        Task<Employee> Update(int id, UpdateRequest request);

        Task<Employee> DeleteById(int id);

    }
}
