using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UPSTest.WPF.AppLayer.ViewModels;
using UPSTest.WPF.Repositories;
using UPSTest.WPF.Repositories.Models;
using UPSTest.WPF.Services;

namespace UPSTest.WPF.AppLayer
{
    /// <summary>
    /// Interaction logic for EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        private EditEmployeeViewModel viewModel;

        private readonly EmployeeService employeeService = new(new EmployeeRepository("0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023"));
        public EditEmployeeWindow()
        {
            InitializeComponent();

            // viewModel = new EditEmployeeViewModel(employeeService);
            // DataContext = viewModel;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is EditEmployeeViewModel editEmployeeViewModel)
            {
                editEmployeeViewModel.UpdateCommand.Execute(editEmployeeViewModel.Employee);
                Close();
            }
        }
    }
}
