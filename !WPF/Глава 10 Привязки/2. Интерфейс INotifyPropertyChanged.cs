// В прошлой теме использовался объект Phone для привязки к текстовым блокам. Однако если мы изменим его, содержимое текстовых блоков не изменится. Например, добавим в окно приложения кнопку:
<Window x:Class="BindingApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:BindingApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="150" Width="300">
    <Window.Resources>
        <local:Phone x:Key="nexusPhone" Title="Nexus X5" Company="Google" Price="25000" />
    </Window.Resources>
    <Grid Background="Black" DataContext="{StaticResource nexusPhone}" TextBlock.Foreground="White">
        <Grid.ColumnDefinitions>
            <ColumnDefinition />
            <ColumnDefinition />
            <ColumnDefinition />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition />
            <RowDefinition />
            <RowDefinition />
        </Grid.RowDefinitions>
        <TextBlock Text="Модель" />
        <TextBlock Text="{Binding Title}" Grid.Row="1" />
        <TextBlock Text="Производитель" Grid.Column="1"/>
        <TextBlock Text="{Binding Company}" Grid.Column="1" Grid.Row="1" />
        <TextBlock Text="Цена" Grid.Column="2" />
        <TextBlock Text="{Binding Price}" Grid.Column="2" Grid.Row="1" />
 
        <Button Foreground="White" Content="Изменить" Click="Button_Click" Background="Black"
            BorderBrush="Silver" Grid.Column="2" Grid.Row="2" />
    </Grid>
</Window>

// Сколько бы мы не нажимали на кнопку, текстовые блоки, привязанные к ресурсу, не изменятся. Чтобы объект мог полноценно реализовать механизм привязки, нам надо реализовать в его классе интерфейс INotifyPropertyChanged. И для этого изменим класс Phone следующим образом:
using System.ComponentModel;
using System.Runtime.CompilerServices;
 
class Phone : INotifyPropertyChanged
{
    private string title;
    private string company;
    private int price;
 
    public string Title
    {
        get { return title; }
        set
        {
            title = value;
            OnPropertyChanged("Title");
        }
    }
    public string Company
    {
        get { return company; }
        set
        {
            company = value;
            OnPropertyChanged("Company");
        }
    }
    public int Price
    {
        get { return price; }
        set
        {
            price = value;
            OnPropertyChanged("Price");
        }
    }
 
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName]string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}







