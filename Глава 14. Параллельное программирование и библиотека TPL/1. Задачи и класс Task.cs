// В эпоху многоядерных машин, которые позволяют параллельно выполнять сразу несколько процессов, стандартных средств работы с потоками в .NET уже оказалось недостаточно. Поэтому во фреймворк .NET была добавлена библиотека параллельных задач TPL (Task Parallel Library), основной функционал которой располагается в пространстве имен System.Threading.Tasks. Данная библиотека позволяет распараллелить задачи и выполнять их сразу на нескольких процессорах, если на целевом компьютере имеется несколько ядер. Кроме того, упрощается сама работа по созданию новых потоков. Поэтому начиная с .NET 4.0. рекомендуется использовать именно TPL и ее классы для создания многопоточных приложений, хотя стандартные средства и класс Thread по-прежнему находят широкое применение.

// В основе библиотеки TPL лежит концепция задач, каждая из которых описывает отдельную продолжительную операцию. В библиотеке классов .NET задача представлена специальным классом - классом Task, который находится в пространстве имен System.Threading.Tasks. Данный класс описывает отдельную задачу, которая запускается асинхронно в одном из потоков из пула потоков. Хотя ее также можно запускать синхронно в текущем потоке.

// Для определения и запуска задачи можно использовать различные способы. Первый способ создание объекта Task и вызов у него метода Start:

Task ts = new Task(() => Console.WriteLine("Hello Task!"));
ts.Start();

// В качестве параметра объект Task принимает делегат Action, то есть мы можем передать любое действие, которое соответствует данному делегату, например, лямбда-выражение, как в данном случае, или ссылку на какой-либо метод. То есть в данном случае при выполнении задачи на консоль будет выводиться строка "Hello Task!".

// А метод Start() запускает задачу

// Второй способ заключается в использовании статического метода Task.Factory.StartNew(). Этот метод также в качестве параметра принимает делегат Action, который указывает, какое действие будет выполняться. При этом этот метод сразу же запускает задачу:

Task task = Task.Factory.StartNew(() => Console.WriteLine("Hello Task!"));

// В качестве результата метод возвращает запущенную задачу.

// Третий способ определения и запуска задач представляет использование статического метода Task.Run():

Task task = Task.Run(() => Console.WriteLine("Hello Task!");

// Метод Task.Run() также в качестве параметра может принимать делегат Action - выполняемое действие и возвращает объект Task.

// https://overcoder.net/q/20620/%D0%B2-%D1%87%D0%B5%D0%BC-%D1%80%D0%B0%D0%B7%D0%BD%D0%B8%D1%86%D0%B0-%D0%BC%D0%B5%D0%B6%D0%B4%D1%83-taskrun-%D0%B8-taskfactorystartnew

/*
Второй метод Task.Run был представлен в более поздней версии.NET Framework (в.NET 4.5).

Однако первый метод Task.Factory.StartNew дает вам возможность определить много полезных вещей о потоке, который вы хотите создать, в то время как Task.Run не предоставляет этого.

Например, скажем, что вы хотите создать длинный поток задач. Если для этой задачи будет использоваться поток пула потоков, то это можно считать злоупотреблением пулом потоков.

Одна вещь, которую вы могли бы сделать, чтобы избежать этого, - это запустить задачу в отдельном потоке. Недавно созданный поток, который будет посвящен этой задаче и будет уничтожен, как только ваша задача будет завершена. Вы не можете достичь этого с помощью Task.Run, в то время как вы можете сделать это с помощью Task.Factory.StartNew, как Task.Factory.StartNew ниже:

Task.Factory.StartNew(..., TaskCreationOptions.LongRunning);
Как сказано здесь:

Итак, в.NET Framework 4.5 Developer Preview, weve представила новый метод Task.Run. Это никоим образом не устаревает Task.Factory.StartNew, а скорее просто следует рассматривать как быстрый способ использования Task.Factory.StartNew без необходимости указывать набор параметров. Его ярлык. Фактически Task.Run фактически реализуется с точки зрения той же логики, что и для Task.Factory.StartNew, просто передавая некоторые параметры по умолчанию. Когда вы передаете действие в задачу. Run:

Task.Run(someAction);
что в точности эквивалентно:

Task.Factory.StartNew(someAction, 
    CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
*/

// Напишем небольшую программу, где используем все эти способы:

using System;
using System.Threading.Tasks;
using System.Threading;

namespace HelloApp
{
	class Program
	{
		static void Main()
		{
			Task task = new Task(Display);
			task.Start();
			
			Console.WriteLine("Завершение метода Main");
			Console.ReadLine();
		}
		static void Display()
		{
			Console.WriteLine("Начало работы метода Display");
			Thread.Sleep(1000);
			Console.WriteLine("Завершение работы метода Display");
		}
	}
}

// Класс Task в качестве параметра принимает метод Display, который соответствует делегату Action. Далее чтобы запустить задачу, вызываем метод Start: task.Start(), и после этого метод Display начнет выполняться во вторичном потоке. В конце метода Main выводит некоторый маркер-строку, что метод Main завершился.

// Мы видим, что даже когда основной код в методе Main уже отработал, запущенная ранее задача еще не завершилась. Чтобы указать, что метод Main должен подождать до конца выполнения задачи, нам надо использовать метод Wait:

static void Main()
{
	Task ts = new Task(Display);
	ts.Start();
	ts.Wait();
	Console.WriteLine("Завершение метода Main");
	Console.ReadLine();
}

/*Класс Task имеет ряд свойств, с помощью которых мы можем получить информацию об объекте. Некоторые из них:

AsyncState: возвращает объект состояния задачи

CurrentId: возвращает идентификатор текущей задачи

Exception: возвращает объект исключения, возникшего при выполнении задачи

Status: возвращает статус задачи
*/

using System;
using System.Threading.Tasks;
using System.Threading;

namespace HelloApp
{
	class Program
	{
static void Main()
		{
			Task task = new Task(Display);
			task.Start();
			Console.WriteLine($"Объект состояния задачи: {task.AsyncState}");
			// Console.WriteLine($"Идентификатор текущей задачи: {ToString(task.CurrentId)}");
			Console.WriteLine($"Статус задачи: {task.Status}");
			task.Wait();
			Console.WriteLine("Завершение метода Main");
			Console.ReadLine();
		}
		
static void Display()
		{
			Console.WriteLine("Начало работы метода Display");

			Console.WriteLine("Завершение работы метода Display");
		}
	}
}