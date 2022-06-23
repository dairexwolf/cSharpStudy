// Этот элемент управления отображает информацию на множестве строк и столбцов. Он унаследован от класса ListBox, поэтому может вести себя простой список:
<ListView>
    <TextBlock>LG Nexus 5X</TextBlock>
    <TextBlock>Huawei Nexus 6P</TextBlock>
    <TextBlock>iPhone 6S</TextBlock>
    <TextBlock>iPhone 6S Plus</TextBlock>
    <TextBlock>Аsus Zenphone 2</TextBlock>
    <TextBlock>Microsoft Lumia 950</TextBlock>
</ListView>

// Но чтобы создать более сложные по структуре данные используется свойство View. Это свойство принимает в качестве значения объект GridView, который управляет отображением данных. GridView определяет коллекцию определений столбцов - GridViewColumn, которое с помощью свойства Header определяет название столбца, а с помощью свойства DisplayMemberBinding можно определить привязку столбца к определенному свойству добавляемого в ListView объекта.
<GridViewColumn DisplayMemberBinding="{Binding Path=Company}">Компания</GridViewColumn>

// Допустим у нас в проекте определен класс Phone:
public class Phone
{
    public string Title { get; set; }
    public string Company { get; set; }
    public int Price { get; set; }
}

// Создадим в xaml-коде коллекцию объектов Phone (в принципе это можно было бы сделать и в файле кода) и объявим привязку столбцов ListView к свойствам объектов Phone:
xmlns:col="clr-namespace:System.Collections;assembly=mscorlib"
        mc:Ignorable="d"
        Title="ListView" Height="220" Width="300">
    <Grid Background="Lavender">
        <ListView Name="phonesList" ItemsSource="{DynamicResource ResourceKey=phones}" >
            <ListView.View>
                <GridView>
                    <GridViewColumn DisplayMemberBinding="{Binding Path=Title}">Модель</GridViewColumn>
                    <GridViewColumn DisplayMemberBinding="{Binding Path=Company}" Width="100">Компания</GridViewColumn>
                    <GridViewColumn DisplayMemberBinding="{Binding Path=Price}">Цена</GridViewColumn>
                </GridView>
            </ListView.View>
            <ListView.Resources>
                <col:ArrayList x:Key="phones">
                    <local:Phone Title="iPhone 6S" Company="Apple" Price="54990" />
                    <local:Phone Title="Lumia 950" Company="Microsoft" Price="39990" Background="LightGrey" />
                    <local:Phone Title="Nexus 5X" Company="Google" Price="29990" />
                    <local:Phone Title = "Galaxy Edge" Company = "Samsung" Price = "45670" Background="LightGrey" />
                </col:ArrayList>
            </ListView.Resources>
        </ListView>
    </Grid>
