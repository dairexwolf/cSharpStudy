// В WPF привязка (binding) является мощным инструментом программирования, без которого не обходится ни одно серьезное приложение.

// Привязка подразумевает взаимодействие двух объектов: источника и приемника. Объект-приемник создает привязку к определенному свойству объекта-источника. В случае модификации объекта-источника, объект-приемник также будет модифицирован. Например, простейшая форма с использованием привязки:
<StackPanel>
        <TextBox x:Name="myTextBox" Height="30" />
        <TextBlock x:Name="myTextBlock" Text="{Binding ElementName=myTextBox,Path=Text}" Height="30" />
    </StackPanel>

// Для определения привязки используется выражение типа:
{Binding ElementName=Имя_объекта-источника, Path=Свойство_объекта-источника}

// То есть в данном случае у нас элемент TextBox является источником, а TextBlock - приемником привязки. Свойство Text элемента TextBlock привязывается к свойству Text элемента TextBox. В итоге при осуществлении ввода в текстовое поле синхронно будут происходить изменения в текстовом блоке.

//																Работа с привязкой в C#
// Ключевым объектом при создании привязки является объект System.Windows.Data.Binding. Используя этот объект мы можем получить уже имеющуюся привязку для элемента:
Binding binding = BindingOperations.GetBinding(myTextBlock, TextBlock.TextProperty);

// В данном случае получаем привязку для свойства зависимостей TextProperty элемента myTextBlock.

// Также можно полностью установить привязку в коде C#:
Binding binding = new Binding();
 
    binding.ElementName = "myTextBox"; // элемент-источник
    binding.Path = new PropertyPath("Text"); // свойство элемента-источника
    myTextBlock.SetBinding(TextBlock.TextProperty, binding); // установка привязки для элемента-приемника

// Если в дальнейшем нам станет не нужна привязка, то мы можем воспользоваться классом BindingOperations и его методами ClearBinding()(удаляет одну привязку) и ClearAllBindings() (удаляет все привязки для данного элемента)
BindingOperations.ClearBinding(myTextBlock, TextBlock.TextProperty);
// или
BindingOperations.ClearAllBindings(myTextBlock);

/* Некоторые свойства класса Binding:

ElementName: имя элемента, к которому создается привязка

IsAsync: если установлено в True, то использует асинхронный режим получения данных из объекта. По умолчанию равно False

Mode: режим привязки

Path: ссылка на свойство объекта, к которому идет привязка

TargetNullValue: устанавливает значение по умолчанию, если привязанное свойство источника привязки имеет значение null

RelativeSource: создает привязку относительно текущего объекта

Source: указывает на объект-источник, если он не является элементом управления.

XPath: используется вместо свойства path для указания пути к xml-данным

*/

/* Свойство Mode объекта Binding, которое представляет режим привязки, может принимать следующие значения:

OneWay: свойство объекта-приемника изменяется после модификации свойства объекта-источника.

OneTime: свойство объекта-приемника устанавливается по свойству объекта-источника только один раз. В дальнейшем изменения в источнике никак не влияют на объект-приемник.

TwoWay: оба объекта - применки и источник могут изменять привязанные свойства друг друга.

OneWayToSource: объект-приемник, в котором объявлена привязка, меняет объект-источник.

Default: по умолчанию (если меняется свойство TextBox.Text, то имеет значение TwoWay, в остальных случаях OneWay).

*/

// Применение режима привязки:
<StackPanel>
        <TextBox x:Name="textBox1" Height="30" />
        <TextBox x:Name="textBox2" Height="30" Text="{Binding ElementName=textBox1, Path=Text, Mode=OneWay}" />
</StackPanel>

// 														Обновление привязки. UpdateSourceTrigger
// Односторонняя привязка от источника к приемнику практически мгновенно изменяет свойство приемника. Но если мы используем двустороннюю привязку в случае с текстовыми полями (как в примере выше), то при изменении приемника свойство источника не изменяется мгновенно. Так, в примере выше, чтобы текстовое поле-источник изменилось, нам надо перевести фокус с текстового поля-приемника. 
// И в данном случае в дело вступает свойство UpdateSourceTrigger класса Binding, которое задает, как будет присходить обновление. Это свойство в качестве принимает одно из значений перечисления UpdateSourceTrigger:

/*
PropertyChanged: источник привязки обновляется сразу после обновления свойства в приемнике

LostFocus: источник привязки обновляется только после потери фокуса приемником

Explicit: источник не обновляется до тех пор, пока не будет вызван метод BindingExpression.UpdateSource()

Default: значение по умолчанию. Для большинства свойств это значение PropertyChanged. А для свойства Text элемента TextBox это значение LostFocus
*/

// В данном случае речь идет об обновлении источника привязки после изменения приемника в режимах OneWayToSource или TwoWay. То есть чтобы у нас оба текстовых поля, которые связаны режимом TwoWay, моментально обновлялись после изменения одного из них, надо использовать значение UpdateSourceTrigger.PropertyChanged:
<StackPanel>
    <TextBox x:Name="textBox1" Height="30" />
    <TextBox x:Name="textBox2" Height="30"
        Text="{Binding ElementName=textBox1, Path=Text, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />
</StackPanel>

// 																	Свойство Source
// Свойство Source позволяет установить привязку даже к тем объектам, которые не являются элементами управления WPF. Например, определим класс Phone:
class Phone
{
    public string Title { get; set; }
    public string Company { get; set; }
    public int Price { get; set; }
}

// Теперь создадим объект этого класса и определим к нему привязку:
<Window.Resources>
        <local:Phone x:Key="nexusPhone" Title="Nexus X5" Company="Google" Price="25000" />
    </Window.Resources>
    <Grid Background="Black">
        <Grid.RowDefinitions>
            <RowDefinition />
            <RowDefinition />
        </Grid.RowDefinitions>
        <Grid.ColumnDefinitions>
            <ColumnDefinition />
            <ColumnDefinition />
        </Grid.ColumnDefinitions>
        <TextBlock Text="Модель:" Foreground="White"/>
        <TextBlock x:Name="titleTextBlock" Text="{Binding Source={StaticResource nexusPhone}, Path=Title}"
                        Foreground="White" Grid.Column="1"/>
        <TextBlock Text="Цена:" Foreground="White" Grid.Row="1"/>
        <TextBlock x:Name="priceTextBlock" Text="{Binding Source={StaticResource nexusPhone}, Path=Price}"
                        Foreground="White" Grid.Column="1" Grid.Row="1"/>
    </Grid>

// 															Свойство TargetNullValue
// На случай, если свойство в источнике привязки вдруг имеет значение null, то есть оно не установлено, мы можем задать некоторое значение по умолчанию. Например:
<Window.Resources>
    <local:Phone x:Key="nexusPhone" Company="Google" Price="25000" />
</Window.Resources>
<StackPanel>
    <TextBlock x:Name="titleTextBlock"
        Text="{Binding Source={StaticResource nexusPhone}, Path=Title, TargetNullValue=Текст по умолчанию}" />
</StackPanel>

// В данном случае у ресурса nexusPhone не установлено свойство Title, поэтому текстовый блок будет выводить значение по умолчанию, указанное в параметре TargetNullValue.

// 															Свойство RelativeSource
// Свойство RelativeSource позволяет установить привязку относительно элемента-источника, который связан какими-нибудь отношениями с элементом-приемником. Например, элемент-источник может быть одним из внешних контейнеров для элемента-приемника. Либо источником и приемником может быть один и тот же элемент.

// Для установки этого свойства используется одноименный объект RelativeSource. У этого объекта есть свойство Mode, которое задает способ привязки. Оно принимает одно из значений перечисления RelativeSourceMode:

/*
Self: привязка осуществляется к свойству этого же элемента. То есть элемент-источник привязки в то же время является и приемником привязки.

FindAncestor: привязка осуществляется к свойству элемента-контейнера.
*/

// Например, совместим источник и приемник привязке в самом элементе:
<TextBox Text="{Binding RelativeSource={RelativeSource Mode=Self}, Path=Background, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}" />

// Здесь текст и фоновый цвет текстового поля связаны двусторонней привязкой. В итоге мы можем увидеть в поле числовое значение цвета, поменять его, и вместе с ним изменится и фон поля.

// Привязка к свойствам контейнера:
<Grid Background="Black">
    <TextBlock Foreground="White"
        Text="{Binding RelativeSource={RelativeSource Mode=FindAncestor,AncestorType={x:Type Grid}}, Path=Background}" />
</Grid>

// При использовании режима FindAncestor, то есть привязке к контейнеру, необходимо еще указывать параметр AncestorType и передавать ему тип контейнера в виде выражения AncestorType={x:Type Тип_элемента-контейнера}. При этом в качестве контейнера мы могли бы выбрать любой контейнер в дереве элементов, в частности, в данном случае кроме Grid таким контейнером также является элемент Window.

// 															Свойство DataContext
// У объекта FrameworkElement, от которого наследуются элементы управления, есть интересное свойство DataContext. Оно позволяет задавать для элемента и вложенных в него элементов некоторый контекст данных. Тогда вложенные элементы могут использовать объект Binding для привязки к конкретным свойствам этого контекста. Например, используем ранее определенный класс Phone и создадим контекст данных из объекта этого класса:
<Window.Resources>
        <local:Phone x:Key="nexusPhone" Title="Nexus X5" Company="Google" Price="25000" />
    </Window.Resources>
    <Grid Background="Black" DataContext="{StaticResource nexusPhone}" TextBlock.Foreground="White">
        <Grid.ColumnDefinitions>
            <ColumnDefinition />
            <ColumnDefinition />
            <ColumnDefinition />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition />
            <RowDefinition />
        </Grid.RowDefinitions>
        <TextBlock Text="Модель" />
        <TextBlock Text="{Binding Title}" Grid.Row="1" />
        <TextBlock Text="Производитель" Grid.Column="1"/>
        <TextBlock Text="{Binding Company}" Grid.Column="1" Grid.Row="1" />
        <TextBlock Text="Цена" Grid.Column="2" />
        <TextBlock Text="{Binding Price}" Grid.Column="2" Grid.Row="1" />
    </Grid>

// Таким образом мы задаем свойству DataContext некоторый динамический или статический ресурс. Затем осуществляем привязку к этому ресурсу.

// Необходимо донести до читателя, что когда мы прописываем Binding в свойстве объекта, то создаётся объект класса Binding и вызывается метод SetBinding() этого объекта, который принимает в качестве первого параметра свойство ,в котором прописываем Binding, а в качестве второго параметра сам объект Binding, свойства которого задаются через запятую после создания объекта класса Binding. При чём это вшито в разметку xaml и явного вызова метода SetBinding() ,вы не увидите ,в отличие от представленной привязки в коде C#. Сам же метод SetBinding() наследуется от класса FrameworkElement ,который в свою очередь наследуется от UIElement ,следовательно его можно вызвать только от классов,его наследующих,
