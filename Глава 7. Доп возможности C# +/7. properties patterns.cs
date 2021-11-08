//Паттерн свойств позволяет сравнивать со значениями определенных свойств объекта. Например, пусть у нас будет следующий класс:
class Person
{
	public string Name {get; set; }		//имя пользователя
	public string Status {get; set; }	//статус пользователя
	public string Language {get; set; }	//язык пользователя	
	
	//Теперь в методе в зависимости от статуса и языка пользователя выведем ему определенное сообщене, применив паттерн свойств:
	

	
	
}

class Program
{
		
		/*
	Паттерны свойств предполагают использование фигурных скобок, внутри которых указываются свойства и через двоеточие их значение {свойство: значение}. И со значением свойства в фигурных скобках сравнивается свойство передаваемого объекта. При этом в фигурных скобках мы можем указать несколько свойств и их значений { Language: "german", Status: "admin" } - тогда свойства передаваемого объекта должны соответствовать всем этим значениям.

Можно оставить пустые фигурные скобки, как в последнем случае { } => "undefined!" - передаваемый объект будет соответствовать пустым фигурным скобкам, если он не соответствует всем предыдущим значениям, или например, если его свойства не указаны или имеют значение null.

То есть в данном случае, если у объекта Person p выполняется равенство Language = "english", будет возвращаться строка "Hello!".

Если у объекта Person p одновременно выполняются два равенства Language = "german" и Role="admin", будет возвращаться строка "Hallo, admin!".

Если у объекта Person p выполняется равенство Language = "french", будет возвращаться строка "Salut!".

Во всех остальных случаях объект Person будет сопоствляться с пустыми фигурными скобками {}, и будет возвращаться строка "Hello world!"
	*/
	
	static string GetMessage(Person p) => p switch
	{
		{ Language: "english" } => "Hello!",
		{ Language: "german", Status: "admin" } => "Hallo, admin!",
		{ Language: "french", Name = "Freeman" } => "Salut, Mr. Freeman!",
		{ } => "undefined",
		null => "null"		// если Person p = null
	};
	
	//Кроме того, мы можем определять в паттерных свойств переменные, передавать этим переменным значения объекта и использовать при возвращении значения:
	
	static string GetMessage2(Person p) => p switch
	{
		{ Language: "german", Status: "admin" } => "Hallo, admin!",
		{ Language: "french", Name: var name } => $"Salut, {name}!",
		{ Language: var lang} => $"Unknown language: {lang}",
		null => "null"
	};
	
	static void Main()
	{
		Person pierre = new Person { Language="french", Status="user", Name = "Pierre" };
		string message = GetMessage(pierre);
		Console.WriteLine(message);				// Salut!
		
		Person tomas = new person { Language = "german", Status="admin", Name="Tomas" };
		Console.WriteLine(GetMessage(tomas));	// Hallo, admin!
		
		Person pablo = new Person { Language="spanish", Status = "user", Name="Pablo" };
		Console.WriteLine(GetMessage(pablo));	// undefined
		
		
		Person pierre = new Person { Language = "french", Status="user", Name = "Pierre"};
		string message = GetMessage(pierre);
		Console.WriteLine(message);             // Salut, Pierre!
	 
		Person tomas = new Person { Language = "german", Status="admin", Name = "Tomas" };
		Console.WriteLine(GetMessage(tomas));     // Hallo, admin!
	 
		Person pablo = new Person { Language = "spanish", Status = "user", Name = "Pablo" };
		Console.WriteLine(GetMessage(pablo));     // Unknown language: spanish
	 
		Person bob = null;
		Console.WriteLine(GetMessage(bob));         // null
	}
}
