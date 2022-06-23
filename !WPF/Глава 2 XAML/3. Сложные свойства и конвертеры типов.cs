// В предыдущих темах было рассмотрено создание элементов в XAML. Например, мы могли бы определить кнопку следующим образом:

<Button x:Name="myButton" Width="120" Height="40" // Height и Width являются простыми свойствами.
Content="Кнопка" HorizontalAlignment="Center" Background="Red" // являются более сложными по своей структуре
/>

// Так, если мы будем определять эту же кнопку в коде c#, то нам надо использовать следующий набор инструкций:

Button myButton = new Button();
myButton.Content = "Кнопка";
myButton.Width = 120;
myButton.Height = 40;
myButton.HorizontalAlignment = HorizontalAlignment.Center;
myButton.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);

// Чтобы выровнять кнопку по центру, применяется перечисление HorizontalAlignment, а для установки фонового цвета - класс SolidColorBrush. Хотя в коде XAML мы ничего такого не увидели и устанавливали эти свойства гораздо проще с помощью строк: Background="Red". Дело в том, что по отношению к коду XAML применяются специальные объекты - type converter или конвертеры типов, которые могут преобразовать значения из XAML к тем типам тех объектов, которые используются в коде C#.

// В WPF имеются встроенные конвертеры для большинства типов данных: Brush, Color, FontWeight и т.д. Все конвертеры типов явлются производными от класса TypeConverter. Например, конкретно для преобразования значения Background="Red" в объект SolidColorBrush используется производный класс BrushConverter. При необходимости можно создать свои конвертеры для каких-то собственных типов данных.

// Фактически установка значения в XAML Background="Red" сводилась бы к следующему вызову в коде c#:
myButton.Background = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString("Red");

// В данном случае программа пытается получить конвертер для типа Brush (базового класса для SolidColorBrush) и затем преобразовать строку "Red" в конкретный цвет. 
// Для получения нужного конвертера, программа обращается к метаданным класса Brush. В частности, он имеет следующий атрибут:
[TypeConverter(typeof(BrushConverter))]
public abstract class Brush

// В то же время мы можем более явно использовать эти объекты в коде XAML:

<Button x:Name="myButton" Width="120" Height="40" Content="Кнопка">
    <Button.HorizontalAlignment>
        <HorizontalAlignment>Center</HorizontalAlignment>
    </Button.HorizontalAlignment>
  
    <Button.Background>
        <SolidColorBrush Opacity="0.5" Color="Red" />
    </Button.Background>
</Button>
