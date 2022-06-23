// Элемент Image
// Элемент Image предназначен для работы с изображениями. Свойство Source позволяет задать путь к изображению, например:
<Image Source = "myPhoto.jpg" />

// WPF поддерживает различны форматы изображений: .bmp, .png, .gif, .jpg и т.д.

// Также элемент позволяет проводить некоторые простейшие транформации с изображениями. Например, с помощью объекта FormatConvertedBitmap и его свойства DestinationFormat можно получить новое изображение:
<Grid Background="Black">
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="2.5*" />
        <ColumnDefinition Width="*" />
    </Grid.ColumnDefinitions>
    <Image Grid.Column="0" x:Name="mainImage">
        <Image.Source>
            <FormatConvertedBitmap Source="3.jpg"
                DestinationFormat="Gray32Float" />
        </Image.Source>
    </Image>
    <StackPanel Grid.Column="1">
        <Image Source="1.jpg" />
        <Image Source="2.jpg" />
        <Image Source="4.jpg" />
        <Image Source="3.jpg" />
    </StackPanel>
</Grid>

// InkCanvas
// InkCanvas представляет собой полотно, на котором можно рисовать. Первоначально оно предназначалось для стилуса, но в WPF есть поддержка также и для мыши для обычных ПК. Его очень просто использовать:
<InkCanvas Background="LightCyan" />

// Либо мы можем вложить в InkCanvas какое-нибудь изображение и на нем уже рисовать:
<InkCanvas>
    <Image Source="2.jpg"  Width="300" Height="250"  />
</InkCanvas>

// Режим рисования
/*
InkCanvas имеет несколько режимов, они задаются с помощью свойства EditingMode, значения для которого берутся из перечисления InkCanvasEditingMode.. Эти значения бывают следующими:

* Ink: используется по умолчанию и предполагает рисование стилусом или мышью

* InkAndGesture: рисование с помощью мыши/стилуса, а также с помощью жестов (Up, Down, Tap и др.)

* GestureOnly: рисование только с помощью жестов пользователя

* EraseByStroke: стирание всего штриха стилусом

* EraseByPoint: стирание только части штриха, к которой прикоснулся стилус

* Select: выделение всех штрихов при касании

* None: отсутствие какого-либо действия

Используя эти значения и обрабатывая события InkCanvas, такие как StrokeCollected (штрих нарисован), StrokeErased (штрих стерли) и др., можно управлять набором штрихов и создавать более функциональные приложения на основе InkCanvas.
*/










