﻿using System;
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
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using CsvHelper;
using Microsoft.Win32;

namespace UPSTest.WPF.AppLayer.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private readonly IEmployeeService employeeService;
        public event EventHandler OnEmployeeAdded;
        public ICommand EditEmployeeCommand { get; private set; }
        public ICommand DeleteEmployeeCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand ExportToCsvCommand { get; private set; }

        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged(nameof(Employees));

                ICollectionView view = CollectionViewSource.GetDefaultView(Employees);
                view.Refresh();
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

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                SearchEmployeesAsync();
            }
        }

        public EmployeeViewModel(IEmployeeService employeeService)
        {
            Employees = new ObservableCollection<Employee>();
            this.employeeService = employeeService;

            OnEmployeeAdded += EmployeeViewModel_OnEmployeeAdded;

            EditEmployeeCommand = new RelayCommand<Employee>(EditEmployee);
            DeleteEmployeeCommand = new RelayCommand<Employee>(DeleteEmployee);
            SearchCommand = new RelayCommand(SearchEmployeesAsync);
            ExportToCsvCommand = new RelayCommand(ExportToCsv);

            Application.Current.Dispatcher.Invoke(() => { Task.Run(LoadEmployeesAsync); });
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
                string errMsg = $"Error in LoadEmployeesAsync: {ex.Message}";
                Console.WriteLine(errMsg);
                MessageBox.Show(errMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            try
            {
                if (employee != null)
                {
                    bool isDeleted = await employeeService.DeleteEmployeeAsync(employee.Id);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (isDeleted)
                        {
                            Employees.Remove(employee);
                        }
                    });
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
                string errMsg = $"Error in LoadEmployeeByIdAsync: {ex.Message}";
                Console.WriteLine(errMsg);
                MessageBox.Show(errMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task SearchEmployeesAsync()
        {
            try
            {
                List<Employee> searchResults = await employeeService.SearchEmployeesAsync(SearchText);
                Employees = new ObservableCollection<Employee>(searchResults);
            }
            catch (Exception ex)
            {
                string errMsg = $"Error in SearchEmployeesAsync: {ex.Message}";
                Console.WriteLine(errMsg);
                MessageBox.Show(errMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ExportToCsv()
        {
            try
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    Title = "Export to CSV",
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    string filePath = saveFileDialog.FileName;
                    using (var writer = new StreamWriter(filePath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(Employees);
                    }
                    MessageBox.Show($"Data exported to {filePath}", "Export Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                string errMsg = $"Error exporting data: {ex.Message}";
                Console.WriteLine(errMsg);
                MessageBox.Show(errMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void NavigateToAddScreen(AddEmployeeViewModel addEmployeeViewModel)
        {
            AddEmployeeView addEmployeeView = new AddEmployeeView();
            addEmployeeView.DataContext = addEmployeeViewModel;
            addEmployeeView.Owner = Application.Current.MainWindow;
            addEmployeeView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addEmployeeView.Show();
        }

        public void NavigateToEditScreen(EditEmployeeViewModel editEmployeeViewModel)
        {
            EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow();
            editEmployeeWindow.DataContext = editEmployeeViewModel;
            editEmployeeWindow.Owner = Application.Current.MainWindow;
            editEmployeeWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            editEmployeeWindow.Show();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
