using Microsoft.EntityFrameworkCore;
using TreeDomainLibrary.Models.Enums;

namespace TreeDomainLibrary.Models
{
    public class OfficeContext : DbContext
    {

        public OfficeContext(DbContextOptions<OfficeContext> options): base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                 .HasOne(e => e.Parent)
                 .WithMany(e => e.DirectReports)
                 .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Employee>().HasData(GetRadomEmployeeData());
        }

        private static List<Employee> GetRadomEmployeeData()
        {
            return [ new Employee { Id = 1, Name = "John Doe", EmployeeId = "JD001", JobTitle = "Founder", Role = RoleEnum.Founder },
                new Employee { Id = 2, Name = "Jane Smith", EmployeeId = "JS002", JobTitle = "Director", Role = RoleEnum.Director, ParentId = 1 },
                new Employee { Id = 3, Name = "Bob Johnson", EmployeeId = "BJ003", JobTitle = "Manager", Role = RoleEnum.Manager, ParentId = 2 },
                new Employee { Id = 4, Name = "Alice Brown", EmployeeId = "AB004", JobTitle = "Team Lead", Role = RoleEnum.TeamLead, ParentId = 3 },
                new Employee { Id = 5, Name = "Mike Davis", EmployeeId = "MD005", JobTitle = "Team Employee", Role = RoleEnum.TeamEmployee, ParentId = 4 },
                new Employee { Id = 6, Name = "Emily Chen", EmployeeId = "EC006", JobTitle = "Software Engineer", Role = RoleEnum.TeamEmployee, ParentId = 4 },
                new Employee { Id = 7, Name = "David Lee", EmployeeId = "DL007", JobTitle = "QA Engineer", Role = RoleEnum.TeamEmployee, ParentId = 4 },
                new Employee { Id = 8, Name = "Sarah Taylor", EmployeeId = "ST008", JobTitle = "DevOps Engineer", Role = RoleEnum.TeamEmployee, ParentId = 4 },
                new Employee { Id = 9, Name = "Kevin White", EmployeeId = "KW009", JobTitle = "Product Manager", Role = RoleEnum.Manager, ParentId = 2 },
                new Employee { Id = 10, Name = "Lisa Nguyen", EmployeeId = "LN010", JobTitle = "UX Designer", Role = RoleEnum.TeamEmployee, ParentId = 9 },
                new Employee { Id = 11, Name = "Michael Patel", EmployeeId = "MP011", JobTitle = "Backend Engineer", Role = RoleEnum.TeamEmployee, ParentId = 9 },
                new Employee { Id = 12, Name = "Rachel Kim", EmployeeId = "RK012", JobTitle = "Frontend Engineer", Role = RoleEnum.TeamEmployee, ParentId = 9 },
                new Employee { Id = 13, Name = "Christopher Hall", EmployeeId = "CH013", JobTitle = "Full Stack Engineer", Role = RoleEnum.TeamEmployee, ParentId = 9 },
                new Employee { Id = 14, Name = "Jessica Martin", EmployeeId = "JM014", JobTitle = "Data Scientist", Role = RoleEnum.TeamEmployee, ParentId = 9 },
                new Employee { Id = 15, Name = "Brian Thompson", EmployeeId = "BT015", JobTitle = "Data Analyst", Role = RoleEnum.TeamEmployee, ParentId = 9 },
                new Employee { Id = 16, Name = "Olivia Walker", EmployeeId = "OW016", JobTitle = "Marketing Manager", Role = RoleEnum.Manager, ParentId = 2 },
                new Employee { Id = 17, Name = "Alexander Brown", EmployeeId = "AB017", JobTitle = "Marketing Specialist", Role = RoleEnum.TeamEmployee, ParentId = 16 },
                new Employee { Id = 18, Name = "Samantha Lee", EmployeeId = "SL018", JobTitle = "Sales Manager", Role = RoleEnum.Manager, ParentId = 2 },
                new Employee { Id = 19, Name = "Julia Kim", EmployeeId = "JK019", JobTitle = "Sales Representative", Role = RoleEnum.TeamEmployee, ParentId = 18 },
                new Employee { Id = 20, Name = "Daniel Patel", EmployeeId = "DP020", JobTitle = "Customer Support", Role = RoleEnum.TeamEmployee, ParentId = 18 },
                new Employee { Id = 21, Name = "Jacob Lee", EmployeeId = "JL021", JobTitle = "Software Engineer", Role = RoleEnum.TeamEmployee, ParentId = 3 },
                new Employee { Id = 22, Name = "Jasmine Chen", EmployeeId = "JC022", JobTitle = "QA Engineer", Role = RoleEnum.TeamEmployee, ParentId = 3 },
                new Employee { Id = 23, Name = "Jordan White", EmployeeId = "JW023", JobTitle = "DevOps Engineer", Role = RoleEnum.TeamEmployee, ParentId = 3 },
                new Employee { Id = 24, Name = "Jessica Brown", EmployeeId = "JB024", JobTitle = "Team Lead", Role = RoleEnum.TeamLead, ParentId = 3 },
                new Employee { Id = 25, Name = "James Davis", EmployeeId = "JD025", JobTitle = "Team Employee", Role = RoleEnum.TeamEmployee, ParentId = 4 },
                new Employee { Id = 26, Name = "Jennifer Chen", EmployeeId = "JC026", JobTitle = "Software Engineer", Role = RoleEnum.TeamEmployee, ParentId = 4 },
                new Employee { Id = 27, Name = "Joseph Lee", EmployeeId = "JL027", JobTitle = "QA Engineer", Role = RoleEnum.TeamEmployee, ParentId = 4 },
                new Employee { Id = 28, Name = "Julia Taylor", EmployeeId = "JT028", JobTitle = "DevOps Engineer", Role = RoleEnum.TeamEmployee, ParentId = 4 },
                new Employee { Id = 29, Name = "Jonathan White", EmployeeId = "JW029", JobTitle = "Product Manager", Role = RoleEnum.Manager, ParentId = 2 },
                new Employee { Id = 30, Name = "Jacqueline Brown", EmployeeId = "JB030", JobTitle = "UX Designer", Role = RoleEnum.TeamEmployee, ParentId = 9 },
                new Employee { Id = 31, Name = "Joshua Davis", EmployeeId = "JD031", JobTitle = "Backend Engineer", Role = RoleEnum.TeamEmployee, ParentId = 9 },
                new Employee { Id = 32, Name = "Jessica Kim", EmployeeId = "JK032", JobTitle = "Frontend Engineer", Role = RoleEnum.TeamEmployee, ParentId = 9 },
                new Employee { Id = 33, Name = "Joseph Hall", EmployeeId = "JH033", JobTitle = "Full Stack Engineer", Role = RoleEnum.TeamEmployee, ParentId = 9 },
                new Employee { Id = 34, Name = "Jennifer Martin", EmployeeId = "JM034", JobTitle = "Data Scientist", Role = RoleEnum.TeamEmployee, ParentId = 9 }
            ];
        }
    }
}
