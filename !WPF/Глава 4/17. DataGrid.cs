// DataGrid во многом похож на ListView, но более сложный по характеру и допускает редактирование содержимого таблицы.

// В разделе о ListView мы создали класс Phone, объекты которого выводили в список:
public class Phone
{
    public string Title { get; set; }
    public string Company { get; set; }
    public int Price { get; set; }
}

// Теперь же выведем объекты в таблицу DataGrid. Чтобы DataGrid автоматически разбивал таблицу на столбцы, установим свойство AutoGenerateColumns="True":
  Title="DataGrid" Height="220" Width="300">
    <Grid Background="Lavender">
        <DataGrid x:Name="phonesGrid" AutoGenerateColumns="True" ItemsSource="{DynamicResource ResourceKey=phones}">
            <DataGrid.Resources>
                <col:ArrayList x:Key="phones">
                    <local:Phone Title="iPhone 6S" Company="Apple" Price="54990" />
                    <local:Phone Title="Lumia 950" Company="Microsoft" Price="39990" />
                    <local:Phone Title="Nexus 5X" Company="Google" Price="29990" />
                </col:ArrayList>
            </DataGrid.Resources>
        </DataGrid>
    </Grid>

// В данном случае префикс local ссылается на пространство имен текущего проекта, в котором определен класс Phone (xmlns:local="clr-namespace:Controls"), а col - префикс-ссылка на пространство имен System.Collections (xmlns:col="clr-namespace:System.Collections;assembly=mscorlib").

// Программная установка источника для DataGrid:
List<Phone> phonesList = new List<Phone>
{
    new Phone { Title="iPhone 6S", Company="Apple", Price=54990 },
    new Phone {Title="Lumia 950", Company="Microsoft", Price=39990 },
    new Phone {Title="Nexus 5X", Company="Google", Price=29990 }
};
phonesGrid.ItemsSource = phonesList;

/*
Некоторые полезные свойства DataGrid

*  RowBackground и AlternatingRowBackground - Устанавливают фон строки. Если установлены оба свойства, цветовой фон чередуется: RowBackground - для нечетных строк и AlternatingRowBackground - для четных

*  ColumnHeaderHeight - Устанавливает высоту строки названий столбцов.

*  ColumnWidth - Устанавливает ширину столбцов.

*  RowHeight - Устанавливает высоту строк.

* GridLinesVisibility - Устанавливает видимость линий, разделяющих столбцы и строки. Имеет четыре значения - All - видны все линии, Horizontal - видны только горизонтальные линии, Vertical - видны только вертикальные линии, None - линии отсутствуют

*  HeadersVisibility - Задает видимость заголовков

*  HorizontalGridLinesBrush и VerticalGridLinesBrush - Задает цвет горизонтальных и вертикальных линий соответственно
*/ 

// Хотя предыдущий пример довольно прост, в нем есть несколько недочетов. Во-первых, у нас нет возможности повлиять на расстановку столбцов. Во-вторых, заголовки определены по названиям свойств, которые на английском языке, а хотелось бы на русском. В этом случае мы должны определить свойства отображения столбцов сами. Для этого надо воспользоваться свойством DataGrid.Columns и определить коллекцию столбцов для отображения в таблице. Причем можно задать также и другой тип столбца, отличный от текстового. DataGrid поддерживает следующие варианты столбцов:

/*
* DataGridTextColumn - Отображает элемент TextBlock или TextBox при редактировании

* DataGridHyperlinkColumn - Представляет гиперссылку и позволяет переходить по указанному адресу

* DataGridCheckBoxColumn - Отображает элемент CheckBox

* DataGridComboBoxColumn - Отображает выпадающий список - элемент ComboBox

* DataGridTemplateColumn - Позволяет задать специфичный шаблон для отображения столбца

*/

// Перепишем предыдущий пример с учетом новой информации:

<DataGrid x:Name="phonesGrid" AutoGenerateColumns="False" HorizontalGridLinesBrush="DarkGray"
    RowBackground="LightGray" AlternatingRowBackground="White">
            
    <DataGrid.Items>
        <local:Phone Title="iPhone 6S" Company="Apple" Price="54990" />
        <local:Phone Title="Lumia 950" Company="Microsoft" Price="39990" />
        <local:Phone Title="Nexus 5X" Company="Google" Price="29990" />
    </DataGrid.Items>
    <DataGrid.Columns>
        <DataGridTextColumn Header="Модель" Binding="{Binding Path=Title}" Width="90" />
        <DataGridHyperlinkColumn Header="Компания" Binding="{Binding Path=Company}" Width="80" />
        <DataGridTextColumn Header="Цена" Binding="{Binding Path=Price}" Width="50" />
    </DataGrid.Columns>
</DataGrid>

// Среди свойств DataGrid одним из самых интересных является RowDetailsTemplate. Оно позволяет задать шаблон отображения дополнительной информации касательно данной строки. Измени элемент DataGrid:
<DataGrid x:Name="phonesGrid" AutoGenerateColumns="False" HorizontalGridLinesBrush="DarkGray"
    RowBackground="LightGray" AlternatingRowBackground="White">
            
    <DataGrid.Items>
        <local:Phone Title="iPhone 6S" Company="Apple" Price="54990" />
        <local:Phone Title="Lumia 950" Company="Microsoft" Price="39990" />
        <local:Phone Title="Nexus 5X" Company="Google" Price="29990" />
    </DataGrid.Items>
    <DataGrid.Columns>
        <DataGridTextColumn Header="Модель" Binding="{Binding Path=Title}" Width="90" />
        <DataGridHyperlinkColumn Header="Компания" Binding="{Binding Path=Company}" Width="80" />
        <DataGridTextColumn Header="Цена" Binding="{Binding Path=Price}" Width="50" />
    </DataGrid.Columns>
     
    <DataGrid.RowDetailsTemplate>
        <DataTemplate>
            <StackPanel Orientation="Horizontal">
                <TextBlock Text="{Binding Path=Price}" />
                <TextBlock Text=" рублей по скидке" />
            </StackPanel>
        </DataTemplate>
    </DataGrid.RowDetailsTemplate>
     
</DataGrid>





