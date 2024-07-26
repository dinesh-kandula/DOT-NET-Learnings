using Microsoft.EntityFrameworkCore;
using TreeDomainLibrary.Models;
using TreeDomainLibrary.Models.ModelsDTO;

namespace TreeDomainLibrary.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private OfficeContext _context;

        public EmployeeRepository(OfficeContext context)
        {
            _context = context;
        }
        
        public async Task<List<Employee>> GetEmployeesAsync()
        {
             return await _context.Employees
                .Include(e => e.Parent)
                .Include(e => e.DirectReports)
                .ToListAsync();
        }

        public async Task<List<EmployeeFlatData>> GetFlatEmployeesDataAsync()
        {
            return await _context.Employees.Select(e => new EmployeeFlatData
            {
                Id = e.Id,
                Name = e.Name,
                EmployeeId = e.EmployeeId,
                JobTitle = e.JobTitle,
                ParentId = e.ParentId,
                Role = e.Role,
                HasChildren = e.DirectReports.Count > 0,
                ChildCount = e.DirectReports.Count
            }).ToListAsync();
            //return await _context.Employees.AsNoTracking().ToListAsync();
        }


        public async Task<Employee> GetEmployeeByEmployeeIdAsync(string employeeId)
        {
            //Employee? employee = await _context.Employees
            //                        .Include(e => e.DirectReports)
            //                        .ThenInclude(dr => dr.DirectReports)
            //                        .FirstOrDefaultAsync(e => e.EmployeeId.Equals(employeeId));
            //return employee!;

            var employee = await _context.Employees
                    .Include(e => e.Parent)
                    .FirstOrDefaultAsync(e => e.EmployeeId == employeeId);
            if(employee != null)
               LoadDirectReports(employee);

            return employee!;
        }


        public async Task<Employee> GetEmployeeByIdAsync(int Id)
        {
            var employee = await _context.Employees
                   .FirstOrDefaultAsync(e => e.Id == Id);
            return employee!;
        }

        public async void CreateEmployee(Employee employee)
        {
            await _context.AddAsync(employee);
            //await _context.Database.ExecuteSqlInterpolatedAsync(
            //    $"INSERT INTO Employees (Name, EmployeeId, JobTitle, Role, ParentId) VALUES('{employee.Name}', '{employee.EmployeeId}', '{employee.JobTitle}', {employee.Role}, {employee.ParentId});"
            //    );
        }


        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }


        /// <summary>
        /// Used to load the child of childs for each employee
        /// </summary>
        /// <param name="employee"></param>
        private void LoadDirectReports(Employee employee)
        {
            _context.Entry(employee)
                .Collection(e => e.DirectReports)
                .Load();

            foreach (var directReport in employee.DirectReports)
            {
                LoadDirectReports(directReport);
            }
        }
    }
}
