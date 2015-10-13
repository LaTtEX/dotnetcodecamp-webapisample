using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class EmployeesController : ApiController
    {
        private IEmployeeRepository repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Gets all employees
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable<Employee>))]
        public IHttpActionResult GetEmployees()
        {
            try
            {
                var result = repository.GetAllEmployees();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Employees/5
        /// <summary>
        /// Gets a specific employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            try
            {
                var employee = repository.GetEmployee(id);
                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Employees/5
        /// <summary>
        /// Updates employee details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != employee.Id)
                {
                    return BadRequest();
                }

                repository.UpdateEmployee(id, employee);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!repository.EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return InternalServerError(ex);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // POST: api/Employees
        /// <summary>
        /// Adds an employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                repository.AddEmployee(employee);

                return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Employees/5
        /// <summary>
        /// Deletes an employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            try
            {
                Employee employee = repository.GetEmployee(id);
                if (employee == null)
                {
                    return NotFound();
                }

                repository.DeleteEmployee(employee);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}