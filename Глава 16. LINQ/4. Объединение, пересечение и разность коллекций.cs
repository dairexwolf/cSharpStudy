// Кроме методов выборки LINQ имеет несколько методов, который позволяют из двух последовательностей объектов сгенерировать множество (то есть некий набор уникальных элементов): разность, объединение и пересечение.
// 													Разность последовательностей
// С помощью метода Except можно получить разность двух последовательностей:

string[] soft = { "Microsoft", "Google", "Apple" }; // из этого
string[] hard = { "Apple", "IBM", "Samsung" };      // удаляем разность этого

// разность последовательностей

var result = soft.Except(hard);     // не будет Apple
string[] res = new string[6];
int i = 0;
foreach (string s in result)
    {
       res[i] = s;
        Console.WriteLine(res[i]);
    }

// В данном случае из массива soft убираются все элементы, которые есть в массиве hard. Результатом операции будут два элемента:
Microsoft
Google

// Пересечение последовательностей
// Для получения пересечения последовательностей, то есть общих для обоих наборов элементов, применяется метод Intersect:

string[] soft = { "Microsoft", "Google", "Apple" }; // из этого
string[] hard = { "Apple", "IBM", "Samsung" };      // удаляем все, чего нет в этом массиве

var result = soft.Intersect(hard);
foreach (string s in result)
	Console.WriteLine(s);

// Так как оба набора имеют только один общий элемент, то соответственно только он и попадет в результирующую выборку:
Apple

//                								Объединение последовательностей
// Для объединения двух последовательностей используется метод Union. Его результатом является новый набор, в котором имеются элементы, как из первой, так и из второй последовательности. Повторяющиеся элементы добавляются в результат только один раз:

var result = soft.Union(hard);

foreach (string s in result)
	Console.WriteLine(s);

// Если же нам нужно простое объединение двух наборов, то мы можем использовать метод Concat:

var result = soft.Concat(hard);
// Те элементы, которые встречаются в обоих наборах, дублируются.

// 												Удаление дубликатов
// Для удаления дублей в наборе используется метод Distinct:
var result = soft.Concat(hard).Distinct();	// Последовательное применение методов Concat и Distinct будет подобно действию метода Union.

string[] soft = { "Microsoft", "Google", "Apple", "Google", "Apple", "Microsoft" };
var result = soft.Distinct();
foreach (string s in result)
	Console.writeLine(s);
