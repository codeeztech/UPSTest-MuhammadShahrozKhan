using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using UPSTest.WPF.Repositories.Models;
using UPSTest.WPF.Services;
using System.Windows;
using System.Linq;
using System.Collections.Generic;

namespace UPSTest.WPF.AppLayer.ViewModels
{
    public class AddEmployeeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IEmployeeService employeeService;

        private readonly EmployeeViewModel employeeViewModel;

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

        private string _employeeName;
        public string EmployeeName
        {
            get { return _employeeName; }
            set
            {
                _employeeName = value;
                OnPropertyChanged(nameof(EmployeeName));
                SaveCommand.CanExecute(null); 
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                SaveCommand.CanExecute(null); 
            }
        }
        public ObservableCollection<string> Genders { get; set; }
        public ObservableCollection<string> Statuses { get; set; }
        public ICommand SaveCommand { get; private set; }

        public AddEmployeeViewModel(IEmployeeService employeeService , EmployeeViewModel employeeViewModel)
        {
            this.employeeService = employeeService;

            Employee = new Employee();

            Genders = new ObservableCollection<string> { "male", "female" };
            Statuses = new ObservableCollection<string> { "active", "inactive" };

            SaveCommand = new RelayCommand(SaveEmployee);

            LoadEmployeesAsync();

            this.employeeViewModel = employeeViewModel;
        }
        public async Task LoadEmployeesAsync()
        {
            try
            {
                List<Employee> employees = await employeeService.GetAllEmployeeAsync();
                if (employees != null)
                    Employees = new ObservableCollection<Employee>(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadEmployeesAsync: {ex.Message}");
            }
        }
        public async Task NewUpdateLoadEmployeesAsync()
        {
            try
            {
                List<Employee> employees = await Task.Run(() => employeeService.GetAllEmployeeAsync());
                Application.Current.Dispatcher.Invoke(() => Employees = new ObservableCollection<Employee>(employees));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadEmployeesAsync: {ex.Message}");
            }
        }
        private async Task SaveEmployee()
        {
            try
            {
                string validationMsg = ValidateEmployee(Employee);
                if (validationMsg.Length > 0)
                {
                    MessageBox.Show(validationMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                Employee addedEmployee = await employeeService.AddEmployeeAsync(Employee);

                if (addedEmployee != null)
                {
                    Employees = [addedEmployee];

                    MessageBox.Show("Employee added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);


                    if (employeeViewModel != null)
                    {
                        employeeViewModel.AddEmployee();

                        MainWindow mw = new MainWindow();
                        mw.ViewModel_OnEmployeeAdded(null, null);
                        CloseAddEmployeeWindow();
                        Task.Run(NewUpdateLoadEmployeesAsync);
                        Employee = new Employee();
                    }
                }
                else
                {
                    MessageBox.Show("Failed to add employee. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                string errMsg = $"Error in SaveEmployee: {ex.Message}";
                Console.WriteLine(errMsg);
                MessageBox.Show(errMsg);
            }
        }
        private void CloseAddEmployeeWindow()
        {
            if (Application.Current.Windows.OfType<AddEmployeeView>().Any())
            {
                Application.Current.Windows.OfType<AddEmployeeView>().First().Close();
            }
        }
        private string ValidateEmployee(Employee employee)
        {
            string errMsg = string.Empty;

            if (string.IsNullOrEmpty(employee.Name))
            {
                errMsg += "Please enter employee name\n";
            }

            if (string.IsNullOrEmpty(employee.Email))
            {
                errMsg += "Please enter email\n";
            }

            if (string.IsNullOrEmpty(employee.Gender))
            {
                errMsg += "Please select gender\n";
            }

            if (string.IsNullOrEmpty(employee.Status))
            {
                errMsg += "Please select status\n";
            }

            return errMsg;
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
