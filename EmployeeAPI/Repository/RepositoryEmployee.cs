using AutoMapper;
using EmployeeAPI.Data;
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

        public async Task<Employee> GetByIdAsync(int id)
        {
            List<Employee> all = await _context.Employees.ToListAsync();

            for(int i=0;i<all.Count;i++) {
                if (all[i].Id == id)
                {
                    return all[i];
                }
            }

            return null;
        }

        public async Task<Employee> GetByNameAsync(string name)
        {

            List<Employee> all = await _context.Employees.ToListAsync();

            for (int i = 0; i < all.Count; i++)
            {
                if (all[i].Name.Equals(name))
                {
                    return all[i];
                }
            }

            return null;

        }
    }
}
