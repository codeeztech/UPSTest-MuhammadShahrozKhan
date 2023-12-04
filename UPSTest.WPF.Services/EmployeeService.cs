using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPSTest.WPF.Repositories.Models;
using UPSTest.WPF.Repositories;

namespace UPSTest.WPF.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            try
            {
                return await employeeRepository.GetAllEmployeesAsync();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error in GetAllEmployeeAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            try
            {
                return await employeeRepository.AddEmployeeAsync(employee);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error in AddEmployeeAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, Employee employee)
        {
            try
            {
                return await employeeRepository.UpdateEmployeeAsync(id, employee);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error in UpdateEmployeeAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                return await employeeRepository.DeleteEmployeeAsync(id);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error in DeleteEmployeeAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            try
            {
                return await employeeRepository.GetEmployeeAsync(id);
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                Console.WriteLine($"Error in GetEmployeeByIdAsync: {ex.Message}");
                throw;
            }
        }
    }


}
