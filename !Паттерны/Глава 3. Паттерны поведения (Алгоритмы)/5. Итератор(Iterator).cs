// Паттерн Итератор (Iterator) предоставляет абстрактный интерфейс для последовательного доступа ко всем элементам составного объекта без раскрытия его внутренней структуры.

// Наверное, всем программистам, работающим с языком C#, приходилось иметь дело с циклом foreach, который перебирает объекты в массиве или коллекции. При этом встроенных классов коллекций существует множество, и каждая из них отличается по своей структуре и поведению.

// Ключевым моментом, который позволяет осуществить перебор коллекций с помощью foreach, является применения этими классами коллекций паттерна итератор, или проще говоря пары интерфейсов IEnumerable / IEnumerator. Интерфейс IEnumerator определяет функционал для перебора внутренних объектов в контейнере:

public interface IEnumerator
{
	bool MoveNext();		// перемещение на одну позицию вперед в контейнере элементов
	object Current {get;}	// текущий элемент в контейнере
	void Reset();			// перемещение в начало контейнера
}
// А интерфейс IEnumerable использует IEnumerator для получения итератора для конкретного типа объекта:
public interface IEnumerable
{
	IEnumerator GetEnumerator();
}

// Используя данные интерфейсы, мы можем свести к одному шаблону - с помощью цикла foreach - любые составные объекты.

/*
Когда использовать итераторы?

- Когда необходимо осуществить обход объекта без раскрытия его внутренней структуры

 -Когда имеется набор составных объектов, и надо обеспечить единый интерфейс для их перебора

- Когда необходимо предоставить несколько альтернативных вариантов перебора одного и того же объекта
*/

// Формальное определение паттерна на C# может выглядеть следующим образом:

class Client
{
	public void Main()
	{
		Aggregate a = new ConcreteAggregate();
		
		Iterator i = a.CreateIterator();
		
		object item = i.First();
		while (!i.isDone())
			item = i.Next();
	}
}

abstract class Aggregate 
{
	public abstract Iterator CreateIterator();
	public abstract int Count { get; protected set;}
	public abstract object this[int index] { get; set;}
}

class ConcreteAggregate : Aggregate
{
	private readonly ArrayList _items = new ArrayList();
	
	public override Iterator CreateIterator()
	{
		return new ConcreteIterator(this);
	}
	public override int Count
	{
		get { return _items.Count;}
		protected set { }
	}
	public override object this[int index]
	{
		get { return _items[index]; }
		set { _items.Insert(index, value); }
	}
}

abstract class Iterator
{
	public abstract object First();
	public abstract object Next();
	public abstract object IsDone();
	public abstract object CurrentItem();
}

class ConcreteIterator : Iterator
{
	private readonly Aggregate _aggregate;
	private int _current;
	
	public ConcreteIterator(Aggregate aggregate)
	{
		this._aggregate = aggregate;
	}
	
	public override object First()
	{
		return _aggregate[0];
	}
	public override object Next()
	{
		object ret = null;
		_current++;
		if(_current < _aggregate.Count)
			ret = _aggregate[_current];
		return ret;
		
	}
	
	public override object CurrentItem()
	{
		return _aggregate[_current];
	}
	
	public override bool IsDone()
	{
		return _current >= _aggregate.Count;
	}
}

/*
Участники

- Iterator: определяет интерфейс для обхода составных объектов

- Aggregate: определяет интерфейс для создания объекта-итератора

- ConcreteIterator: конкретная реализация итератора для обхода объекта Aggregate. Для фиксации индекса текущего перебираемого элемента использует целочисленную переменную _current

- ConcreteAggregate: конкретная реализация Aggregate. Хранит элементы, которые надо будет перебирать

- Client: использует объект Aggregate и итератор для его обхода
*/

// Теперь рассмотрим конкретный пример. Допустим, у нас есть классы книги и библиотеки:

class Book
{
	public string Name { get; set; }
}

class Library
{
	private Book[] books;
}

// И, допустим, у нас есть класс читателя, который хочет получить информацию о книгах, которые находятся в библиотеке. И для этого надо осуществить перебор объектов с помощью итератора:

using System;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        Library library = new Library();
        Reader reader = new Reader();
        reader.SeeBooks(library);

        Console.Read();
    }
}

class Reader
{
    public void SeeBooks(Library lib)
    {
        IBookIterator iterator = lib.CreateNumerator();
        while (iterator.HasNext())
        {
            Book book = iterator.Next();
            Console.WriteLine(book.Name);
        }
    }
}

interface IBookIterator
{
    bool HasNext();
    Book Next();
}

interface IBookNumerable
{
    IBookIterator CreateNumerator();
    int Count { get; }
    Book this[int index] { get; }
}

class Book
{
    public string Name { get; set; }
}

class Library : IBookNumerable
{
    private Book[] books;
    public Library()
    {
        books = new Book[]
        {
            new Book { Name = "Funkin Slaaaaves" },
            new Book { Name = "kNigga" },
            new Book { Name = "AutoBiografy Billy" },
            new Book { Name = "Cyberpunk" }

        };
    }

    public int Count
    {
        get { return books.Length; }
    }
    public Book this[int index]
    {
        get { return books[index]; }
    }
    public IBookIterator CreateNumerator()
    {
        return new LibraryNumerator(this);
    }
}

class LibraryNumerator : IBookIterator
{
    IBookNumerable aggregate;
    int index = 0;
    public LibraryNumerator(IBookNumerable a)
    {
        aggregate = a;
    }
    public bool HasNext()
    {
        return index < aggregate.Count;
    }
    public Book Next()
    {
        return aggregate[index++];
    }
}
	
	
// Интерфейс IBookIterator представляет итератор наподобие интерфейса IEnumerator. Роль интерфейса составного агрегата представляет тип IBookNumerable. Клиентом здесь является класс Reader, который использует итератор для обхода объекта библиотеки.


