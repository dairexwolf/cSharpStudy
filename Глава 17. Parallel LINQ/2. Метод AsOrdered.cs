// При выполнении параллельного запроса порядок данных в результирующей выборки может быть не предсказуем. Например:

static int Factorial(int x)
{
	int res = 1;
	for (int i = 1; i<=x; i++)
		res *= i;
	return res;
}

static void Main()
{
	int[] numbers = new int[] { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
	var factorials = from n in numbers.AsParallel() where n>0 select Factorial(n);
	// var factorials = numbers.AsParallel().Where( x => x > 0).Select(x => Factorial(x));
	
	foreach (var n in factorials)
		Console.WriteLine(n);
	
}

// То есть данные склеиваются в общий набор неупорядоченно. А если в запросе применяются операторы или методы сортировки в запросе, данные автоматически упорядочиваются:

var facts = from n in numbers.AsParallel() where n > 0 orderby n select Factorial(n);

// Однако не всегда оператор orderby или метод OrderBy используются в запросах. Более того они упорядочивают результирующую выборку в соответствии с результатами, а не в соответствии с исходной последовательностью. В этих случаях мы может применять метод AsOrdered():

var factorials = from n in numbers.AsParallel().AsOrdered() where n > 0 select Factorial(n);
// Метод расширения
var factorials = numbers.AsParallel().AsOrdered().Select(x => Factorial(x));

// В этом случае результат уже будет упорядоченным в соответствии с тем, как элементы располагаются в исходной последовательности

// В то же время надо понимать, что упорядочивание в параллельной операции приводит к увеличению издержек, поэтому подобный запрос будет выполняться медленнее, чем неупорядоченный. И если задача не требует возвращение упорядоченного набора, то лучше не применять метод AsOrdered.

// Кроме того, если в программе предстоят какие-нибудь манипуляции с полученным набором, однако упорядочивание больше не требуется, мы можем применить метод AsUnordered():

var factorials = from n in numbers.AsParallel().AsOrdered() where n > 0 select Factorial(n);

var query = from n in factorials.AsUnordered() where n>100 select n;