using CRUD.API.ASP.NetCore.Data;
using CRUD.API.ASP.NetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.API.ASP.NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Get All Departments
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var depts = await _context.departments.ToListAsync();
            return Ok(depts);
        }


        // Get single Department
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetDepartment")]
        public async Task<IActionResult> GetDepartment([FromRoute] Guid id)
        {
            var dept = await _context.departments.FirstOrDefaultAsync(x => x.Id == id);
            if (dept != null)
            {
                return Ok(dept);
            }
            else
            {
                return NotFound("This Department Is Not Found !");
            }

        }


        // Create Department
        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] Department department)
        {
            department.Id = Guid.NewGuid();
            await _context.departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
        }


        // Update Department 
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] Guid id, [FromBody] Department department)
        {
            var existdepartment = await _context.departments.FirstOrDefaultAsync(x => x.Id == id);
            if (existdepartment != null)
            {
                existdepartment.DepartmentName = department.DepartmentName;
                await _context.SaveChangesAsync();
                return Ok(existdepartment);
            }
            return NotFound("This Department Is Not Found !");
        }

        // Delete Department
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] Guid id)
        {
            var existdepartment = _context.departments.FirstOrDefault(x => x.Id == id);
            if (existdepartment != null)
            {
                _context.departments.Remove(existdepartment);
                await _context.SaveChangesAsync();
                return Ok(existdepartment);
            }
            return NotFound("This Department Is Not Found !");
        }
    }
}
