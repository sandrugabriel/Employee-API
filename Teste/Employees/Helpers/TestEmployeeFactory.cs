using EmployeeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Employees.Helpers
{
    public class TestEmployeeFactory
    {

        public static Employee CreateEmployee(int id)
        {
            return new Employee
            {
                Id = id,
                Departament = "test",
                Name = "test",
                Salary = 1000*id

            };
        }

        public static List<Employee> CreateEmployees(int count)
        {

            List<Employee> list = new List<Employee>();
            for (int i = 1; i < count; i++)
            {
                list.Add(CreateEmployee(i));
            }

            return list;
        }
    }
}
