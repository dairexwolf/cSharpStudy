// Паттерн Заместитель (Proxy) предоставляет объект-заместитель, который управляет доступом к другому объекту. То есть создается объект-суррогат, который может выступать в роли другого объекта и замещать его.

/*
Когда использовать прокси?

- Когда надо осуществлять взаимодействие по сети, а объект-проси должен имитировать поведения объекта в другом адресном пространстве. Использование прокси позволяет снизить накладные издержки при передачи данных через сеть. Подобная ситуация еще называется удалённый заместитель (remote proxies)

- Когда нужно управлять доступом к ресурсу, создание которого требует больших затрат. Реальный объект создается только тогда, когда он действительно может понадобится, а до этого все запросы к нему обрабатывает прокси-объект. Подобная ситуация еще называется виртуальный заместитель (virtual proxies)

- Когда необходимо разграничить доступ к вызываемому объекту в зависимости от прав вызывающего объекта. Подобная ситуация еще называется защищающий заместитель (protection proxies)

- Когда нужно вести подсчет ссылок на объект или обеспечить потокобезопасную работу с реальным объектом. Подобная ситуация называется "умные ссылки" (smart reference)
*/

// На C# паттерн формально может выглядеть следующим образом:

class Client
{
	void Main()
	{
		Subject subject = new Proxy();
		subject.Request();
	}
}

abstract class Subject
{
	public abstract void Request();
}

class RealSubject : Subject
{
	public override void Request()
	{ }
}

class Proxy : Subject
{
	RealSubject realSubject;
	public override void Request()
	{
		if (realSubject == null)
			realSubject = new RealSubject();
		realSubject.Request();
	}
}

/*
Участники паттерна

- Subject: определяет общий интерфейс для Proxy и RealSubject. Поэтому Proxy может использоваться вместо RealSubject

- RealSubject: представляет реальный объект, для которого создается прокси

- Proxy: заместитель реального объекта. Хранит ссылку на реальный объект, контролирует к нему доступ, может управлять его созданием и удалением. При необходимости Proxy переадресует запросы объекту RealSubject

- Client: использует объект Proxy для доступа к объекту RealSubject
*/

// Рассмотрим применение паттерна. Допустим, мы взаимодействуем с базой данных через Entity Framework. У нас есть модель и контекст данных:

// Класс Page представляет отдельную страницу книги, у которой есть номер и текст.
class Page
{
	public int Id{ get; set; }
	public int Number { get; set; }
	public string Text { get; set; }
}

// 
class PageContext : DbContext
{
	public DbSet<Page> Pages { get; set; }
}

// Взаимодействие с базой данных может уменьшить производительность приложения. Для оптимизации приложения мы можем использовать паттерн Прокси. Для этого определим репозиторий и его прокси-двойник:

class Program
{
	static void Main()
	{
		using (IBook book = new BookStoreProxy())
		{
			// читаем первую страницу
			Page page1 = book.GetPage(1);
			Console.WriteLine(page1.Text);
			// читаем вторую страницу
			Page page2 = book.GetPage(2);
			Console.WriteLine(page2.Text);
			// возращяемся на 1 страницу
			page1 = book.GetPage(1)
			Console.WriteLine(page1.Text);
		}
		Console.Read();
	}
}

class Page
{
	public int Id{ get; set; }
	public int Number { get; set; }
	public string Text { get; set; }
}

class PageContext : DbContext
{
	public DbSet<Page> Pages { get; set; }
}

interface IBook : IDisposable
{
	Page GetPage(int number);
}

class BookStore : IBook
{
	PageContext db;
	public BookStore()
	{
		db = new PageContext();
	}
	
	public Page GetPage(int number)
	{
		return db.Pages.FirstOrDefault(p => p.Number == number)
	}
	
	public void Dispose()
	{
		db.Dispose();
	}
}

class BookStoreProxy : IBook
{
	List<Page> pages;
	BookStore bookStore;
	public BookStoreProxy()
	{
		pages = new List<Page>();
	}
	
	public Page GetPage(int number)
	{
		Page page = pages.FirstOrDefault(p=>p.Number == number);
		if (page == null)
		{
			if (bookStore == null)
				bookStore = new BookStore();
			page = bookStore.GetPage(number);
			pages.Add(page);
		}
		return page;
	}
	
	public void Dispose()
	{
		if (BookStore != null)
			bookStore.Dispose();
	}
}

// Итак, здесь определен общий интерфейс IBook для реального объекта и для его прокси-класса. Он определяет один метод GetPage() для получения страницы по номеру.

// Реальный объект BookStore использует контекст данных для извлечения информации о странице из базы данных. Действие же прокси-класса отличается. Прокси определяет дополнительный объект - список pages. При получении страницы прокси сначала смотрит в этот список, и если там страницы не окажется, то идет обращение к реальному объекту BookStore и его методу. То есть фактически будет реализована функциональность кэша страниц.

// Клиент, в роли которого в данном случае выступает класс Program, вообще не будет знать, использует ли он функционал класса BookStore или его прокси.

// В то же время паттерн Прокси имеет недостаток: поскольку иногда будет выполняться сначала функционал прокси, а потом функционал реального объекта, например, если страницы не окажется в списке-кэше, то это может привести к замедлению выполнения программы.