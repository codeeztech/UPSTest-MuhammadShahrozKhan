using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UPSTest.WPF.AppLayer.ViewModels;
using UPSTest.WPF.Repositories.Models;
using UPSTest.WPF.Services;

namespace UPSTest.WPF.AutomationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestLoadEmployeesAsync()
        {
            var employeeServiceMock = new Mock<IEmployeeService>();
            employeeServiceMock.Setup(s => s.GetAllEmployeeAsync())
                .ReturnsAsync(new List<Employee> { new Employee { Id = 5814924, Name = "Shahroz" } });

            var viewModel = new EmployeeViewModel(employeeServiceMock.Object);

            await viewModel.LoadEmployeesAsync();

            Assert.IsTrue(viewModel.Employees.Any());
            Assert.IsTrue(viewModel.Employees.All(e => e != null));

            var expectedEmployee = new Employee { Id = 5814924, Name = "Shahroz" };
            var actualEmployee = viewModel.Employees.Single();

            Assert.AreEqual(expectedEmployee.Id, actualEmployee.Id);
            Assert.AreEqual(expectedEmployee.Name, actualEmployee.Name);
        }

        [TestMethod]
        public async Task TestSearchEmployeesAsync()
        {
            var employeeServiceMock = new Mock<IEmployeeService>();
            var viewModel = new EmployeeViewModel(employeeServiceMock.Object);

            var expectedResult = new List<Employee> { new Employee { Id = 5814924, Name = "Shahroz" } };
            employeeServiceMock.Setup(s => s.SearchEmployeesAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedResult);

            viewModel.SearchText = "Shahroz";
            viewModel.SearchCommand.Execute(null);
            await Task.Delay(500);

            Assert.IsNotNull(viewModel.Employees);
            Assert.AreEqual(5814924, viewModel.Employees[0].Id);
            Assert.AreEqual("Shahroz", viewModel.Employees[0].Name);
        }

        [TestMethod]
        public async Task TestDeleteEmployeeAsync()
        {
         
            var employeeServiceMock = new Mock<IEmployeeService>();
            var viewModel = new EmployeeViewModel(employeeServiceMock.Object);

            var employeeToDelete = new Employee { Id = 5816341, Name = "NewShahrozUnitTest" , Email= "NewShahrozUnitTest@test.com",Gender="male",Status="active" };
            employeeServiceMock.Setup(s => s.DeleteEmployeeAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            viewModel.Employees.Add(employeeToDelete);
            viewModel.SelectedEmployee = employeeToDelete;
            viewModel.DeleteEmployeeCommand.Execute(employeeToDelete);
            await Task.Delay(1000); 

            Assert.AreEqual(0, viewModel?.Employees.Count);
        }

        [TestMethod]
        public async Task TestSaveEmployee()
        {
            var employeeServiceMock = new Mock<IEmployeeService>();
            var viewModel = new AddEmployeeViewModel(employeeServiceMock.Object, null);

            var addedEmployee = new Employee { Id = 5814995, Name = "NewAddedShahrozUnitTest" , Email= "newshahroz123@unittest.com", Gender="male",Status="active" };
            employeeServiceMock.Setup(s => s.AddEmployeeAsync(It.IsAny<Employee>()))
                .ReturnsAsync(addedEmployee);

            viewModel.Employee = new Employee { Name = "NewAddedShahrozUnitTest", Email = "newshahroz123@unittest.com", Gender = "male", Status = "active" };
            viewModel.SaveCommand.Execute(addedEmployee);
            await Task.Delay(500); 

            Assert.AreEqual("NewAddedShahrozUnitTest", viewModel.Employees[0].Name);
        }

        [TestMethod]
        public async Task TestUpdateEmployeeAsync()
        {
            var employeeServiceMock = new Mock<IEmployeeService>();
            var viewModel = new EditEmployeeViewModel(employeeServiceMock.Object);

            var updatedEmployee = new Employee { Id = 5814924, Name = "UpdatedShahroz", Email = "UpdatedShahroz@unittest.com", Gender = "female", Status = "inactive" };
            employeeServiceMock.Setup(s => s.UpdateEmployeeAsync(It.IsAny<int>(), It.IsAny<Employee>()))
                .ReturnsAsync(updatedEmployee);

            viewModel.Employee = new Employee { Id = 5814924, Name = "Shahroz", Email = "shahroz@test.com", Gender = "male", Status = "active" };
            viewModel.UpdateCommand.Execute(null);
            await Task.Delay(500); 

            Assert.AreEqual("UpdatedShahroz", viewModel.Employees[0].Name);
        }
    }
}
