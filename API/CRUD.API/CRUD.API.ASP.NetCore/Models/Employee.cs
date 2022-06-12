using System;
using System.ComponentModel.DataAnnotations;

namespace CRUD.API.ASP.NetCore.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public DateTime BirthDate { get; set; }
        [Required]
        public int JobNumber { get; set; }

        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
