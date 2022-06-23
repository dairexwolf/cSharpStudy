// Прикрепляемые свойства (attached properties) также являются свойствами зависимостей с той разницей, что они определяются в одном классе, а применяются в другом. Например, при установке столбца или строки грида, в которых размещается элемент управления, используются свойства Grid.Row и Grig.Column, которые как раз и представляют прикрепляемые свойства. То есть эти свойства определены в классе Grid, но используются в других вложенных элементах:
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition />
        <RowDefinition />
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition />
        <ColumnDefinition />
    </Grid.ColumnDefinitions>
    <Button x:Name="button1" Content="Hello" Grid.Column="1" Grid.Row="0" />
</Grid>

// В коде C# установка значения для прикрепленных свойств производится с помощью статических методов в формате Класс.Set[Свойство](). Например, установим для выше определенной кнопки столбец и строку грида:
Grid.SetRow(button1, 1); // вторая строка
Grid.SetColumn(button1, 1); // второй столбец

// С помощью методов типа Класс.GetСвойство() мы можем получать в коде значения прикрепляемых свойств:	
int column = Grid.GetColumn(button1); 	// получаем номер столбца
button1.Content = column;				// Ставим значение номера столбца

// Если более детально взглянуть на прикрепляемые свойства, то можно увидеть, что их определение немного отличается от стандартных свойств зависимостей. Для регистрации прикрепляемого свойства применяется метод RegisterAttached():
Grid.ColumnProperty = DependencyProperty.RegisterAttached(
                        "Column", 
                        typeof(int), 
                        typeof(Grid), 
                        new FrameworkPropertyMetadata(0, 
                        new PropertyChangedCallback(Grid.OnCellAttachedPropertyChanged)), 
                        new ValidateValueCallback(Grid.IsIntValueNotNegative));

// Метод RegisterAttached() принимает в прицнипе те же параметры, что и метод Register().
// Другое отличие от обычных свойств зависимостей состоит в том, что для прикрепляемых свойств не создается обертка в виде стандартного свойства C#. Вместо этого используется пара статических методов SetСвойство() GetСвойство():
public static int GetColumn(UIElement element)
{
    if (element == null)
    {
        throw new ArgumentNullException(...);
    }
    return (int)element.GetValue(Grid.ColumnProperty);
}
public static void SetColumn(UIElement element, int value)
{
    if (element == null)
    {
        throw new ArgumentNullException();
    }
    element.SetValue(Grid.ColumnProperty, value);
}

// Было бы полезным добавить, что присоединённые свойства могут быть определены в классе который не наследует от DependencyObject.















