/* Свойство ShutdownMode
Свойство ShutdownMode указывает на способ выхода из приложения и может принимать одно из следующих значений:

OnMainWindowClose: приложение работает, пока открыто главное окно

OnLastWindowClose: приложение работает, пока открыто хотя бы одно окно

OnExplicitShutdown: приложение работает, пока не будет явно вызвано Application.Shutdown()

*/

// Задать свойство ShutdownMode можно в коде xaml:
<Application x:Class="LifecycleApp.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:LifecycleApp"
             StartupUri="MainWindow.xaml" 
			 ShutdownMode="OnLastWindowClose" > 

// либо определить в App.xaml.cs:
public partial class App : Application
    {
        public App()
        {
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;
        }
    }

// 																	События приложения
/* Класс Application определяет ряд событий, который могут использоваться для всего приложения:

* Startup: происходит после вызова метода Application.Run() и перед показом главного окна

* Activated: происходит, когда активизируется одно из окон приложения

* Deactivated: возникает при потере окном фокуса

* SessionEnding: происходит при завершении сеанса Windows при перезагрузке, выключении или выходе из системы текущего пользователя

* DispatcherUnhandledException: возникает при возникновении необработанных исключени

* LoadCompleted: возникает при завершении загрузки приложения

* Exit: возникает при выходе из приложения Это может быть закрытие главного или последнего окна, метод Application.Shutdown() или завершение сеанса

*/

// Так же, как и для элементов, обработчики события определяются для одноименных атрибутов в разметке xaml. Например, обработаем событие Startup:
<Application x:Class="LifecycleApp.App"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:local="clr-namespace:LifecycleApp"
        StartupUri="MainWindow.xaml"  
		Startup="App_Startup">
    <Application.Resources>
          
    </Application.Resources>
</Application>

// В файле связанного кода App.xaml.cs пропишем обработчик события:
namespace LifecycleApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Запуск одной копии приложения
        System.Threading.Mutex mutex;

        private void AppStart(object sender, StartupEventArgs e)
        {
            bool createdNew;
            string mutName = "Приложение";
            mutex = new System.Threading.Mutex(true, mutName, out createdNew);
            if (!createdNew)
                this.Shutdown();
        }
    }
}

// В результате при запуске каждой новой копии данного приложение обработчик события будет определять, запущенj ли уже приложение, и если оно запущено, то данная копия завершит свою работу.

//																		Создание заставки приложения
// Приложения бывают разные, одни открываются быстро, другие не очень. И чтобы пользователя как-то известить о том, что идет загрузка приложения, нередко используют заставку или сплеш-скрин. В WPF простые заставки на основе изображений очень легко сделать. Для этого добавьте в проект какое-нибудь изображение и после добавления установите для него в окне Properties (Свойства) для свойства BuildAction значение SplashScreen. И после запуска до появления окна приложения на экране будет висеть изображение заставки.
// https://www.youtube.com/watch?v=_bUxlHL-62Q
// 																Обращение к текущему приложению
// Мы можем обратиться к текущему приложению из любой точки данного приложения. Это можно сделать с помощью свойства Current, определенном в классе Application. Так, изменим код нажатия кнопки на следующий:

// Таким образом, при нажатии на кнопку мы получим одно или несколько сообщений с именами всех окон данного приложения.
















