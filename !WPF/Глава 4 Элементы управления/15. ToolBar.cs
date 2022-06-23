// Этот элемент, как правило, применяется для обеспечения быстрого доступа к наиболее часто используемым операциям. Он может содержать прочие элементы как кнопки, текстовые поля, объекты Menu и др.

<ToolBar Height="25" VerticalAlignment="Top">
    <ToggleButton><Image Source="icon0.gif" /></ToggleButton>
    <Separator />
    <Button><Image Source="icon1.gif" /></Button>
    <Separator />
    <Button><Image Source="icon2.png" /></Button>
    <Separator />
    <Button><Image Source="icon3.png" /></Button>
    <TextBox Foreground="LightGray" Width="100">Поиск...</TextBox>
</ToolBar>


// Также можно создавать сразу несколько связанных элементов ToolBar внутри ToolBarTray. Преимущество его использования заключается в возможности задать как горизонтальное, так и вертикальное расположение элементов ToolBar в окне приложения.
<ToolBarTray>
        <ToolBar Height="25" VerticalAlignment="Top">
            <ToggleButton>
                0
            </ToggleButton>
            <Separator />
            <Button>
                1
            </Button>
            <Separator />
            <Button>
                2
            </Button>
            <Separator />
            <Button>
                3
            </Button>
            <TextBox Foreground="LightGray" Width="100" >Поиск...</TextBox>
        </ToolBar>
        <ToolBar>
            <Button>
                <StackPanel Orientation="Horizontal">
                    <Ellipse Width="10" Height="10" Fill="Black" HorizontalAlignment="Left"/>
                    <TextBlock HorizontalAlignment="Right" Width="60" Margin="5 0 0 0" >Найти</TextBlock>
                </StackPanel>
            </Button>
        </ToolBar>
    </ToolBarTray>

// Используя свойство Orientation мы можем настроить у ToolBarTray ориентацию. По умолчанию она горизонтальная, но мы можем расположить его вертикально:
<ToolBarTray Orientation="Vertical">
     <ToolBar Width="25" VerticalAlignment="Top">
     <!-- здесь остальной код -->

// Еще один элемент - StatusBar, во многом напоминает ToolBar и выполняет схожие функции, только в отличие о ToolBar его располагают обычно внизу окна приложения.

<StackPanel>
    <ToolBarTray Orientation="Vertical">
        <ToolBar Width="25" VerticalAlignment="Top">
            <ToggleButton>
                0
            </ToggleButton>
            <Separator />
            <Button>
                1
            </Button>
            <Separator />
            <Button>
                2
            </Button>
            <Separator />
            <Button>
                3
            </Button>
            <TextBox Foreground="LightGray" Width="100" >Поиск...</TextBox>
        </ToolBar>
        <ToolBar>
            <Button>
                <StackPanel Orientation="Vertical">
                    <Ellipse Width="10" Height="10" Fill="Black" HorizontalAlignment="Left"/>
                    <TextBlock HorizontalAlignment="Right" Width="60" Margin="5 0 0 0" >Найти</TextBlock>
                </StackPanel>
            </Button>
        </ToolBar>
    </ToolBarTray>
        <StatusBar HorizontalAlignment="Center">
            <StatusBarItem>
                <TextBlock>
                    Статус
                </TextBlock>
            </StatusBarItem>
        </StatusBar>
    </StackPanel>
