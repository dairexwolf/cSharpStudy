using System;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*Button myButton = new Button();
            myButton.Width = 100;
            myButton.Height = 30;
            myButton.Content = "Кнопка";
            myButton.Click +=Button_Click;
            layoutGrid.Children.Add(myButton);
            */
        }
    }

    public class Phone
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"Смартфон {this.Name}; Цена: {this.Price}";
        }
    }
}
