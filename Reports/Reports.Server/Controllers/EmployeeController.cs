using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reports.DAL.Entities;
using Reports.Server.Database;
using Reports.Server.Services;

namespace Reports.Server.Controllers
{
    [ApiController]
    [Route("/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService service)
        {
            _employeeService = service;
        }

        [HttpPost]
        [Route("/employees/create")]
        public Employee Create([FromQuery] string name, [FromQuery] Guid bossId)
        {
            
            Employee employee = _employeeService.Create(name, bossId);
            return employee;
        }
        
        [HttpPost]
        [Route("/employees/createTeamlead")]
        public Employee CreateTeamlead([FromQuery] string name)
        {
            Employee employee = _employeeService.Create(name, Guid.Empty);
            return employee;
        }

        [HttpGet]
        [Route("/employees/find")]
        public IActionResult Find([FromQuery] string name, [FromQuery] Guid id)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Employee result = _employeeService.FindByName(name);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            if (id == Guid.Empty) return StatusCode((int) HttpStatusCode.BadRequest);
            {
                Employee result = _employeeService.FindById(id);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }
        }

        [HttpDelete]
        [Route("/employees/delete")]
        public IActionResult Delete([FromQuery] Guid id)
        {
            _employeeService.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("/employees/getall")]
        public IActionResult GetAll()
        {
            return Ok(_employeeService.GetAll());
        }

        [HttpPut]
        [Route("/employees/update")]
        public IActionResult Update([FromQuery] Guid id, [FromQuery] Guid bossId)
        {
            Employee employee = _employeeService.FindById(id);
            var newEmployee = new Employee(employee.Id, employee.Name, bossId);
            _employeeService.Update(newEmployee);
            return Ok();
        }
    }
}