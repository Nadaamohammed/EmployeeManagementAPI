using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Service;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetEmployeeAsync(CancellationToken ct);
    Task<Employee?> GetEmployeeByIdAsync(int id, CancellationToken ct);
    Task<Employee> CreateEmployeeAsync(Employee employee, CancellationToken ct);

    Task<bool> UpdateAsync(Employee employee, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
