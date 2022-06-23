// При работе с данными важную роль играет валидация данных. Прежде чем использовать полученные от пользователя данные, нам надо убедиться, что они введены правильно и представляют корректные значения. Один из встроенных способов проверки введенных данных в WPF представлен классом ExceptionValidationRule. Этот класс обозначает введенные данные как некорректные, если в процессе ввода возникает какое-либо исключение, например, исключение преобразования типов.

// Итак, допустим, у нас определен следующий класс:
public class PersonModel
{
    public string Name { get; set; }
    public int Age { get;set;}
    public string Position { get; set; }
}

// Этот класс представляет человека и предполагает три свойства: имя, возраст и должность. Понятно, что возраст должен представлять числовое значение. Однако пользователи могут ввести что угодно. 
// Мы можем обрабатывать ввод с клавиатуры, а можем воспользоваться классом ExceptionValidationRule, который в случае неудачи преобразования строки в число установит красную границу вокруг текстового поля.

// Сначала создадим в файле кода объект нашего класса PersonModel и установим контекст данных окна:
public partial class MainWindow : Window
    {
        PersonModel person;
        public MainWindow()
        {
            InitializeComponent();
            person = new PersonModel();
            this.DataContext = person;
        }
    }

// Теперь установим привязку в xaml-коде:
<Window x:Class="WpfValidData.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:WpfValidData"
        mc:Ignorable="d"
        Title="MainWindow" Height="350" Width="525">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition />
            <RowDefinition />
            <RowDefinition />
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition />
            <ColumnDefinition />
            <ColumnDefinition />
        </Grid.ColumnDefinitions>

        <TextBox Grid.Column="1" Height="30" Margin="0 0 15 0" />

        <TextBox Grid.Column="1" Grid.Row="1" Height="30" Margin="0 0 15 0">
            <TextBox.Text>
                <Binding Path="Age">
                    <Binding.ValidationRules>
                        <ExceptionValidationRule />
                    </Binding.ValidationRules>
                </Binding>
            </TextBox.Text>
        </TextBox>
        <TextBox Grid.Column="1" Grid.Row="2" Height="30" Margin="0 0 15 0"  />

        <Label Height="30">
            Введите имя
        </Label>
        <Label Grid.Row="1" Content="Введите возраст" Height="30" />
        <Label Grid.Row="2" Content="Введите должность" Height="30" />
    </Grid>
</Window>

// В данном случае мы задаем объект Binding для свойства Text. Данный объект имеет коллекцию правил валидации вводимых данных - ValidationRules. Эта коллекция принимает только одно правило валидации, представленное классом ExceptionValidationRule. Запустим приложение на выполнение и попробуем ввести в текстовое поле какое-нибудь нечисловое значение. В этом случае текстовое поле будет обведено красным цветом, указывая на то, что в вводимых данных имеются ошибки.

// Мы также можем реализовать свою логику валидации для класса модели. Для этого модель должна реализовать интерфейс IDataErrorInfo. Этот интерфейс имеет следующий синтаксис:
public interface IDataErrorInfo
{
    string Error {get;}
    string this[string columnName] { get;}
}

// Допустим, мы хотим ограничить возраст человека только положительными значениями от 0 до 200. Тогда валидация модели будет выглядеть следующим образом:
public class PersonModel : IDataErrorInfo
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }

        public string this[string columnName] {
            get
            {
                string error = String.Empty;
                switch(columnName)
                {
                    case "Age":
                        if((Age < 0) || (Age > 200))
                        {
                            error = "Возраст должен быть больше 0 и меньше 200";
                        }
                        break;

                    case "Name":
                        // Обработка ошибок для свойства Name
                        break;
                    case "Position":
                        break;
                }
                return error;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

    }
	
// И последнее - нам осталось немного подкорректировать xaml-код. Теперь нам надо использовать в качестве правила валидации класс DataErrorValidationRule:
<TextBox Grid.Column="1" Grid.Row="1" Height="30" Margin="0 0 15 0">
            <TextBox.Text>
                <Binding Path="Age">
                    <Binding.ValidationRules>
                        <DataErrorValidationRule />
                    </Binding.ValidationRules>
                </Binding>
            </TextBox.Text>
        </TextBox>

// 															Настройка внешнего вида при ошибке валидации
// Но это еще не все. Мы можем сами управлять через шаблоны отображением ошибки ввода. В предыдущем случае у нас граница текстового поля при ошибке окрашивалась в красный цвет. Попробуем настроить данное действие. Для этого нам нужно использовать элемент AdornedElementPlaceholder. Итак изменим разметку приложения следующим образом, добавив в нее шаблон элемента управления:
<Window.Resources>
        <ControlTemplate x:Key="validationFailed">
            <StackPanel Orientation="Horizontal">
                <Border BorderBrush="Violet" BorderThickness="2">
                    <AdornedElementPlaceholder />
                </Border>
                <TextBlock Foreground="Red" FontSize="16" FontWeight="Bold">!</TextBlock>
            </StackPanel>
        </ControlTemplate>
    </Window.Resources>
    
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition />
            <RowDefinition />
            <RowDefinition />
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition />
            <ColumnDefinition />
            <ColumnDefinition />
        </Grid.ColumnDefinitions>

        <TextBox Grid.Column="1" Height="30" Margin="0 0 15 0" />

        <TextBox Grid.Column="1" Grid.Row="1" Height="30" Margin="0 0 15 0"
                 Validation.ErrorTemplate="{StaticResource validationFailed}"
                 >
            <TextBox.Text>
                <Binding Path="Age">
                    <Binding.ValidationRules>
                        <DataErrorValidationRule />
                    </Binding.ValidationRules>
                </Binding>
            </TextBox.Text>
        </TextBox>
        <TextBox Grid.Column="1" Grid.Row="2" Height="30" Margin="0 0 15 0"  />

        <Label Height="30">
            Введите имя
        </Label>
        <Label Grid.Row="1" Content="Введите возраст" Height="30" />
        <Label Grid.Row="2" Content="Введите должность" Height="30" />
    </Grid>

// С помощью свойства Validation.ErrorTemplate мы получаем шаблон, который будет отрабатывать при ошибке валидации. Этот шаблон, определенный выше в ресурсах окна, определяет границу фиолетового цвета вокруг элемента ввода, а также отображает рядом с ним восклицательный знак красного цвета.

// Мы также можем определить поведение и визуализацию через триггер при установке свойства Validation.HasError в True. А с помощью свойства ToolTip можно создать привязку к сообщению ошибки:
<Style TargetType="TextBox" >
            <Style.Triggers>
                <Trigger Property="Validation.HasError" Value="True">
                    <Setter Property="ToolTip"
                            Value="{Binding RelativeSource={RelativeSource Self}, Path=(Validation.Errors)[0].ErrorContent}" />
                </Trigger>
            </Style.Triggers>
        </Style>


// 															Обработка событий валидации
// WPF предоставляет механизм обработки ошибки валидации с помощью события Validation.Error. Данное событие можно использовать в любом элементе управления. Например, пусть при ошибке валидации при вводе в текстовое поле выскакивает сообщение с ошибкой. Для этого изменим текстовое поле следующим образом:
<TextBox Grid.Column="1" Grid.Row="1" Height="30" Margin="0 0 15 0"
                 Validation.ErrorTemplate="{StaticResource validationFailed}"
                 Validation.Error="TextBox_Error">
            <TextBox.Text>
                <Binding Path="Age" NotifyOnValidationError="True">
                    <Binding.ValidationRules>
                        <DataErrorValidationRule />
                    </Binding.ValidationRules>
                </Binding>
            </TextBox.Text>
        </TextBox>

// Здесь, во-первых, надо отметить установку свойства NotifyOnValidationError="True". Это позволит вызывать событие валидации:
<Binding Path="Age" NotifyOnValidationError="True">

// И также устанавливается сам обработчик события валидации:
<TextBox 
    Validation.Error="TextBox_Error">

// При этом следует отметить, что событие Validation.Error является поднимающимся (bubbling events), поэтому мы можем установить для него обработчик и в контейнере Grid или в любых других контейнерах, в которых находится это текстовое поле. И в случае ошибки событие также будет генерироваться и обрабатываться.
// И в конце определим в файле кода c# сам обработчик:
private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            MessageBox.Show(e.Error.ErrorContent.ToString());
        }

