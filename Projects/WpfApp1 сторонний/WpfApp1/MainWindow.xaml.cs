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
using MahApps.Metro.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static List<ClientsTB> list = new List<ClientsTB>();

        public MainWindow()
        {
            InitializeComponent();

            list.Add(new ClientsTB(1, "Алфёров", "Алексей", "Николаевич", "+68889444", ""));
            list.Add(new ClientsTB(2, "Баль", "Валентина", "Сергеевна", "+7998888444", ""));
            list.Add(new ClientsTB(3, "Денисова", "Олеся", "Леонидовна", "", "dummy@email.ru", true));
            list.Add(new ClientsTB(4, "Ефремов", "Владислав", "Николаевич", "", "paramparam@mail.ru", true));
            list.Add(new ClientsTB(5, "Жиляков", "Владимир", "Владимироваич", "445588", "445588@gmail.com", true));

            dataGrid.ItemsSource = list;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Fam.ToLower().Contains(SearchBox.Text.ToLower()) && SearchBox.Text != null && SearchBox.Text.Length != 0)
                {
                    list[i].Search = true;
                    if (list.Count - 1 == i)
                    {
                        dataGrid.Items.Refresh();
                    }
                }
                else
                {
                    list[i].Search = false;
                    if (list.Count - 1 == i)
                    {
                        dataGrid.Items.Refresh();
                    }
                }
            }
        }
    }
}
