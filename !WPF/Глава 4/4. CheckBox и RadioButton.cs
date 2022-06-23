// CheckBox
// Элемент CheckBox представляет собой обычный флажок. Данный элемент является производным от класса ToggleButton и поэтому может принимать также три состояния: Checked, Unchecked и Intermediate.

// Чтобы получить или установить определенное состояние, надо использовать свойство IsChecked, которое также унаследовано от ToggleButton:

        <CheckBox x:Name="checkBox1" IsThreeState="False" IsChecked="False" Height="20" Content="Неотмечено" />
        <CheckBox x:Name="checkBox2" IsThreeState="False" IsChecked="True" Height="20" Content="Отмечено" />
        <CheckBox x:Name="checkBox3" IsThreeState="True" IsChecked="{x:Null}" Height="20" Content="Неопределено"/>

// Установка свойства IsChecked="{x:Null}" задает неопределенное состояние для элемента checkbox. Остальные два состояния задаются с помощью True и False. В данном примере также привязан к двум флажкам обработчик события Checked. Это событие возникает при установке checkbox в отмеченное состояние.

// А атрибут IsThreeState="True" указывает, что флажок может находиться в трех состояниях.

// Ключевыми событиями флажка являются события Checked (генерируется при установке флажка в отмеченное состояние), Unchecked (генерируется при снятии отметки с флажка) и Indeterminate (флажок переведен в неопределенное состояние). Например, определим флажок:
<CheckBox x:Name="checkBox 3" IsChecked="False" Height="20" Content="Флажок"
    IsThreeState="True"
    Unchecked="checkBox_Unchecked"
    Indeterminate="checkBox_Indeterminate"
    Checked="checkBox_Checked" />

// А в файле кода C# пропишем для него обработчики:
private void checkBox_Checked(object sender, RoutedEventArgs e)
{
    MessageBox.Show(checkBox.Content.ToString() + " отмечен");
}
 
private void checkBox_Unchecked(object sender, RoutedEventArgs e)
{
    MessageBox.Show(checkBox.Content.ToString() + " не отмечен");
}
 
private void checkBox_Indeterminate(object sender, RoutedEventArgs e)
{
    MessageBox.Show(checkBox.Content.ToString() + " в неопределенном состоянии");
}

// Программное добавление флажка:

using System.Windows;
using System.Windows.Controls;
 
namespace ControlsApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // создаем флажок
            CheckBox checkBox4 = new CheckBox { Content = "Новый флажок", MinHeight = 20, IsChecked=true };
            // установка обработчика
            checkBox4.Checked += checkBox_Checked;
            // добавление в StackPanel
            stackPanel.Children.Add(checkBox4);
        }
    }
}

// RadioButton
// Элемент управления, также производный от ToggleButton, представляющий переключатель. Главная его особенность - поддержка групп. Несколько элементов RadioButton можно объединить в группы, и в один момент времени мы можем выбрать из этой группы только один переключатель.
<StackPanel x:Name="stackPanel">
    <RadioButton GroupName="Languages" Content="C#" IsChecked="True" />
    <RadioButton GroupName="Languages" Content="VB.NET" />
    <RadioButton GroupName="Languages" Content="C++" />
    <RadioButton GroupName="Technologies" Content="WPF" IsChecked="True" />
    <RadioButton GroupName="Technologies" Content="WinForms" />
    <RadioButton GroupName="Technologies" Content="ASP.NET" />
</StackPanel>

// Чтобы включить элемент в определенную группу, используется свойство GroupName. В данном случае у нас две группы - Languages и Technologies. Мы можем отметить не более одного элемента RadioButton в пределах одной группы, зафиксировав тем самым выбор из нескольких возможностей.

// Чтобы проследить за выбором того или иного элемента, мы также можем определить у элементов событие Checked и его обрабатывать в коде:

<RadioButton GroupName="Languages" Content="VB.NET" Checked="RadioButton_Checked" />

// C#
private void RadioButton_Checked(object sender, RoutedEventArgs e)
{
    RadioButton pressed = (RadioButton)sender;
    MessageBox.Show(pressed.Content.ToString());
}

// Без XMAL
using System.Windows;
using System.Windows.Controls;
 
namespace ControlsApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
 
            RadioButton rb = new RadioButton { IsChecked = true, GroupName = "Languages", Content = "JavaScript" };
            rb.Checked += RadioButton_Checked;
            stackPanel.Children.Add(rb);
        }
 
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            MessageBox.Show(pressed.Content.ToString());
        }
    }
}







