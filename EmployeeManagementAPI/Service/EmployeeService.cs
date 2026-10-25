using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Service;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;
    public EmployeeService(AppDbContext context)
    {
        _context = context;

    }

    public async Task<Employee> CreateEmployeeAsync(Employee employee, CancellationToken ct)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(ct);
        return employee;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var Emp = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if(Emp is null)
        {
            return false;
        }
        _context.Employees.Remove(Emp);
        await _context.SaveChangesAsync(ct);
        return true;
    }

    public async Task<IEnumerable<Employee>> GetEmployeeAsync(CancellationToken ct)
    {
        return await _context.Employees
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int id, CancellationToken ct)
    {
        return await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id, ct);
    }

    public async Task<bool> UpdateAsync(Employee employee, CancellationToken ct)
    {
        var existingEmp = await _context.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);
        if (existingEmp is null)
            return false;
        existingEmp.Name = employee.Name;
        existingEmp.Salary = employee.Salary;
        existingEmp.Email = employee.Email;
        existingEmp.Department = employee.Department;

        await _context.SaveChangesAsync(ct);
        return true;
    }
}
