// Для сортировки набора данных по возрастанию используется оператор orderby:

int[] numbers = { 3, 12, 6, 10, 34, 20, 55, -66, 75, 82, 4 };

var orderedNums = 	from i in numbers
					orderby i
					select i;
// var orderedNums = numbers.OrderBy(i => i);
foreach (int i in orderedNums)
	Console.WriteLine(i);

// Оператор orderby принимает критерий сортировки. В данном случае в качестве критерия выступает само число.

// Возьмем посложнее пример. Допустим, надо отсортировать выборку сложных объектов. Тогда в качестве критерия мы можем указать свойство класса объекта:

List<User> users = new List<User>()
{
	new User { Name = "Tom", Age = 33 },
	new User { Name = "Bob", Age = 30 },
	new User { Name = "Tom", Age = 21 },
	new User { Name = "Sam", Age = 43 }
};

var sortedUsers = 	from u in users
					orderby u.Name
					select u;
// var sortedUsers = users.OrderBy(u => u.Name);

foreach (User u in sortedUsers)
	Console.WriteLine(u.Name);

// По умолчанию оператор orderby производит сортировку по возрастанию. Однако с помощью ключевых слов ascending (сортировка по возрастанию) и descending (сортировка по убыванию) можно явным образом указать направление сортировки:

var sortedUsers = from u in users orderby u.Name descending	select u;		// по убыванию
var sortedUsers = from u in users orderby u.Name ascending select u;		// по возрастанию

// var sortedUsers = users.OrderByDescending(u => u.Name);					// по убыванию
// var sortedUsers = users.OrderBy(u => u.Name);							// по возрастанию


//																					Множественные критерии сортировки
// В наборах сложных объектов иногда встает ситуация, когда надо отсортировать не по одному, а сразу по нескольким полям. Для этого в запросе LINQ все критерии указываются в порядке приоритета через запятую:

List<User> users = new List<User>()
{
	new User { Name = "Tom", Age = 33 },
	new User { Name = "Bob", Age = 30 },
	new User { Name = "Tom", Age = 21 },
	new User { Name = "Sam", Age = 43 }
};
var result = from user in users orderby user.Name, user.Age select user;
foreach (User u in result)
	Console.WriteLine($"{u.Name} - {u.Age}");

// С помощью методов расширения то же самое можно сделать через метод ThenBy()(для сортировки по возрастанию) и ThenByDescending() (для сортировки по убыванию):

var result = users.OrderBy(u => u.Name).
