using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TreeDomainLibrary.Models;
using TreeDomainLibrary.Models.ModelsDTO;
using TreeDomainLibrary.Services;
using ProblemDetails = TreeDomainLibrary.ErrorHandlingHelper.ProblemDetails;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TreeStructureWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        /// <summary>
        /// To Get the employee in tree strcture
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// GET:
        /// https://localhost:7060/Employee/GetEmployeeTree
        /// </remarks>
        [HttpGet("GetEmployeeTree")]
        public async Task<IActionResult> GetEmployeeTree()
        {
            _logger.LogInformation("Started Fetching Employee Tree");
            var employeeTree = await _employeeService.GetEmployeesTreeAsync();
            _logger.LogInformation("Fetching Employee Tree Completed");
            await Console.Out.WriteLineAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            return Ok(employeeTree);
        }


        /// <summary>
        /// Get the all list of employees in table format 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// GET:
        /// {
        /// https://localhost:7060/Employee/GetFlatEmployeesData
        /// }
        /// </remarks>
        [HttpGet("GetFlatEmployeesData")]
        public async Task<IActionResult> GetFlatEmployeesData()
        {
            List<EmployeeFlatData> employee = await _employeeService.GetFlatEmployeesDataAsync();
            return Ok(employee);
        }


        /// <summary>
        /// GET Specific Employee and his children employee's
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        /// <remarks>
        /// GET:
        /// {
        /// https://localhost:7060/Employee/GetEmployee?employeeId=RK012
        /// }
        /// </remarks>
        [HttpGet("GetEmployee")]
        public async Task<IActionResult> GetEmployee([FromQuery] string employeeId)
        {
            var employee = await _employeeService.GetEmployeeById(employeeId);
            return Ok(employee);
        }

        /// <summary>
        /// POST /Employee/AddEmployee
        /// </summary>
        /// <param name="employeeDTO"></param>
        /// <returns></returns>
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> Post([FromForm] EmployeeDTO employeeDTO)
        {
            EmployeeTreeNode employee = await _employeeService.CreateEmployee(employeeDTO);
            return StatusCode(StatusCodes.Status201Created, employee);
        }

        /// <summary>
        /// PUT /Employee/JD005
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="updateEmployeeDTO"></param>
        /// <returns></returns>
        [HttpPut("{employeeId}")]
        public async Task<IActionResult> Put(string employeeId, [FromForm] UpdateEmployeeDTO updateEmployeeDTO)
        {
            var updatedEmployee = await _employeeService.UpdateEmployee(employeeId, updateEmployeeDTO);
            return Ok(updatedEmployee);
        }

        /// <summary>
        /// DELETE /Employee/DeleteEmployee/5
        /// Delete the employee and update the parentID of direct report children
        /// </summary>
        /// <param DeleteEmployeeDTO="DeleteEmployeeDTO"></param>
        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> Delete([FromForm] DeleteEmployeeDTO deleteEmployeeDTO)
        {
            var deletedEmployee = await _employeeService.DeleteEmployee(deleteEmployeeDTO.EmployeeId, deleteEmployeeDTO.NewParentId);
            return Ok(deletedEmployee);
        }
    }
}
