// В предыдущем примере мы рассмотрели, как запускать в отдельных потоках методы без параметров. А что, если нам надо передать какие-нибудь параметры в поток?

// Для этой цели используется делегат ParameterizedThreadStart. Его действие похоже на функциональность делегата ThreadStart. Рассмотрим на примере:

using System;
using System.Threading;

class Program
{
	public static void Count (object x)
	{
		int n = (int)x;
		for (int i = 1; i < 9; i++)
		{
			Console.WriteLine($"Второй поток: {i*n}");
			Thread.Sleep(500);
		}
	}
	
	static void Main()
	{
		int number = 4;
		// создаем новый поток
		Thread myTh = new Thread(new ParameterizedThreadStart(Count));
		myTh.Start(number);
		
		for (int i = 1; i < 9; i++)
		{
			Console.WriteLine($"Главный поток: {i*i}");
			Thread.Sleep(300);
		}
	}
}

// После создания потока мы передаем метод myThread.Start(number); переменную, значение которой хотим передать в поток.

// При использовании ParameterizedThreadStart мы сталкиваемся с ограничением: мы можем запускать во втором потоке только такой метод, который в качестве единственного параметра принимает объект типа object. Поэтому в данном случае нам надо дополнительно привести переданное значение к типу int, чтобы его использовать в вычислениях.

// Но что делать, если нам надо передать не один, а несколько параметров различного типа? В этом случае на помощь приходит классовый подход:

public class Counter
{
	public int x;
	public int y;
}

class Program
{
	public static void Count(object obj)
	{
		Counter c = (Counter)obj;
		
		for (int i = 1; i < 9; i++)
			Console.WriteLine($"Второй поток: {i * c.x * c.y}");
	}
	

	
	static void Main()
	{
		Counter counter = new Counter();
		counter.x = 4;
		counter.y = 5;
		
		Thread myThread = new Thread(new ParameterizedThreadStart(Count));
		myThread.Start(counter);
		
		for (int i=1; i<9; i++)
			Console.WriteLine($"Главный поток: {i*i}");
	}
}

// Сначала определяем специальный класс Counter, объект которого будет передаваться во второй поток, а в методе Main передаем его во второй поток.

// Но тут опять же есть одно ограничение: метод Thread.Start не является типобезопасным, то есть мы можем передать в него любой тип, и потом нам придется приводить переданный объект к нужному нам типу. Для решения данной проблемы рекомендуется объявлять все используемые методы и переменные в специальном классе, а в основной программе запускать поток через ThreadStart. Например:

public class Counter
{
	private int x;
	private int y;
	
	public Counter(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
	public void Count()
	{
		for (int i = 1; i < 9; i++)
		{
			Console.WriteLine($"Второй поток: {i*x*y}");
			Thread.Sleep(400);
		}
	}
}

class Program
{
	static void Main()
	{
		Counter counter = new Counter(4,5);
		
		Thread myThread = new Thread(new ThreadStart(counter.Count));
		myThread.Start();
		
		for(int i=1;i<9;i++)
		{
			Console.WriteLine("Главный поток: " +i*i);
			Thread.Sleep(300);
		}
	}
}

// Ещё при передаче параметра типа object можно передавать не класс, а массив object[] , а потом его приводить в методе сначала к массиву, а потом к нужным типам по индексу.Конечно так выходить чуть больше кода, зато для передачи параметров не нужно создавать отдельные классы. Да много чего можно: например кортежи, словари, коллекции - это ж все объекты )

// Метод в качестве возвращаемого значения должен принимать void, иначе компилятор ругается