//Паттерны кортежей позволяют сравнивать значения кортежей. Например, передадим конструкци switch кортеж с названием языка и времени дня и в зависимости от переданных данных возвратим определенное сообщение:

class Program
{
	//Здесь в метод передаются два значения, из которых создается кортеж (можно и сразу передать в метод кортеж). Далее в конструкции switch с помощью круглых скобок определяются значения, которым должны соответствовать элементы кортежа. Например, выражение ("english", "morning") => "Good morning" будет выполняться, если одновременно lang="english" и datetime="morning".
	static string GetWelcome(string lang, string daytime) => (lang, daytime) switch
	{
		("english","morning") => "Good morning",
		("english","evening") => "Good evening",
		("german","morning") => "Good morgen",
		("german","evening") => "Good abend",
		_ => "Hi Neggass"
	};
	static void Main()
	{
		string message = GetWelcome("english", "evening");
		Console.WriteLine(message);		//Good evening
		
		string message = GetWelcome("russian", "nig");
		Console.WriteLine(message);		//Hi Niggass
	}
	
}

//Другой пример
//Нам не обязательно сравнивать все значения кортежа, мы можем использовать только некоторые элементы кортежа. В случае, если мы не хотим использовать элемент кортежа, то вместо него ставим прочерк:
static string GetWelcome(string lang, string daytime, string status) => (lang, daytime, status) switch
{
	("english", "morning", _) => "Good morning",
	("english", "evening", _) => "Good evening",
	("german", "morning", _) => "Good morgen",
	("german", "evening"< _) => "Good abend",
	(_,_,"admin") => "Hello Admin",
	_ => "Doroy"
};
//Применение
static void Main() 
{
	string mes = GetWelcome("english","evening","user");
	Console.Writeline(mes);		//Good evening
	
	mes=GetWelcome("ass","we","admin");
	Console.WriteLine(mes);		//Hello Admin
	
}
	
		