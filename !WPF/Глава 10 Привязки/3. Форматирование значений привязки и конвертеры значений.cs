// 															Форматирование значений
// Привязка представляет очень простой механизм, однако иногда этому механизму требуется некоторая кастомизация. Так, нам может потребоваться небольшое форматирование значение. Для примера возьмем класс Phone из прошлых тем:
class Phone
{
public string Title { get; set; }
public string Company { get; set; }
public int Price { get; set; }
}

// Допустим, нам надо в текстовый блок вывести не только цену, но и еще какой-нибудь текст:
<Window.Resources>
        <local:Phone x:Key="nexusPhone" Title="Nexus X5" Company="Google" Price="25000" />
    </Window.Resources>
    <Grid>
        <TextBlock Text="{Binding StringFormat=Итоговая цена {0} рублей, Source={StaticResource nexusPhone}, Path=Price}" />
    </Grid>

// Свойство StringFormat получает набор параметров в фигурных скобках. Фигурные скобки ({0}) передают собственно то значение, к которому идет привязка. Можно сказать, что действие свойства StringFormat аналогично методу String.Format(), который выполняет форматирование строк.
// При необходимости мы можем использовать дополнительные опции форматирования, например, {0:C} для вывода валюты, {0:P} для вывода процентов и т.д.:
<TextBlock Text="{Binding StringFormat={}{0:C}, Source={StaticResource nexusPhone}, Path=Price}" />

// При этом если у нас значение в StringFormat начинается с фигурных скобок, например, "{0:C}", то перед ними ставятся еще пара фигурных скобок, как в данном случае. По сути они ничего важно не несут, просто служат для корректной интерпретации строки.
// Либо в этом случае нам надо экранировать скобки слешами:
<TextBlock Text="{Binding StringFormat=Итоговая цена \{0:C\}, Source={StaticResource nexusPhone}, Path=Price}" />

/* В зависимости от типа элемента доступны различные типы форматировщиков значений:

* StringFormat: используется для класса Binding

* ContentStringFormat: используется для классов ContentControl, ContentPresenter, TabControl

* ItemStringFormat: используется для класса ItemsControl

* HeaderStringFormat: используется для класса HeaderContentControl

* ColumnHeaderStringFormat: используется для классов GridView, GridViewHeaderRowPresenter

* SelectionBoxItemStringFormat: используется для классов ComboBox, RibbonComboBox

*/
// Их применение аналогично. Например, так как Button представляет ContentControl, то для этого элемента надо использовать ContentStringFormat:
<Button ContentStringFormat="{}{0:C}"
    Content="{Binding Source={StaticResource nexusPhone}, Path=Price}" />

// 																		Конвертеры значений
// Конвертеры значений (value converter) также позволяют преобразовать значение из источника привязки к типу, который понятен приемнику привязки. Так как не всегда два связываемых привязкой свойства могут иметь совместимые типы. И в этом случае как раз и нужен конвертер значений.

// Допустим, нам надо вывести дату в определенном формате. Для этой задачи создадим в проекте класс конвертера значений:
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
 
public class DateTimeToDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((DateTime)value).ToString("dd.MM.yyyy");
    }
     
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }  
}

// Конвертер значений должен реализовать интерфейс System.Windows.Data.IValueConverter. Этот интерфейс определяет два метода: Convert(), который преобразует пришедшее от привязки значение в тот тип, который понимается приемником привязки, и ConvertBack(), который выполняет противоположную операцию.
/* Оба метода принимают четыре параметра:

object value: значение, которое надо преобразовать

Type targetType: тип, к которому надо преобразовать значение value

object parameter: вспомогательный параметр

CultureInfo culture: текущая культура приложения

*/

// В данном случае метод Convert возвращает строковое представление даты в формате "dd.MM.yyyy". То есть мы ожидаем, что в качесве параметра value будет передаваться объект DateTime.

// Метод ConvertBack в данном случае не имеет значения, поэтому он просто возвращает пустое значение для свойста. В другоим случае мы бы здесь получали строковое значение и преобразовывали его в DateTime.

// Теперь применим этот конвертер в xaml:
<Window.Resources>
        <sys:DateTime x:Key="myDate">2/12/2016</sys:DateTime>
        <local:DateTimeToDateConverter x:Key="myDateConverter" />
    </Window.Resources>
    <StackPanel>
        <TextBlock Text="{Binding Source={StaticResource myDate},Converter={StaticResource myDateConverter}}" />
        <TextBlock Text="{Binding Source={StaticResource myDate}}" />
    </StackPanel>

// десь искомая дата, которая выводится в текстовые блоки, задана в ресурсах. Также в ресурсах задан конвертер значений. Чтобы применить этот конвертер в конструкции привязки используется параметр Converter с указанием на ресурс: Converter={StaticResource myDateConverter}

// Для сравнения я здесь определил два текстовых блока. Но поскольку к одному из них применяется конвертер, то отображение даты будет отличаться.

// Немного изменим код конвертера и используем передаваемый параметр:
public class DateTimeToDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(parameter!=null && parameter.ToString()=="EN")
            return ((DateTime)value).ToString("MM-dd-yyyy");
         
        return ((DateTime)value).ToString("dd.MM.yyyy");
    }
         
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}

// В качестве параметра может передаваться любой объект. Если параметр в xaml не используется, то передается null. В данном случае мы проверяем, равен ли параметр строке "EN", то есть мы ожидаем, что параметр будет передавать строковое значение. И если равен, то возвращаем дату немного в другом формате.
// Для применения параметра изменим код xaml:
<StackPanel>
    <TextBlock Text="{Binding Source={StaticResource myDate},Converter={StaticResource myDateConverter}}" />
    <TextBlock Text="{Binding Source={StaticResource myDate}, ConverterParameter=EN, Converter={StaticResource myDateConverter}}" />
    <TextBlock Text="{Binding Source={StaticResource myDate}}" />
</StackPanel>

// Параметр привязки задается с помощью свойства ConverterParameter. Итак, у нас тут три текстовых блока, и применя конвертер, мы получим три разных отображения даты
