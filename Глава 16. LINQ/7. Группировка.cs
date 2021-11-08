// Для группировки данных по определенным параметрам применяется оператор group by или метод GroupBy(). Допустим, у нас есть набор из объектов следующего типа:

class Phone
{
	public string Name { get; set; }
	public string Company { get; set; }
}

class Program
{
	static void Main()
	{
		List<Phone> phones = new List<Phone>
		{
			new Phone {Name="Lumia 430", Company="Microsoft" },
			new Phone {Name="Mi 5", Company="Xiaomi" },
			new Phone {Name="LG G 3", Company="LG" },
			new Phone {Name="iPhone 5", Company="Apple" },
			new Phone {Name="Lumia 930", Company="Microsoft" },
			new Phone {Name="iPhone 6", Company="Apple" },
			new Phone {Name="Lumia 630", Company="Microsoft" },
			new Phone {Name="LG G 4", Company="LG" }
		};
		
		
		var phoneGroups = from phone in phones group phone by phone.Company;	// Если в выражении LINQ последним оператором, выполняющим операции над выборкой, является group, то оператор select не применяется.
		// Оператор group принимает критерий по которому проводится группировка: group phone by phone.Company - в данном случае группировка по свойству Company.
		//Аналогичный запрос можно построить с помощью метода расширения GroupBy:
		var phoneGroups = phones.GroupBy(p => p.Company);
		
		foreach (IGrouping<string, Phone> g in phoneGroups)
		{
			Console.WriteLine(g.Key);	// общий ключ - название фирмы
			
			foreach (var t in g)
				Console.WriteLine(t.Name);	// элементы группы
			Console.WriteLine();
		}
	}
}

// Результатом оператора group является выборка, которая состоит из групп. Каждая группа представляет объект IGrouping<string, Phone>: параметр string указывает на тип ключа, а параметр Phone - на тип сгруппированных объектов.
// Каждая группа имеет ключ, который мы можем получить через свойство Key: g.Key
// Все элементы группы можно получить с помощью дополнительной итерации. Элементы группы имеют тот же тип, что и тип объектов, которые передавались оператору group, то есть в данном случае объекты типа Phone.

// Теперь изменим запрос и получим команду и создадим из группы новый объект:

var phoneGroups2 = from phone in phones group phone by phone.Company into g select new { Name = g.Key, Count = g.Count() };
var phoneGroups2 = phones.GroupBy(p => p.Company).Select(g => new { Name = g.Key, Count = g.Count() });

foreach (var group in phoneGroups2)
	Console.WriteLine($"{group.Name} : {group.Count}");

// Выражение group phone by phone.Company into g определяет переменную g, которая будет содержать группу. С помощью этой переменной мы можем затем создать новый объект анонимного типа: select new { Name = g.Key, Count = g.Count() } Теперь результат запроса LINQ будет представлять набор объектов таких анонимных типов, у которых два свойства Name и Count.

// Также мы можем осуществлять вложенные запросы:

var phoneGroups3 = from phone in phones group phone by phone.Company into g select new 
{
	Name = g.Key,
	Count = g.Count(),
	Phones = from p in g select p
};


foreach (var group in phoneGroups3)
{
	Console.WriteLine($"{group.Name} : {group.Count}");
	foreach (Phone phone in group.Phones)
		Console.WriteLine(phone.Name);
	Console.WriteLine();
}

// Здесь свойство Phones каждой группы формируется с помощью дополнительного запроса, выбирающего все телефоны в этой группе.

var phoneGroups3 = phones.GroupBy(p => p.Company).Select( g => new
{
	Name = g.Key,
	Count = g.Count(),
	Phones = g.Select(p => p)
});