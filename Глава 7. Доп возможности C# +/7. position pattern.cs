//Позиционный паттерн применяется к типу, у которого определен метод деконструктора. Например, определим следующий класс:
class MessageDetails
{
	public string Language {get;set;}	//язык пользователя
	public string DateTime {get;set;}	//время пользователя
	public string Status {get;set;}	//статус пользователя
	
	public void Deconstruct (out string lang, out string datetime, out string status)
	{
		lang = Language;
		datetime = DateTime;
		status = Status;
	}
}

class Program
{
//Теперь используем позиционный паттерн и в зависимости от значений объекта MessageDetails возвратим определенное сообщение:
//Фактически этот паттерн похож на пример с кортежами выше, только теперь вместо кортежа в конструкцию switch передается объект MessageDetails. Через метод деконструктора мы можем получить набор выходных параметров в виде кортежа и опять же по позиции сравнивать их с некоторыми значениями в конструкции switch.
static string GetWelcome(MessageDetais detais) => details switch
{
	("english", "morning", _) => "Good morning",
	("english", "evening", _) => "Good evening",
	("german", "morning", _) => "Guten morgen",
	("german", "evening", _) => "Guten abend",
	(_,_, "admin") => "Hi boss of this gym",
	(var lang, var datetime, var status) => $"{lang} not found, {datetime} unknown, {status} undefined", 
	_ => "Привет"
};

static void Main()
{
	MessageDetails details1 = new MessageDetails {Language = "english", DateTime = "evening", Status = "user" };
	string message = GetWelcome(details1);
	Console.WriteLine(message);		//Good evening
	
	MessageDetails details2 = new MessageDetails {Language = "french", DateTime = "morning", Status = "admin" };
	message = GetWelcome(details2);
	Console.WriteLine(details2);	//Hi boss of this gym
	
}