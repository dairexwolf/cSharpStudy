   M S B A M L   `   `   `  ����G  CWpfValidData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WpfValidDataWpfValidData  5   	       WpfValidData.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:WpfValidData     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � 	Resources 5	         validationFailed �� Orientation��%�   &	        �� 5
   
   .R�5      �� $ 
Horizontal=�6   .+�5      �� 
������$��2q�6.   ._�5      �� 5      �� 5      
���  ��$��16(�6-   $
��Bold"�6;   !6P   5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      .+�5"   
   �� $��1��6   $��30��6"   $��0 0 15 0q�6.   �� 5$   
   -   5        $��1��5$      $��1��6"   $��30��6/   $��0 0 15 0q�6;    =� ErrorTemplate# [  5%      .��5&   3   ��5'      �� 5(       �� Path$	 Age�6    �� NotifyOnValidationError .  �� ValidationRules 5)      5  /System.Windows.Controls.DataErrorValidationRule  5*      5,      .��5-      5.      �� 5/   
   $��1��6   $��2��6"   $��30��6/   $��0 0 15 0q�6;   �� 51   
   $��30��6   .��6   Введите имя53      �� 54   
   $��1��6   $#��Введите возраст��6   $��30��67   �� 55   
   $��2��6   $'��!Введите должность��6   $��30��69   56      57      