using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIDemo.Models
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Update(int id, T t);
        void Add(T t);
        void Delete(T t);
        void Exists(int id);
    }

    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllDepartments();
        Department GetDepartment(int id);
        void UpdateDepartment(int id, Department department);
        void AddDepartment(Department department);
        void DeleteDepartment(Department department);
        bool DepartmentExists(int id);
        void Dispose();
    }
}
