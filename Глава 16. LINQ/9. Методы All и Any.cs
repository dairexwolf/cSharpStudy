// Методы All, Any и Contains позволяют определить, соответствует ли коллекция определенному условию, и в зависимости от результата они возвращают true или false.

// Метод All проверяет, соответствуют ли все элементы условию. Например, узнаем, у всех ли пользователей возраст превышает 20 и имя начинается с буквы T:

class User
{
	public string Name { get; set; }
	public int Age { get; set; }
}

List<User> users = new List<User>()
{
	new User { Name = "Tom", Age = 23 },
	new User { Name = "Sam", Age = 43 },
	new User { Name = "Bill", Age = 35 }
};

bool result = users.All( u => u.Age > 20);	// true
if (result)
	Console.WriteLine("У всех пользователей возраст больше 20");
else 
	Console.WriteLine("Не у всех пользователей возраст больше 20");

result = users.All(u => u.Name.StartsWith("T"));	// false
if (result)
	Console.WriteLine("У всех пользователей имя начитается с \"T\"");
else 
	Console.WriteLine("Не у всех пользователей имя начитается с \"T\"");

// Поскольку у всех пользователей возвраст больше 20, то переменная result1 будет равна true. В то же время не у всех пользователей имя начинаяется с буквы T, поэтому вторая переменная result2 будет равна false.

// Метод Any действует подобным образом, только позволяет узнать, соответствует ли хотя бы один элемент коллекции определенному условию:

bool result = users.Any(u => u.Age < 20);	//false
if (result)
	Console.WriteLine("Есть пользователи с возрастом меньше 20");
else
	Console.WriteLine("У всех пользователей возраст больше 20");

result = users.Any(u => u.Name.StartsWith("T"));	// true
if (result)
	Console.WriteLine("Есть пользователи, у которых имя начинается на \"T\"");
else
	Console.WriteLine("Нет пользователей, у которых имя начинается на \"T\"");

// Первое выражение вернет false, поскольку у всех пользователей возвраст больше 20. Второе выражение возвратит true, так как у нас есть в коллекции пользователь с именем Tom.
