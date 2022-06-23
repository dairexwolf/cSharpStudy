// Для практики с шаблонами переопределим шаблон окна, чтобы оно имело нестандартную непрямоугольную форму:
<Window x:Class="ControlsTemplateApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:ControlsTemplateApp"
        mc:Ignorable="d"
        Title="Круглое окно" Height="350" Width="525"
        AllowsTransparency="True" WindowStyle="None" Background="LightBlue">

    <Window.Template>
        <ControlTemplate TargetType="Window">
            <Border Name="newBorder" CornerRadius="250" Opacity="0.7" Background="LightBlue">
                <Grid>
                    <Grid.RowDefinitions>
                        <RowDefinition Height="Auto" />
                        <RowDefinition />
                        <RowDefinition Height="Auto" />
                    </Grid.RowDefinitions>
                    <!--Заголовок-->
                    <TextBlock Text="{TemplateBinding Title}" FontWeight="Bold" HorizontalAlignment="Center"
                               MouseButtonDown="TextBlock_MouseLeftVuttonDown" />
                    <!--Основное содержание-->
                    <Border Grid.Row="1">
                        <AdornerDecorator>
                            <ContentPresenter />
                        </AdornerDecorator>
                    </Border>
                    
                    <!--Элемент захвата и изменения размера - работает только для прямоугольных окон-->
                    <ResizeGrip Grid.Row="2" HorizontalAlignment="Right" VerticalAlignment="Bottom" Visibility="Collapsed" IsTabStop="False" />
                </Grid>
            </Border>
        </ControlTemplate>
    </Window.Template>
    <Grid>
        <Button x:Name="closeButton" Content="Закрыть" Click="closeButton_Click" Width="100" Height="30" Background="Aqua" />
    </Grid>
        
</Window>

/* Важно здесь отметить установку трех свойств элемента Window:

* AllowsTransparency: позволяет сделать форму прозрачной, невидимой

* WindowStyle: позволяет убрать у окна стиль при установке значения "None"

* Background: устанавливает прозрачнй фон с помощью значения Transparent
*/

// И также в файле кода c# изменим код окна, определив необходимые обработчики событий:	
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Служит для перемещения окна за заголовок формы
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }














