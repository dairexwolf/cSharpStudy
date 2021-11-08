// Для отмены асинхронных операций используются классы CancellationToken и CancellationTokenSource.
// CancellationToken содержит информацию о том, надо ли отменять асинхронную задачу. Асинхронная задача, в которую передается объект CancellationToken, периодически проверяет состояние этого объекта. Если его свойство IsCancellationRequested равно true, то задача должна остановить все свои операции.
// Для создания объекта CancellationToken применяется объект CancellationTokenSource. Кроме того, при вызове у CancellationTokenSource метода Cancel() у объекта CancellationToken свойство IsCancellationRequested будет установлено в true.

// Рассмотрим применение этих классов на примере:

using System;
using System.Threading;
using System.Threading.Tasks;

class App
{
	static void Factorial (int n, CancellationToken token)
	{
		int res = 1;
		for (int i = 1; i <= n; i++)
		{
			if (token.IsCancellationRequested)
			{
				Console.WriteLine("Операция прервана токеном");
				return;
			}
			res *= i;
			Console.WriteLine($"Факториал числа {i} равен: {res}");
			Thread.Sleep(1000);
		}
	}
	
	// определение асинхронного метода
	static async void FactorialAsync(int n, CancellationToken token)
	{
		if (token.IsCancellationRequested)
			return;
		await Task.Run(() => Factorial(n, token));
	}
	static void Main()
	{
		CancellationTokenSource cts = new CancellationTokenSource();
		CancellationToken token = cts.Token;
		FactorialAsync(6, token);
		Thread.Sleep(4000);
		cts.Cancel();
		Console.Read();
	}
}

// Для создания токена определяется объект CancellationTokenSource. Метод FactorialAsync в качестве параметра принимает токен, и если где-то во внешнем коде произойдет отмена операции через вызов cts.Cancel, то в методе Factorial свойство token.IsCancellationRequested будет равно true, и соответственно при очередной итерации цикла в методе Factorial произойдет выход из метода. И асинхронная операция завершится. 