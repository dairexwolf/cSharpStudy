// Обработка ошибок в асинхронных методах, использующих ключевые слова async и await, имеет свои особенности.

// Для обработки ошибок выражение await помещается в блок try:

using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Factorial(int n)
    {
        if (n < 1)
            throw new Exception($"{n}: число не должно быть меньше 1");

        int result = 1;
        for (int i = 1; i <= n; i++)
            result *= i;
        Console.WriteLine("Факториал числа {1} равен: {0}", result, n);
    }
    static async void FactorialAsync(int n)
    {
        try
        {
            await Task.Run(() => Factorial(n));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void Main()
    {
        for (int i = -1; i < 4; i++)
        {
            FactorialAsync(i);
        }
        FactorialAsync(-4);
        FactorialAsync(5);
        Console.Read();
    }
}

// В данном случае метод Factorial генерирует исключение, если методу передается число меньше 1. Для обработки исключения в методе FactorialAsync выражение await помещено в блок try. В методе Main вызывается асинхронный метод с передачей ему отрицательного числа: FactorialAsync(-4), что привет к генерации исключения. Однако программа не остановит аварийно свою работу, а обработает исключение и продолжит дальнейшие вычисления.
// Следует учитывать, что если мы запускаем данный код в режиме отладки в Visual Studio, то VS просигнализирует нам о генерации исключении и остановит выполнение а строке throw new Exception($"{n} : число не должно быть меньше 1");. В режиме запуска без отладки VS не будет останавливаться.

// 													Исследование исключения
// При возникновении ошибки у объекта Task, представляющего асинхронную задачу, в которой произошла ошибка, свойство IsFaulted имеет значение true. Кроме того, свойство Exception объекта Task содержит всю информацию об ошибке. 
// Чтобы проинспектировать свойство, изменим метод FactorialAsync следующим образом:

static async void FactorialAsync(int n)
{
	Task ts = null;
	try
	{
		ts = Task.Run(() => Factorial(n));
		await ts;
	}
	catch (Exception ex)
	{
		Console.WriteLine(ts.Exception.InnerException.Message);	// Сообщение об ошибке
		Console.WriteLine($"IsFaulted: {ts.IsFaulted}");		// И если мы передадим в метод число -1, то task.IsFaulted будет равно true.
	}
}

// 													Обработка нескольких исключений. WhenAll
// Если мы ожидаем выполнения сразу нескольких задач, например, с помощью Task.WhenAll, то мы можем получить сразу несколько исключений одномоментно для каждой выполняемой задачи.
// В этом случае мы можем получить все исключения из свойства Exception.InnerExceptions:

static async Task DoMultipleAsync()
{
	Task allTasks = null;
	
	try
	{
		Task t1 = Task.Run(() => Factorial(-3));
		Task t2 = Task.Run(() => Factorial(-5));
		Task t3 = Task.Run(() => Factorial(-10));
		allTasks = Task.WhenAll(t1, t2, t3);
		await allTasks;
	}
	catch (Exception ex)
	{
		Console.WriteLine("Исключение: " + ex.Message);
		Console.WriteLine("IsFaulted: "+ allTasks.IsFaulted);
		foreach (var inx in allTasks.Exception.InnerExceptions)
		{
			Console.WriteLine("Внутреннее исключение: " + inx.Message);
		}
	}
}

//											await в блоках catch и finally
//Начиная с версии C# 6.0 в язык была добавлена возможность вызова асинхронного кода в блоках catch и finally. Так, возьмем предыдущий пример с подсчетом факториала:

static async void FactorialAsync(int n)
{
    try
    {
        await Task.Run(() => Factorial(n)); ;
    }
    catch (Exception ex)
    {
        await Task.Run(()=>Console.WriteLine(ex.Message));
    }
    finally
    {
        await Task.Run(() => Console.WriteLine("await в блоке finally"));
    }
}


