using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UPSTest.WPF.Repositories.Models;
using UPSTest.WPF.Services;
using UPSTest.WPF.AppLayer;
using System.Windows.Data;

namespace UPSTest.WPF.AppLayer.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private readonly IEmployeeService employeeService;
        public event EventHandler OnEmployeeAdded;
        public ICommand EditEmployeeCommand { get; private set; }
        public ICommand DeleteEmployeeCommand { get; private set; }

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));

                // Refresh the view
                ICollectionView view = CollectionViewSource.GetDefaultView(Employees);
                view.Refresh();

                Console.WriteLine("Employees collection updated.");
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }


        public EmployeeViewModel(IEmployeeService employeeService)
        {
            Employees = new ObservableCollection<Employee>();
            this.employeeService = employeeService;

            Task.Run(() => LoadEmployeesAsync());

            OnEmployeeAdded += EmployeeViewModel_OnEmployeeAdded;

            EditEmployeeCommand = new RelayCommand<Employee>(EditEmployee);
            DeleteEmployeeCommand = new RelayCommand<Employee>(DeleteEmployee);
        }

        private void EmployeeViewModel_OnEmployeeAdded(object? sender, EventArgs e)
        {
            Task.Run(() => NewUpdateLoadEmployeesAsync());
        }

        public async void AddEmployee()
        {
            OnEmployeeAdded?.Invoke(this, EventArgs.Empty);
        }

        private void EditEmployee(Employee employee)
        {
            EditEmployeeViewModel editEmployeeViewModel = new EditEmployeeViewModel(employeeService);
            editEmployeeViewModel.Employee = employee;
            NavigateToEditScreen(editEmployeeViewModel);


        }

        private async void DeleteEmployee(Employee employee)
        {
            MessageBoxResult result = MessageBox.Show($"Delete employee: {employee.Name}?", "Confirmation", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                await DeleteEmployeeAsync(employee);
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
        public async Task LoadEmployeesAsync()
        {
            try
            {
                List<Employee> employees = await Task.Run(() => employeeService.GetAllEmployeeAsync());
                Employees = new ObservableCollection<Employee>(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadEmployeesAsync: {ex.Message}");
            }
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                    bool isDeleted = await employeeService.DeleteEmployeeAsync(employee.Id);
                    if (isDeleted)
                    {
                        Employees.Remove(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteEmployeeAsync: {ex.Message}");
            }
        }

        public async Task LoadEmployeeByIdAsync(int employeeId)
        {
            try
            {
                SelectedEmployee = await employeeService.GetEmployeeByIdAsync(employeeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in LoadEmployeeByIdAsync: {ex.Message}");
            }
        }

        public void NavigateToAddScreen(AddEmployeeViewModel addEmployeeViewModel)
        {
            AddEmployeeView addEmployeeView = new AddEmployeeView();
            addEmployeeView.DataContext = addEmployeeViewModel;
            addEmployeeView.Show();
        }

        public void NavigateToEditScreen(EditEmployeeViewModel editEmployeeViewModel)
        {
            EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow();
            editEmployeeWindow.DataContext = editEmployeeViewModel;
            editEmployeeWindow.Show();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
