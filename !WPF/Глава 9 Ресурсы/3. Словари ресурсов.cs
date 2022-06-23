// Мы можем определять ресурсы на уровне отдельных элементов окна, например, как ресурсы элементов Window, Grid и т.д. Однако есть еще один способ определения ресурсов, который предполагает использование словаря ресурсов.

// Нажмем правой кнопкой мыши на проект и в контекстном меню выберем Add -> New Item..., И в окне добавления выберем пункт Resource Dictionary (WPF):

// Оставим у него название по умолчанию Dictionary1.xaml и нажмем на кнопку OK.
// После этого в проект добавляется новый файл. Он представляет собой обычный xaml-файл с одним корневым элементом ResourceDictionary:
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:local="clr-namespace:ResourcesApp">
     
</ResourceDictionary>

// Изменим его код, добавив какой-нибудь ресурс:
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    xmlns:local="clr-namespace:ResourcesApp">
    <LinearGradientBrush x:Key="buttonBrush">
        <GradientStopCollection>
            <GradientStop Color="White" Offset="0" />
            <GradientStop Color="Green" Offset="0.7" />
        </GradientStopCollection>
    </LinearGradientBrush>
</ResourceDictionary>

// После определения файла ресурсов его надо подсоединить к ресурсам приложения. Для этого откроем файл App.xaml, который есть в проекте по умолчанию и изменим его:
<Application x:Class="ResourcesApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:ResourcesApp"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Dictionary1.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>

// Элемент ResourceDictionary.MergedDictionaries здесь представляет колекцию объектов ResourceDictionary, то есть словарей ресурсов, которые добавляются к ресурсам приложения. Затем в любом месте приложения мы сможем сослаться на этот ресурс:
<Button Content="OK" MaxHeight="40" MaxWidth="80" Background="{StaticResource buttonBrush}" />

// При этом одновременно мы можем добавлять в коллекцию ресурсов приложения множество других словарей или параллельно с ними определять еще какие-либо ресурсы:
<Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Dictionary1.xaml" />
                <ResourceDictionary Source="Dictionary2.xaml" />
                <ResourceDictionary Source="ButtonStyles.xaml" />
                <SolidColorBrush Color="LimeGreen" x:Key="limeButton" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>

// 															Загрузка словаря ресурсов
// Нам необязательно добавлять словарь ресурсов через ресурсы приложения. У объекта ResourceDictionary имеется свойство Source, через которое мы можем связать ресурсы конкретного элемента со словарем: (Не получилось)
<Window.Resources>
        <ResourceDictionary Source="Dictionary1.xaml" />
    </Window.Resources>
    <Grid>
        <Button Content="OK" MaxHeight="40" MaxWidth="80" Background="{StaticResource buttonBrush}" />
    </Grid>

// Также мы можем загружать словарь динамически в коде C#. Так, загрузим в коде C# вышеопределенный словарь:
this.Resources = new ResourceDictionary() { Source = new Uri("pack://application:,,,/Dictionary1.xaml") };

// При динамической загрузке, если мы определяем ресурсы через xaml, то они должны быть динамическими:
<Button Content="OK" MaxHeight="40" MaxWidth="80" Background="{DynamicResource buttonBrush}" />