using CRUD.API.ASP.NetCore.Data;
using CRUD.API.ASP.NetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.API.ASP.NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get All Employees
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var emps = await _context.employees.Include(x=> x.Department).ToListAsync();
            return Ok(emps);
        }

        // Get single Employee
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetEmployee")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid id)
        {
            var emp = await _context.employees.FirstOrDefaultAsync(x => x.Id == id);
            if(emp != null)
            {
                return Ok(emp);
            }
            else
            {
                return NotFound("Employee Is Not Found !");
            }
            
        }

        // Create Employee
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            employee.Id = Guid.NewGuid();
            await _context.employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // Update Employee 
        [HttpPut]
        public async  Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            var existemployee = await _context.employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
            if(existemployee != null)
            {
                existemployee.Name = employee.Name;
                existemployee.BirthDate = employee.BirthDate;
                existemployee.JobNumber = employee.JobNumber;
                existemployee.DepartmentId = employee.DepartmentId;
                await _context.SaveChangesAsync();
                return Ok(existemployee);
            }
            return NotFound("Employee Is Not Found !");
        }

        // Delete Employee
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var existemployee = _context.employees.FirstOrDefault(x => x.Id == id);
            if (existemployee != null)
            {
                _context.employees.Remove(existemployee);
                await _context.SaveChangesAsync();
                return Ok(existemployee);
            }
            return NotFound("Employee Is Not Found !");
        }
    }
}
