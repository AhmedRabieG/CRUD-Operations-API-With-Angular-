using System.ComponentModel.DataAnnotations;

namespace CRUD.API.ASP.NetCore.Models
{
    public class Department
    {
        [Key] 
        public Guid Id { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        //public List<Employee> Employees { get; set; }

    }
}
