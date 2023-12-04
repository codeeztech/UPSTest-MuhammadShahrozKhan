using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using UPSTest.WPF.Repositories.Models;
using UPSTest.WPF.Services;

namespace UPSTest.WPF.AppLayer.ViewModels
{
    public class EditEmployeeViewModel : INotifyPropertyChanged
    {
        private readonly IEmployeeService employeeService;
        public ObservableCollection<string> Genders { get; set; }
        public ObservableCollection<string> Statuses { get; set; }

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));
            }
        }

        private Employee _employee;

        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                OnPropertyChanged(nameof(Employee));
            }
        }

        public ICommand UpdateCommand { get; private set; }

        public EditEmployeeViewModel(IEmployeeService _employeeService)
        {
            this.employeeService = _employeeService;
            UpdateCommand = new RelayCommand<Employee>(UpdateEmployeeAsync);

            Genders = new ObservableCollection<string> { "male", "female" };
            Statuses = new ObservableCollection<string> { "active", "inactive" };
            Task.Run(() => LoadEmployeesAsync());

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task LoadEmployeesAsync()
        {
            try
            {
                List<Employee> employees = await employeeService.GetAllEmployeeAsync();
                Employees = new ObservableCollection<Employee>(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadEmployeesAsync: {ex.Message}");
            }
        }
        public async void UpdateEmployeeAsync(Employee emp)
        {
            try
            {
                if (Employee != null)
                {
                    Employee updatedEmployee = await employeeService.UpdateEmployeeAsync(Employee.Id, Employee);
                    if (updatedEmployee != null)
                    {
                        int index = Employees.IndexOf(Employee);
                        Employees[index] = updatedEmployee;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateEmployeeAsync: {ex.Message}");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}