using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UPSTest.WPF.AppLayer.ViewModels;
using UPSTest.WPF.Repositories.Models;
using UPSTest.WPF.Services;
namespace UPSTest.WPF.Tests
{
    [TestClass]
    public class EmployeeViewModelTests
    {
        [TestMethod]
        public async Task LoadEmployeesAsync_ShouldPopulateEmployees()
        {
            // Arrange
            var employeeServiceMock = new Mock<IEmployeeService>();
            employeeServiceMock.Setup(s => s.GetAllEmployeeAsync())
                .ReturnsAsync(new List<Employee> { new Employee { Id = 1, Name = "John" } });

            var viewModel = new EmployeeViewModel(employeeServiceMock.Object);

            // Act
            await viewModel.LoadEmployeesAsync();

            // Assert
            Assert.IsNotNull(viewModel.Employees);
            Assert.AreEqual(1, viewModel.Employees.Count);
            Assert.AreEqual("John", viewModel.Employees[0].Name);
        }

        // Add more tests as needed
    }

}
