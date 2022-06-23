// Для создания вкладок в WPF, как и в WinForms, предназначен элемент TabControl, а отдельная вкладка представлена элементом TabItem:
<TabControl>
    <TabItem Header="Вкладка 1">Первая вкладка</TabItem>
    <TabItem Header="Вкладка 2">Вторая вкладка</TabItem>
</TabControl>

// Элемент TabItem является элементом управления содержимым, поэтому в него можно вложить другие элементы:
<TabControl x:Name="products">
        <TabItem x:Name="smartphonesTab">
            <TabItem.Header>
                <StackPanel Orientation="Horizontal">
                    <Ellipse Height="10" Width="10" Fill="Black" />
                    <TextBlock Margin="3">Смартфоны</TextBlock>
                </StackPanel>
            </TabItem.Header>
            <TabItem.Content>
                <StackPanel>
                    <RadioButton IsChecked="True">iPhone S6</RadioButton>
                    <RadioButton>LG G 4</RadioButton>
                    <RadioButton>Lumia 550</RadioButton>
                </StackPanel>
            </TabItem.Content>
        </TabItem>
        <TabItem x:Name="tabletsTab">
            <TabItem.Header>
                <StackPanel Orientation="Horizontal">
                    <Rectangle Height="10" Width="10" Fill="Black" />
                    <TextBlock Margin="3">Планшеты</TextBlock>
                </StackPanel>
            </TabItem.Header>
            <TabItem.Content>
                <StackPanel>
                    <RadioButton IsChecked="True">iPad</RadioButton>
                    <RadioButton>Штука 2</RadioButton>
                    <RadioButton>Штука 3</RadioButton>
                </StackPanel>
            </TabItem.Content>
        </TabItem>
        
    </TabControl>

// Класс TabItem наследуется от класса HeaderedContentControl, поэтому кроме свойства Content, определедяющее содержимое вкладки, имеет также свойство Header, которое определяет заголовок. И в этот заголовок мы можем вложить различное содержимое, как в примере выше.
// И также, как и в случае с ListBoxItem и ComboBoxItem, мы можем вложить в TabControl и другие элементы, которые неявно образуют отдельные вкладки:
<TabControl>
    <TextBlock>Первая вкладка</TextBlock>
    <TextBlock>Вторая вкладка</TextBlock>
    <TextBlock>Третья вкладка</TextBlock>
</TabControl>

// Программное добавление вкладок
// Допустим, у нас на форме есть TabControl:
<TabControl x:Name="products">
</TabControl>

// Через код C# добавим в него вкладку:
// формируем содержимое вкладки в виде списка
ListBox notesList = new ListBox();
notesList.Items.Add("Macbook Pro");
notesList.Items.Add("HP Pavilion 5478");
notesList.Items.Add("Acer LK-08");
// добавление вкладки
products.Items.Add(new TabItem
{
    Header = new TextBlock { Text = "Ноутбуки" }, // установка заголовка вкладки
    Content = notesList // установка содержимого вкладки
});




































