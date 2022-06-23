// Данный элемент управления предназначен для древовидного отображения данных в окне приложения. Может содержать как коллекцию элементов TreeViewItem, так и другое содержимое, например, текстовые блоки:
<TreeView>
    <TextBox>Элемент TreeView</TextBox>
    <TreeViewItem Header="Базы данных">
        <TreeViewItem Header="MS SQL Server" />
        <TreeViewItem Header="MySQL" />
        <TreeViewItem Header="MongoDB" />
        <TreeViewItem Header="Postgres" />
    </TreeViewItem>
    <TreeViewItem Header="Языки программирования">
        <TreeViewItem Header="C-языки">
            <TreeViewItem Header="C#" />
            <TreeViewItem Header="C/C++" />
            <TreeViewItem Header="Java" />
        </TreeViewItem>
        <TreeViewItem Header="Basic">
            <TreeViewItem Header="Visual Basic" />
            <TreeViewItem Header="VB.Net" />
            <TreeViewItem Header="PureBasic" />
        </TreeViewItem>
    </TreeViewItem>
</TreeView>

// Однако все же лучше обертывать элементы в объекты TreeViewItem. С помощью его свойства Header мы можем установить текстовую метку или заголовок узла дерева. Элемент TreeViewItem предлагает также ряд свойств для управления состоянием: IsExpanded (принимает логическое значение и показывает, раскрыт ли узел) и IsSelected (показывает, выбран ли узел). 
// Чтобы отследить выбор или раскрытие узла, мы можем обработать соответствующие события. Событие Expanded возникает при раскрытии узла, а событие Collapsed, наоборот, при его сворачивании.
// Выбор узла дерева мы можем обработать с помощью обработки события Selected. Например:

<TreeViewItem Header="C-языки" Expanded="TreeViewItem_Expanded" Collapsed = "TreeViewItem_Collapsed">
        <TreeViewItem Header="C#" Selected="TreeViewItem_Selected" />
        <TreeViewItem Header="C/C++" Selected="TreeViewItem_Selected" />
        <TreeViewItem Header="Java" Selected="TreeViewItem_Selected" />
    </TreeViewItem>

// В C# в обработчике:
private void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
{
    TreeViewItem tvItem = (TreeViewItem)sender;
    MessageBox.Show("Узел " + tvItem.Header.ToString() + " раскрыт");
}
 
private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
{
    TreeViewItem tvItem = (TreeViewItem)sender;
    MessageBox.Show("Выбран узел: " + tvItem.Header.ToString());
}

private void TreeViewItem_Collapsed(object sender, RoutedEventArgs e)
{
	TreeViewItem tvItem = (TreeViewItem)sender;
	MessageBox.Show("Узел " + tvItem.Header.ToString() + " закрыт");
}
























