// Класс AutoResetEvent также служит целям синхронизации потоков. Этот класс является оберткой над объектом ОС Windows "событие" и позволяет переключить данный объект-событие из сигнального в несигнальное состояние. Так, пример из предыдущей темы мы можем переписать с использованием AutoResetEvent следующим образом:

class Program
{
	// Во-первых, создаем переменную типа AutoResetEvent. Передавая в конструктор значение true, мы тем самым указываем, что создаваемый объект изначально будет в сигнальном состоянии.
	static AutoResetEvent waitHandler = new AutoResetEvent(true);
	static int x = 0;
	static void Main()
	{
		for(int i = 0; i<5; i++)
		{
			Thread myThread = new Thread(new ThreadStart(Count));
			myThread.Name = "Поток " + i.ToString();
			myThread.Start();
		}
	}
	public static void Count()
	{
		waitHandler.WaitOne();	// Когда начинает работать поток, то первым делом срабатывает определенный в методе Count вызов waitHandler.WaitOne(). Метод WaitOne указывает, что текущий поток переводится в состояние ожидания, пока объект waitHandler не будет переведен в сигнальное состояние. И так все потоки у нас переводятся в состояние ожидания.
		x = 1;
		for (int i = 1; i<9; i++)
		{
			Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
			x++;
			Thread.Sleep(100);
		}
		waitHandler.Set();	// После завершения работы вызывается метод waitHandler.Set, который уведомляет все ожидающие потоки, что объект waitHandler снова находится в сигнальном состоянии, и один из потоков "захватывает" данный объект, переводит в несигнальное состояние и выполняет свой код. А остальные потоки снова ожидают.
	}
}

// Так как в конструкторе AutoResetEvent мы указываем, что объект изначально находится в сигнальном состоянии, то первый из очереди потоков захватывает данный объект и начинает выполнять свой код.
// Но если бы мы написали AutoResetEvent waitHandler = new AutoResetEvent(false), тогда объект изначально был бы в несигнальном состоянии, а поскольку все потоки блокируются методом waitHandler.WaitOne() до ожидания сигнала, то у нас попросту случилась бы блокировка программы, и программа не выполняла бы никаких действий.

// Если у нас в программе используются несколько объектов AutoResetEvent, то мы можем использовать для отслеживания состояния этих объектов методы WaitAll и WaitAny, которые в качестве параметра принимают массив объектов класса WaitHandle - базового класса для AutoResetEvent.

// Так, мы тоже можем использовать WaitAll в вышеприведенном примере. Для этого надо строку waitHandler.WaitOne(); заменить на следующую:

class Program
{
	// Во-первых, создаем переменную типа AutoResetEvent. Передавая в конструктор значение true, мы тем самым указываем, что создаваемый объект изначально будет в сигнальном состоянии.
	static AutoResetEvent waitHandler = new AutoResetEvent(true);
	static int x = 0;
	static void Main()
	{
		for(int i = 0; i<5; i++)
		{
			Thread myThread = new Thread(new ThreadStart(Count));
			myThread.Name = "Поток " + i.ToString();
			myThread.Start();
		}
	}
	public static void Count()
	{
		AutoResetEvent.WaitAll( new WaitHandle[] {waitHandler});
		x = 1;
		for (int i = 1; i<9; i++)
		{
			Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
			x++;
			Thread.Sleep(100);
		}
		waitHandler.Set();	// метод waitHandler.Set, который уведомляет все ожидающие потоки, что объект waitHandler снова находится в сигнальном состоянии, и один из потоков "захватывает" данный объект, переводит в несигнальное состояние и выполняет свой код. А остальные потоки снова ожидают.
	}
}