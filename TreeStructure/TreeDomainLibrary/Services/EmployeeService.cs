using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TreeDomainLibrary.Models;
using TreeDomainLibrary.Models.Enums;
using TreeDomainLibrary.Models.ModelsDTO;
using TreeDomainLibrary.Repositories;

namespace TreeDomainLibrary.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IUnitOfWork unitOfWork, ILogger<EmployeeService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Get the Single Employee and its childrens Trees
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<EmployeeTreeNode> GetEmployeeById(string employeeId)
        {
            Employee employee = await _unitOfWork.EmployeeRepository.GetEmployeeByEmployeeIdAsync(employeeId)
                 ??
                throw new NullReferenceException($"No Employee found with employee id: {employeeId}");

            var tree = GetEmployeeTreeNode(employee);
            return tree;
        }

        /// <summary>
        /// Getting all the Employee list by creating the Employee Tree
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<List<EmployeeTreeNode>> GetEmployeesTreeAsync()
        {
            _logger.LogInformation("Calling Repo to get the data from DB");
            List<Employee> employees = await _unitOfWork.EmployeeRepository.GetEmployeesAsync();
            
            if(employees.Count == 0)
            {
                _logger.LogWarning("No Employees Data found.");
                throw new NullReferenceException("No Employees Data found.");
            }
            var rootEmployees = employees.Where(e => e.ParentId == null);
            var tree = new List<EmployeeTreeNode>();

            foreach (var rootEmployee in rootEmployees)
            {
                tree.Add(GetEmployeeTreeNode(rootEmployee));
            }

            return tree;
        }

        /// <summary>
        /// Get all the Employee Raw data [Table data]
        /// </summary>
        /// <returns></returns>
        public async Task<List<EmployeeFlatData>> GetFlatEmployeesDataAsync() {

            List<EmployeeFlatData> employees = await _unitOfWork.EmployeeRepository.GetFlatEmployeesDataAsync();
            if (employees.Count == 0)
            {
                throw new InvalidOperationException($"No Employees found.");
            }
            return employees;
        }

        /// <summary>
        /// Add new Employee and return Employee
        /// </summary>
        /// <param name="employeeDTO"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<EmployeeTreeNode> CreateEmployee(EmployeeDTO employeeDTO)
        {
            bool parentRequire = true;

            if (employeeDTO.ParentId == null)
            {
                if (employeeDTO.Role == RoleEnum.Founder)
                    parentRequire = false;
                else
                {
                    _logger.LogError($"Please provide the Parent ID, unless your are haveing the Founder Role.");
                    throw new InvalidOperationException($"Please provide the Parent ID, unless your are haveing the Founder Role.");
                }
            }

            if (parentRequire)
            {
                Employee parentEmployee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync((int)employeeDTO.ParentId);
                if (parentEmployee == null) {
                    _logger.LogError($"Invalid Parent ID {employeeDTO.ParentId}");
                    throw new InvalidOperationException($"Invalid Parent ID {employeeDTO.ParentId}");
                }
            }

            Employee employeeExsist = await _unitOfWork.EmployeeRepository.GetEmployeeByEmployeeIdAsync(employeeDTO.EmployeeId);
            if(employeeExsist != null)
            {
                _logger.LogError($"Employee Id already exsits: {employeeDTO.EmployeeId}");
                throw new InvalidOperationException($"Employee Id already exsits: {employeeDTO.EmployeeId}");
            }

            Employee employee = new Employee()
            {
                Name = employeeDTO.Name,
                EmployeeId = employeeDTO.EmployeeId,
                JobTitle = employeeDTO.JobTitle,
                Role = employeeDTO.Role,
                ParentId = employeeDTO.ParentId
            };

            try
            {
                _unitOfWork.EmployeeRepository.CreateEmployee(employee);
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateException ex)
            {
                await _unitOfWork.RollbackAsync();
                _logger.LogError(message: "Failed to create employee", ex);
                throw new InvalidOperationException("Failed to create employee", ex);
            }

            return await GetEmployeeById(employee.EmployeeId);
        }

        /// <summary>
        /// Delete the employee, assign a new parent if it has children employee's
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="newParentId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Employee> DeleteEmployee(string employeeId, int newParentId)
        {
            // Validating the Employee ID
            Employee employeeToDelete = await _unitOfWork.EmployeeRepository.GetEmployeeByEmployeeIdAsync(employeeId) 
                ?? 
                throw new InvalidOperationException($"Invalid Employee Id: {employeeId}");
            
            Employee parentEmployee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(newParentId) 
                ?? 
                throw new InvalidOperationException($"Invalid Parent ID {newParentId}");
            
            try
            {
                // Updating the employeeToDelete Direct Childeren to newParentId
                if (employeeToDelete.DirectReports.Count > 0)
                {
                    foreach (var child in employeeToDelete.DirectReports)
                    {
                        child.ParentId = newParentId;
                    }
                }

                //Removing the Employee
                _unitOfWork.EmployeeRepository.DeleteEmployee(employeeToDelete);

                await _unitOfWork.CompleteAsync();

                return employeeToDelete;
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException($"Failed to Delete the employee: {employeeId}");
            }
        }

        /// <summary>
        /// Update the employee - Name, JobTitle, Role, ParentId
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="updateEmployeeDTO"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<EmployeeTreeNode> UpdateEmployee(string employeeId, UpdateEmployeeDTO updateEmployeeDTO)
        {
            Employee employeeToUpdate = 
                await _unitOfWork.EmployeeRepository.GetEmployeeByEmployeeIdAsync(employeeId) 
                ?? 
                throw new InvalidOperationException($"Invalid Employee Id: {employeeId}");

            employeeToUpdate.Name = updateEmployeeDTO.Name;
            employeeToUpdate.JobTitle = updateEmployeeDTO.JobTitle;
            employeeToUpdate.Role = updateEmployeeDTO.Role;

            if (updateEmployeeDTO.ParentId != null)
            {
                if (employeeToUpdate.ParentId != updateEmployeeDTO.ParentId)
                {
                    Employee parentEmployee = 
                        await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync((int)updateEmployeeDTO.ParentId) 
                        ?? 
                        throw new InvalidOperationException($"Invalid Parent ID {updateEmployeeDTO.ParentId}");

                    employeeToUpdate.ParentId = updateEmployeeDTO.ParentId;
                }
            }
            try
            {
                await _unitOfWork.CompleteAsync();
                return await GetEmployeeById(employeeToUpdate.EmployeeId);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException($"Something went while updating the employee..");
            }
        }


        /// <summary>
        /// Create EmployeeTreeNode and will create EmployeeTreeNode for each of it's children also using recursion
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private static EmployeeTreeNode GetEmployeeTreeNode(Employee employee)
        {
            var treeNode = new EmployeeTreeNode
            {
                Id = employee.Id,
                Name = employee.Name,
                EmployeeId = employee.EmployeeId,
                Role = employee.Role.ToString(),
                JobTitle = employee.JobTitle,
                ParentId = employee.ParentId,
                HasChildren = employee.DirectReports.Count > 0,
                Children = new List<EmployeeTreeNode>()
            };

            foreach (var directReport in employee.DirectReports)
            {
                treeNode.Children.Add(GetEmployeeTreeNode(directReport));
            }

            return treeNode;
        }

    }
}
