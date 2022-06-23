// В WPF кнопки представлены целым рядом классов, которые наследуются от базового класса ButtonBase
/*
* Button
* RepeatButton
* CheckBox
* RadioButton
*/

// Button
// Элемент Button представляет обычную кнопку:
<Button x:Name="button1" Width="60" Height="30" Background="LightGray" />

// От класса ButtonBase кнопка наследует ряд событий, например, Click, которые позволяют обрабатывать пользовательский ввод.

// Чтобы связать кнопку с обработчиком события нажатия, нам надо определить в самой кнопке атрибут Click. А значением этого атрибута будет название обработчика в коде C#. А затем в самом коде C# определить этот обработчик.
<Button x:Name="button1" Width="60" Height="30" Content="Нажать" Click="Button_Click" />

// И обработчик в коде C#:
private void Button_Click(object sender, RoutedEventArgs e)
{
    MessageBox.Show("Кнопка нажата");
}

// Либо можно не задавать обработчик через атрибут, а стандартным образом для C# прописать в коде: button1.Click+=Button_Click;

// К унаследованным свойствам кнопка имеет такие свойства как IsDefault и IsCancel, которые принимают значения true и false. 
// Если свойство IsDefault установлено в true, то при нажатии клавиши Enter будет вызываться обработчик нажатия этой кнопки. 
// Аналогично если свойство IsCancel будет установлено в true, то при нажатии на клавишу Esc будет вызываться обработчик нажатия этой кнопки.

// Например, определим код xaml:
    <StackPanel>
        <Button x:Name="acceptButton" Content="ОК" IsDefault="True" Click="acceptButton_Click" />
        <Button x:Name="escButton" Content="Выход" IsCancel="True" Click="escButton_Click" />
    </StackPanel>
	
// А в коде MainWindow.xaml.cs определим следующий код C#:
private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Действие выполнено");
        }
 
        private void escButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // закрытие окна
        }

// RepeatButton
// Отличительная особенность элемента RepeatButton - непрерывная генерация события Click, пока нажата кнопка. Интервал генерации события корректируется свойствами Delay и Interval.
// XAML
<stackpanel>
<repeatbutton x:name="acceptButton" delay="4" height="53" interval="155" content="ОК" click="acceptButton_Click"/>
</stackpanel>

// C#
private void acceptButton_Click(object sender, RoutedEventArgs e)
{
acceptButton.Content += "L";
}
// Сам по себе элемент RepeatButton редко используется, однако он может служить основой для создания ползунка в элементах ScrollBar и ScrollViewer, в которых нажатие на ползунок инициирует постоянную прокрутку.


// ToggleButton
// Представляет элементарный переключатель. Может находиться в трех состояниях - true, false и "нулевом" (неотмеченном) состоянии, а его значение представляет значение типа bool в языке C#. Состояние можно установить или получить с помощью свойства IsChecked. Также добавляет три события - Checked (переход в отмеченное состояние), Unchecked (снятие отметки) и Intermediate (если значение равно null). Чтобы отрабатывать все три события, надо установить свойство IsThreeState="True"
// XAML
<ToggleButton x:Name="toogle" Height="50" IsChecked="False" Checked="toogleCheck" Unchecked="toogleUncheck" />

// C#
private void toogleCheck(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Check");
        }

        private void toogleUncheck(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("UnCheck");
        }

// ToggleButton, как правило, сам по себе тоже редко используется, однако при этом он служит основой для создания других более функциональных элементов, таких как checkbox и radiobutton.





















