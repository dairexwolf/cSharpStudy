// Параллельное выполнение задач может занимать много времени. И иногда может возникнуть необходимость прервать выполняемую задачу. Для этого .NET предоставляет класс CancellationToken:

static void Main()
{
	// Для отмены операции нам надо создать и использовать токен. Вначале создается объект CancellationTokenSource:
	CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
	// Затем из него получаем сам токен:
	CancellationToken token = cancelTokenSource.Token;
	
	int number = 6;
	
	Task task1 = new Task(() =>
	{
		int result = 1;
		for (int i = 1; i <= number; i++)
		{
			// В самой операции мы можем отловить выставление токена с помощью условной конструкции:
			if (token.IsCancellationRequested)
			{
				Console.WriteLine("Операция прервана");
				return;
			}
			result *= i;
			Console.WriteLine($"Факториал числа {number} равен {result}");
			Thread.Sleep(5000);
		}
	});
	task1.Start();
	
	ConsoleWriteLine("Введите Y для отмены операции или другой символ для ее продолжения");
	string s = Console.ReadKey();
	// Чтобы отменить операцию, необходимо вызвать метод Cancel() у объекта CancellationTokenSource:
	if (s == "Y")
		cancelTokenSource.Cancel();
	Console.Read();
}

// Если операция представляет внешний метод, то ему надо передавать в качестве одного из параметров токен:

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{

    static void Main()
    {
		// Для отмены операции нам надо создать и использовать токен. Вначале создается объект CancellationTokenSource:
		CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
		// Затем из него получаем сам токен:
        CancellationToken token = cancelTokenSource.Token;

        Task task = new Task(() => Factorial(5, token));	// передаем токен функции
        task.Start();

        Console.WriteLine("Введите Y для отмены операции или другой символ для ее продолжения");
        string s = Console.ReadLine();
        // Чтобы отменить операцию, необходимо вызвать метод Cancel() у объекта CancellationTokenSource:
		if (s == "Y")
            cancelTokenSource.Cancel();

        Console.ReadLine();
    }

    static void Factorial(int x, CancellationToken token)
    {
        int result = 1;
        for (int i = 1; i <= x; i++)
        {
			// В самой операции мы можем отловить выставление токена с помощью условной конструкции:
            if (token.IsCancellationRequested)
            {
                Console.WriteLine("Операция прервана токеном");
                return;
            }
            result *= i;
            Console.WriteLine($"Факториал числа {x} равен {result}");
            Thread.Sleep(5000);
        }

    }
}

// 													Отмена параллельных операций Parallel
// Для отмены выполнения параллельных операций, запущенных с помощью методов Parallel.For() и Parallel.ForEach(), можно использовать перегруженные версии данных методов, которые принимают в качестве параметра объект ParallelOptions. Данный объект позволяет установить токен:

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

static void Factorial(int x)
{
	int result = 1;
	
	for (int i = 1; i <= x; i++)
		result *= i;
	Console.WriteLine($"Факториал числа {x} равен {result}");
	Thread.Sleep(3000);
}

static void Main()
{
	CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
	CancellationToken token = cancelTokenSource.Token;
	
	new Task(() =>
	{
		Thread.Sleep(400);
		cancelTokenSource.Cancel();
	}).Start();
	
	try
	{
		Parallel.ForEach<int> (new List<int>() {1, 2, 3, 4, 5, 6, 7, 8 },
						new ParallelOptions { CancellationToken = token}, Factorial);
		// более полнятно
		//Parallel.For(1, 8, new ParallelOptions { CancellationToken = token }, Factorial);
		
	}
	catch (OperationCanceledException ex)
	{
		Console.WriteLine("Операция прервана");
	}
	finally
	{
		cancelTokenSource.Dispose();
	}
	Console.ReadLine();
}

// В параллельной запущеной задаче через 400 миллисекунд происходит вызов cancelTokenSource.Cancel(), в результате программа выбрасывает исключение OperationCanceledException, и выполнение параллельных операций прекращается.