using TreeDomainLibrary.Models;
using TreeDomainLibrary.Models.ModelsDTO;

namespace TreeDomainLibrary.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync();

        Task<List<EmployeeFlatData>> GetFlatEmployeesDataAsync();

        Task<Employee> GetEmployeeByEmployeeIdAsync(string employeeId);

        Task<Employee> GetEmployeeByIdAsync(int id);

        void CreateEmployee(Employee employee);

        void DeleteEmployee(Employee employee);

    }
}
