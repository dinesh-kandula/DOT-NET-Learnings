using TreeDomainLibrary.Models;
using TreeDomainLibrary.Models.ModelsDTO;

namespace TreeDomainLibrary.Services
{
    public interface IEmployeeService
    {

        Task<List<EmployeeTreeNode>> GetEmployeesTreeAsync();

        Task<List<EmployeeFlatData>> GetFlatEmployeesDataAsync();

        Task<EmployeeTreeNode> GetEmployeeById(string employeeId);

        Task<EmployeeTreeNode> CreateEmployee(EmployeeDTO employeeDTO);

        Task<Employee> DeleteEmployee(string employeeId, int newParentId);

        Task<EmployeeTreeNode> UpdateEmployee(string employeeId, UpdateEmployeeDTO updateEmployeeDTO);

    }
}
