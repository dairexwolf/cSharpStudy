// По умолчанию все элементы коллекции в LINQ обрабатываются последовательно, но начиная с .NET 4.0 в пространство имен System.Linq был добавлен класс ParallelEnumerable, который инкапсулирует функциональность PLINQ (Parallel LINQ) и позволяет выполнять обращения к коллекции в параллельном режиме.
// При обработке коллекции PLINQ использует возможности всех процессоров в системе. Источник данных разделяется на сегменты, и каждый сегмент обрабатывается в отдельном потоке. Это позволяет произвести запрос на многоядерных машинах намного быстрее.
// В то же время по умолчанию PLINQ выбирает последовательную обработку данных. Переход к параллельной обработке осуществляется в том случае, если это приведет к ускорению работы. Однако, как правило, при параллельных операциях возрастают дополнительные издержки. Поэтому если параллельная обработка потенциально требует больших затрат ресурсов, то PLINK в этом случае может выбрать последовательную обработку, если она не требует больших затрат ресурсов.
// Поэтому смысл применения PLINQ имеется преимущественно на больших коллекциях или при сложных операциях, где действительно выгода от распараллеливания запросов может перекрыть возникающие при этом издержки.
// Также следует учитывать, что при доступе к общему разделяемому состоянию в параллельных операциях будет неявно использоваться синхронизация, чтобы избежать взаимоблокировки доступа к этим общим ресурсам. Затраты на синхронизацию ведут к снижению производительности, поэтому желательно избегать или ограничивать применения в параллельных операциях разделяемых ресурсов.
// 													Метод AsParallel
// Метод AsParallel() позволяет распараллелить запрос к источнику данных. Он реализован как метод расширения LINQ у массивов и коллекций. При вызове данного метода источник данных разделяется на части (если это возможно) и над каждой частью отдельно производятся операции.
// Рассмотрим простейший пример нахождения факториалов чисел:

static void Main()
{
	int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
	var facts = from n in numbers.AsParallel() select Factorial(n);
	foreach (var n in facts)
		Console.WriteLine(n);
		
}

static int Factorial(int x)
{
	int res = 1;
	for (int i = 1; i <= x; i++)
		res *= i;
	Console.WriteLine($"Факториал числа {x} равен {res}");
	return res;
}

// Фактически здесь обычный запрос LINQ, только к источнику данных применяется метод AsParallel. Результат работы программы говорит о том, что данные выбирались для нахождения факториала не последовательно. То есть произошло распараллеливание работы программы.

// С помощью методов расширения:
var facts = numbers.AsParallel().Select( x => Factorial(x));

// 														Метод ForAll
// Выше рассмотренный код по подсчету факториала можно еще больше оптимизировать с точки зрения параллелизации. В частности, для вывода результата параллельной операции используется цикл foreach. Но его использование приводит к увеличению издержек - необходимо склеить полученные в разных потоках данные в один набор и затем их перебрать в цикле. Более оптимально в данном случае было бы использование метода ForAll(), который выводит данные в том же потоке, в котором они обрабатываются:

int[] numbers = new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8 };
(from n in numbers.AsParallel() where n > 0 select Factorial(n)).ForAll(Console.WriteLine);

// Метод ForAll() в качестве параметра принимает делегат Action, который указывает на выполняемое действие.
// Метод расширения
numbers.AsParallel().Where( x => x>0 ).Select(x => Factorial(x)).ForAll(Console.WriteLine);