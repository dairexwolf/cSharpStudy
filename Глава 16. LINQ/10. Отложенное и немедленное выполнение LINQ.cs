// Есть два способа выполнения запроса LINQ: отложенное и немедленное выполнение.

// При отложенном выполнении LINQ-выражение не выполняется, пока не будет произведена итерация или перебор по выборке. Рассмотрим отложенное выполнение:

string[] teams = { "Liquid", "Na'Vi", "Virtus.Pro", "Spirit", "Nakovalya", "NAut" };

var selectedTeams = from t in teams where t.ToUpper().StartsWith("N") orderby t select t;

// выполнение LINQ запроса
foreach (string s in selectedTeams)
	Console.WriteLine(s);

// То есть фактическое выполнение запроса происходит не в строке определения: var selectedTeams = from t..., а при переборе в цикле foreach.

/*
Фактически LINQ-запрос разбивается на три этапа:

1. Получение источника данных

2. Создание запроса

3. Выполнение запроса и получение его результатов
*/
// Как это происходит в нашем случае:

//	1. Получение источника данных - определение массива teams:
string[] teams = { "Liquid", "Na'Vi", "Virtus.Pro", "Spirit", "Nakovalya", "NAut" };
// 2. Создание запроса - определение переменной selectedTeams:
var selectedTeams = from t in teams where t.ToUpper().StartsWith("N") orderby t select t;
// 3. Выполнение запроса и получение его результатов:
foreach (string s in selectedTeams)
	Console.WriteLine(s);

// После определения запроса он может выполняться множество раз. И до выполнения запроса источник данных может изменяться. Чтобы более наглядно увидеть это, мы можем изменить какой-либо элемент до перебора выборки:

var selectedTeams = from t in teams where t.ToUpper().StartsWith("N") orderby t select t;

// Изменение массива после определение LINQ-запроса
teams[5] = "ENCE";
// выполнение LINQ запроса
foreach (string s in selectedTeams)
	Console.WriteLine(s);

// Теперь выборка будет содержать два элемента, а не три, так как второй элемент после изменения не будет соответствовать условию.
// Важно понимать, что переменная запроса сама по себе не выполняет никаких действий и не возвращает никаких данных. Она только хранит набор команд, которые необходимы для получения результатов. То есть выполнение запроса после его создания откладывается. Само получение результатов производится при переборе в цикле foreach.

// 													Немедленное выполнение запроса
// С помощью ряда методов мы можем применить немедленное выполнение запроса. Это методы, которые возвращают одно атомарное значение или один элемент. Например, Count(), Average(), First() / FirstOrDefault(), Min(), Max() и т.д. Например, метод Count() возвращает числовое значение, которое представляет количество элементов в полученной последовательности. А метод First() возвращает первый элемент последовательности. Но чтобы выполнить эти методы, вначале надо получить саму последовательность, то есть результат запроса, и пройтись по ней циклом foreach, который вызывается неявно внутри структуры запроса.
// Рассмотрим пример с методом Count(), который возвращает число элементов последовательности:

string[] teams = { "Liquid", "Na'Vi", "Virtus.Pro", "Spirit", "Nakovalya", "NAut" };
// определение и выполнение LINQ-запроса
int i = (from t in teams where t.ToUpper().StartsWith("N") orderby t select t).Count();
Console.WriteLine(i);	// 3
teams[1] = "ENCE";
Console.WriteLine(i);	// 3

// Результатом метода Count будет объект int, поэтому сработает немедленное выполнение.
// Сначала создается запрос: from t in teams where t.ToUpper().StartsWith("N") orderby t select t. Далее к нему применяется метод Count(), который выполняет запрос, неявно выполняет перебор по последовательности элементов, генерируемой этим запросом, и возвращает число элементов в этой последовательности.

// Также мы можем изменить код таким образом, чтобы метод Count() учитывал изменения и выполнялся отдельно от определения запроса:

string[] teams = { "Liquid", "Na'Vi", "Virtus.Pro", "Spirit", "Nakovalya", "NAut" };
// определение LINQ запроса
var selectedTeams = from t in teams where t.ToUpper().StartsWith("N") orderby t select t;
// выполнение запроса
Console.WriteLine(selectedTeams.Count());	// 3
teams[1]  = "ENCE";
Console.WriteLine(selectedTeams.Count());	// 2

// Также для немедленного выполнения LINQ-запроса и кэширования его результатов мы можем применять методы преобразования ToArray<T>(), ToList<T>(), ToDictionary() и т.д.. Эти методы получают результат запроса в виде объектов Array, List и Dictionary соответственно. Например:

string[] teams = { "Liquid", "Na'Vi", "Virtus.Pro", "Spirit", "Nakovalya", "NAut" };
// определение LINQ запроса
var selectedTeams = (from t in teams where t.ToUpper().StartsWith("N") orderby t select t).ToList<string>();
// изменение массива никак не затронет список selectedTeams
teams[1] = "ENCE"
foreach (string s in selectedTeams)
Console.WriteLine(s);
