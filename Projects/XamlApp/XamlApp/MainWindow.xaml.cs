﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace XamlApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Button1.Click += Button1_Click;
        }

        // обработчик, подключаемый в XAML
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ButClick");
        }

        // обработчик, подключаемый в конструкторе
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("But1Click");
        }
    }
}
