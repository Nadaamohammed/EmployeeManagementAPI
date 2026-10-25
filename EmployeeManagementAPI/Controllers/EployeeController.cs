
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(
            IEmployeeService service,
            ILogger<EmployeesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAll(
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving all employees.");

            var employees = await _service.GetEmployeeAsync(
                cancellationToken);

        

            return Ok(employees);
        }

        // GET: api/employees/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetById(
            int id,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Retrieving employee with ID {EmployeeId}.", id);

            var employee = await _service.GetEmployeeByIdAsync(
                id, cancellationToken);

            if (employee is null)
            {
                _logger.LogWarning(
                    "Employee with ID {EmployeeId} was not found.", id);

                return NotFound(
                    new { message = "Employee not found." });
            }

            return Ok(employee);
        }

        // POST: api/employees
        [HttpPost]
        public async Task<ActionResult<Employee>> Create(
            [FromBody] Employee employee,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new employee.");

            var createdEmployee = await _service.CreateEmployeeAsync(
                employee, cancellationToken);

            _logger.LogInformation(
                "Employee created successfully with ID {EmployeeId}.",
                createdEmployee.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdEmployee.Id },
                createdEmployee);
        }

        // PUT: api/employees/1
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Employee employee,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Updating employee with ID {EmployeeId}.", id);

            // Ensure the route ID matches the employee being updated.
            if (id != employee.Id)
            {
                _logger.LogWarning(
                    "Route ID {RouteId} does not match employee ID {EmployeeId}.",
                    id, employee.Id);

                return BadRequest(
                    new { message = "Route ID does not match employee ID." });
            }

            var updated = await _service.UpdateAsync(
                employee, cancellationToken);

            if (!updated)
            {
                _logger.LogWarning(
                    "Employee with ID {EmployeeId} was not found for update.",
                    id);

                return NotFound(
                    new { message = "Employee not found." });
            }

            _logger.LogInformation(
                "Employee with ID {EmployeeId} updated successfully.", id);

            return NoContent();
        }

        // DELETE: api/employees/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Deleting employee with ID {EmployeeId}.", id);

            var deleted = await _service.DeleteAsync(
                id, cancellationToken);

            if (!deleted)
            {
                _logger.LogWarning(
                    "Employee with ID {EmployeeId} was not found for deletion.",
                    id);

                return NotFound(
                    new { message = "Employee not found." });
            }

            _logger.LogInformation(
                "Employee with ID {EmployeeId} deleted successfully.", id);

            return NoContent();
        }
    }
}