// Стили позволяют определить набор некоторых свойств и их значений, которые потом могут применяться к элементам в xaml. Стили хранятся в ресурсах и отделяют значения свойств элементов от пользовательского интерфейса. Также стили могут задавать некоторые аспекты поведения элементов с помощью триггеров. Аналогом стилей могут служить каскадные таблицы стилей (CSS), которые применяются в коде html на веб-страницах.
// Зачем нужны стили? Стили помогают создать стилевое единообразие для определенных элементов. Допустим, у нас есть следующий код xaml:
<StackPanel x:Name="buttonsStack" Background="Black" >
    <Button x:Name="button1" Margin="10" Content="Кнопка 1" FontFamily="Verdana" Foreground="White" Background="Black" />
    <Button x:Name="button2" Margin="10" Content="Кнопка 2" FontFamily="Verdana" Foreground="White" Background="Black"/>
</StackPanel>

// Здесь обе кнопки применяют ряд свойств с одними и теми же значениями. Однако в данном случае мы вынуждены повторяться. Частично, проблему могло бы решить использование ресурсов:
<Window.Resources>
        <FontFamily x:Key="buttonFont">Verdana</FontFamily>
        <SolidColorBrush Color="White" x:Key="buttonFontColor" />
        <SolidColorBrush Color="Black" x:Key="buttonBackColor" />
        <Thickness x:Key="buttonMargin" Bottom="10" Left="10" Top="10" Right="10" />
    </Window.Resources>
    <StackPanel x:Name="buttonsStack" Background="Black" >
        <Button x:Name="button1" Content="Кнопка 1"
                Margin="{StaticResource buttonMargin}"
                FontFamily="{StaticResource buttonFont}"
                Foreground="{StaticResource buttonFontColor}"
                Background="{StaticResource buttonBackColor}" />
        <Button x:Name="button2" Content="Кнопка 2"
                Margin="{StaticResource buttonMargin}"
                FontFamily="{StaticResource buttonFont}"
                Foreground="{StaticResource buttonFontColor}"
                Background="{StaticResource buttonBackColor}"/>
    </StackPanel>

// Однако в реальности код раздувается, опть же приходится писать много повторяющейся информации. И в этом плане стили предлагают более элегантное решение:
Title="Стили" Height="250" Width="300">
    <Window.Resources>
        <Style x:Key="BlackAndWhite">
            <Setter Property="Control.FontFamily" Value="Verdana" />
            <Setter Property="Control.Background" Value="Black" />
            <Setter Property="Control.Foreground" Value="White" />
            <Setter Property="Control.Margin" Value="10" />
        </Style>
    </Window.Resources>
    <StackPanel x:Name="buttonsStack" Background="Black" >
        <Button x:Name="button1" Content="Кнопка 1"
                Style="{StaticResource BlackAndWhite}" />
        <Button x:Name="button2" Content="Кнопка 2"
                Style="{StaticResource BlackAndWhite}"/>
    </StackPanel>

// Результат будет тот же, однако теперь мы избегаем ненужного повторения. Более того теперь мы можем управлять всеми нужными нам свойствами как единым целым - одним стилем. Стиль создается как ресурс с помощью объекта Style, который представляет класс System.Windows.Style. И как любой другой ресурс, он обязательно должен иметь ключ. С помощью коллекции Setters определяется группа свойств, входящих в стиль.

/* В нее входят объекты Setter, которые имеют следующие свойства:

* Property: указывает на свойство, к которому будет применяться данный сеттер. Имеет следующий синтаксис: Property="Тип_элемента.Свойство_элемента". Выше в качестве типа элемента использовался Control, как общий для всех элементов. Поэтому данный стиль мы могли бы применить и к Button, и к TextBlock, и к другим элементам. Однако мы можем и конкретизировать элемент, например, Button:*/
<Setter Property="Button.FontFamily" Value="Arial" />

/* 
* Value: устанавливает значение
*/

// Если значение свойства представляет сложный объект, то мы можем его вынести в отдельный элемент:
<Style x:Key="BlackAndWhite">
    <Setter Property="Control.Background">
        <Setter.Value>
            <LinearGradientBrush>
                <LinearGradientBrush.GradientStops>
                    <GradientStop Color="White" Offset="0" />
                    <GradientStop Color="Black" Offset="1" />
                </LinearGradientBrush.GradientStops>
            </LinearGradientBrush>
        </Setter.Value>
    </Setter>
    <Setter Property="Control.FontFamily" Value="Verdana" />
    <Setter Property="Control.Foreground" Value="White" />
    <Setter Property="Control.Margin" Value="10" />
</Style>

//																TargetType
// Hам необязательно прописывать для всех кнопок стиль. Мы можем в самом определении стиля с помощью свойства TargetType задать тип элементов. В этом случае стиль будет автоматически применяться ко всем кнопкам в окне:
<Window.Resources>
        <Style TargetType="Button">									// тогда мы не можем использовать свойство x:Key
            <Setter Property="FontFamily" Value="Verdana" />
            <Setter Property="Background" Value="Black" />
            <Setter Property="Foreground" Value="White" />
            <Setter Property="Margin" Value="10" />
        </Style>
    </Window.Resources>
    <StackPanel x:Name="buttonsStack" Background="Black" >
        <Button x:Name="button1" Content="Кнопка 1"  />
        <Button x:Name="button2" Content="Кнопка 2" />
    </StackPanel>

// Причем в этом случае нам уже не надо указывать у стиля ключ x:Key несмотря на то, что это ресурс.
// Также если используем свойство TargetType, то в значении атрибута Property уже необязательно указывать тип, то есть Property="Control.FontFamily". И в данном случае тип можно просто опустить: 
Property="FontFamily"

// Если же необходимо, чтобы к какой-то кнопке не применялся автоматический стиль, то ее стилю присваивают значение null
<Button x:Name="button2" Content="Кнопка 2" Style="{x:Null}" />

// 										Определение обработчиков событий с помощью стилей
// Кроме коллекции Setters стиль может определить другую коллекцию - EventSetters, которая содержит объекты EventSetter. Эти объекты позволяют связать события элементов с обработчиками. Например, подключим все кнопки к одному обработчику события Click:

<Style TargetType="Button">
    <Setter Property="Button.Background" Value="Black" />
    <Setter Property="Button.Foreground" Value="White" />
    <Setter Property="Button.FontFamily" Value="Andy" />
    <EventSetter Event="Button.Click" Handler="Button_Click" />
</Style>

// Соответственно в файле кода c# у нас должен быть определен обработчик Button_Click:
private void Button_Click(object sender, RoutedEventArgs e)
{
    Button clickedButton = (Button)sender;
    MessageBox.Show(clickedButton.Content.ToString());
}

// 												Наследование стилей и свойство BasedOn
// У класса Style еще есть свойство BasedOn, с помощью которого можно наследовать и расширять существующие стили:
<Window.Resources>
    <Style x:Key="ButtonParentStyle">
        <Setter Property="Button.Background" Value="Black" />
        <Setter Property="Button.Foreground" Value="White" />
        <Setter Property="Button.FontFamily" Value="Andy" />
    </Style>
    <Style x:Key="ButtonChildStyle" BasedOn="{StaticResource ButtonParentStyle}">
        <Setter Property="Button.BorderBrush" Value="Red" />
        <Setter Property="Button.FontFamily" Value="Verdana" />
    </Style>
</Window.Resources>
<StackPanel x:Name="buttonsStack" Background="Black" >
        <Button x:Name="button1" Content="Кнопка 1" Style="{StaticResource ButtonParentStyle}" />
        <Button x:Name="button2" Content="Кнопка 2" Style="{StaticResource ButtonChildStyle}" />
    </StackPanel>

// Cвойство BasedOn в качестве значения принимает существующий стиль, определяя его как статический ресурс. В итоге он объединяет весь функционал родительского стиля со своим собственным. Если в дочернем стиле есть сеттеры для свойств, которые также используются в родительском стиле, как в данном случае сеттер для свойства Button.FontFamily, то дочерний стиль переопределяет родительский стиль.

// 																Стили в C#
// В C# стили представляют объект System.Windows.Style. Используя его, мы можем добавлять сеттеры и устанавливать стиль для нужных элементов:
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
 
namespace StylesApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
 
            Style buttonStyle = new Style();
            buttonStyle.Setters.Add(new Setter { Property = Control.FontFamilyProperty, Value = new FontFamily("Verdana") });
            buttonStyle.Setters.Add(new Setter { Property = Control.MarginProperty, Value = new Thickness(10) });
            buttonStyle.Setters.Add(new Setter { Property = Control.BackgroundProperty, Value = new SolidColorBrush(Colors.Black) });
            buttonStyle.Setters.Add(new Setter { Property = Control.ForegroundProperty, Value = new SolidColorBrush(Colors.White) });
            buttonStyle.Setters.Add(new EventSetter { Event= Button.ClickEvent, Handler= new RoutedEventHandler( Button_Click) });
 
            button1.Style = buttonStyle;
            button2.Style = buttonStyle;
        }
 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            MessageBox.Show(clickedButton.Content.ToString());
        }
    }
}

// При создании сеттера нам надо использовать свойство зависимостей, например, Property = Control.FontFamilyProperty. Причем для свойства Value у сеттера должен быть установлен объект именно того типа, которое хранится в этом свойстве зависимости. Так, свойство зависимости MarginProperty хранит объект типа Thickness, поэтому определение сеттера выглядит следующим образом:
new Setter { Property = Control.MarginProperty, Value = new Thickness(10) }