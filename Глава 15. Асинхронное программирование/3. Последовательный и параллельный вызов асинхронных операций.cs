// Асинхронный метод может содержать множество выражений await. Когда система встречает в блоке кода оператор await, то выполнение в асинхронном методе останавливается, пока не завершится асинхронная задача. После завершения задачи управление переходит к следующему оператору await и так далее. Это позволяет вызывать асинхронные задачи последовательно в определенном порядке. Например:
using System;
using System.Threading.Tasks;

class Program
{
	static void Factorial(int n)
	{
		int res = 1;
		for (int i = 1; i <= n; i++)
			res *= i;
		Console.WriteLine($"Факториал числа {n} = {res}");
	}
	
	// определение асинхронного метода
	static async void FactorialAsync()
	{
		await Task.Run(() => Factorial(4));	// сначало это
		await Task.Run(() => Factorial(3));	// потом это
		await Task.Run(() => Factorial(5)); // и в конце это
	}
	static void Main()
	{
		FactorialAsync();
		Console.Read();
	}
}
// То есть мы видим, что факториалы вычисляются последовательно. И в данном случае вывод строго детерминирован.

// Нередко такая последовательность бывает необходима, если одна задача зависит от результатов другой.

// Однако не всегда существует подобная зависимость между задачами. В этом случае мы можем запустить все задачи параллельно и через метод Task.WhenAll отследить их завершение. Например, изменим метод FactorialAsync:

static async void FactorialAsync()
{
	Task t1 = Task.Run(() => Factorial(4));
	Task t2 = Task.Run(() => Factorial(3));
	Task t3 = Task.Run(() => Factorial(5));
	await Task.WhenAll(new[] { t1, t2, t3 });
}

// Вначале запускаются три задачи. Затем Task.WhenAll создает новую задачу, которая будет автоматически выполнена после выполнения всех предоставленных задач, то есть задач t1, t2, t3. А с помощью оператора await ожидаем ее завершения.

// В итоге все три задачи теперь будут запускаться параллельно, однако вызывающий метод FactorialAsync благодаря оператору await все равно будет ожидать, пока они все не завершатся. И в этом случае вывод программы не детерминирован.

// И если задача возвращает какое-нибудь значение, то это значение потом можно получить с помощью свойства Result.

static int Factorial(int n)
{
	int res = 1;
	for (int i=1; i <= n; i++)
		res *= i;
	return res;
}

// определение асинхронного метода
static async void FactorialAsync()
{
	Task<int> t1 = Task.Run(() => Factorial(4));
	Task<int> t2 = Task.Run(() => Factorial(3));
	Task<int> t3 = Task.Run(() => Factorial(5));
	await Task.WhenAll(new[] { t2, t3, t1 });
	
	Console.WriteLine($"Result 1 = {t1.Result}");
	Console.WriteLine($"Result 2 = {t2.Result}");
	Console.WriteLine($"Result 3 = {t3.Result}");
}

// На собесе был вопрос. Мы запускаем параллельно 2 потока, они выполняют какую-то длительную работу. Мы знаем, что потом А выполнится быстрее потока B. Есть ли возможность после завершения А передать его результат потоку B?

// Task<t>.Result позволяет передать результат работы одного потока другому. Он приостанавливает поток, который его вызвал, пока не будет получен результат. В итоге не важно, какой поток выполнится быстрее, тот поток "которому нужен результат" подождёт.
static void Main()
      {
            Task<int> task1 = new Task<int>(() =>
            {
                Thread.Sleep(5000);
                return 1;
            });


            Task task2 = new Task(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine($"Task1 resut:{task1.Result}");
            });


            task1.Start();
            task2.Start();


            Console.ReadLine();
       }
	   
// По другому
static int Factorial(int n)
{
	int res = n*n-n;
	int time = n*1000-5;
	Thread.Sleep(time);
	return res;
}

static async void FactorialAsync()
{
	Task<int> t1 = Task.Run(() => Factorial(4));
	Task<int> t2 = Task.Run(() => Factorial(t1.Result));
	Task<int> t3 = Task.Run(() => Factorial(t2.Result));
	await Task.WhenAll(new[] { t2, t3, t1 });
	
	Console.WriteLine($"Result 1 = {t1.Result}");
	Console.WriteLine($"Result 2 = {t2.Result}");
	Console.WriteLine($"Result 3 = {t3.Result}");
}