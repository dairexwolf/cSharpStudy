// Приложение может использовать множество классов и объектов. Однако есть вероятность, что не все создаваемые объекты будут использованы. Особенно это касается больших приложений. Например:

class Reader
{
	Library library = new Library();
	public void ReadBook()
	{
		library.GetBook();
		Console.WriteLine("Читаем книжку");
	}
	
	public void ReadEBook()
	{
		Console.WriteLine("Читаем электронную книжку");
	}
	
}

class Library
{
	private string[] books = new string[99];
	
	public void GetBook()
	{
		Console.WriteLine("Выдаем книгу читателю");
	}
}

public class Program
    {
        public static void Main(string[] args)
        {
            Reader Ivan = new Reader();
			Ivan.ReadBook();
			Ivan.ReadEBook();
        }
    }

// Есть класс Library, представляющий библиотеку и хранящий некоторый набор книг в виде массива. Есть класс читателя Reader, который хранит ссылку на объект библиотеки, в которой он записан. У читателя определено два метода: для чтения электронной книги и для чтения обычной книги. Для чтения обычной книги необходимо обратиться к методу класса Library, чтобы получить эту книгу.
// Но что если читателю вообще не придется читать обычную книгу, а только электронные? В этом случае объект library в классе читателя никак не будет использоваться и будет только занимать место памяти. Хотя надобности в нем не будет.

// Для подобных случаев в .NET определен специальный класс Lazy<T>. Изменим класс читателя следующим образом:

// изменения только тут
class Reader
{
	Lazy<Library> library = new Lazy<Library>();	// тут добавлил Lazy<>
	public void ReadBook()
	{
		library.Value.GetBook();					// добавили .Value.
		// Чтобы обратиться к самой библиотеке и ее методам, надо использовать выражение library.Value - это и есть объект Library.
		Console.WriteLine("Читаем книжку");
	}
	
	public void ReadEBook()
	{
		Console.WriteLine("Читаем электронную книжку");
	}
	
}

class Library
{
	private string[] books = new string[99];
	
	public void GetBook()
	{
		Console.WriteLine("Выдаем книгу читателю");
	}
}

// Класс Library остается прежнем. Но теперь класс читателя содержит ссылку на библиотеку в виде объекта Lazy<Library>.
// Что меняет в поведении класса Reader эта замена? Рассмотрим его применение:

public class Program
    {
        public static void Main(string[] args)
        {
            Reader Ivan = new Reader();
			Ivan.ReadBook();
			Ivan.ReadEBook();
        }
    }
	
// Непосредственно объект Library задействуется здесь только на третьей строке в методе reader.ReadBook(), который вызывает в свою очередь метод library.Value.GetBook(). Поэтому вплоть до третьей строки объект Library, используемый читателем, не будет создан. Если мы не будем применять в программе метод reader.ReadBook(), то объект библиотеки тогда вобще не будет создан, и мы избежим лишних затрат памяти. Таким образом, Lazy<T> гарантирует нам, что объект будет создан только тогда, когда в нем есть необходимость.

