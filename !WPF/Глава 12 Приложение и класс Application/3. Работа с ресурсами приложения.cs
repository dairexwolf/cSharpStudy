// Большинство приложений WPF так или иначе используют различные бинарные ресурсы - файлы изображений, мультимедиа и т.д. Для добавления ресурсов достаточно добавить уже существующий файл в проект. Нередко для оптимизации структуры проекта для хранения ресурсов создаются отдельные папки в проекте.
/* При добавлении ресурсов в проект мы можем установить одну из двух опций компиляции в окне свойств в поле Build Action:

Resources: ресурс встраивается в сборку (значение по умолчанию)

Content: ресурс физически находится отдельно от сборки, однако сборка содержит информацию о его местоположении

*/

// Находится это все в свойствах ресурса в оброзревателе решений.
// 																Доступ к ресурсам
// Для доступа к ресурсам в WPF можно использовать объект Uri. Например, в проекте в папке Images помещен ресурс - файл river.jpg. И чтобы обратиться к нему из элемента Image мы можем использовать относительный путь Images/river.jpg:
<Image x:Name="myImage" Source = "Images/river.jpg" />

// Для доступа к ресурсу здесь применяется относительный путь с указанием папки и названия файла. Однако в реальности все используемые в xaml пути трансформируются в объекты Uri. Так, если бы нам пришлось устанавливать изображение в коде c#, то нам пришлось бы написать что-то вроде следующего:
public MainWindow()
        {
            InitializeComponent();

            myImage.Source = new BitmapImage(new Uri("Images/river.jpg", UriKind.Relative));

        }

// Конструктор Uri принимает два параметра: собственно относительный путь и значение из перечисления UriKind, которое указывает, что путь стоит расценивать как относительный. А относительный - это значит, как он смотрит исходя из папки с программой (сборкой)
// Кроме относительных путей мы также можем использовать абсолютные:
 myImage.Source = new BitmapImage(new Uri("C:/Users/vyatkin/Documents/Visual Studio 2015/Projects/LifecycleApp/LifecycleApp/Images/river.jpg", UriKind.Absolute));
 
 // Кроме относительных и абсолютных путей в WPF можно использовать определения "упакованных" URI (packageURI). Фактически относительные пути являются сокращениями упакованных Uri. Так путь "Images/river.jpg" является сокращением пути "pack://application:,,,/images/river.jpg":
 myImage.Source = new BitmapImage(new Uri("pack://application:,,,/images/river.jpg"));
 
 // Аналог в XAML
<Grid Width="513">
        <Grid.RowDefinitions>
            <RowDefinition />
            <RowDefinition />
        </Grid.RowDefinitions>
        <Button Click="Button1_Click" Content="Не нажимай подумой" Height="30" Width="150" />
        
		<Image x:Name="myImage" Source="pack://application:,,,/images/river.jpg" Grid.Row="1" />
    
	</Grid>

// Синтаксис упакованных URI заимствован из спецификации стандарта XML Paper Specification (XPS). Для ресурсов, встроенных в сборку приложения, он имеет следующую форму:
pack://application:,,,/[относительный _путь_к_ресурсу]

// Три запятых здесь фактически передают три слеша. То есть по факту начало пути выглядит так: application:///
// Если же наши ресурсы находятся вне приложения, например, это некий файл по пути D://forest.jpg, тогда вместо application использется название логического диска:
Source="pack://C:,,,Users/vyatkin/Documents/Visual Studio 2015/Projects/LifecycleApp/LifecycleApp/Images/river.jpg"

// 																	Ресурсы в других сборках
// Может сложиться ситуация, когда наше приложение использует внешнюю сборку, в которой были упакованы ресурсы. И мы также можем обратиться к этим ресурсам, используя синтаксис "упакованных" Uri. В этом случае путь будет иметь следующий формат:
pack://application,,,/[название_библиотеки];component/[ауть_к_ресурсу]

// Допустим, файл сборки называется libs.dll, а ресурс, а сам ресурс в ней находится по пути images/forest.jpg, тогда получить его мы сможем следующим образом:
MyImage.Source = new BitmapImage(new Uri("pack://application:,,,/libs;component/images/river.jpg"));

// Либо можно использовать эквивалентный способ доступа к ресурсу:
MyImage.Source = new BitmapImage(new Uri("libs;component/images/river.jpg", UriKind.Relative));

// 													Получение ресурсов
// Используя выше рассмотренные пути можно в программе получать сам файл ресурса и производить с ним манипуляции. Для получения ресурса приложения применяется метод Application.GetResourceStream(), например:
System.Windows.Resources.StreamResourceInfo res = 
    Application.GetResourceStream(new Uri("images/river.jpg", UriKind.Relative));
 
res.Stream.CopyTo(new System.IO.FileStream("D://newRiver.jpg", System.IO.FileMode.OpenOrCreate));

// В данном случае мы считываем ресурс приложения и выполняем его копирование по новому пути вне приложения.
