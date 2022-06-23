// При создании нового проекта WPF в дополнение к создаваемому файлу MainWindow.xaml создается также файл отделенного кода MainWindow.xaml.cs, где, как предполагается, должна находится логика приложения связанная с разметкой из MainWindow.xaml. Файлы XAML позволяют нам определить интерфейс окна, но для создания логики приложения, например, для определения обработчиков событий элементов управления, нам все равно придется воспользоваться кодом C#.

// По умолчанию в разметке окна используется атрибут x:Class:
<Window x:Class="WpfApp.MainWindow">
.......
</Window>
// Атрибут x:Class указывает на класс, который будет представлять данное окно и в который будет компилироваться код в XAML при компиляции. То есть во время компиляции будет генерироваться класс XamlApp.MainWindow, унаследованный от класса System.Windows.Window.

// Кроме того в файле отделенного кода MainWindow.xaml.cs, который Visual Studio создает автоматически, мы также можем найти класс с тем же именем - в данном случае класс XamlApp.MainWindow. По умолчанию он имеет некоторый код

// По сути пустой класс, но этот класс уже выполняет некоторую работу. Во время компиляции этот класс объединяется с классом, сгенерированном из кода XAML. Чтобы такое слияние классов во время компиляции произошло, класс XamlApp.MainWindow определяется как частичный с модификатором partial. А через метод InitializeComponent() класс MainWindow вызывает скомпилированный ранее код XAML, разбирает его и по нему строит графический интерфейс окна.

// В приложении часто требуется обратиться к какому-нибудь элементу управления. Для этого надо установить у элемента в XAML свойство Name.
// Еще одной точкой взаимодействия между xaml и C# являются события. С помощью атрибутов в XAML мы можем задать события, которые будут связанны с обработчиками в коде C#.
// Итак, создадим новый проект WPF, который назовем XamlApp. В разметке главного окна определим два элемента: кнопку и текстовое поле.

<Grid>
    <TextBox x:Name="textBox1" Width="100" Height="40" VerticalAlignment="Top" Margin="20" />
    <Button Content="Нажми меня" FontSize="22" Width="200" Height="80" Click="Button_Click" />
</Grid>

// И изменим файл отделенного кода, добавив в него обработчик нажатия кнопки:

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string text = textBox1.Text;
            if (text != "")
            {
                MessageBox.Show(text);
            }
            else MessageBox.Show("Текст введи");
        }

// При определении имен в XAML надо учитывать, что оба пространства имен "http://schemas.microsoft.com/winfx/2006/xaml/presentation" и "http://schemas.microsoft.com/winfx/2006/xaml" определяют атрибут Name, который устанавливает имя элемента. Во втором случае атрибут используется с префиксом x: x:Name. Какое именно пространство имен использовать в данном случае, не столь важно, а следующие определения имени x:Name="button1" и Name="button1" фактически будут равноценны.

// Еще одну форму взаимодействия C# и XAML представляет создание визуальных элементов в коде C#. Например, изменим код xaml следующим образом:

<Window x:Class="XamlApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:XamlApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <Grid x:Name="layoutGrid">
 
    </Grid>
</Window>

// Здесь для элемента Grid установлено свойство x:Name, через которое мы можем к нему обращаться в коде. И также изменим код C#:

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
            Button myButton = new Button();
            myButton.Width = 100;
            myButton.Height = 30;
            myButton.Content = "Кнопка";
            myButton.Click +=Button_Click;
            layoutGrid.Children.Add(myButton);
        }

        private void Button_Click(object o, RoutedEventArgs e)
        {
            MessageBox.Show("Макс пидор");
        }
    }
}




