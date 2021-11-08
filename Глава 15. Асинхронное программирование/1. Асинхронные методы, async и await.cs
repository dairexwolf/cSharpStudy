// Асинхронность позволяет вынести отдельные задачи из основного потока в специальные асинхронные методы или блоки кода. Особенно это актуально в графических программах, где продолжительные задачи могу блокировать интерфейс пользователя. И чтобы этого не произошло, нужно задействовать асинхронность. Также асинхронность несет выгоды в веб-приложениях при обработке запросов от пользователей, при обращении к базам данных или сетевым ресурсам. При больших запросах к базе данных асинхронный метод просто уснет на время, пока не получит данные от БД, а основной поток сможет продолжить свою работу. В синхронном же приложении, если бы код получения данных находился в основном потоке, этот поток просто бы блокировался на время получения данных.

// Ключевыми для работы с асинхронными вызовами в C# являются два ключевых слова: async и await, цель которых - упростить написание асинхронного кода. Они используются вместе для создания асинхронного метода.

/*
Асинхонный метод обладает следующими признаками:

 В заголовке метода используется модификатор async

 Метод содержит одно или несколько выражений await

 В качестве возвращаемого типа используется один из следующих:

	void

	Task

	Task<T>

	ValueTask<T>


 Асинхронный метод, как и обычный, может использовать любое количество параметров или не использовать их вообще. Однако асинхронный метод не может определять параметры с модификаторами out и ref.

Также стоит отметить, что слово async, которое указывается в определении метода, не делает автоматически метод асинхронным. Оно лишь указывает, что данный метод может содержать одно или несколько выражений await.
*/

// Асинхронный метод, как и обычный, может использовать любое количество параметров или не использовать их вообще. Однако асинхронный метод не может определять параметры с модификаторами out и ref.

// Также стоит отметить, что слово async, которое указывается в определении метода, не делает автоматически метод асинхронным. Оно лишь указывает, что данный метод может содержать одно или несколько выражений await.

using System;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
	class Program
	{
		// обычный метод
		static void Factorial()
		{
			int result = 1;
			for (int i = 1; i <= 6; i++)
				result *= i;
			Thread.Sleep(8000);
			Console.WriteLine($"Факториал равен {result}");
		}
		
		// определение асинхронного метода
		static async void FactorialAsync()
		{
			Console.WriteLine("Начало метода FactorialAsync");	// выполняется синхронно
			await Task.Run(()=>Factorial());					// выполняется асинхронного
			Console.WriteLine("Конец метода FactorialAsync");
		}
		
		static void Main()
		{
			FactorialAsync();	// вызов асинхронного метода
			Console.WriteLine("Напишите пока что нибудь");
			string str = Console.ReadLine();
			Console.WriteLine("Мы сохранили что вы написали и отправили в ФСБ");
			Console.Read();
		}
	}
}

// Здесь прежде всего определен обычный метод подсчета факториала. Для имитации долгой работы в нем используется задержка на 8 секунд с помощью метода Thread.Sleep(). Условно это некоторый метод, который выполняет некоторую работу продолжительное время. Но для упрощения понимания он просто подсчитывает факториал числа 6.
// Также здесь определен асинхронный метод FactorialAsync(). Асинхронным он является потому, что имеет в определении перед возвращаемым типом модификатор async, его возвращаемым типом является void, и в теле метода определено выражение await.
// Выражение await определяет задачу, которая будет выполняться асинхронно. В данном случае подобная задача представляет выполнение функции факториала
await Task.Run(()=>Factorial());

// По негласным правилам в названии асинхроннных методов принято использовать суффикс Async - FactorialAsync(), хотя в принципе это необязательно делать.
// Сам факториал мы получаем в асинхронном методе FactorialAsync. Асинхронным он является потому, что он объявлен с модификатором async и содержит использование ключевого слова await.

// И в методе Main мы вызываем этот асинхронный метод.

/*
Разберем поэтапно, что здесь происходит:

Запускается метод Main, в котором вызывается асинхронный метод FactorialAsync.

Метод FactorialAsync начинает выполняться синхронно вплоть до выражения await.

Выражение await запускает асинхронную задачу Task.Run(()=>Factorial())

Пока выполняется асинхронная задача Task.Run(()=>Factorial()) (а она может выполняться довольно продожительное время), выполнение кода возвращается в вызывающий метод - то есть в метод Main. В методе Main нам будет предложено ввести число для вычисления квадрата числа.

В этом и преимущество асинхронных методов - асинхронная задача, которая может выполняться довольно долгое время, не блокирует метод Main, и мы можем продолжать работу с ним, например, вводить и обрабатывать данные.

Когда асинхронная задача завершила свое выполнение (в случае выше - подсчитала факториал числа), продолжает работу асинхронный метод FactorialAsync, который вызвал асинхронную задачу.
*/

// Функция факториала, возможно, представляет не самый показательный пример, так как в реальности в данном случае нет смысла делать ее асинхронной. Но рассмотрим другой пример - чтение-запись файла:

/*
using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace AppAsync
{
    class Program
    {
        static async void ReadWriteAsync()
        {
            string s = "Hello world! One step at a time";

            // hello.txt - файл, в который будет записываться и считываться
            using (StreamWriter writer = new StreamWriter("hello.txt", false))
            {
                await writer.WriteLineAsync(s); // асинхронная запись в файл
            }
            using (StreamReader reader = new StreamReader("hello.txt"))
            {
                string result = await reader.ReadToEndAsync();  // асинхронное чтение из файла
                Console.WriteLine(result);
            }
        }

        static void Main()
        {
            ReadWriteAsync();
            Console.WriteLine("Идет работа");
            Console.Read();
        }
    }
}
*/

using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace AppAsync
{
	class Program
	{
		static async void ReadWriteAsync(string str)
		{
			string s = "Новая строчка";
			
			// hello.txt - файл, в который будет записываться и считываться
			// Асинхронный метод ReadWriteAsync() выполняет запись в файл некоторой строки и затем считывает записанный файл. Подобные операции могут занимать продолжительное время, особенно при больших объемах данных, поэтому такие операции лучше делать асинхронными.
			using (StreamWriter writer = new StreamWriter("hello.txt", false))
			{
				writer.WriteLine(s);				// синхронная запись в файл
				await writer.WriteLineAsync(str);	// асинхронная запись в файл
				// Во фреймворке .NET Core определено много подобных методов. Как правило, они связаны с работой с файлами, отправкой сетевых запросов или запросов к базе данных. Их легко узнать по суффиксу Async. То есть если метод имеет подобный суффикс в названии, то с большей степенью вероятности его можно использовать в выражении await.
			}
			using (StreamReader reader = new StreamReader("hello.txt"))
			{
				string result = await reader.ReadToEndAsync();	// асинхронное чтение из файла
				Console.WriteLine(result);
			}
		}
		
		static void Main()
		{
			string text = "";
			for (;;)
			{
				Console.WriteLine("Введите то, что хотите записать");
				text = Console.ReadLine();
				// Далее в методе Main вызывается асинхронный метод ReadWriteAsync:
				ReadWriteAsync(text);
				// И опять же, когда выполнение в методе ReadWriteAsync доходит до первого выражения await, управление возвращается в метод Main, и мы можем продолжать с ним работу. Запись в файл и считывание файла будут производиться параллельно и не будут блокировать работу метода Main.
				Console.WriteLine("Идет работа");
				Console.WriteLine("Введите \"q\" для выхода из цикла");
				text = Console.ReadLine();
				if (text == q)
					break;
				Console.WriteLine("Новая запись");
			}
		}
	}
}

// 														Определение асинхронной операции
// Как выше уже было сказано, фреймворк .NET Core имеет много встроенных методов, которые представляют асинхронную операцию. Они заканчиваются на суффикс Async. И перед вызывами подобных методов мы можем указывать оператор await. Например:

StreamWriter writer = new StreamWriter("hello.txt", false);
await writer.WriteLineAsync("Hello");

// Либо мы сами можем определить асинхронную операцию, используя метод Task.Run():

static void Factorial()
{
	int result = 1;
	for (int i = 1; i <=6; i++)
	{
		result *=i;
	}
	Thread.Sleep(8000);
	Console.WriteLine($"Факториал = {result}");
	
} 
// определение асинронного метода
static async void FactorialAsync()
{
	await Task.Run(()=>Factorial());	// вызов асинхронной  операции
	
}

// Можно определить асинхронную операцию с помощью лямбда-выражения:
static async void FactorialAsync()
{
	await Task.Run(() =>
	{
		int result = 1;
		for (int i; i <= 6; i++)
			result *= i;
		Thread.Sleep(8000);
		Console.WriteLine($"Факториал равен {result}");
	});
}

// 										Передача параметров в асинхронную операцию
// Выше вычислялся факториал 6, но, допустим, мы хотим вычислять факториалы разных чисел:

using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
	static void Factorial (int n)
	{
		int result = 1;
		for (int i = 1; i <= n; i++)
			result *= i;
		Thread.Sleep(3000);
		Console.WriteLine($"Факториал {n} равен {result}");
	}
	
	// определение асинхронного метода
	static async void FactorialAsync (int n)
	{
		await Task.Run(()=>Factorial(n));
	}
	
	static void Main()
	{
		for (int i=1; i<=10; i++)
			FactorialAsync(i);
		Console.WriteLine("Пошла жара");
		Console.Read();
	}
}

// 									Получение результата из асинхронной операции
// Асинхронная операция может возвращать некоторый результат, получить который мы можем так же, как и при вызове обычного метода:

using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
	static int Factorial (int n)
	{
		int result = 1;
		for (int i = 1; i <= n; i++)
			result *= i;
		Thread.Sleep(3000);
		return result;
	}
	
	// определение асинхронного метода
	static async void FactorialAsync (int n)
	{
		int x = await Task.Run(()=>Factorial(n));
		Console.WriteLine($"Факториал {n} равен {x}");
	}
	
	static void Main()
	{
		for (int i=1; i<=10; i++)
			FactorialAsync(i);
		Console.WriteLine("Пошла жара");
		Console.Read();
	}
}

// Метод Factorial возвращает значение типа int, это значение мы можем получить, просто присвоив результат асинхронной операции переменной данного типа: int x = await Task.Run(()=>Factorial(n));