// Особая группа элементов управления образована от класса HeaderedContentControl, который является подклассом ContentControl. Эта группа отличается тем, что позволяет задать заголовок содержимому. В эту группу элементов входят GroupBox и Expander.

// GroupBox
// Элемент GroupBox организует наборы элементов управления в отдельные группы. При этом мы можем определить у группы заголовок:
<GroupBox Header="Выбрать блюдо" Padding="5">
    <StackPanel>
        <RadioButton IsChecked="True" Margin="3">Салат Оливье</RadioButton>
        <RadioButton Margin="3">Котлеты по-киевски</RadioButton>
        <RadioButton Margin="3">Селедка под шубой</RadioButton>
        <Button Width="80" Margin="3">Заказать</Button>
    </StackPanel>
</GroupBox>

// GroupBox включает группу различных элементов, однако, как и всякий элемент управления содержимым, он принимает внутри себя только один контейнер, поэтому сначала мы вкладываем в GroupBox общий контейнер, а в него уже все остальные элементы.

// Однако заголовок GroupBox необязательно представляет простой текст. Мы можем пойти дальше и изменить предыдущий пример, засунув кнопку заказа прямо в заголовок:
<GroupBox Padding="5">
    <GroupBox.Header>
        <Button Background="Lavender">Выбрать блюдо</Button>
    </GroupBox.Header>
    <StackPanel>
        <RadioButton IsChecked="True" Margin="3">Салат Оливье</RadioButton>
        <RadioButton Margin="3">Котлеты по-киевски</RadioButton>
        <RadioButton Margin="3">Селедка под шубой</RadioButton>
    </StackPanel>
</GroupBox>

// Осталось добавить обработчик нажатия кнопки Click для обработки заказа и можно заказывать блюда.

// Expander
// Представляет скрытое содержимое, раскрывающееся по нажатию мышкой на указатель в виде стрелки. Причем содержимое опять же может быть самым разным: кнопки, текст, картинки и т.д. С помощью свойства IsExpanded можно задать раскрытие узла при старте приложения. По умолчанию узел скрыт. Пример использования:
<StackPanel>
    <Expander Header="Некрасов"  IsExpanded="true">
        <TextBlock>Однажды в студеную зимнюю пору...</TextBlock>
    </Expander>
    <Expander Header="Пушкин">
        <TextBlock>Онегин был, по мнению многих, ученый малый, но ...</TextBlock>
    </Expander>
    <Expander Header="Опрос">
        <StackPanel>
            <TextBlock>Отметьте, что вам больше нравится</TextBlock>
            <CheckBox>WinForms</CheckBox>
            <CheckBox>WPF</CheckBox>
            <CheckBox>ASP.NET</CheckBox>
        </StackPanel>
    </Expander>
</StackPanel>

// Опять же мы можем изменить заголовок, вложив в него, например, кнопку или изображение:
<Expander>
    <Expander.Header>
        <Button Background="Lavender">Опрос</Button>
    </Expander.Header>
    <StackPanel>
        <TextBlock>Выберите технологию</TextBlock>
        <CheckBox>WinForms</CheckBox>
        <CheckBox>WPF</CheckBox>
        <CheckBox>ASP.NET</CheckBox>
    </StackPanel>
</Expander>

// Если мы хотим обработать открытие экспандера, то нам надо обработать событие Expanded (а при обработке закрытия - событие Collapsed). Данные события вызываются до самого действия, поэтому мы можем перед открытием, например, динамически устанавливать содержание экспандера:
// XAML
<Expander Expanded="Expander_Expanded" Collapsed="Expander_Collapsed" />

//C#
private void Expander_Expanded(object sender, RoutedEventArgs e)
{
    ((Expander)sender).Content = new Button() { Width = 80, Height = 30, Content = "Привет" }; // вот это интересно. Sender - это объект, который запрашивает или вызывает это событие.
}
 
private void Expander_Collapsed(object sender, RoutedEventArgs e)
{
    MessageBox.Show("Экспандер свернут");
}

// В итоге при раскрытии элемента вместо начального содержимого там будет определенная в коде кнопка. Новая кнопка будет создаваться всякий раз при открытии экспандера.

// Программное создание Expandera:
StackPanel expanderPanel = new StackPanel();
expanderPanel.Children.Add(new CheckBox { Content = "WinForms" });
expanderPanel.Children.Add(new CheckBox { Content = "WPF" });
expanderPanel.Children.Add(new CheckBox { Content = "ASP.NET" });
 
Expander expander = new Expander();
expander.Header = "Выберите технологию";
expander.Content = expanderPanel;
 
stackPanel.Children.Add(expander);

// А можно сделать так, чтобы когда Expander разворачивается, его контент был над родительским элементом. То есть высота родительского элемента не изменялась.
<Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="*"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>
        <StackPanel Grid.Row="0" VerticalAlignment="Bottom">
            <Expander Header="Example" ExpandDirection="Up" Margin="5" Padding="5">
                <StackPanel>
                    <CheckBox Content="I realized" Margin="20,5"/>
                </StackPanel>
            </Expander>
        </StackPanel>
        </Grid>