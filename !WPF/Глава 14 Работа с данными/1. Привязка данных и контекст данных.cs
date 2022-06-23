// Большую роль при работе с данными играет механизм привязки. Ранее в одной из прошлых тем рассматривалась привязка элементов и их свойств. В этой же теме сделаем больший упор на привязку данных.

/* Для создания привязки применяется элемент Binding и его свойства:

ElementName: имя элемента, к которому идет привязка. Если мы говорим о привязке данных, то данное свойство задействуется редко за исключением тех случаев, когда данные определены в виде свойства в определенном элементе управления

Path: ссылка на свойство объекта, к которому идет привязка

Source: ссылка на источник данных, который не является элементом управления

*/

// Свойства элемента Binding помогают установить источник привязки. Для установки источника или контекста данных в элементах управления WPF предусмотрено свойство DataContext. Рассмотрим на примерах их использование.

// Пусть у нас в проекте определен следующий класс Property:
 public class Property
        {
            public int ID{ get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public Company Company { get; set; }
        }

        public class Company
        {
            public string Title { get; set; }
        }

// Это сложный класс, который включает кроме простых данных типа string и decimal также и сложный объект Company.
// Определим в классе окна MainWindow свойство, которое будет представлять объект Property:
public partial class MainWindow : Window
    {
        public Property myProperty { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            myProperty = new Property
            {
                ID = 0,
                Name = "Стол ебат",
                Price = 10000,
                Company = new Company { Title = "ООО \"Мебель\"" }
            };
            this.DataContext = myProperty;
        }
                
    }

// Здесь установлено свойство DataContext класса MainWindow, после чего мы сможем получить значения из myProperty в любом элементе в пределах MainWindow:
<Window x:Class="DataApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:DataApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <StackPanel>
        <TextBlock Text="{Binding Path=Name}" />
        <TextBlock DataContext="{Binding Path=Company}" Text="{Binding Path=Title}" />
    </StackPanel>
</Window>

// Причем контекст данных переходит от корневых элементов к вложенным вниз по логическому дереву. Так, мы установили в качестве контекста для всего окна объект MyPhone. Однако элементы внутри окна могут конкретизировать контекст, взять какую-то его часть

// Данный текстовый блок устанавливает в качестве контекста объект Company из общего контекста myProperty

// В тоже время нам необязательно конкретизировать контекст для текстового блока, вместо этого мы могли бы с помощью нотации точки обратиться к вложенным свойствам:
<TextBlock Text="{Binding Path=Company.Title}" />

// При этом надо учитывать, что когда мы определяем простое свойство объекта в коде c#, то установить в качестве контекста данных мы можем его только там же в коде C#, как, например, выше контекст данных устанавливается в конструкторе окна. Однако если мы вместо простого свойства определим свойство зависимостей, тогда мы сможем устанавливать контекст данных и в коде xaml:
public partial class MainWindow : Window
    {
        public static readonly DependencyProperty DepProperty;
        public Property Property
        {
            get { return (Property)GetValue(DepProperty); }
            set { SetValue(DepProperty, value); }
        }
        static MainWindow()
        {
            

            DepProperty = DependencyProperty.Register(
                "Property",
                typeof(Property),
                typeof(MainWindow));
            
        }
        public MainWindow()
        {
            InitializeComponent();

            Property = new Property
            {
                Name = "Стол учебный",
                Company = new Company { Title = "ОАО\"Столы\"" },
                Price = 10000
            };
        }
                
    }

// В данном случае контекст данных уже не устанавливается, а вместо обычного свойства определено свойство зависимостей. Тогда в коде xaml мы можем обратиться к этому свойству следующим образом:
<Window x:Class="DataApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:DataApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525" Name="mainWindow">
    <StackPanel>
        <TextBlock Text="{Binding ElementName=mainWindow, Path=Property.Name}" />
        <TextBlock Text="{Binding ElementName=mainWindow, Path=Property.Company.Title}" />
    </StackPanel>
</Window>

// Для ссылки на свойство необходимо установить имя окна: Name="mainWindow". Также мы могли бы сделать то же самое, используя контекст данных:
    <StackPanel DataContext="{Binding ElementName=mainWindow, Path=Property }">
        <TextBlock Text="{Binding Path=Name}" />
        <TextBlock DataContext="{Binding Path=Company}" Text="{Binding Path=Title}" />
    </StackPanel>

// 																	Подключение к ресурсам
// Также в качестве контекста данных можно установить какой-нибудь ресурс. Например:
<Window x:Class="DataApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:DataApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525" Name="mainWindow">
    <Window.Resources>
        <local:Company x:Key="anotherProp" Title="Еще мебель" />
        <local:Property x:Key="chair" ID="1" Name="Стул учебный" Price="3000" Company="{StaticResource anotherProp}" />
    </Window.Resources>
    <StackPanel DataContext="{Binding ElementName=mainWindow, Path=Property }">
        <TextBlock Text="{Binding Path=Name}" />
        <TextBlock DataContext="{Binding Path=Company}" Text="{Binding Path=Title}" />
        <StackPanel DataContext="{StaticResource chair}">
            <TextBlock Text="{Binding Path=Name}" />
            <TextBlock DataContext="{Binding Path=Company}" Text="{Binding Path=Title}" />
        </StackPanel>
    </StackPanel>
</Window>










