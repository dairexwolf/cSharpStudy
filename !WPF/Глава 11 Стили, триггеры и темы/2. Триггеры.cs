/* Тригерры позволяют декларативно задать некоторые действия, которые выполняются при изменении свойств стиля. Существует виды триггеров:

Триггеры свойств: вызываются в ответ на изменения свойствами зависимостей своего значения

Мультитриггеры: вызываются при выполнении ряда условий

Триггеры событий: вызываются в ответ на генерацию событий

Триггеры данных: вызываются в ответ на изменения значений любых свойств (они необязательно должны быть свойствами зависимостей)

*/

//  								Триггеры свойств
// Простые триггеры свойств задаются с помощью объекта Trigger. Они следят за значением свойств и в случае их изменения с помощью объекта Setter устанавливают значение других свойств.

// Например, в следующем примере по наведению на кнопку высота шрифта устанавливается в 14, а цвет шрифта становится красным:
<Window.Resources>
        <Style TargetType="Button">
            <Style.Setters>
                <Setter Property="Button.Background" Value="Black" />
                <Setter Property="Button.Foreground" Value="White" />
                <Setter Property="Button.FontFamily" Value="Verdana" />
                <Setter Property="Button.Margin" Value="10" />
            </Style.Setters>
            <Style.Triggers>
                <Trigger Property="IsMouseOver" Value="True">
                    <Setter Property="FontSize" Value="14" />
                    <Setter Property="Foreground" Value="Red" />
                </Trigger>
            </Style.Triggers>
        </Style>
    </Window.Resources>
    <StackPanel Background="Black" >
        <Button x:Name="button1" Content="Кнопка 1"/>
        <Button x:Name="button2" Content="Кнопка 2" />
    </StackPanel>
	
// Здесь объект Trigger применяет два свойства: Property и Value. Свойство Property указывает на отслеживаемое свойство, а свойство Value указывает на значение, по достижении которого триггер начнет действовать. Например, в данном случае триггер отслеживает свойство IsMouseOver - если оно будет равно true, тогда сработает триггер.

// Trigger имеет вложенную коллекцию Setters, в которой можно определить сеттеры, реализующие логику триггера.

// 													MultiTrigger
// При необходимости отслеживания не одного, а сразу нескольких свойств используют объект MultiTrigger. Он содержит коллекцию элементов Condition, каждый из которых, как и обычный триггер, определяет отслеживаемое свойство и его значение:
<Window.Resources>
        <Style TargetType="Button">
            <Style.Setters>
                <Setter Property="Button.Background" Value="Black" />
                <Setter Property="Button.Foreground" Value="White" />
                <Setter Property="Button.FontFamily" Value="Verdana" />
                <Setter Property="Button.Margin" Value="10" />
            </Style.Setters>
            <Style.Triggers>
                <MultiTrigger>
                    <MultiTrigger.Conditions>
                        <Condition Property="IsMouseOver" Value="True" />
                        <Condition Property="IsPressed" Value="True" />
                    </MultiTrigger.Conditions>
                    <MultiTrigger.Setters>
                        <Setter Property="FontSize" Value="14" />
                        <Setter Property="Foreground" Value="Red" />
                    </MultiTrigger.Setters>
                </MultiTrigger>
            </Style.Triggers>
        </Style>
    </Window.Resources>
    <StackPanel Background="Black" >
        <Button x:Name="button1" Content="Кнопка 1"/>
        <Button x:Name="button2" Content="Кнопка 2" />
    </StackPanel>

// В данном случае если удовлетворены одновременно оба условия:
<Condition Property="IsMouseOver" Value="True" />
<Condition Property="IsPressed" Value="True" />

// то срабатывают сеттеры мультитриггера:
<Setter Property="FontSize" Value="14" />
<Setter Property="Foreground" Value="Red" />

// 																EventTrigger
// Если простой триггер наблюдает за изменением свойства, то EventTrigger реагирует на определенные события совсем как обработчики событий. Правда, триггеры событий более ограничены в своих возможностях.

// EventTrigger определяет анимацию и, если событие происходит, запускает ее на выполнение.
<Window.Resources>
        <Style TargetType="Button">
            <Style.Setters>
                <Setter Property="Button.Background" Value="Black" />
                <Setter Property="Button.Foreground" Value="White" />
                <Setter Property="Button.FontFamily" Value="Verdana" />
                <Setter Property="Button.Margin" Value="10" />
            </Style.Setters>
            <Style.Triggers>
                <EventTrigger RoutedEvent="Click">
                    <EventTrigger.Actions>
                        <BeginStoryboard>
                            <Storyboard>
                                <DoubleAnimation Storyboard.TargetProperty="Width" Duration="0:0:1" To="220" AutoReverse="True" />
                                <DoubleAnimation Storyboard.TargetProperty="Height" Duration="0:0:1" To="80" AutoReverse="True" />
                            </Storyboard>
                        </BeginStoryboard>
                    </EventTrigger.Actions>
                </EventTrigger>
            </Style.Triggers>
        </Style>
    </Window.Resources>
    <StackPanel Background="Black" >
        <Button x:Name="button1" Width="100" Height="30" Content="Кнопка 1"/>
    </StackPanel>

// В данном случае свойство RoutedEvent задает событие, на которое подписывается объект EventTrigger. Затем определяется свойство EventTrigger.Actions, которое задает анимацию, производимую в случае возникновения события.

// Объект BeginStoryboard начинает анимацию, которая задается объектом Storyboard. Сама анимация определяется в объекте DoubleAnimation. Его свойство Storyboard.TargetProperty указывает на свойство элемента, изменяемое в процессе анимации, Duration задает время анимации, а свойство To - финальное значение свойства, на котором анимация заканчивается.

// Суть данной анимации - значение свойства, указанного в To, сравнивается с текущим значением свойства и, если значение в To больше, то свойство увеличивает значение, иначе уменьшается.

// 														Триггер данных DataTrigger
// DataTrigger отслеживает изменение свойств, которые необязательно должны представлять свойства зависимостей. Для соединения с отслеживаемыми свойства триггеры данных используют выражения привязки:
<Window.Resources>
        <Style TargetType="Button">
            <Style.Triggers>
                <DataTrigger Binding="{Binding ElementName=checkBox1, Path=IsChecked}"
                        Value="True">
                    <Setter Property="IsEnabled" Value="False"/>
                </DataTrigger>
            </Style.Triggers>
        </Style>
    </Window.Resources>
    <StackPanel >
        <CheckBox x:Name="checkBox1" Content="Disable" />
        <Button x:Name="button1" Content="Кнопка 1" />
    </StackPanel>

// С помощью свойства Binding триггер данных устанавливает привязку к отслеживаемому свойству. Свойство Value задает значение отлеживаемого свойства, при котором сработает триггер. Так, в данном случае, если чекбокс будет отмечен, то сработает триггер, который установит у кнопки IsEnabled="False".
