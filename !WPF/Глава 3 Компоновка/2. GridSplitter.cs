//Элемент GridSplitter помогает создавать интерфейсы наподобие элемента SplitContainer в WinForms, только более функциональные. Он представляет собой некоторый разделитель между столбцами или строками, путем сдвига которого можно регулировать ширину столбцов и высоту строк. В качестве примера можно привести стандартный интерфейс проводника в Windows, где разделительная полоса отделяет древовидный список папок от панели со списком файлов. Например,
<Grid>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*" />
        <ColumnDefinition Width="Auto" />
        <ColumnDefinition Width="*" />
    </Grid.ColumnDefinitions>
    <Button Grid.Column="0" Content="Левая кнопка" />
    <GridSplitter Grid.Column="1" ShowsPreview="False" Width="3"
        HorizontalAlignment="Center" VerticalAlignment="Stretch" />
    <Button Grid.Column="2" Content="Правая кнопка" />
</Grid>

// Двигая центральную линию, разделяющую правую и левую части, мы можем устанавливать их ширину.

// Итак, чтобы использовать элемент GridSplitter, нам надо поместить его в ячейку в Gride. По сути это обычный элемент, такой же, как кнопка. Как выше, у нас три ячейки (так как три столбца и одна строка), и GridSplitter помещен во вторую ячейку. Обычно строка или столбец, в которые помещают элемент, имеет для свойств Height или Width значение Auto.

// Если у нас несколько строк, и мы хотим, чтобы разделитель распространялся на несколько строк, то мы можем задать свойство Grid.RowSpan:

    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="Auto" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition></RowDefinition>
            <RowDefinition></RowDefinition>
        </Grid.RowDefinitions>
        <Button Content="A" Grid.Column="0"/>
        <GridSplitter Grid.Column="1" Grid.RowSpan="2" ShowsPreview="False" Width="3"
    HorizontalAlignment="Center" VerticalAlignment="Stretch" />
        <Button Content="B" Grid.Column="2" />
        <Button Content="C" Grid.Row="1" />
        <Button Content="D" Grid.Column="2" Grid.Row="1" />
    </Grid>

// В случае, если мы задаем горизонтальный разделитель, то тогда соответственно надо использовать свойство Grid.ColumnSpan
// Затем нам надо настроить свойства. Во-первых, надо настроить ширину (Width) для вертикальных сплитеров и высоту (Height) для горизонтальных. Если не задать соответствующее свойство, то сплитер мы не увидим, так как он изначально очень мал.
// Затем нам надо задать выравнивание. Если мы хотим, что сплитер заполнял всю высоту доступной области (то есть если у нас вертикальный сплитер), то нам надо установить для свойства VerticalAlignment значение Stretch.

    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="Auto" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>
        <Grid.RowDefinitions>
            <RowDefinition Height="*"></RowDefinition>
            <RowDefinition Height="Auto"></RowDefinition>
            <RowDefinition Height="*"></RowDefinition>
            
        </Grid.RowDefinitions>
        <Button Content="A" Grid.Column="0"/>
        <GridSplitter Grid.Column="1" Grid.Row="0" ShowsPreview="False" Width="3"
    HorizontalAlignment="Center" VerticalAlignment="Stretch" />
        <GridSplitter Grid.Row="1" Grid.ColumnSpan="3" Height="3"
                      HorizontalAlignment="Stretch" VerticalAlignment="Center" />
        <Button Content="B" Grid.Column="2" />
        <Button Content="F" Grid.ColumnSpan="3" Grid.Row="2" />
    </Grid>

// Здесь у нас сразу два сплитера: один между двумя кнопками: А и В и нижней кнопкой F, а второй - между A и B.