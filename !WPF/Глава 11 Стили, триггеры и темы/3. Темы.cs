// Стили позволяют задать стилевые особенности для определенного элемента или элементов одного типа. Но иногда возникает необходимость применить ко всем элементам какое-то общее стилевое единообразие. И в этом случае мы можем объединять стили элементов в темы. Например, все элементы могут выполнены в светлом стиле, или, наоборот, к ним может применяться так называемая "ночная тема". Более того может возникнуть необходимость не просто определить общую тему для всех элементов, но и иметь возможность динамически выбирать понравившуюся тему из списка тем. И в данной статье рассмотрим, как это сделать.

// Пусть у нас есть окно приложения с некоторым набором элементов:
<Window x:Class="StylesApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:StylesApp"
        mc:Ignorable="d"
        Title="Стили" Height="250" Width="300" Style="{DynamicResource WindowStyle}" >
    <StackPanel>
    <ComboBox x:Name="styleBox" />
    <Button Content="Hello WPF" Style="{DynamicResource ButtonStyle}" />
    <TextBlock Text="Windows Presentation Foundation" Style="{DynamicResource TextBlockStyle}" />
    </StackPanel>
</Window>


// Для примера здесь определены кнопка, текстовый блок и выпадающий список, в котором позже будут выбираться темы.

// К элементам окна уже применяются некоторые стили. Причем следует отметить, что стили указывают на динамические (не статические) ресурсы. Однако сами эти ресурсы еще не заданы. Поэтому зададим их.

// Для этого добавим в проект новый файл словаря ресурсов, который назовем light.xaml, и определим в нем некоторый набор ресурсов:
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:local="clr-namespace:StylesApp">
    <Style x:Key="TextBlockStyle" TargetType="TextBlock" >
        <Setter Property="Background" Value="White" />
        <Setter Property="Foreground" Value="Black" />
    </Style>

    <Style x:Key="WindowStyle" TargetType="Window">
        <Setter Property="Background" Value="White" />
    </Style>
    <Style x:Key="ButtonStyle" TargetType="Button">
        <Setter Property="Background" Value="Gray" />
        <Setter Property="Foreground" Value="Black" />
        <Setter Property="BorderBrush" Value="Black" />
    </Style>

</ResourceDictionary>

// Здесь указаны все те стили, которые применяются элементами окна.

// Но теперь также добавим еще один словарь ресурсов, который назовем dark.xaml и в котором определим следующий набор ресурсов:
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:local="clr-namespace:ThemesApp">
    <Style x:Key="TextBlockStyle" TargetType="TextBlock">
        <Setter Property="Background" Value="Black" />
        <Setter Property="Foreground" Value="Gray" />
    </Style>
    <Style x:Key="WindowStyle" TargetType="Window">
        <Setter Property="Background" Value="Black" />
    </Style>
    <Style x:Key="ButtonStyle" TargetType="Button">
        <Setter Property="Background" Value="Black" />
        <Setter Property="Foreground" Value="Gray" />
        <Setter Property="BorderBrush" Value="Gray" />
    </Style>
    
</ResourceDictionary>

// Здесь определены те же самые стили, только их значения уже отличаются. То есть фактически мы создали две темы: для светлого и темного стилей.
// Теперь применим эти стили. Для этого изменим файл MainWindow.xaml.cs следующим образом:
 public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<string> styles = new List<string> { "light", "dark" };
            styleBox.SelectionChanged += ThemeChange;
            styleBox.ItemsSource = styles;
            styleBox.SelectedItem = "light";
        }

        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            string style = styleBox.SelectedItem as string;
            // определяем путь к файлу ресурсов
            Uri uri = new Uri(style + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь
            Application.Current.Resources.MergedDictionaries.Add(resDict);
        }
    }

// К элементу ComboBox цепляется обработчик ThemeChange, который срабатывает при выборе элемента в списке.

// В методе ThemeChange получаем выделенный элемент, который представляет название темы. По нему загружаем локальный словарь ресурсов и добавляем этот словарь в коллекцию ресурсов приложения.

// В итоге при выборе элемента в списке будет меняться применяемая к приложению тема:







