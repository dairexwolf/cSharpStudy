// В качестве возвращаемого типа в асинхронном методе должны использоваться типы void, Task, Task<T> или ValueTask<T>

//									void
// При использовании ключевого слова void асинхронный метод ничего не возвращает:

using System;
using System.Threading;
using System.Threading.Tasks;
class Program
{
    static void Factorial(int n)
    {
        int result = 1;
        for (int i = 1; i <= n; i++)
            result *= i;
        Console.WriteLine($"Факториал равен {result}");
    }

    // определение асинхронного метода
    static async Task FactorialAsync(int n)
    {
        await Task.Run(() => Factorial(n));
    }

    static void Main()
    {
        FactorialAsync(5);
        FactorialAsync(6);
        Console.WriteLine("Работает");
        Console.Read();
    }
}

// Формально метод FactorialAsync не использует оператор return для возвращения результата. Однако если в асинхронном методе выполняется в выражении await асинхронная операция, то мы можем возвращать из метода объект Task.

// 													Task
// Метод может возвращать некоторое значение. Тогда возвращаемое значение оборачивается в объект Task, а возвращаемым типом является Task<T>:

using System;
using System.Threading.Tasks;
 
class Program
{
	static int Factorial(int n)
	{
		int res = 1;
		for (int i=1; i <= n; i++)
			res *= i;
		Console.WriteLine($"Факториал равен: {res}");
	}
	
	// определение асинхронного метода
	static async Task FactorialAsync(int n)
	{
		await Task.Run(() => Factorial(n));
	}
	
	static void Main()
	{
		FactorialAsync(5);
		FactorialAsync(6);
		Console.WriteLine($"Работаем");
		Console.Read();
	}
}

// Формально метод FactorialAsync не использует оператор return для возвращения результата. Однако если в асинхронном методе выполняется в выражении await асинхронная операция, то мы можем возвращать из метода объект Task.
// 													Task<T>
// Метод может возвращать некоторое значение. Тогда возвращаемое значение оборачивается в объект Task, а возвращаемым типом является Task<T>:

using System;
using System.Threading.Tasks;

class Program
{
    static int Factorial(int n)
    {
        int result = 1;
        for (int i = 0; i <= n; i++)
            result *= i;
        return result;
    }

    // определение асинхронного метода
    static async Task<int> FactorialAsync(int n)
    {
        return await Task.Run(() => Factorial(n));
    }

    static async Task Main()	// Требуется C# 7.1
    {
        int n1 = await FactorialAsync(5);
        int n2 = await FactorialAsync(6);
        Console.WriteLine($"n1 = {n1}/; \tn2 = {n2}");
        Console.Read();
    }
}

// В данном случае функция Factorial возвращает значение типа int. В асинхронном методе FactorialAsync мы получаем и возвращаем это число. Поэтому возвращаемым типом в данном случае является типа Task<int>. Если бы метод Factorial возвращал строку, то есть данные типа string, то возвращаемым типом асинхронного метода был бы тип Task<string>

// Чтобы получить результат асинхронного метода в методе Main, который тоже определен как асинхронный, применяем оператор await при вызове FactorialAsync.

// ValueTask<T>
// Использование типа ValueTask<T> во многом аналогично применению Task<T> за исключением некоторых различий в работе с памятью, поскольку ValueTask - структура, а Task - класс. По умолчанию тип ValueTask недоступен, и чтобы использовать его, вначале надо установить через NuGet пакет System.Threading.Tasks.Extensions.

using System;
using System.Threading.Tasks;

class Program
{
	static int Factorial(int n)
	{
		int res = 1;
		for (int i=1;i<=n;i++)
			res *= i;
		return res;
	}
	
	// определение асинхронного метода
	static async Valuetask<int> FactorialAsync(int n)
	{
		return await Task.Run(() => Factorial(n));
	}
	
	static async Task main()
	{
		int n1 = await FactorialAsync(5);
		int n2 = await factorialAsync(6);
		Console.WriteLine($"n1={n1}\tn2 = {n2}");
		Console.Read();
	}
}