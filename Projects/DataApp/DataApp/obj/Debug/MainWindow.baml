   M S B A M L   `   `   `  ����B  >DataApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DataAppDataApp  5   	       DataApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:DataApp     5   	   -   5        $��
mainWindow��5   5     � Title$  
MainWindow��6	   $	��350��6   $	��525��6)    � 	Resources 5
         propertysProvider � Source � XPath  nodesProvider��%�   &	        &	 8     � 5   
   $ 	props.xml?�64   $ 	propertys��6G   � 5   
   $ 	nodes.xml?�60   $ 
nodes/node��6C   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��0.3* �6#   �� 5      $
��0.6* �6    � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      .+�5   
   �� -   5         ��Name$ propertysList��5   /   $��1��6   $��1��6"    �� DisplayMemberPath$
 Name��6F   ��6_   ��  �� Source# [  	 �� XPath$	 property��R� 5   
   -   5        
 R�Name$
 	treeView1��5   0   $��0��6   $��1��6#   ��6C   �� # [ ��5      �� 5       �� ItemsSource 6+   �� $
	 node��.R�5      �� ��6    �� $	 @title��5      5      o� 5    
   -   5         o�Name$ mainMenu��5    #   $��2��6   $	��Top=�65   $��30��6M   ��5!      �� 5"       6+   ��  �� Path$ Props�.R�5#      �� ��6    �� *Title+ �� ItemTemplate 5$      �� 5%      .R�5&      �� ��6(   �� *Name+5'      5)      5+      5,      5-      