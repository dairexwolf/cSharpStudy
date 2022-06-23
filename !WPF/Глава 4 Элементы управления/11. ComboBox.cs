// ComboBox содержит коллекцию элементов и образует выпадающий список:
<ComboBox Name="phonesList" Height="30" VerticalAlignment="Top">
    <TextBlock>LG Nexus 5X</TextBlock>
    <TextBlock>Huawai Nexus 6P</TextBlock>
    <TextBlock>iPhone 6S</TextBlock>
    <TextBlock>iPhone 6S Plus</TextBlock>
    <TextBlock>Microsoft Lumia 950</TextBlock>
</ComboBox>

//																		ComboBoxItem
// В качестве элементов в ComboBox-e мы можем использовать различные компоненты, но наиболее эффективным является применение элемента ComboBoxItem. ComboBoxItem представляет элемент управления содержимым, в который через свойство Content мы можем поместить другие элементы. Например:
<ComboBox Height="50" Width="150" VerticalAlignment="Top">
    <ComboBoxItem IsSelected="True">
        <StackPanel Orientation="Horizontal">
            <Image Source="cats.jpg"  Width="60" />
            <TextBlock>cats.jpg</TextBlock>
        </StackPanel>
    </ComboBoxItem>
    <StackPanel Orientation="Horizontal">
        <Image Source="windowcat.jpg" Width="60" />
        <TextBlock>windowcat.jpg</TextBlock>
    </StackPanel>
    <StackPanel Orientation="Horizontal">
        <Image Source="234.jpg" Width="60" />
        <TextBlock>234.jpg</TextBlock>
    </StackPanel>
</ComboBox>

// Для создания первого элемента использовался элемент ComboBoxItem. Для второго и третьего такие элементы создаются неявно. Однако использование ComboBoxItem имеет преимущество, так как мы можем выделить данный элемент, установив свойство IsSelected="True", либо можем сделать недоступным с помощью установки свойства IsEnabled="False".

// 																Событие SelectionChanged
// Обрабатывая событие SelectionChanged, мы можем динамически получать выделенный элемент:
<ComboBox Height="25" Width="150" SelectionChanged="ComboBox_Selected">
        <ComboBoxItem>Пицца</ComboBoxItem>
    </ComboBox>

// В C#
private void ComboBox_Selected(object sender, RoutedEventArgs e)
{
    ComboBox comboBox = (ComboBox)sender;
    ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
    MessageBox.Show(selectedItem.Content.ToString());
}

// Правда, для элементов со сложным содержимым подобный способ может не пройти, и если мы захотим получить текст, до него придется добираться, спускаясь по дереву вложенных элементов.

// Свойства

// * Установка свойства IsEditable="True" позволяет вводить в поле списка начальные символы, а затем функция автозаполнения подставит подходящий результат. По умолчанию свойство имеет значение False.

// * Это свойство работает в комбинации со свойством IsReadOnly: оно указывает, является поле ввода доступным только для чтения. По умолчанию имеет значение False, поэтому если IsEditable="True", то мы можем вводить туда произвольный текст.

// * Еще одно свойство StaysOpenOnEdit при установке в True позволяет сделать список раскрытым на время ввода значений в поле ввода.
























