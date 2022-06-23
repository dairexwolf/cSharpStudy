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

namespace LifecycleApp
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

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Resources.StreamResourceInfo res =
    Application.GetResourceStream(new Uri("images/river.jpg", UriKind.Relative));

            res.Stream.CopyTo(new System.IO.FileStream("C://newRiver.jpg", System.IO.FileMode.OpenOrCreate));
        }
    }
}
