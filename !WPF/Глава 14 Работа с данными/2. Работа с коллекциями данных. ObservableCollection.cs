// В прошлых темах были рассмотрены примеры привязки отдельных объектов к элементам интерфейса. Но, как правило, приложения оперируют не одиночными данными, а большими наборами, коллекциями объектов. Для работы непосредственно с наборами данных в WPF определены различные элементы управления списками, такие как ListBox, ListView, DataGrid, TreeView, ComboBox.

// Их отличительной особенностью является то, что они наследуются от базового класса ItemsControl и поэтому наследуют ряд общей функциональности для работы с данными.
/* Прежде всего можно выделить свойства:

Items: устанавливает набор объектов внутри элемента

ItemsSource: ссылка на источник данных

ItemStringFormat: формат, который будет использоваться для форматирования строк, например, при переводе в строку числовых значений

ItemContainerStyle: стиль, который устанавливается для контейнера каждого элемента (например, для ListBoxItem или ComboBoxItem)

ItemTemplate: представляет шаблон данных, который используется для отображения элементов

ItemsPanel: панель, которая используется для отображения данных. Как правило, применяется VirtualizingStackPanel

DisplayMemberPath: свойство, которое будет использоваться для отображения в списке каждого объекта
*/

// При работе с элементами управления списками важно понимать, что эти элементы предназначены прежде всего для отображения данных, а не для хранения. В каких-то ситуациях мы, конечно, можем определять небольшие списки непосредственно внутри элемента. Например:
<ListBox>
	<ListBox.Items>
		<ListBoxItem>Number 1</ListBoxItem>
		<ListBoxItem>Number 2</ListBoxItem>
		<ListBoxItem>Number 3</ListBoxItem>
	</ListBox.Items>
</ListBox>

// Но в большинстве случае предпочтительнее использовать привязку к спискам и разделять источник данных от их представления или визуализации. Например, определим ListBox:
<ListBox x:Name="propsList" />

// А в коде c# создадим источник данных и установим привязку к нему:
public partial class MainWindow : Window
    {
        List<Property> propsList;

        public MainWindow()
        {
            
            InitializeComponent();

            propsList = new List<Property> {
                new Property
                {
                    ID=1, Name="Стол", Price=5000, Company = new Company {Title = "ОАО \"Столы блин-блинский\"" }
                },
                new Property
                {
                    ID=2, Name="Стул", Price=2000, Company = new Company {Title = "ООО \"Столы Петровича\"" }
                },
                new Property
                {
                    ID=3, Name="Телевизор LG10500", Price=25000, Company = new Company {Title = "LG" }
                }
            };

            List<String> propsLestString = new List<string>();
            foreach (var prop in propsList)
            {
                propsLestString.Add(prop.ToString()); 
            }
            props.ItemsSource = propsLestString;
            
        }
                
    }
    public class Property
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Company Company { get; set; }
         public override string ToString()
        {
            return (string)Name;
        }
    }

    public class Company
    {
        public string Title { get; set; }
    }

// ObservableCollection
// В примере выше в качестве источника данных использовался список List. Также в качестве источника мы бы могли использовать другой какой-нибудь тип набора данных - массив, объект HashSet и т.д. Но нередко в качестве источника применяется класс ObservableCollection, который находится в пространстве имен System.Collections.ObjectModel. Его преимущество заключается в том, что при любом изменении ObservableCollection может уведомлять элементы, которые применяют привязку, в результате чего обновляется не только сам объект ObservableCollection, но и привязанные к нему элементы интерфейса.

// Например, рассмотрим следующую ситуацию. У нас кроме элемента ListBox есть текстовое поле и кнопка для добавления нового объекта:
Title="MainWindow" Height="350" Width="525" Name="mainWindow">

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>
        <ListBox x:Name="props" />
        <StackPanel Grid.Row="1" Orientation="Horizontal" Margin="0 15 0 0" HorizontalAlignment="Center">
            <TextBox Name="propNameTextBox" Width="415" />
            <Button Content="Сохранить" MaxWidth="70" Margin="10 0 0 0" Click="Button_Click" />
        </StackPanel>
    </Grid>

// В файле кода определим обработчик кнопки, в котором новый элемент добавлялся бы в источник данных:
public partial class MainWindow : Window
    {
        List<Property> propsList;
        List<String> propsLestString = new List<string>();

        public MainWindow()
        {
            
            InitializeComponent();

            propsList = new List<Property> {
                new Property
                {
                    ID=1, Name="Стол", Price=5000, Company = new Company {Title = "ОАО \"Столы блин-блинский\"" }
                },
                new Property
                {
                    ID=2, Name="Стул", Price=2000, Company = new Company {Title = "ООО \"Столы Петровича\"" }
                },
                new Property
                {
                    ID=3, Name="Телевизор LG10500", Price=25000, Company = new Company {Title = "LG" }
                }
            };

            
            foreach (var prop in propsList)
            {
                propsLestString.Add(prop.ToString()); 
            }
            props.ItemsSource = propsLestString;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string prop = propNameTextBox.Text;
            // добавление нового объекта
            propsLestString.Add(prop);
        }
    }
    public class Property
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Company Company { get; set; }
         public override string ToString()
        {
            return (string)Name;
        }
    }

    public class Company
    {
        public string Title { get; set; }
    }

// Сохраненный результат не будет отображаться, ибо используется List. По нажатию на кнопку должно произойти добавления в список props введенной в текстовое поле строки. И мы ожидаем, что после добавления ListBox отобразит нам добавленный объект. Однако так как в качестве источника применяется List, то обновления элемента ListBox не произойдет. 
// Поэтому заменим List на ObservableCollection:
using System.Collections.ObjectModel;

namespace DataApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Property> propsList;
        ObservableCollection<String> propsLestString;

        public MainWindow()
        {
            
            InitializeComponent();

            propsList = new List<Property> {
                new Property
                {
                    ID=1, Name="Стол", Price=5000, Company = new Company {Title = "ОАО \"Столы блин-блинский\"" }
                },
                new Property
                {
                    ID=2, Name="Стул", Price=2000, Company = new Company {Title = "ООО \"Столы Петровича\"" }
                },
                new Property
                {
                    ID=3, Name="Телевизор LG10500", Price=25000, Company = new Company {Title = "LG" }
                }
            };

            propsLestString = new ObservableCollection<string>();
            foreach (var prop in propsList)
            {
                propsLestString.Add(prop.ToString()); 
            }
            props.ItemsSource = propsLestString;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string prop = propNameTextBox.Text;
            // добавление нового объекта
            propsLestString.Add(prop);
        }
    }
    public class Property
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Company Company { get; set; }
         public override string ToString()
        {
            return (string)Name;
        }
    }

    public class Company
    {
        public string Title { get; set; }
    }
}

// И теперь у нас уже не возникнет подобной проблемы
