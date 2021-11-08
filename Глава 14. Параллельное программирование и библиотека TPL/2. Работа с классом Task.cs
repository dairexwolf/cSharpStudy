// Вложенные задачи

// Одна задача может запускать другую - вложенную задачу. При этом эти задачи выполняются независимо друг от друга. Например:

static void Main()
{
	var outer = Task.Factory.StartNew(() =>		// внешняя задача
	{
		Console.WriteLine("Outer task starting...");
		var inner = Task.Factory.StartNew(() =>	// вложенная задача
		{
			Console.WriteLine("Inner task starting...");
			Thread.Sleep(2000);
			Console.WriteLine("Inner task finished.");
		});
		Console.WriteLine("Outer task finished.");
	});
	outer.Wait();		// ожидаем выполнение внешней задачи
	
Console.WriteLine("Main is finished.");
Console.ReadLine();
	
}

// Несмотря на то, что здесь мы ожидаем выполнения внешней задачи, но вложенная задача может завершить выполнение даже после завершения метода Main
/* Result:
Outer task starting...
Outer task finished.
Main is finished.
Inner task starting...
Inner task finished.
*/

// Если необходимо, чтобы вложенная задача выполнялась вместе с внешней, необходимо использовать значение TaskCreationOptions.AttachedToParent:

static void Main()
{
	Task outer = Task.Factory.StartNew(() =>			// внешняя задача
	{
		Console.WriteLine("Outer task starting...");
		Task inner = Task.Factory.StartNew(() =>		// вложенная задача
		{
			Console.WriteLine("Inner task starting...");
			Thread.Sleep(2000);
			Console.WriteLine("Inner task finished.");
		}, TaskCreationOptions.AttachedToParent);
		Console.WriteLine("Outer task finished.");
	});
	outer.Wait();		// ожидаем выполнение внешней задачи
	Console.WriteLine("Main is finished.");
	Console.ReadLine();
}

/* Result:
Outer task starting...
Outer task finished.
Inner task starting...
Inner task finished.
Main is finished.
*/

// 												Массив задач
// Также как и с потоками, мы можем создать и запустить массив задач. Можно определить все задачи в массиве непосредственно через объект Task:
Task[] tasks = new Task[3]
{
	new Task(() => Console.WriteLine("First Task")),
	new Task(() => Console.WriteLine("Second Task")),
	new Task(() => Console.WriteLine("Third Task"))
};
//Запуск задач в массиве
foreach (Task t in tasks)
	t.Start();
Console.ReadLine();

// Либо также можно использовать методы Task.Factory.StartNew или Task.Run и сразу запускать все задачи:
Task[] tasks2 = New Task[3];
int j = 1;
int l = tasks2.Length;
for (int i = 0; i < l; i++) 	
{
	tasks2[i] = Task.Factory.StartNew(() => Console.WriteLine($"Task {j++}");
}


// Но в любом случае мы опять же можем столкнуться с тем, что все задачи из массива могут завершиться после того, как отработает метод Main, в котором запускаются эти задачи:

static void Main()
{
	Task[] tasks = new Task[3]
	{
		new Task(() => Console.WriteLine("First Task")),
		new Task(() => Console.WriteLine("Second Task")),
		new Task(() => Console.WriteLine("Third Task"))
	};
	foreach (Task ts in tasks)
		ts.Start();
	Task[] tasks2 = new Task[3];
	int j = 1;
	int length = tasks2.Length;
	for (int i = 0; i < length; i++)
		tasks2[i] = Task.Factory.StartNew(() => Console.WriteLine($"Task {j++}"));
	
	Console.WriteLine("Завершение метода Main");
	
	Console.ReadLine();
}

// Если необходимо выполнять некоторый код лишь после того, как все задачи из массива завершатся, то применяется метод Task.WaitAll(tasks):

 static void Main()
        {
            Task[] tasks = new Task[3]
            {
        new Task(() => Console.WriteLine("First Task")),
        new Task(() => Console.WriteLine("Second Task")),
        new Task(() => Console.WriteLine("Third Task"))
            };
            foreach (Task ts in tasks)
                ts.Start();
            Task.WaitAll(tasks);                            // Ждет, пока завершатся все задачи
            Task[] tasks2 = new Task[3];
            int j = 1;
            int length = tasks2.Length;
            for (int i = 0; i < length; i++)
                tasks2[i] = Task.Factory.StartNew(() => Console.WriteLine($"Task {j++}"));
            Task.WaitAny(tasks2);                           // ждет, пока завершится одна задача
            Console.WriteLine("Завершение метода Main");

            Console.ReadLine();
        }
// 										Возвращение результатов из задач
// Задачи могут не только выполняться как процедуры, но и возвращать определенные результаты:

class Program
{
    static void Main()
    {
        Task<int> task1 = new Task<int>(() => Factorial(5));
        task1.Start();
        Console.WriteLine($"Факториал числа 5 равен {task1.Result}");

        Task<Book> task2 = new Task<Book>(() =>
        {
            return new Book { Title = "Война и мир", Autor = "Толстый Лев" };
        });
        task2.Start();

        Book b = task2.Result;   // записываем результат в переменную
        Console.WriteLine($"Название книги: {b.Title}, автор: {b.Autor}");
        Console.ReadKey();

    }
    static int Factorial(int x)
    {
        int result = 1;
        for (int i = 1; i <= x; i++)
            result *= i;
        return result;
    }

    public class Book
    {
        public string Title { get; set; }
        public string Autor { get; set; }
    }
}

// Во-первых, чтобы задать возвращаемый из задачи тип объекта, мы должны типизировать Task. Например, Task<int> - в данном случае задача будет возвращать объект int.
// И, во-вторых, в качестве задачи должен выполняться метод, возвращающий данный тип объекта. Например, в первом случае у нас в качестве задачи выполняется функция Factorial, которая принимает числовой параметр и также на выходе возвращает число.
// Возвращаемое число будет храниться в свойстве Result: task1.Result. Нам не надо его приводить к типу int, оно уже само по себе будет представлять число.
// То же самое и со второй задачей task2. В этом случае в лямбда-выражении возвращается объект Book. И также мы его получаем с помощью task2.Result
// !!!При этом при обращении к свойству Result программа текущий поток останавливает выполнение и ждет, когда будет получен результат из выполняемой задачи.