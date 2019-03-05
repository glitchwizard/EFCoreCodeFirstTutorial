using EFCoreCodeFirstSample.Models;
using EFCoreCodeFirstSample.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSample.Controllers
{
  [Route("api/employee")]
  [ApiController]
  public class EmployeeController : ControllerBase
  {
    private readonly IDataRepository<Employee> _dataRepository;

    public EmployeeController(IDataRepository<Employee> dataRepository)
    {
      _dataRepository = dataRepository;
    }

    // GET: api/employee
    [HttpGet]
    public IActionResult Get()
    {
      IEnumerable<Employee> employees = _dataRepository.GetAll();
      return Ok(employees);
    }

    // GET: api/employee/5
    [HttpGet("{id}", Name = "Get")]
    public IActionResult Get(long id)
    {
      Employee employee = _dataRepository.Get(id);

      if (employee == null)
      {
        return NotFound("The employee record couldn't be found.");
      }

      return Ok(employee);
    }

    
    [HttpPost]
    public IActionResult Post([FromBody] Employee employee)
    {
      if (employee == null)
      {
        return BadRequest("Employee is null.");
      }

      _dataRepository.Add(employee);
      return CreatedAtRoute(
        "Get",
        new { Id = employee.EmployeeId },
        employee);
    }

    // GET: api/employee/5
    [HttpPost("{id}")]
    public IActionResult Edit(long id, [FromBody] Employee employee)
    {
      if (employee == null)
      {
        return BadRequest("Employee is null.");
      }

      Employee employeeToUpdate = _dataRepository.Get(id);
      if (employeeToUpdate == null)
      {
        return NotFound("The employee record could not be found.");
      }

      _dataRepository.Update(employeeToUpdate, employee);

      return NoContent();
    }

    // GET: api/employee/Delete/5
    [HttpPost("{id}")]
    public IActionResult Delete(long id)
    {

      Employee employee = _dataRepository.Get(id);
      if (employee == null)
      {
        return NotFound("The employee record couldn't be found.");
      }

      _dataRepository.Delete(employee);

      return NoContent();
    }
  }
}