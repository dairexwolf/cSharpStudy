// Элемент ScrollViewer обеспечивает прокрутку содержимого. Может вмещать в себя только один элемент, поэтому все элементы, помещаемые внутрь ScrollViewer необходимо облачить в еще один контейнер. Например:
    <Grid>
        <ScrollViewer>
            <StackPanel>
                <Button MinHeight="60" Background="Red"/>
                <Button MinHeight="60" Background="Orange"/>
                <Button MinHeight="60" Background="Yellow"/>
                <Button MinHeight="60" Background="Green"/>
                <Button MinHeight="60" Background="Blue"/>
            </StackPanel>
        </ScrollViewer>
    </Grid>

// ScrollViewer поддерживает как вертикальную, так и горизонтальную прокрутку. Ее можно установить с помощью свойств HorizontalScrollBarVisibility и VerticalScrollBarVisibility. Эти свойства принимают одно из следующих значений:
/*
* Auto: наличие полос прокрутки устанавливается автоматически
* Visible: полосы прокрутки отображаются в окне приложения
* Hidden: полосы прокрутки не видно, но прокрутка возможна с помощью клавиш клавиатуры
* Disabled: полосы прокрутки не используются, а сама прокрутка даже с помощью клавиатуры невозможна
*/

// Среди свойств нужно отметить еще CanContentScroll. Если оно установлено в True, то прокрутка осуществляется не на несколько пикселей, а к началу следующего элемента.

/*
Кроме того, прокрутку можно организовать программным способом - с помощью следующих методов элемента ScrollViewer:

* LineUp(), LineDown(), LineRight(), LineLeft(): прокрутка соответственно вверх, вниз, вправо, влево.
* ScrollToEnd(), ScrollToHome(): прокрутка в конец окна и в начало.
* ScrollToRightEnd(), ScrollToLeftEnd(): прокрутка в правый и левый конец окна.
*/

// В качестве примера обернем несколько элементов RadioButton в элемент ScrollViewer:
<StackPanel>
        <ScrollViewer Name="scroll" CanContentScroll="True" Height="150">
            <GroupBox Header="Смартфон 2015" Padding="5">
                <StackPanel>
                    <RadioButton GroupName="Phones" Margin="4">iPhone 6S</RadioButton>
                    <RadioButton GroupName="Phones" Margin="4">iPhone 6S Plus</RadioButton>
                    <RadioButton GroupName="Phones" Margin="4">Lumia 550</RadioButton>
                    <RadioButton GroupName="Phones" Margin="4">Lumia 950</RadioButton>
                    <RadioButton GroupName="Phones" Margin="4">Nexus 5X</RadioButton>
                    <RadioButton GroupName="Phones" Margin="4">Nexus 6P</RadioButton>
                    <RadioButton GroupName="Phones" Margin="4">Galaxy S6 Edge</RadioButton>
                </StackPanel>
            </GroupBox>
        </ScrollViewer>
        <Grid>
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*" />
                <ColumnDefinition Width="*" />
            </Grid.ColumnDefinitions>
            <Button Content="Up" Grid.Column="0" Margin="4" Click="Up_Click" />
            <Button Content="Down" Grid.Column="1" Margin="4" Click="Down_Click" />
        </Grid>
    </StackPanel>

// А в файле кода C# пропишем обработчики кнопок, которые будут выполнять программно прокрутку:

private void Up_Click(object sender, RoutedEventArgs e)
        {
            scroll.LineUp();
        }
 
        private void Down_Click(object sender, RoutedEventArgs e)
        {
            scroll.LineDown();
        }