// 														Фильтрация
// Для выбора элементов из некоторого набора по условию используется метод Where. Например, выберем все четные элементы, которые больше 10.

// Фильтрация с помощью операторов LINQ:

int[] numbers = { 1,2,3,4,10,34,55,68,77,82 };

IEnumerable<int> evens = from i in numbers where i%2==0 && i>10 select i;
foreach (int i in evens)
Console.WriteLine(i);

// Здесь используется конструкция from: from i in numbers

// Тот же запрос с помощью метода расширения:

int[] numbers = { 1,2,3,4,10,34,55,68,77,82 };
IEnumerable<int> evens = numbers.Where( i => i % 2 == 0 && i > 10);

// Если выражение в методе Where для определенного элемента будет равно true (в данном случае выражение i % 2 == 0 && i > 10), то данный элемент попадает в результирующую выборку.

// 													Выборка сложных объектов
// Допустим, у нас есть класс пользователя:

class User
{
	public string Name{ get; set; }
	public int Age { get; set; }
	public List<string> Languages{ get; set; }
	public User()
	{
		Languages = new List<string>();
	}
}
// Создадим набор пользователей и выберем из них тех, которым больше 25 лет:

List<User> users = new List<User>
{
	new User {Name="Том", Age=23, Languages = new List<string> {"английский", "немецкий" }},
    new User {Name="Боб", Age=27, Languages = new List<string> {"английский", "французский" }},
    new User {Name="Джон", Age=29, Languages = new List<string> {"английский", "испанский" }},
    new User {Name="Элис", Age=24, Languages = new List<string> {"испанский", "немецкий" }}
};

var selectedUsers = from user in users where user.Age > 25 select user;
// Аналогичный запрос с помощью метода расширения Where:
// var selectedUsers = users.Where(u=> u.Age > 25);
foreach (User user in selectedUsers)
Console.WriteLine($"{user.Name} - {user.Age}");

// 													Сложные фильтры

// Теперь рассмотрим более сложные фильтры. Например, в классе пользователя есть список языков, которыми владеет пользователь. Что если нам надо отфильтровать пользователей по языку:

var selectedUsers = from user in users
                    from lang in user.Languages
                    where user.Age < 28
                    where lang == "английский"
                    select user;
// Для создания аналогичного запроса с помощью методов расширения применяется метод SelectMany:

var selectedUsers = users.SelectMany(u => u.Languages,				// последовательность, которую надо проецировать
					(u, l) => new { User = u, Lang = l })			// функцию преобразования, которая применяется к каждому элементу
					.Where(u => u.Lang =="английский" && u.User.Age < 28)
					.Select(u => u.User);

// Метод SelectMany() в качестве первого параметра принимает последовательность, которую надо проецировать, а в качестве второго параметра - функцию преобразования, которая применяется к каждому элементу. На выходе она возвращает 8 пар "пользователь - язык" (new { User = u, Lang = l }), к которым потом применяетс фильтр с помощью Where.

// 														Проекция

// Проекция позволяет спроектировать из текущего типа выборки какой-то другой тип. Для проекции используется оператор select. Допустим, у нас есть набор объектов следующего класса, представляющего пользователя:

class User
{
	public string Name { get; set; }
	public int Age { get; set; }
}

// Но нам нужен не весь объект, а только его свойство Name:

List<User> users = new List<User>();
users.Add(new User { Name = "Sam", Age = 43 });
users.Add(new User { Name = "Tom", Age = 33 });

var names = from u in users select u.Name;

foreach (string n in names)
	Console.WriteLine(n);

// Результат выражения LINQ будет представлять набор строк, поскольку выражение select u.Name выбирают в результирующую выборку только значения свойства Name.

// Аналогично можно создать объекты другого типа, в том числе анонимного:

var items = from u in users select new
						{
							FirstName = u.Name,
							DateOfBirth = DateTime.Now.Year - u.Age
						};
foreach (var n in items)
	Console.WriteLine($"{n.FirstName} - {n.DateOfBirth}");

// Здесь оператор select создает объект анонимного типа, используя текущий объект User. И теперь результат будет содержать набор объектов данного анонимного типа, в котором определены два свойства: FirstName и DateOfBirth.

// В качестве альтернативы мы могли бы использовать метод расширения Select():

// выборка имен
var names = users.Select(u => u.Name);

// выборка объектов анонимного типа
var items = users.Select(u => new
{
	FirstName = u.Name,
	DateOfBirth = Datetime.Now.Year - u.Age
});

// 														Переменые в запросах и оператор let
// Иногда возникает необходимость произвести в запросах LINQ какие-то дополнительные промежуточные вычисления. Для этих целей мы можем задать в запросах свои переменные с помощью оператора let:

List<User> users = new List<User>();
users.Add(new User { Name = "Sam", Age = 43 });
users.Add(new User { Name = "Tom", Age = 33 });
 var people = from u in users 
				let name = "Mr. " + u.Name 
				select new
				{
					Name = name,
					Age = u.Age
				};

// В данном случае создается переменная name, значение которой равно "Mr. " + u.Name.
// Возможность определения переменных наверное одно из главных преимуществ операторов LINQ по сравнению с методами расширения.

// 															Выборка из нескольких источников
// В LINQ можно выбирать объекты не только из одного, но и из большего количества источников. Например, возьмем классы:

class Phone 
{
	public string Name { get; set; }
	public string Company { get; set; }
}
class User
{
	public string Name { get; set; }
	public int Age { get; set; }
}
// Создадим два разных источника данных и произведем выборку:
List<User> users = new List<User>()
{
	new User { Name = "Alice", Age = 22 },
	new User { Name = "Sam", Age = 33 },
	new User { Name = "Din", Age = 38 }
};
List<Phone> phones = new List<Phone>()
{
	new Phone { Name = "Galaxy ASS", Company = "Samsung" },
	new Phone { Name = "iPhone 11", Company = "Apple" }
};

var people = 	from user in users
				from phone in phones
				select new { Name = user.Name, Phone = phone.Name };
foreach (var p in people)
	Console.WriteLine($"{p.Name} - {p.Phone}");





