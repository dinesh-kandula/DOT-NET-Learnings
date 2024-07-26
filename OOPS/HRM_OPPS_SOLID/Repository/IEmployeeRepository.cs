using HRM_OPPS_SOLID.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace HRM_OPPS_SOLID.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeAsync(int id);
        Task<ICollection<Employee>> GetEmployeesAsync();
        Task CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
    }
}
