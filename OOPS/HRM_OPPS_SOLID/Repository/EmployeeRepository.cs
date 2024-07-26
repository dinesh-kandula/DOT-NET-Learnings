using HRM_OPPS_SOLID.DBContext;
using HRM_OPPS_SOLID.DomainModel;
using Microsoft.EntityFrameworkCore;


namespace HRM_OPPS_SOLID.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SQLDBContext _context;

        public EmployeeRepository(SQLDBContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<ICollection<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await GetEmployeeAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}
