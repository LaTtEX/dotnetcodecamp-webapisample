using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemo.Models
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        void UpdateEmployee(int id, Employee employee);
        void AddEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        bool EmployeeExists(int id);
        void Dispose();
    }
}
