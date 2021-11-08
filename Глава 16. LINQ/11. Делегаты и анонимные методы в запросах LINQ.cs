// Хотя, как правило, в качестве параметров в методах расширения LINQ удобно использовать лямбда-выражения. Но лямбда-выражения являются сокращенной нотацией анонимных методов. И если мы обратимся к определению этих методов, то увидим, что в качестве параметра многие из них принимают делегаты типа Func<TSource, bool>.
// Например, определение метода Where:
public static IEnumerable<TSource> Where<TSource>(
this IEnumerable<TSource> source,
Func<TSource, bool> predicate
)

// Зададим параметры через делегаты:

int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };

Func<int, bool> MoreThenTen = delegate(int i) { return i > 10; };

var result = numbers.Where(MoreThenTen);

foreach (int i in result)
Console.WriteLine(i);

// Так как набор элементов, к которому применяется метод Where, содержит объекты int, то в делегат в качестве параметра передается объект этого типа. Возвращаемые тип должен представлять тип bool: если true, то объект int соответствует условию и попадает в выборку.

// Альтернативный поход представляет перемещение всей логики в отдельный метод:

static void Main()
{
	int[] numbers = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
	var result = numbers.Where(MoreThanTen);
	foreach (int i in result)
	Console.WriteLine(i);
	Console.Read();
}
private static bool MoreThanTen(int i)
{
	return i > 10;
}

// Рассмотрим другой пример. Пусть метод Select() добавляет в выборку не текущий элемент-число, а его факториал:

static void Main()
{
	int[] nums = { -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
	
	var result = nums.Where(i => i > 0).Select(Factorial);
	
	foreach (int i in result)
		Console.WriteLine(i);
}

private static int Factorial(int x)
{
	int result = 1;
	for (int i = 1; i <= x; i++)
		result *= 1;
	return result;
}

// Метод Select в качестве параметра принимает тип Func<TSource, TResult> selector. Так как у нас набор объектов int, то входным параметром делегата также будет объект типа int. В качестве типа выходного параметра выберем int, так как факториал числа - это целочисленное значение.