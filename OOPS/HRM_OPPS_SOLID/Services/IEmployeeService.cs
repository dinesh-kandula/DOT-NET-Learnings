using HRM_OPPS_SOLID.DomainModel;
using HRM_OPPS_SOLID.Repository;

namespace HRM_OPPS_SOLID.Services
{
    /// <summary>
    /// Serivces are used to connect with repositories to get the data
    /// and manipulate, services can connect wth different repositories
    /// Service contains main bussiness logic code related to Employee.
    /// </summary>
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeAsync(int id);
        Task<ICollection<Employee>> GetEmployeesAsync();
        Task CreateEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);

        // With Help of employeeId it will validate the valid employee and get the leave id -- Employee Repo
        // with leave Id, we will get the available leaves  --Leave Repo
        Task<Leave> GetAvailableLeaves(int employeeId);
    }
}
