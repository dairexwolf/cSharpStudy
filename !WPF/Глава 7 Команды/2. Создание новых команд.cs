// В WPF определено много команд, но даже их может оказаться недостаточно. Поэтому нередко разработчики создают свои собственные команды. Наиболее простой способ создания команды - использование готовых классов RoutedCommand и RoutedUICommand, в которых уже реализован интерфейс ICommand. Итак, в файле кода под классом окна создадим новый класс, назовем его WindowCommands, который будет содержать новые команды:
using System.Windows.Input;
 
public class WindowCommands
{
    static WindowCommands()
    {
        Exit = new RoutedCommand("Exit", typeof(MainWindow));
    }
    public static RoutedCommand Exit { get; set; }
}

// В данном случае команда называется Exit и представляет собой объект RoutedCommand. В конструктор этого объекта в данном случае передается название команды и элемент-владелец команды (здесь объект MainWindow). Причем команда определяется как статическое свойство.

// Теперь в коде XAML установим привязку к этой команде и задействуем ее:
<Window x:Class="CommandsApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:CommandsApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="250" Width="300">
    <Window.CommandBindings>
        <CommandBinding Command="local:WindowCommands.Exit" Executed="Exit_Executed"/>
    </Window.CommandBindings>
    <Grid>
        <Menu VerticalAlignment="Top" MinHeight="25">
            <MenuItem Header="Выход" Command="local:WindowCommands.Exit"  />
        </Menu>
        <Button x:Name="Button1" Width="80" Height="30" Content="Выход"
                Command="local:WindowCommands.Exit"  />
    </Grid>
</Window>

// Итак, здесь есть пункт меню и есть кнопка, которые вызывают эту команду. Так как команды определены в локальном пространстве имен, которое соответствует префиксу local и представляют статические свойства, то к ним обращаемся с помощью выражения local:WindowCommands.Exit. Привязка команды связывает ее с обработчиком Exit_Executed, который определим в коде c#:
private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
{
    using (System.IO.StreamWriter writer = new System.IO.StreamWriter("log.txt", true))
    {
        writer.WriteLine("Выход из приложения: " + DateTime.Now.ToShortDateString() + " " +
        DateTime.Now.ToLongTimeString());
        writer.Flush();
    }
 
    this.Close();
}



