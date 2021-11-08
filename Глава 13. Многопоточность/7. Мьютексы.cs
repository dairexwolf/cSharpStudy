// Еще один инструмент управления синхронизацией потоков представляет класс Mutex, также находящийся в пространстве имен System.Threading. Данный класс является классом-оболочкой над соответствующим объектом ОС Windows "мьютекс". Перепишем пример из прошлой темы, используя мьютексы:

class Program
{
	// Сначала создаем объект мьютекса
	static Mutex mutexObj = new Mutex();
	static int x = 0;
	
	static void Main()
	{
		for int i=0; i<5; i++
		{
			Thread myThread = new Thread(Count);
			myThread.Name = "Поток" + ToString(i);
			myThread.Start();
		}
		
	}
	
	public static void Count()
	{
		// // Метод mutexObj.WaitOne() приостанавливает выполнение потока до тех пор, пока не будет получен мьютекс mutexObj.
		mutexObj.WaitOne();
		x = 1;
		for (int i = 1; i < 9; i++)
		{
			Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
			x++;
			Thread.Sleep(100);
		}
		// После выполнения всех действий, когда мьютекс больше не нужен, поток освобождает его с помощью метода mutexObj.ReleaseMutex()
		mutexObj.ReleaseMutex();
	}
}

// Основную работу по синхронизации выполняют методы WaitOne() и ReleaseMutex(). 
// Таким образом, когда выполнение дойдет до вызова mutexObj.WaitOne(), поток будет ожидать, пока не освободится мьютекс. И после его получения продолжит выполнять свою работу.



