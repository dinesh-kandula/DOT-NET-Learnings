using HRM_OPPS_SOLID.DomainModel;
using HRM_OPPS_SOLID.Repository;

namespace HRM_OPPS_SOLID.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        private EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            return await _repository.GetEmployeeAsync(id);
        }

        public async Task<ICollection<Employee>> GetEmployeesAsync()
        {
            return await _repository.GetEmployeesAsync();
        }

        public async Task CreateEmployeeAsync(Employee employee)
        {
            await _repository.CreateEmployeeAsync(employee);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _repository.UpdateEmployeeAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _repository.DeleteEmployeeAsync(id);
        }

        public Task<Leave> GetAvailableLeaves(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
