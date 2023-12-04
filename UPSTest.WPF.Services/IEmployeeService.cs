
using UPSTest.WPF.Repositories.Models;

namespace UPSTest.WPF.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeeAsync();
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(int id, Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<Employee> GetEmployeeByIdAsync(int id);
    }

}
