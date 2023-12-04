
using UPSTest.WPF.Repositories.Models;

namespace UPSTest.WPF.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(int id, Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<Employee> GetEmployeeAsync(int id);
    }

}
