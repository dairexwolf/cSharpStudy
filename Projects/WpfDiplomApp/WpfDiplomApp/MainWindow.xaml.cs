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
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Data.Entity;
using System.Windows.Threading;

namespace WpfDiplomApp
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static bool isConnected = false;
        PropContext db = new PropContext();
        public MainWindow()
        {
            object obj = new object();
            EventArgs e = new EventArgs();
            DispatcherTimer autosaveTimer = new DispatcherTimer(TimeSpan.FromSeconds(900), DispatcherPriority.Background, new EventHandler(AutoSave), Application.Current.Dispatcher);
            InitializeComponent();

            loadXML();
            // loadDb();
            
            this.Closing += MainWindow_Closing;

        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        private void saveXML(object sender, RoutedEventArgs e)
        {
            // передаем в конструктор тип класса Person
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PropertyRepozitory));

            // получаем поток, куда будем записывать сериализованный объект

                using (FileStream fs = new FileStream("data.xml", FileMode.Create))
                {
                    if (!isConnected)
                        xmlSerializer.Serialize(fs, TryFindResource("propertyRepozitory"));
                    else
                    {

                        PropertyRepozitory pz = new PropertyRepozitory();
                        int x = db.Props.Count();
                        List<Property> lp = db.Props.ToList();
                        int i = 0;
                        while (i < x)
                        {
                            pz.Add(lp[i]);
                            i++;
                        }
                        xmlSerializer.Serialize(fs, pz);
                    }
                }
            statusText.Text = "XML файл сохранен";
        }

        private async Task loadXML()
        {
            // передаем в конструктор тип класса Person
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PropertyRepozitory));
            if (File.Exists("data.xml"))
            {
                using (FileStream fs = new FileStream("data.xml", FileMode.Open))
                {
                    PropertyRepozitory _pz = xmlSerializer.Deserialize(fs) as PropertyRepozitory;
                    PropertyRepozitory pz = (PropertyRepozitory)this.TryFindResource("propertyRepozitory");
                    pz.Clear();

                    // добавляем с помощью цикла
                    foreach(Property prop in _pz)
                        pz.Add(prop);
                    mainPool.ItemsSource = pz;
                }
                statusText.Text = "XML файл загружен";
            }
            else
                statusText.Text = "XML файл не найден и не загружен";
        }

        private async Task loadDb()
        {
            try
            {
                db = new PropContext();
                await db.Props.LoadAsync();        // загружаем данные
                mainPool.ItemsSource = db.Props.Local.ToBindingList(); // устанавливаем привязку к кэшу
                isConnected = true;
                statusText.Text = "БД загружена";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
                PropertyRepozitory pz = (PropertyRepozitory)this.TryFindResource("propertyRepozitory");
                mainPool.ItemsSource = pz;
                isConnected = false;
            }
        }
        private void unloadDb()
        {
            db.Dispose();
            isConnected = false;
            statusText.Text = "БД выгружена";
        }

        private void mainPool_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            statusText.Text = "Добавлен новый объект";
        }

        private void saveDb(object sender, RoutedEventArgs e)
        {
            try
            {
                db.SaveChanges();
                statusText.Text = "В БД все сохранено";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
                PropertyRepozitory pz = (PropertyRepozitory)this.TryFindResource("propertyRepozitory");
                mainPool.ItemsSource = pz;
                isConnected = false;
            }
        }

        private async void LoadAsync(object sender, RoutedEventArgs e)
        {
            
            BusyAppOn();
            if (isConnected)
            {
                await loadDb();
            }
            else

            {
                unloadDb();
                await loadXML();
            }
            BusyAppOff();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            BusyAppOn();
            if (isConnected)
            {
                saveDb(sender, e);
            }
            else
            {
                saveXML(sender, e);
            }
            BusyAppOff();
        }

        private async void SaveFromXMLtoDB(object sender, RoutedEventArgs e)
        {
            BusyAppOn();
            bool temp = isConnected;
            await loadXML();
            await loadDb();
            await Aoaaa(sender, e);

            isConnected = temp;
            LoadAsync(sender, e);
            BusyAppOff();
        }

            private async Task Aoaaa(object sender, RoutedEventArgs e)
        {
            db.Props.RemoveRange(db.Props);
            PropertyRepozitory pz = (PropertyRepozitory)this.TryFindResource("propertyRepozitory");
            List<Property> temp_pz = pz.ToList();
            db.Props.AddRange(temp_pz);
            saveDb(sender, e);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            BusyAppOn();
            if (!isConnected)
            {
                PropertyRepozitory pz =(PropertyRepozitory)this.TryFindResource("propertyRepozitory");
                pz.RemoveAt(mainPool.SelectedIndex);
            }

            else
            {
                if (mainPool.SelectedItems.Count > 0)
                {
                    for (int i = 0; i < mainPool.SelectedItems.Count; i++)
                    {
                        Property prop = mainPool.SelectedItems[i] as Property;
                        if (prop != null)
                        {
                            db.Props.Remove(prop);
                        }
                    }
                }
                db.SaveChanges();
            }
            BusyAppOff();
            statusText.Text = "Объект удален";
        }

        private void filter_TextChanged(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BusyAppOn();
                if (!isConnected)
                {
                    PropertyRepozitory pz = (PropertyRepozitory)this.TryFindResource("propertyRepozitory");
                    foreach (Property prop in pz)
                        if (prop.ID.ToLower().Equals(filter.Text.ToLower()) && filter.Text != null && filter.Text.Length != 0)
                        {
                            object item = prop;
                            mainPool.SelectedItem = item;
                            mainPool.ScrollIntoView(item);
                            statusText.Text = "Объект найден";
                            break;
                        }
                        else
                            statusText.Text = "Объект не найден";
                    
                }
                else
                {
                    foreach (Property prop in db.Props)
                        if (prop.ID.ToLower().Equals(filter.Text.ToLower()) && filter.Text != null && filter.Text.Length != 0)
                        {
                            object item = prop;
                            mainPool.SelectedItem = item;
                            mainPool.ScrollIntoView(item);
                            statusText.Text = "Объект найден";
                            break;
                        }
                        else
                            statusText.Text = "Объект не найден";
                }
                BusyAppOff();
            }
       }

        private void ConnectedChanged(object sender, RoutedEventArgs e)
        {
            if (isConnected)
                {
                    Save(sender, e);
                    isConnected = false;
                    ConnectedCheck.IsChecked = true;
                    LoadAsync(sender, e);
                }
            else
                {
                    Save(sender, e);
                    isConnected = true;
                    ConnectedCheck.IsChecked = false;
                    LoadAsync(sender, e);
                }
        }

     private void AutoSave(object sender, EventArgs e)
        {
            RoutedEventArgs er = new RoutedEventArgs();
            Save(sender, er);
            statusText.Text = "Автосохранение";
        }
        private void BusyAppOn()
        {

                busy.IsEnabled = true;
                busy.Visibility = Visibility.Visible;
        }
        private void BusyAppOff()
        {
                busy.IsEnabled = false;
                busy.Visibility = Visibility.Hidden;
        }
        private void NewWindow(object sender, RoutedEventArgs e)
        {
            List<Property> pl;
            if (isConnected)
                pl = this.db.Props.ToList();
            else
            {
                PropertyRepozitory tempPz = (PropertyRepozitory)this.TryFindResource("propertyRepozitory");
                pl = tempPz.ToList();
            }

            Analysis analysis = new Analysis(pl);

            analysis.Owner = this;
            

            analysis.ShowDialog();

            
        }
        public PropContext DB
        {
            get { return db; }
        }
        public PropertyRepozitory GetPZ()
        {
            return (PropertyRepozitory)this.TryFindResource("propertyRepozitory");
        }
    }

    // контекст данных для БД
    public class PropContext : DbContext
    {
        public PropContext() : 
            base("DefaultConnection")
        {

        }
        public DbSet<Property> Props { get; set; }

    }

    // создадим вспомогательный класс WPFBase, имплементирующий INotifyPropertyChanged для сообщения об изменении данных в объекте
    public class WPFBase : INotifyPropertyChanged
    {
        
    public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }


        public void OnPropertyChanged(MethodBase MethodeBase)
        {
            this.OnPropertyChanged(MethodeBase.Name.Substring(4));
        }
    }
        
    [Serializable]
    public class Property : WPFBase, IDataErrorInfo, IEditableObject
    {
        // требуемые переменные
        private string id;
        private string name;
        private string company;
        private int price;
        private string responsible;
        private string objectType;


        // свойства получения требуемых данных, а также реализация WPFBase
        public string ID {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged(MethodInfo.GetCurrentMethod());
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(MethodInfo.GetCurrentMethod());
            }
        }

        public string Company
        {
            get
            {
                return company;
            }
            set
            {
                company = value;
                OnPropertyChanged(MethodInfo.GetCurrentMethod());
            }
        }

        public string Responsible
        {
            get
            {
                return responsible;
            }
            set
            {
                responsible = value;
                OnPropertyChanged(MethodInfo.GetCurrentMethod());
            }
        }
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                OnPropertyChanged(MethodInfo.GetCurrentMethod());
            }
        }
        public string ObjectType
        {
            get
            {
                return objectType;
            }
            set
            {
                objectType = value;
                OnPropertyChanged(MethodInfo.GetCurrentMethod());
            }
        }

        // реализация IDataErrorInfo, генерирующий строку ошибки
        public string Error
        {
            get
            {
                string error;
                // StringBuilder sb = new StringBuilder();
                error = this["ID"];

                /*
                    if (error != string.Empty)
                    sb.AppendLine(error);
                 */

                if (!string.IsNullOrEmpty(error))
                    // return sb.ToString();
                    return error;

                return "";
            }
        }
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "ID":
                        if (string.IsNullOrEmpty(this.ID))
                            return "Введите инвентарный номер объекта";
                        break;
                    default:
                        break;
                }
                return "";
            }
        }

        // Реализация IEditableObject, необходимая, чтобы в случае ввода недействительных данных в объект при отмене операции ввода данные вернулись в исходное положение
        private bool inEdit = false;
        private Property tempProp;

        public void BeginEdit()
        {
            if (inEdit)
                return;

            inEdit = true;
            tempProp = new Property();
            tempProp.ID = this.ID;
            tempProp.Name = this.Name;
            tempProp.Company = this.Company;
            tempProp.Responsible = this.Responsible;
            tempProp.ObjectType = this.ObjectType;
            tempProp.Price = this.Price;
        }
        public void CancelEdit()
        {
            if (!inEdit)
                return;

            inEdit = false;
            this.ID = tempProp.ID;
            this.Name = tempProp.Name;
            this.Company = tempProp.Company;
            this.Responsible = tempProp.Responsible;
            this.ObjectType = tempProp.ObjectType;
            this.Price = tempProp.Price;
            tempProp = null;
        }
        public void EndEdit()
        {
            if (!inEdit)
                return;

            inEdit = false;
            tempProp = null;
        }

    }

    public class PropertyRepozitory: ObservableCollection<Property>
    {
        
        public PropertyRepozitory()
        {
            // this.Add(new Property {ID = "P01", Name = "Стол", Price = 1300 });
            // this.Add(new Property { ID = "P02", Name = "Стул", Price = 400 });
        }
    }

    class ColumnValidation : ValidationRule

    {
        
        private PropertyRepozitoryHelper propertyRepozitory;

        // Данное свойство является вспомогателым, служит для реализации свойства зависимости
        public PropertyRepozitoryHelper PropertyRepozitory
        {
            get { return propertyRepozitory; }
            set { propertyRepozitory = value; }
        }

        // Проверка на валидность
        public override ValidationResult Validate (object value, System.Globalization.CultureInfo cultureInfo)
        {
            BindingGroup bg = value as BindingGroup;
            Property prop = bg.Items[0] as Property;

            // Если в коллекции находится объект с таким же ID, то сообщаем об ошибке
            var res = from p in PropertyRepozitory.Items where p != prop && (p.ID == prop.ID) select p;
            if (res.Any())
            {
                (Application.Current.MainWindow as MainWindow).statusText.Text = "ID уже занято";
                return new ValidationResult(false, "ID уже занято");
            }

            return ValidationResult.ValidResult;
        }
    }

    // Вспомогательный класс для реализации своства зависимости
    public class PropertyRepozitoryHelper : DependencyObject
    {
        //К свойству Items можно привиязать данные в XAML, так как оно объявленно как DependencyProperty
        public PropertyRepozitory Items
        {
            get { return (PropertyRepozitory)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", 
            typeof(PropertyRepozitory),
            typeof(PropertyRepozitoryHelper),
            new UIPropertyMetadata(null)
            );
    }
    
}
