//Словарь хранит объекты, которые представляют пару ключ-значение. Каждый такой объект является объектом структуры KeyValuePair<TKey, TValue>. Благодаря свойствам Key и Value, которые есть у данной структуры, мы можем получить ключ и значение элемента в словаре.

Dictionary<int, string> countries = new Dictionary<int, string>(5);
countries.Add(1, "Coldland");
countries.Add(3, "Tealand");
countries.Add(2, "Gunland");
countries.Add(5, "Bagettiland");
countries.Add(4, "Badqualityland");

// перебор коллекции с помощью объекта KeyValuePair
foreach (KeyValuePair<int, string> keyValue in countries)
{
	Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
}

Console.WriteLine();
// получение элемента по ключу
string country = countries[4];
// изменение объекта
countries[3] = "Talkingsnowland";
// удаление по ключу
countries.Remove(4);

foreach (KeyValuePair<int, string> keyValue in countries)
{
	Console.WriteLine(keyValue.Key + " - " + keyValue.Value);
}


// Класс словарей также, как и другие коллекции, предоставляет методы Add и Remove для добавления и удаления элементов.
// Только в случае словарей в метод Add передаются два параметра: ключ и значение.
// А метод Remove удаляет не по индексу, а по ключу.
//Так как в нашем примере ключами является объекты типа int, а значениями - объекты типа string, то словарь в нашем случае будет хранить объекты KeyValuePair<int, string>. В цикле foreach мы их можем получить и извлечь из них ключ и значение.

// Кроме того, мы можем получить отдельно коллекции ключей и значений словаря:

Console.WriteLine();
// перебор ключей
foreach (int c in countries.Keys)
{
	Console.WriteLine(c);
}

//  перебор по значениями
foreach (string str in countries.Values)
{
	Console.WriteLine(str);
}

// все тоже самое, но с объектами класса
class Person 
{
	public string Name { get; set; }
}

class Program
{
	public static void Main()
	{
		Dictionary<char, Person> people = new Dictionary<char, Person>();
		people.Add('b', new Person() { Name = "Bill"});
		people.Add('t', new Person() { Name = "Tom"});
		people.Add('j', new Person() { Name = "John"});
		
		foreach (KeyValuePair<char, Person> keyValue in people)
		{
			// keyValue представляет класс Person
			Console.WriteLine(keyValue.Key + " - "+ keyValue.Value.Name);
		}
		Console.WriteLine();
		
		// перебор ключей
		foreach (char ch in people.Keys)
		{
			Console.WriteLine(ch);
		}
		Console.WriteLine();
		
		// перебор значений
		foreach (Person p in people.Values)
		{
			Console.WriteLine(p.Name);
		}
	}
}

//Для добавления необязательно применять метод Add(), можно использовать сокращенный вариант:
Dictionary<char, Person> peopleList = new Dictionary<char, person>();
people.Add('b', new Person() { Name = "Billy" });
people['v'] = new Person() { Name = "Van"}; //Несмотря на то, что изначально в словаре нет ключа 'a' и соответствующего ему элемента, то он все равно будет установлен. 
//Если же он есть, то элемент по ключу 'a' будет заменен на новый объект new Person() { Name = "Alice" }

												//Инициализация
// В C# 5.0 мы могли инициализировать словари следующим образом:
Dictionary<string, string> countries = new Dictionary<string, string>
{
	{"Boss", "Of This Gym"},
	{"Billy", "Herrington"}
};
foreach (var pair in countries)
	Console.WriteLine(pair.Key + " - " + pair.Value);
// То начиная с C# 6.0 доступен также еще один способ инициализации:
Dictionary<string, string> countries = new Dictionary<string, string>
{
    ["Франция"]= "Париж",
    ["Германия"]= "Берлин",
    ["Великобритания"]= "Лондон"
};     
