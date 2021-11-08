// Наряду с оператором lock для синхронизации потоков мы можем использовать мониторы, представленные классом System.Threading.Monitor. Фактически конструкция оператора lock из прошлой темы инкапсулирует в себе синтаксис использования мониторов. А рассмотренный в прошлой теме пример будет эквивалентен следующему коду:

class Program
{
	static int x = 0;
	static object locker = new object();
	static void Count()
	{
		bool acquiredLock = false;
		try
		{
			Monitor.Enter(locker, ref acquiredLock);
			x = 1;
			for (int i = 1; i<9; i++)
			{
				Thread.Sleep(100);
				Console.WriteLine($"{Thread.CurrentThread.Name}: {x}");
				x++;
			}
		}
		finally
		{
			if(acquiredLock) Monitor.Exit(locker);
		}
	}
	
	static void Main()
	{
		for (int i = 1; i<6; i++)
		{
			Thread myThread = new Thread(new ThreadStart(Count));
			myThread.Name = "Поток: " + i.ToString();
			myThread.Start();
		}
	}
}

// Метод Monitor.Enter принимает два параметра - объект блокировки и значение типа bool, которое указывает на результат блокировки (если он равен true, то блокировка успешно выполнена). Фактически этот метод блокирует объект locker так же, как это делает оператор lock. С помощью А в блоке try...finally с помощью метода Monitor.Exit происходит освобождение объекта locker, если блокировка осуществлена успешно, и он становится доступным для других потоков.

// Кроме блокировки и разблокировки объекта класс Monitor имеет еще ряд методов, которые позволяют управлять синхронизацией потоков. 
// Так, метод Monitor.Wait освобождает блокировку объекта и переводит поток в очередь ожидания объекта. Следующий поток в очереди готовности объекта блокирует данный объект. 
// А все потоки, которые вызвали метод Wait, остаются в очереди ожидания, пока не получат сигнала от метода Monitor.Pulse или Monitor.PulseAll, посланного владельцем блокировки. Если метод Monitor.Pulse отправил сигнал, то поток, находящийся во главе очереди ожидания, получает сигнал и блокирует освободившийся объект. Если же метод Monitor.PulseAll отправлен, то все потоки, находящиеся в очереди ожидания, получают сигнал и переходят в очередь готовности, где им снова разрешается получать блокировку объекта.

// Вот другой пример. Задача вывести в консоль "One - " и "Two" с помощью двух разных методов в разных потоках.

class Program
    {
        static object locker = new object();
        static void Main()
        {
            Thread writeOneThread = new Thread(() => WriteOneOrTwoFiveTimes("One"));
            Thread writeTwoThread = new Thread(() => WriteOneOrTwoFiveTimes("Two"));


            writeOneThread.Start();
            writeTwoThread.Start();
            writeOneThread.Join();
            writeTwoThread.Join();


            Console.WriteLine("Well done!");
        }
        static void WriteOne(bool isRunning)
        {
            lock (locker)
            {
                if (!isRunning)
                {
                    Monitor.Pulse(locker);  /*Снимаем блокировку с локера*/
                    return;                 /*завершаем работу метода*/
                }


                Console.Write("One - ");


                Monitor.Pulse(locker);      /*Снимаем блокировку с локера*/
                Monitor.Wait(locker);       /*Останавливаем работу потока и ожидаем снятия блокировки с локера (сигнала от Monitor.Pulse(locker) вызваного другим потоком)*/
            }
        }
        static void WriteTwo(bool isRunning)
        {
            lock (locker)
            {
                if (!isRunning)
                {
                    Monitor.Pulse(locker);
                    return;
                }
                Console.WriteLine("Two");
                Monitor.Pulse(locker);
                Monitor.Wait(locker);
            }
        }
        static void WriteOneOrTwoFiveTimes(string oneOrTwo)
        {
            if (oneOrTwo == "One")
            {
                for (int i = 0; i < 5; i++)
                    WriteOne(true);    /*Вызываем метод 5 раз*/
                WriteOne(false);      /*Вызываем для разблокировки lockerа и завершения работы*/
            }
            else
            {
                for (int i = 0; i < 5; i++)
                    WriteTwo(true);
                WriteTwo(false);
            }
        }
    }
	
// "locker"- это объект-заглушка для оператора "lock". Поток, доходя до кода обернутого в оператор "lock", проверяет объект "locker" и если он не заблокирован , то блокирует его и идёт работать дальше. Если "locker" заблокирован - поток становится в очередь и ждет освобождения этого объекта. Monitor.Pulse(locker) снимает блокировку локера и один поток может пройти, заблокировав тем самым "locker". Monitor.Wait(locker) заставляет текущий поток притормозить и проверить локер, если локер заблокирован - значит нужно ждать когда его разблокируют, чтобы иметь возможность продолжить выполнения кода. Так что мы таки блокируем locker, а потоки жду его разблокировки.

