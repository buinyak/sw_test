using Microsoft.AspNetCore.Mvc;
using sw_test.Models;
using sw_test.Services.Interfaces;

namespace sw_test.Controllers
{
    [ApiController]
    [Route("employee/[action]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartementService _departementService;

        public EmployeeController(IEmployeeService employeeService,IDepartementService departementService)
        {
            _employeeService = employeeService;
            _departementService = departementService;
        }
        [HttpPost]
        public IActionResult Create([FromBody] Employee employee)
        {
            try
            {
                if (employee.DepartmentId == 0)
                {
                    return BadRequest(new { error = "Не указан DepartmentId" });
                }
                else if (_departementService.Get(employee.DepartmentId) == null)
                {
                    return BadRequest(new { error = "Неверный DepartmentId, такого отдела не существует", departments= _departementService.GetAll() });
                }
                return Ok(new { id =_employeeService.Create(employee) });
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_employeeService.Get(id) == null)
                {
                    return BadRequest(new { error = "Сотрудника с таким Id не существует" });
                }
                _employeeService.Delete(id);
                return Ok("Сотрудник с id " + id + " удален");
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public ActionResult ByCompanyId(int id)
        {
            try
            {
                return Ok(_employeeService.GetByCompanyId(id));

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public ActionResult ByDepartmentId(int id)
        {
            try
            {
                return Ok(_employeeService.GetByDepartmentId(id));

            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult Update([FromBody]  Employee employee)
        {
            try
            {
                if (employee.Id == 0)
                {
                    return BadRequest(new { error = "Не указан Id сотрудника" });
                }
                else if (_employeeService.Get(employee.Id) == null)
                {
                    return BadRequest(new { error = "Сотрудника с таким Id не существует" });
                } 
                else if (_departementService.Get(employee.DepartmentId) == null)
                {
                    return BadRequest(new { error = "Неверный DepartmentId, такого отдела не существует", departments = _departementService.GetAll() });
                }
                return Ok("Сотрудник с id "+_employeeService.Update(employee).Id+" изменен");
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
