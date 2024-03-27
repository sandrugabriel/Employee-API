using AutoMapper;
using EmployeeAPI.Data;
using EmployeeAPI.Dto;
using EmployeeAPI.Models;
using EmployeeAPI.Repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Repository
{
    public class RepositoryEmployee :IRepository
    {
        private AppDbContext _context;
        private IMapper _mapper;

        public RepositoryEmployee(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }



        public async Task<Employee> Create(CreateRequest request)
        {

            var employee = _mapper.Map<Employee>(request);

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return employee;

        }

        public async Task<Employee> Update(int id, UpdateRequest request)
        {

            var employee = await _context.Employees.FindAsync(id);

            employee.Name = request.Name ?? employee.Name;
            employee.Departament = request.Departament ?? employee.Departament;
            employee.Salary = request.Salary ?? employee.Salary;

            _context.Employees.Update(employee);

            await _context.SaveChangesAsync();

            return employee;

        }

        public async Task<Employee> DeleteById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return employee;
        }


    }
}
