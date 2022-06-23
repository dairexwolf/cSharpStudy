// По умолчанию в WPF в XAML подключается предустановленный набор пространств имен xml. Но мы можем задействовать любые другие пространства имен и их функциональность в том числе и стандартные пространства имен платформы .NET, например, System или System.Collections. Например, по умолчанию в определении элемента Window подключается локальное пространство имен:

xmlns:local="clr-namespace:WpfApp"

// Локальное пространство имен, как правило, называется по имени проекта (в моем случае проект называется WpfApp) и позволяет подключить все классы, которые определены в коде C# в нашем проекте. Например, добавим в проект следующий класс:
public class Phone
{
	public string Name {get; set;}
	public int Price {get; set;}
	
	public override string ToString()
	{
		return $"Смартфон {this.Name}; Цена: {this.Price}";
	}
}

// Используем этот класс в коде xaml:

<Window x:Class="WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfApp"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <Grid x:Name="layoutGrid">

        <Button x:Name="phoneButton" Width="250" Height="40" HorizontalAlignment="Center" >
            <Button.Content>
                <local:Phone Name="Samsung G6" Price="230" />
            </Button.Content>
        </Button>
    </Grid>
</Window>

// Так как пространство имен проекта проецируется на префикс local, то все классы проекта используются в форме local:Название_Класса. Так в данном случае объект Phone устанавливается в качестве содержимого кнопки через свойство Content. Для сложных объектов это свойство принимает их строковое представление, которое возвращается методом ToString():

// Мы можем подключить любые другие пространства имен, классы которых мы хотим использовать в приложении. Например:

<Window x:Class="WpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfApp"
        
        xmlns:col="clr-namespace:System.Collections;assembly=mscorlib"
        xmlns:sys="clr-namespace:System;assembly=mscorlib"
        
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <Window.Resources>
        <col:ArrayList x:Key="days">
            <sys:String>Monday</sys:String>
            <sys:String>Tuesday</sys:String>
            <sys:String>Wednesday</sys:String>
            <sys:String>Thursday</sys:String>
            <sys:String>Friday</sys:String>
            <sys:String>Saturday</sys:String>
            <sys:String>Sunday</sys:String>
        </col:ArrayList>
    </Window.Resources>
    
    <Grid x:Name="layoutGrid">

        <Button x:Name="myButton" Width="250" Height="40" HorizontalAlignment="Center" >
            <Button.Content>
                <sys: />
            </Button.Content>
        </Button>
    </Grid>
</Window>

// Здесь определены два дополнительных пространства имен:
xmlns:col="clr-namespace:System.Collections;assembly=mscorlib"
xmlns:sys="clr-namespace:System;assembly=mscorlib"

// Благодаря этому нам становятся доступными объекты из пространств имен System.Collections и System. И затем используя префикс, мы можем использовать объекты, входящие в данные пространства имен: <col:ArrayList....

// Общий синтаксис подключения пространств имен следующий: xmlns:Префикс="clr-namespace:Пространство_имен;assembly=имя_сборки". Так в предыдущем случае мы подключили пространство имен System.Collections, классы которого находятся в сборке mscorlib. И данное подключенное пространство имен у нас отображено на префикс col.

// Только я не понял, как именно использовать days