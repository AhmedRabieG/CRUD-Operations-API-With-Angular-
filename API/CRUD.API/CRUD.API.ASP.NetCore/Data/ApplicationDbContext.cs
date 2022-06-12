using CRUD.API.ASP.NetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.API.ASP.NetCore.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        // DbSet
        public DbSet<Employee> employees { get; set; }
        public DbSet<Department> departments { get; set; }

    }
}
