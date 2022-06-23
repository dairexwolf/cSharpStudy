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

namespace WpfValidData
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PersonModel person;
        public MainWindow()
        {
            InitializeComponent();
            person = new PersonModel();
            this.DataContext = person;
        }

        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show(e.Error.ErrorContent.ToString());
        }
    }

    public class PersonModel : IDataErrorInfo
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }

        public string this[string columnName] {
            get
            {
                string error = String.Empty;
                switch(columnName)
                {
                    case "Age":
                        if((Age < 0) || (Age > 200))
                        {
                            error = "Возраст должен быть больше 0 и меньше 200";
                        }
                        break;

                    case "Name":
                        // Обработка ошибок для свойства Name
                        break;
                    case "Position":
                        break;
                }
                return error;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

    }
}
