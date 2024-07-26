using HRM_OPPS_SOLID.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace HRM_OPPS_SOLID.DBContext
{
    public class SQLDBContext : DbContext
    {
        public SQLDBContext(DbContextOptions<SQLDBContext> options) : base(options)
        {
           
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Leave> Leaves { get; set; }

    }
}
