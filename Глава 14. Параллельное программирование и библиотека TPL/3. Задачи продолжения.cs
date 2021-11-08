// Задачи продолжения или continuation task позволяют определить задачи, которые выполняются после завершения других задач. Благодаря этому мы можем вызвать после выполнения одной задачи несколько других, определить условия их вызова, передать из предыдущей задачи в следующую некоторые данные.

using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
	static void Display(Task t)
	{
		Console.WriteLine($"ID задачи: {Task.CurrentId}");
		Console.WriteLine($"ID предыдущей задачи: {t.Id}");
		Thread.Sleep(3000);
	}
	static void Main()
	{
		Task task1 = new Task(() => 
		{
			Console.WriteLine($"ID задачи: {Task.CurrentId}");
		});
		
		// задача продолжения
		Task task2 = task1.ContinueWith(Display);
		task1.Start();
		
		// Ждем окончание второй задачи
		task2.Wait();
		Console.WriteLine("Задачи выполнены");
		Console.ReadKey();
	}
}

// Первая задача задается с помощью лямбда-выражения, которое просто выводит id этой задачи. Вторая задача - задача продолжения задается с помощью метода ContinueWith, который в качестве параметра принимает делегат Action<Task>. 
// 
// То есть метод Display, который передается в данный метод в качестве значения параметра, должен принимать параметр типа Task. Благодаря передачи в метод параметра Task, мы можем получить различные свойства предыдущей задачи, как например, в данном случае получает ее Id.

// И после завершения задачи task1 сразу будет вызываться задача task2. Также мы можем передавать конкретный результат работы предыдущей задачи:

class Program
{
    static void Main()
    {
        Task<int> task1 = new Task<int>(() => Sum(4, 5));

        // задача продолжения
        Task task2 = task1.ContinueWith(sum => Display(sum.Result));

        task1.Start();

        // ждем окончания второй задачи
        task2.Wait();
        Console.WriteLine("End of Main");
        Console.ReadKey();
    }

    static int Sum(int a, int b) => a + b;
    static void Display(int sum) => Console.WriteLine($"Sum = {sum}");
}

// Подобным образом можно построить целую цепочку последовательно выполняющихся задач:

class Program
{
    static void Main()
    {
        Task task1 = new Task(() =>
        {
            Console.WriteLine($"task1. ID задачи: {Task.CurrentId}");
        });

        // задача продолжения 
        Task task2 = task1.ContinueWith(Display);
        Task task3 = task1.ContinueWith((Task t) =>
        {
            Console.WriteLine($"task3. Id задачи: {Task.CurrentId}");
        });

        Task task4 = task2.ContinueWith((Task t) => Console.WriteLine($"task4. Id задачи: {Task.CurrentId}"));
        task1.Start();
        Console.ReadKey();

    }

    static void Display(Task t)
    {
        Console.WriteLine($"task2. Id задачи: {Task.CurrentId}");
    }
}