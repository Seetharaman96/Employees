using Employees.Data;
using Employees.Models;
using Employees.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Employees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = dbContext.Employee.ToList();
            return Ok(employees);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = dbContext.Employee.Find(id);
            if (employee == null)
            {
                return NotFound();
            };
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployee)
        {
            var employee = new Employee()
            {
                Name = addEmployee.Name,
                Phone = addEmployee.Phone,
                City = addEmployee.City,
                Email = addEmployee.Email,
            };
            dbContext.Employee.Add(employee);
            dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, EditEmployeeDto editEmployee)
        {
            var employee = dbContext.Employee.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Name = editEmployee.Name;
            employee.Phone = editEmployee.Phone;
            employee.City = editEmployee.City;
            employee.Email = editEmployee.Email;

            dbContext.Employee.Update(employee);
            dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employee.Find(id);
            if(employee is null)
            {
                return NotFound();
            }
            dbContext.Employee.Remove(employee);
            dbContext.SaveChanges();
            return Ok();
        }
    }
}
