   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   H DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5   	   !localclr-namespace:WpfApp1  5   	     � Title$  
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      $��0.12* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGrid��5      $��Stretch=�6%   $��Stretch=�6C   ���	#FFB53351$��2q�6w   $��5,5,5,6q�5         AutoGenerateColumns .    
IsReadOnly . $��2��6Q    � 	Resources 5        WpfApp1.SearchConverter   SearchConverter��%   &	          5      6(   5         RowStyle 5      �� 5       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5      �� 	 �� Property	 � ��
 �� Value
 63   �� *	SearchBox+ �� 	Converter# [  5      5       �� 5"   
   $��10 0 5 0q�6    �� Orientation$ 
Horizontal=�6'   $��Center=�6@   $��1��6[   .+�5#      �� $��
Поиск��6   $��Center=�6%   �� 5$      -   5         ��Name$ 	SearchBox��5$      $��5 0 0 0q�6M   $	��200��6^   $	��250��6m   5%      5&      5'      