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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace BindingApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Phone phone = (Phone)this.Resources["nexusPhone"];
            phone.Company = "LG"; // Меняем с Google на LG
        }
    }

    class Phone
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }
    }
    public class DateTimeToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null && parameter.ToString() == "EN")
                return ((DateTime)value).ToString("MM-dd-yyyy");

            return ((DateTime)value).ToString("dd.MM.yyyy");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
