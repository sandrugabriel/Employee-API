using EmployeeAPI.Dto;
using EmployeeAPI.Models;

namespace EmployeeAPI.Service.interfaces
{
    public interface ICommandService
    {
        Task<Employee> Create(CreateRequest request);

        Task<Employee> Update(int id, UpdateRequest request);

        Task<Employee> Delete(int id);
    }
}
