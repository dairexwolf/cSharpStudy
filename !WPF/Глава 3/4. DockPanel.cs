// Этот контейнер прижимает свое содержимое к определенной стороне внешнего контейнера. Для этого у вложенных элементов надо установить сторону, к которой они будут прижиматься с помощью свойства DockPanel.Dock. Например:

<DockPanel LastChildFill="True">
        <Button DockPanel.Dock="Top" Background="AliceBlue" Content="Верхняя кнопка" />
        <Button DockPanel.Dock="Bottom" Background="BlanchedAlmond" Content="Нижняя кнопка" />
        <Button DockPanel.Dock="Left" Background="Aqua" Content="Левая кнопка" />
        <Button DockPanel.Dock="Right" Background="Aquamarine" Content="Правая кнопка" />
        <Button Background="LightGray" Content="Центр" />
    </DockPanel>
	
// В итоге получаем массив кнопок, каждая из которых прижимается к определенной стороне элемента DockPanel

// Причем у последней кнопки мы можем не устанавливать свойство DockPanel.Dock. Она уже заполняет все оставшееся пространство. Такой эффект получается благодаря установке у DockPanel свойства LastChildFill="True", которое означает, что последний элемент заполняет все оставшееся место. Если у этого свойства поменять True на False, то кнопка прижмется к левой стороне, заполнив только о место, которое ей необходимо.

// Мы также можем прижать к одной стороне сразу несколько элементов. В этом случае они просто будут располагаться по порядку

<DockPanel LastChildFill="True">
        <Button DockPanel.Dock="Top" Background="AliceBlue" Content="Верхняя кнопка" />
        <Button DockPanel.Dock="Bottom" Background="BlanchedAlmond" Content="Нижняя кнопка" />
        <Button DockPanel.Dock="Bottom" Background="BlanchedAlmond" Content="Нижняя кнопка 2" />
        <Button DockPanel.Dock="Left" Background="Aqua" Content="Левая кнопка" />
        <Button DockPanel.Dock="Left" Background="Aqua" Content="Левая кнопка 2" />
        <Button DockPanel.Dock="Right" Background="Aquamarine" Content="Правая кнопка" />
        <Button Background="LightGray" Content="Центр" />
    </DockPanel>

// Контейнер DockPanel особенно удобно использовать для создания стандартных интерфейсов, где верхнюю и левую часть могут занимать какие-либо меню, нижнюю - строка состояния, правую - какая-то дополнительная информация, а в центре будет находиться основное содержание.


