   M S B A M L   `   `   `  ����  UPSTest.WPF.AppLayer*    $UPSTest.WPF.AppLayer.AddEmployeeView   5   	   _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Add Employee��5   	   $	��300��6   $	��500��6+   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��Center=�6   $��Center=�63   $	��400��6N   .+�5      �� $��0��6   $��Name:��6!   �� 5   
   -   5        $��txtEmployeeName��5      $��1��6   ��66   �� *Employee.Name+ �� UpdateSourceTrigger$ PropertyChanged=��� 5   
   $��2��6   $��Email:��6   �� 5   
   $��3��6   ��6   �� *Employee.Email+$ PropertyChanged=��� 5   
   $��4��6   $��Gender:��6   �� 5   
   $��5��6   ��6    �� *	Genders+ �� SelectedItem 6@   �� *Employee.Gender+�� 5    
   $��6��6   $��Status:��6   �� 5!   
   $��7��6   ��6    �� *
Statuses+ 6A   �� *Employee.Status+�� 5#      $��8��6   $��0 10 0 0q�6B   ��6"   �� *SaveCommand+��5$      �� 5%       �� Orientation$ 
Horizontal=�6!   $��Center=�6:   .+�5&      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ ContentSave=�62   $��0 0 5 0q�6E   �� 5'      $
��Save��6$   $��Center=�60   5(      5*      5,      5-      5.      