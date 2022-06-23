// Представляет собой обычный список. Содержит коллекцию элементов ListBoxItem, которые являются типичными элементами управления содержимым. Также ListBox может содержать любые другие элементы, например:
<ListBox Name="phonesList">
    <TextBlock FontWeight="Bold" TextDecorations="Underline" Text="Новинки 2015 года" />
    <ListBoxItem Background="LightGray">LG Nexus 5X</ListBoxItem>
    <ListBoxItem>Huawei Nexus 6P</ListBoxItem>
    <ListBoxItem Background="LightGray">iPhone 6S</ListBoxItem>
    <ListBoxItem>iPhone 6S Plus</ListBoxItem>
    <ListBoxItem Background="LightGray">Аsus Zenphone 2</ListBoxItem>
    <ListBoxItem>Microsoft Lumia 950</ListBoxItem>
</ListBox>

// Все эти элементы будут находиться в коллекции phonesList.Items и, таким образом, по счетчику можно к ним обращаться, например, phonesList.Items[0] - первый элемент ListBox, который в данном случае представляет TextBlock. Также мы можем установить элемент: 
phonesList.Items[2]="LG G 4";

// Компонент ListBoxItem представляет элемент управления содержимым, поэтому также мы можем задавать через его свойство Content более сложные композиции элементов, например:
<ListBox Name="Photos" Background="Lavender">
    <ListBoxItem Margin="3">
        <StackPanel Orientation="Horizontal">
            <Image Source="cats.jpg" Width="60" />
            <TextBlock>cats.jpg</TextBlock>
        </StackPanel>
    </ListBoxItem>
    <StackPanel Orientation="Horizontal">
        <Image Source="windowcat.jpg" Width="60" />
        <TextBlock>windowcat.jpg</TextBlock>
    </StackPanel>
    <StackPanel Orientation="Horizontal">
        <Image Source="234.jpg" Width="60" />
        <TextBlock>234.jpg</TextBlock>
    </StackPanel>
</ListBox>

// Мы можем использовать элементы как внутри элемента ListBoxItem, так и непосредственно вставляя их в список. Однако на следующем примере видно, что использование ListBoxItem имеет небольшое преимущество, так как мы можем задать некоторые дополнительные свойства, например, отступы.

// Выделение элементов
// ListBox поддерживает множественный выбор. Для этого нужно установить свойство SelectionMode="Multiple" или SelectionMode="Extended". В последнем случае, чтобы выделить несколько элементов, необходимо держать нажатой клавишу Ctrl или Shift. По умолчанию SelectionMode="Single", то есть допускается только единственное выделение.
