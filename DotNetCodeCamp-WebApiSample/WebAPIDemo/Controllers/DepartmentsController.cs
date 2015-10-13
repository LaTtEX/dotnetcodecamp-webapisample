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
    public class DepartmentsController : ApiController
    {
        private IDepartmentRepository repository;

        public DepartmentsController(IDepartmentRepository repository)
        {
            this.repository = repository;
        }

        [ResponseType(typeof(IEnumerable<Department>))]
        public IHttpActionResult GetDepartments()
        {
            try
            {
                var departments = repository.GetAllDepartments();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Departments/5
        [ResponseType(typeof(Department))]
        public IHttpActionResult GetDepartment(int id)
        {
            try
            {
                Department department = repository.GetDepartment(id);
                if (department == null)
                {
                    return NotFound();
                }

                return Ok(department);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

        // PUT: api/Departments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartment(int id, [FromBody]Department department)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != department.Id)
                {
                    return BadRequest();
                }

                repository.UpdateDepartment(id, department);

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!repository.DepartmentExists(id))
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

        // POST: api/Departments
        [ResponseType(typeof(Department))]
        public IHttpActionResult PostDepartment([FromBody]Department department)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                repository.AddDepartment(department);

                return CreatedAtRoute("DefaultApi", new { id = department.Id }, department);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Departments/5
        [ResponseType(typeof(Department))]
        public IHttpActionResult DeleteDepartment(int id)
        {
            try
            {
                Department department = repository.GetDepartment(id);
                if (department == null)
                {
                    return NotFound();
                }

                repository.DeleteDepartment(department);

                return Ok(department);
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