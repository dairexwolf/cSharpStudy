// Эта панель, подобно StackPanel, располагает все элементы в одной строке или колонке в зависимости от того, какое значение имеет свойство Orientation - Horizontal или Vertical. Главное отличие от StackPanel - если элементы не помещаются в строке или столбце, создаются новые столбец или строка для не поместившихся элементов.

    <WrapPanel>
        <Button Background="AliceBlue" Content="Кнопка 1" />
        <Button Background="Blue" Content="Кнопка 2" />
        <Button Background="Aquamarine" Content="Кнопка 3" Height="30"/>
        <Button Background="DarkGreen" Content="Кнопка 4" Height="20"/>
        <Button Background="LightGreen" Content="Кнопка 5"/>
        <Button Background="RosyBrown" Content="Кнопка 6" Width="80" />
        <Button Background="GhostWhite" Content="Кнопка 7" />
    </WrapPanel>

// В горизонтальном стеке те элементы, у которых явным образом не установлена высота, будут автоматически принимать высоту самого большого элемента из стека.

// Вертикальный WrapPanel делается аналогично:

    <WrapPanel Orientation="Vertical">
        <Button Background="AliceBlue" Content="Кнопка 1" Height="100" />
        <Button Background="Blue" Content="Кнопка 2" />
        <Button Background="Aquamarine" Content="Кнопка 3" Width="60"/>
        <Button Background="DarkGreen" Content="Кнопка 4" Width="80"/>
        <Button Background="LightGreen" Content="Кнопка 5"/>
        <Button Background="RosyBrown" Content="Кнопка 6" Height="80" />
        <Button Background="GhostWhite" Content="Кнопка 7" />
        <Button Background="Bisque" Content="Кнопка 8" />
    </WrapPanel>
	
// В вертикальном стеке элементы, у которых явным образом не указана ширина, автоматически принимают ширину самого широкого элемента.

// Мы также можем установить для всех вложенных элементов какую-нибудь определенную ширину (с помощью свойства ItemWidth) или высоту (свойство ItemHeight):

<WrapPanel ItemHeight="60" ItemWidth="120" Orientation="Vertical">
        <Button Background="AliceBlue" Content="Кнопка 1" />
        <Button Background="Blue" Content="Кнопка 2" />
        <Button Background="Aquamarine" Content="Кнопка 3" Width="60"/>
        <Button Background="DarkGreen" Content="Кнопка 4" Width="80"/>
        <Button Background="LightGreen" Content="Кнопка 5"/>
        <Button Background="RosyBrown" Content="Кнопка 6"  />
        <Button Background="GhostWhite" Content="Кнопка 7" />
        <Button Background="Bisque" Content="Кнопка 8" />
    </WrapPanel>
