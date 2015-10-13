using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAPIDemo.Models
{
    public class WebAPIRepository : IDepartmentRepository, IEmployeeRepository, IDisposable
    {
        private WebAPIModel db = new WebAPIModel();

        public void AddDepartment(Department department)
        {
            db.Departments.Add(department);
            db.SaveChanges();
        }

        public void AddEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public void DeleteDepartment(Department department)
        {
            db.Departments.Remove(department);
            db.SaveChanges();
        }

        public void DeleteEmployee(Employee employee)
        {
            db.Employees.Remove(employee);
            db.SaveChanges();
        }

        public bool DepartmentExists(int id)
        {
            return db.Departments.Count(e => e.Id == id) > 0;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.Id == id) > 0;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return db.Departments;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return db.Employees;
        }

        public Department GetDepartment(int id)
        {
            Department department = db.Departments.Find(id);
            return department;
        }

        public Employee GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            return employee;
        }

        public void UpdateDepartment(int id, Department department)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(int id, Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}