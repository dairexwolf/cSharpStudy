// Элемент управления обладает определенными состояниями, которые могут влиять на визуальное отображение. Например, для кнопки это может быть состояние MouseOver, когда указатель мыши наведен на кнопку, или состояние Pressed, когда кнопка нажата. У большинства элементов управления есть несколько состояний. И мы можем управлять переходом в эти состояния.

// Чтобы изменить внешний вид в зависимости от состояния элемента используется объект ViewState. Объект ViewState через другой объект - Storyboard позволяет определить анимацию, изменяющую внешний вид элемента.

// Так, используем несколько состояний, определенных в элементе Button:
<Button x:Name="myButton" Content="Привет" Height="40" Width="100">
    <Button.Template>
        <ControlTemplate TargetType="Button">
            <Border CornerRadius="25"
                BorderBrush="{TemplateBinding BorderBrush}"
                BorderThickness="{TemplateBinding BorderThickness}"
                Height="{TemplateBinding Height}"
                Width="{TemplateBinding Width}">
                 
                <Border.Background>
                    <SolidColorBrush x:Name="BorderColor" Color="LightPink" />
                </Border.Background>
                <ContentControl Margin="{TemplateBinding Padding}"
                    HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                    VerticalAlignment="{TemplateBinding VerticalContentAlignment}"
                    Content="{TemplateBinding Content}" />
                                 
                <VisualStateManager.VisualStateGroups>
                    <VisualStateGroup Name="CommonStates">
                        <VisualState Name="MouseOver">
                            <Storyboard>
                                <ColorAnimation Storyboard.TargetName="BorderColor"
                                    Storyboard.TargetProperty="Color" To="LightBlue" />
                            </Storyboard>
                        </VisualState>
                        <VisualState Name="Normal">
                            <Storyboard>
                                <ColorAnimation Storyboard.TargetName="BorderColor"
                                    Storyboard.TargetProperty="Color" To="LightPink" />
                            </Storyboard>
                        </VisualState>
                    </VisualStateGroup>
                </VisualStateManager.VisualStateGroups>
            </Border>
        </ControlTemplate>
    </Button.Template>
</Button>

// При применении данного шаблона при наведении мыши на кнопку она будет приобретать светло-синий цвет, а если мышь покинет пределы кнопки, цвет опять станет розовой.

// За счет чего это происходит? Корневой элемент Border имеет объект VisualStateManager, у которого, в свою очередь, есть коллекция объектов VisualStateGroup. В данном случае у нас используется только один объект VisualStateGroup - CommonStates. Данная группа содержит такие состояния как MouseOver, Pressed, Normal. А объекты VisualState как раз и задают состояния и применяемую к ним анимацию.

// Анимацией управляет объект Storyboard. Его свойство TargetName указывает на имя объекта, который анимируется, а TargetProperty - свойство объекта, которое анимируется.

// Если объект VisualState используется для визуализации элемента в определенном состоянии, то объект VisualTransition визуализирует элемент в момент перехода от одного состояния в другое. Так, если мы задали два визуальных состояния, то мы можем определить визуальный переход из одного состояния в другое:
<Button x:Name="myButton" Content="Привет" Height="40" Width="100">
    <Button.Template>
        <ControlTemplate TargetType="Button">
            <Border CornerRadius="25"
                BorderBrush="{TemplateBinding BorderBrush}"
                BorderThickness="{TemplateBinding BorderThickness}"
                Height="{TemplateBinding Height}"
                Width="{TemplateBinding Width}">
                 
                <Border.Background>
                    <SolidColorBrush x:Name="BorderColor" Color="LightPink" />
                </Border.Background>
                <ContentControl Margin="{TemplateBinding Padding}"
                    HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                    VerticalAlignment="{TemplateBinding VerticalContentAlignment}"
                    Content="{TemplateBinding Content}" />
                                 
                <VisualStateManager.VisualStateGroups>
                    <VisualStateGroup Name="CommonStates">
                        <VisualState Name="MouseOver">
                            <Storyboard>
                                <ColorAnimation Storyboard.TargetName="BorderColor"
                                    Storyboard.TargetProperty="Color" To="LightBlue" />
                            </Storyboard>
                        </VisualState>
                        <VisualState Name="Normal">
                            <Storyboard>
                                <ColorAnimation Storyboard.TargetName="BorderColor"
                                    Storyboard.TargetProperty="Color" To="LightPink" />
                            </Storyboard>
                        </VisualState>
                        <VisualStateGroup.Transitions>
                            <VisualTransition From="MouseOver" To="Normal"
                                    GeneratedDuration="0:0:1.5">
                                <Storyboard>
                                    <ColorAnimationUsingKeyFrames Storyboard.TargetName="BorderColor"
       Storyboard.TargetProperty="Color" FillBehavior="HoldEnd">
   <ColorAnimationUsingKeyFrames.KeyFrames>
       <LinearColorKeyFrame Value="Yellow" KeyTime="0:0:0.5" />
       <LinearColorKeyFrame Value="Red" KeyTime="0:0:1" />
       <LinearColorKeyFrame Value="Green" KeyTime="0:0:1.5" />
   </ColorAnimationUsingKeyFrames.KeyFrames>
                                    </ColorAnimationUsingKeyFrames>
                                </Storyboard>
                            </VisualTransition>
                        </VisualStateGroup.Transitions>
                    </VisualStateGroup>
                </VisualStateManager.VisualStateGroups>
            </Border>
        </ControlTemplate>
    </Button.Template>
</Button>

// В данном случае в момент перехода опять же анимируется фон кнопки, только теперь вся анимация длится 1.5 секунды, в течение которых кнопка последовательно приоретает желтый, красный и зеленый цвет, после чего возвращается в исходное состояние.
// В чем отличие использования визуальных состояний от триггеров? Триггер определяет действие и когда оно происходит, а визуальное состояние описывает КАК происходит действие.

