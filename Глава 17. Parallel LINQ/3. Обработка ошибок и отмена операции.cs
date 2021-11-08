// При выполнении параллельных операций также могут возникать ошибки, обработка которых имеет свои особенности. При параллельной обработке коллекция разделяется а части, и каждая часть обрабатывается в отдельном потоке. Однако если возникнет ошибка в одном из потоков, то система прерывает выполнение всех потоков.

// При генерации исключений все они агрегируются в одном исключении типа AggregateException

// Например, пусть в метод факториала передается массив объектов, который содержит не только числа, но и строки:

object[] numbers2 = new object[] { 1, 2, 3, 4, 5, "hello" };

var facts = from n in numbers2.AsParallel() 
// Выражение приведение типов
let x = (int)n 
select Factorial(x);

try
{
	facts.ForAll( n => Console.WriteLine(n));
}
catch (AggregateException)
{
	foreach (var e in ex.InnerExceptions)
		Console.WriteLine(e.Message);
}

// Так как массив содержит строку, то попытка приведения закончится неудачей, и при запуске приложения в Visual Studio в режиме отладки выполнение остановится на строке преобразования. А после продолжения сработает перехват исключения в блоке catch, и на консоль будет выведено сообщение об ошибке.

// 												Прерывание параллельной операции

// Вполне вероятно, что нам может потребоваться прекратить операцию до ее завершения. В этом случае мы можем использовать метод WithCancellation(), которому в качестве параметра передается токен CancellationToken:

static int Factorial(int x)
{
	int res = 1;
	for (int i = 1; i <= x; i++)
		res *= i;
	Console.WriteLine($"Факториал числа {x} равен {res}");
	Thread.Sleep(1000); 
	return res;
}

static void Main()
{
	CancellationTokenSource cts = new CancellationTokenSource();
	new Task (() =>
	{
		Thread.Sleep(400);
		cts.Cancel();
	}).Start();
	
	try
	{
		int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
		var factorials = from n in numbers.AsParallel().WithCancellation(cts.Token)
		select Factorial(n);
		foreach (var n in factorials)
			Console.WriteLine(n);
	}
	catch (OperationCanceledException ex)
	{
		Console.WriteLine("Операция была прервана");
	}
	catch (AggregateException ex)
	{
		if (ex.InnerExceptions != null)
		{
			foreach (Exception e in ex.InnerExceptions)
				Console.WriteLine(e.Message);
		}
	}
	finally
	{
		cts.Dispose();
	}
}

// В параллельной запущенной задаче вызывается метод cts.Cancel(), что приводит к завершению операции и генерации исключения OperationCanceledException:

// При этом также имеет смысл обрабатывать исключение AggregateException, так как если параллельно возникает еще одно исключение, то это исключение, а также OperationCanceledException помещаются внутрь одного объекта AggregateException.
