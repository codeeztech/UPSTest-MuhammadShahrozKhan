   M S B A M L   `   `   `  ����O  KUPSTest.WPF.AppLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:UPSTest.WPF.AppLayer.ViewModelsUPSTest.WPF.AppLayer.ViewModels  5   P   %    UPSTest.WPF.AppLayer.MainWindow   5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   D
viewmodels-clr-namespace:UPSTest.WPF.AppLayer.ViewModels     6P     � Title$  Employee Management��5   	   $	��600��6%   $
��1000��62   .��5
      � .+�5   
   � -   5        $��loadingSpinner��5      $��50��5      $��50��5      $��Center=�5      $��Center=�5       � IsIndeterminate$
 True��5      $y�	Collapsed=�5      �� 5   
   $
��Auto=�6   .��5      �� .+�5      �� $��1q�6    �� CornerRadius$ 5��6-   $��10q�6>    �� Padding$ 3q�6J   
�������._�5      �� .+�5      �� $��Center=�6!   $��14(�6>   $
��0 10q�6L   .��6Z   #!UPS Test By Muhammad Shahroz Khan6}   �� 5      $��EMPLOYEE MANAGEMENT��6    $��24(�6;   $
��Bold"�6I   $
��0 10q�6[   $��Center=�6i   5      5      �� 5       �� Orientation$ 
Horizontal=�6   $��Center=�66   $��Center=�6S   .+�5      �� -   5        $��
btnRefresh��5      $��Right=�6I   $��10 10q�6e   $	��100��6t   ��5      �� 5      $ 
Horizontal=�6)   $��Center=�6B   .+�5   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ RefreshCircle=�6:   $��0 0 5 0q�6O   �� 5    "   $��Refresh��6,   $��Center=�6;   5!      5#      �� 5$      -   5        $��btnAddEmployee��5$      $��Right=�6P   $��10 10q�6l   $	��200��6{   ��5%      �� 5&      $ 
Horizontal=�6)   $��Center=�6B   .+�5'   "     $ AccountPlus=�6:   $��0 0 5 0q�6M   �� 5(   "   $��Add New Employee��6,   $��Center=�6D   5)      5+      �� 5,      $
��10 0q�6E   ��6   �� *ExportToCsvCommand+��5-      �� 5.      $ 
Horizontal=�6)   $��Center=�6B   .+�5/   "     $ 
FileExport=�6:   $��0 0 5 0q�6L   �� 50   "   $��Export to CSV��6,   $��Center=�6A   51      53      54      �� 55      $ 
Horizontal=�6   $��Center=�66   $��Center=�6S   $
��0 25q�6o   .+�56      �� $
��0 10q�6   .��6*   Search by Employee Name:6D   �� 57      -   5        $��	txtSearch��57   ?   $��30��6   
�������$	��200��6Q   $
��0 10q�6�   ��6]   �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�.��6�   5;      5<      �� 5=      $��1q�6   $ 5��6-   $��10q�6>   $ 3q�6J   ._�5>      �� $ 
Horizontal=�6   $��Center=�66   $��Center=�6S   .+�5?      &   System.Windows.Controls.DataGrid  -   5          Name$ EmployeesDataGrid��5?      
�������   AutoGenerateColumns$ False��5A      	   
IsReadOnly$
	 True��5B      ��5@      �� *	Employees+
 �� Mode$
 TwoWay=�   Columns 5D      0  *System.Windows.Controls.DataGridTextColumn  5E      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *Id+  5F      $
 Name��6)    67   �� *Name+  5G      $ Email��6)    68   �� *Email+  5H      $ Gender��6)    69   �� *Gender+  5I      $ Status��6)    69   �� *Status+4  .System.Windows.Controls.DataGridTemplateColumn  5K      $ Actions��6-      CellTemplate 5L      �� 5M      .R�5N   "   �� $ 
Horizontal=�6-   .+�5O   2   ��  $   MaterialDesignRaisedAccentButton#��[  6�   $��0 0 10 0q�6�   ��6:   �� *!DataContext.EditEmployeeCommand+ �� RelativeSource ��  �� AncestorType
 Window��6�   �� ��5P   6   �� 5Q   :   $ 
Horizontal=�6E   $��Center=�6^   .+�5R   >     $ Pencil=�6V   $��0 0 5 0q�6d   �� 5S   >   $
��Edit��6H   $��Center=�6T   5T   ;   5V   3   �� 5W   2   #��[  6�   ��6:   �� *#!DataContext.DeleteEmployeeCommand+ �� 
 Window��6�   �� .��5X   6   �� $ 
Horizontal=�6A   $��Center=�6Z   .+�5Y   :     $ Delete=�6R   $��0 0 5 0q�6`   �� 5Z   :   $��Delete��6D   $��Center=�6R   5[   7   5\   3   5]   #   5^      5`      5b      5c      5d      5e      5f      5g      5h      