//  Все визуальные элементы в WPF уже имеют встроенные шаблоны, которые определяют визуальное дерево, структуру и даже поведение элементов. Однако мощь шаблонов состоит в том, что мы можем их переопределить по своему вкусу. Например, сделать круглое окно, а не квадратное, или кнопку в виде морской звезды.

// К примеру создадим округлый элемент Button:
<Window.Resources>
        <ControlTemplate TargetType="Button" x:Key="btTemplate">
            <Border CornerRadius="25" BorderBrush="CadetBlue" BorderThickness="2"
                    Background="LightBlue" Height="40" Width="100" >
                <ContentControl Margin="5" HorizontalAlignment="Center" VerticalAlignment="Center" Content="Hello" />
            </Border>
        </ControlTemplate>
    </Window.Resources>
    <Grid>
        <Button x:Name="myButton" Template="{StaticResource btTemplate}" Height="40" Width="100">Hello</Button>
    </Grid>

// Мы можем определять шаблоны с через стили, а можем в виде отдельных ресурсов. Так, в данном случае с помощью элемента ControlTemplate определяется ресурс с ключом "btTemplate". В ControlTemplate вложены элементы Border и ContentControl, которые через свои свойства определяют, как будет выглядеть кнопка.

// 																TemplateBinding
// Вышеопределенный шаблон устанавливал цвет, ряд других параметров, которые мы не могли бы изменить. Например, если мы установим в кнопке цвет фона, то этот цвет никак не повлияет, так как в реальности будет работать только цвет, определенный в элементе Border:
<ContentControl Margin="5" HorizontalAlignment="Center" VerticalAlignment="Center" Content="Hello" Background="Red"/>

// Чтобы мы могли из элемента, к которому применяется шаблон, влиять на свойства, определенные в этом шаблоне, нам надо использовать элемент TemplateBinding. Он служит для установки в шаблоне привязки к свойствам элемента. Так, изменим шаблон следующим образом:
<Window.Resources>
        <ControlTemplate TargetType="Button" x:Key="btTemplate">
            <Border CornerRadius="25"
                    BorderBrush="{TemplateBinding BorderBrush}"
                    BorderThickness="{TemplateBinding BorderThickness}"
                    Background="{TemplateBinding Background}"
                    Height="{TemplateBinding Height}"
                    Width="{TemplateBinding Width}" >
                <ContentControl Margin="{TemplateBinding Padding}"
                                HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                                VerticalAlignment="{TemplateBinding VerticalContentAlignment}"
                                Content="{TemplateBinding Content}" />
            </Border>
        </ControlTemplate>
    </Window.Resources>
<Grid>
        <Button x:Name="myButton" Template="{StaticResource btTemplate}" Height="40" Width="100" Background="LightPink">Привет</Button>
    </Grid>

// Выражение типа Background="{TemplateBinding Background}" в элементе Border указывает, что фон элемента Border будет привязан к свойству Background элемента Button. Таким образом, здесь практически все свойства Border и ContentControl (кроме свойства CornerRadius) устанавливаются из разметки элемента Button
// Если их не указывать, их не будет.

// 																		Свойство Template
// Используя свойство Template, можно определить шаблон напрямую в самом элементе:
<Button x:Name="myButton" Content="Привет" Height="40" Width="100" Background="LightPink">
    <Button.Template>
        <ControlTemplate TargetType="Button">
            <Border CornerRadius="25"
                BorderBrush="{TemplateBinding BorderBrush}"
                BorderThickness="{TemplateBinding BorderThickness}"
                Background="{TemplateBinding Background}"
                Height="{TemplateBinding Height}"
                Width="{TemplateBinding Width}" >
                <ContentControl Margin="{TemplateBinding Padding}"
                        HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                        VerticalAlignment="{TemplateBinding VerticalContentAlignment}"
                        Content="{TemplateBinding Content}" />
            </Border>
        </ControlTemplate>
    </Button.Template>
</Button>

// 																		Триггеры в шаблонах
// Если мы возьмем стандартные кнопки (или другие элементы управления), то мы можем заметить, что они меняют свои свойства в различных состояниях. Например, кнопка изменяет фон при нажатии. Рассмотрим, как мы сможем сделать подобные вещи.

// Для реагирования на изменение свойств в шаблонах, как и в стилях, используются триггеры. Для триггеров в элементе ControlTemplate определена коллекция Triggers. Например, добавим к ранее определенному шаблону несколько триггеров:
<Window.Resources>
        <ControlTemplate TargetType="Button" x:Key="btTemplate">
            <Border CornerRadius="25"
                    BorderBrush="{TemplateBinding BorderBrush}"
                    BorderThickness="{TemplateBinding BorderThickness}"
                    Background="{TemplateBinding Background}"
                    Height="{TemplateBinding Height}"
                    Width="{TemplateBinding Width}" >
                <ContentControl Margin="{TemplateBinding Padding}"
                                HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                                VerticalAlignment="{TemplateBinding VerticalContentAlignment}"
                                Content="{TemplateBinding Content}" />
            </Border>
        </ControlTemplate>
    </Window.Resources>
    <Grid>
        <Button x:Name="myButton" Content="Привет" Height="40" Width="100" >
            <Button.Template>
                <ControlTemplate TargetType="Button">
                    <Border x:Name="buttonBorder" CornerRadius="25"
                BorderBrush="{TemplateBinding BorderBrush}"
                BorderThickness="{TemplateBinding BorderThickness}"
                Background="LightPink"
                Height="{TemplateBinding Height}"
                Width="{TemplateBinding Width}" >
                        <ContentControl Margin="{TemplateBinding Padding}"
                        HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                        VerticalAlignment="{TemplateBinding VerticalContentAlignment}"
                        Content="{TemplateBinding Content}" />
                    </Border>
                    <ControlTemplate.Triggers>
                        <Trigger Property="IsMouseOver" Value="true">
                            <Setter Property="FontWeight" Value="Bold" />
                        </Trigger>
                        <Trigger Property="IsPressed" Value="true">
                            <Setter TargetName="buttonBorder" Property="Background" Value="Red" />
                            <Setter TargetName="buttonBorder" Property="BorderBrush" Value="Yellow" />
                        </Trigger>
                        <Trigger Property="IsEnabled" Value="false">
                            <Setter Property="Foreground" Value="Gray" />
                            <Setter TargetName="buttonBorder" Property="Background" Value="LightGray"/>
                        </Trigger>
                    </ControlTemplate.Triggers>
                </ControlTemplate>
            </Button.Template>
        </Button>

// Здесь для элемента Border определено свойство x:Name="buttonBorder", благодаря чему мы можем ссылаться на этот элемент в триггерах.

// В коллекции триггеров шаблона определено три триггера. Первый срабатывает, когда свойство IsMouseOver элемена Button будет равно true. То есть при наведении указателя мыши на кнопку. В этом случае триггер делает жирным шрифт кнопки. Второй триггер срабатывает при нажатии на кнопку, а третий - когда свойство IsEnabled равно false. Причем в этих триггерах свойство TargetName, что позволяет установить свойства не самой кнопки, а элемента Border в шаблоне.









