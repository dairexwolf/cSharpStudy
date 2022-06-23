// Элементы управления содержимым (content controls) представляют такие элементы управления, которые содержат в себе другой элемент. Все элементы управления содержимым наследуются от класса ContentControl, который в свою очередь наследуется от класса System.Window.Controls.Control.

// К элементам управления содержимым относятся такие элементы как Button, Label, ToggleButton, ToolTip, RadioButton, CheckBox, GroupBox, TabItem, Expander, ScrollViewer. Также элементом управления содержимым является и главный элемент окна - Window.

// Отличительной чертой всех этих элементов является наличие свойства Content, которое и устанавливает вложенный элемент. В этом элементы управления содержимым схожи с контейнерами компоновки. Только контейнеры могут иметь множество вложенных элементов, а элементы управления содержимым только один.

/*
Свойство Content может представлять любой объект, который может относиться к одному из двух типов:

* Объект класса, не наследующего от UIElement. Для такого объекта вызывается метод ToString(), который возвращает строковое преставление объекта. Затем эта строка устанавливается в качестве содержимого.
* Объект класса, наследующего от UIElement. Для такого объекта вызывается метод UIElement.OnRender(), который выполняет отрисовку внутри элемента управления содержимым.
*/
// Рассмотрим на примере кнопки, которая является элементом управления содержимым:

<Button Content="Hello World!" />

// В качестве содержимого устанавливается обычная строка. Этот же пример мы можем в XAML прописать иначе:
<Button>
    <Button.Content>
        Hello World!
    </Button.Content>
</Button>

// Либо мы можем использовать сокращенное неявное определения свойства Content:
<Button>
    Hello World!
</Button>

//     Возьмем другой пример. Определим кнопку с именем button1:
<StackPanel>
        <Button x:Name="button1" />
    </StackPanel>

// А в файле коде MainWindow.xaml.cs присвоим ее свойству Content какой-либо объект:
public MainWindow()
        {
            InitializeComponent();
            double d = 5.6;
            button1.Content = d;
        }

// В итоге число конвертируется в строку и устанавливается в качесте содержимого.

// Иначе все будет работать, если мы в качестве содержимого используем объект, унаследованный от UIElement:
<Button x:Name="button1">
    <Button Content="Hello" />
</Button>

// Теперь в качестве содержимого будет использоваться другая кнопка, для которой при визуализации будет вызываться метод OnRender()
// Для создания той же кнопки через код C# мы бы могли прописать следующее выражение:
button1.Content = new Button { Content = "Hello" };

// В отличие от контейнеров компоновки для элементов управления содержимым мы можем задать только один вложенный элемент. Если же нам надо вложить в элемент управления содержимым несколько элементов, то мы можем использовать те же контейнеры компоновки:

<Button x:Name="mainButton">
        <StackPanel>
            <Button Background="White" Content="White" Height="50"/>
            <Button Background="Blue" Content="Blue" Height="50"/>
            <Button Background="Red" Content="Red" Height="50" Width="150"/>
            <Button x:Name="button1"/>
        </StackPanel>
    </Button>

// То же самое мы могли бы прописать через код C#:
StackPanel stackPanel = new StackPanel();
stackPanel.Children.Add(new TextBlock { Text = "Набор кнопок" });
stackPanel.Children.Add(new Button { Content = "Red", Height = 20, Background = new SolidColorBrush(Colors.Red) });
stackPanel.Children.Add(new Button { Content = "Yellow", Height = 20, Background = new SolidColorBrush(Colors.Yellow) });
stackPanel.Children.Add(new Button { Content = "Green", Height = 20, Background = new SolidColorBrush(Colors.Green) });
mainButton.Content = stackPanel;

// Content Alignment
// Выравнивание содержимого внутри элемента задается свойствами HorizontalContentAlignment (выравнивание по горизонтали) и VerticalContentAlignment (выравнивание по вертикали), аналогичны свойствам VerticalAlignment/HorizontalAlignment. Свойство HorizontalContentAlignment принимает значения Left, Right, Center (положение по центру), Stretch (растяжение по всей ширине). Например:

    <StackPanel>
        <Button Margin="5" HorizontalContentAlignment="Left" Content="Left" Height="45" Width="500" />
        <Button Margin="5" HorizontalContentAlignment="Right" Content="Right" Height="45" Width="500" />
        <Button Margin="5" HorizontalContentAlignment="Center" Content="Center" Height="45" Width="500" />
        <Button Margin="5" VerticalContentAlignment="Stretch" Content="Stretch" Height="50" Width="500" />
        <Button Margin="5" HorizontalContentAlignment="Right" VerticalContentAlignment="Bottom" Content="RightBottom" Height="30" Width="150" />
        <Button Margin="5" HorizontalContentAlignment="Left" VerticalContentAlignment="Top" Content="RightBottom" Height="30" Width="150" />
    </StackPanel>
	
// VerticalContentAlignment принимает значения Top (положение в верху), Bottom (положение внизу), Center (положение по центру), Stretch (растяжение по всей высоте)

// Padding
// С помощью свойства Padding мы можем установить отступ содержимого элемента:
    <StackPanel>
        <Button x:Name="button1" Padding="50 30 0 40" HorizontalContentAlignment="Left">
            Hello World
        </Button>
        <Button x:Name="button2" Padding="60 20 0 30" HorizontalContentAlignment="Center">
            Hello World
        </Button>
		<Button x:Name="button3" Padding="20"  Content="Hello World" />
    </StackPanel>
	
// Свойство Padding задается в формате Padding="отступ_слева отступ_сверху отступ_справа отступ_снизу".
// Если со всех четырех сторон предполагается один и тот же отступ, то, как и в случае с Margin, мы можем задать одно число:

// Важно понимать, от какой точки задается отступ. В случае с первой кнопкой в ней контект выравнивается по левому краю, поэтому отступ слева будет предполагать отступ от левого края элемента Button. А вторая кнопка располагается по центру. Поэтому для нее отступ слева предполагает отступ от той точки, в которой содержимое бы находилось при центрировании без применения Padding.

// Комбинация значений свойств HorizontalContentAlignment/VerticalContentAlignment и Padding позволяет оптимальным образом задать расположение содержимого.
