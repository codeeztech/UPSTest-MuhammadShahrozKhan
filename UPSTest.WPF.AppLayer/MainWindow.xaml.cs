using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UPSTest.WPF.AppLayer.ViewModels;
using UPSTest.WPF.Repositories;
using UPSTest.WPF.Repositories.Models;
using UPSTest.WPF.Services;

namespace UPSTest.WPF.AppLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EmployeeService employeeService = new(new EmployeeRepository("0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023"));
        public MainWindow()
        {
            InitializeComponent();

            loadingSpinner.Visibility = Visibility.Visible;
            OnLoadWindow();
            loadingSpinner.Visibility = Visibility.Collapsed;

            txtSearch.Focus();
        }

        private void OnLoadWindow()
        {
            var viewModel = new EmployeeViewModel(employeeService);
            DataContext = viewModel;

            viewModel.OnEmployeeAdded += ViewModel_OnEmployeeAdded;

            PresentationTraceSources.Refresh();
            PresentationTraceSources.SetTraceLevel(this, PresentationTraceLevel.High);

        }

        public void ViewModel_OnEmployeeAdded(object? sender, EventArgs e)
        {
            btnRefresh_Click(null, null);
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is EmployeeViewModel employeeViewModel)
            {
                AddEmployeeViewModel addEmployeeViewModel = new(employeeService,new EmployeeViewModel(employeeService));
                employeeViewModel.NavigateToAddScreen(addEmployeeViewModel);
            }
        }

        private async void btnRefresh_Click(object? sender, RoutedEventArgs? e)
        {
            if (DataContext is EmployeeViewModel employeeViewModel)
            {
                await Task.Run(() => employeeViewModel.LoadEmployeesAsync());

                Application.Current.Dispatcher.Invoke(() =>
                {
                    EmployeesDataGrid.ItemsSource = employeeViewModel.Employees;
                    EmployeesDataGrid.Items.Refresh();
                    
                });
            }
        }
    }
}
