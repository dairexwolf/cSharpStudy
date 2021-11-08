// Итератор по сути представляет блок кода, который использует оператор yield для перебора набора значений. Данный блок кода может представлять тело метода, оператора или блок get в свойствах.
// Итератор использует две специальных инструкции:

// yield return: определяет возвращаемый элемент

// yield break: указывает, что последовательность больше не имеет элементов

using System;
using System.Collections;

namespace AWP
{
	class Numbers
	{
		public IEnumerator GetEnumerator()
		{
			for (int i = 0; i < 6; i++)
				yield return i*i;
		}
	}
	
	class Program 
	{
		static void Main()
		{
			Numbers nums = new Numbers();
			foreach (int n in nums)
				Console.WriteLine(n);
			//Console.Read();
		}
	}
}

// В классе Numbers метод GetEnumerator() фактически представляет итератор. С помощью оператора yield return возвращается некоторое значение (в данном случае квадрат числа).

// В программе с помощью цикла foreach мы можем перебрать объект Numbers как обычную коллекцию. При получении каждого элемента в цикле foreach будет срабатывать оператор yield return, который будет возвращать один элемент и запоминать текущую позицию.

// Другой пример: пусть у нас есть коллекция Library, которая представляет хранилище книг - объектов Book. Используем оператор yield для перебора этой коллекции:

// класс объекта для библиотеки
class Book
{
	public Book(string name)
	{
		this.Name = name;
	}
	public string Name { get; set; }
}
// коллекция для объектов типа "Книга"
class Library
{
	// массив объектов
	private Book[] books;
	// конструктор
	public Library()
	{
		books = new Book[] { new Book("Отцы и slaves"), new Book("Война и ass"), new Book ("Billy Онегин") };
	}
	// получение длинны коллекции
	public int Length 
	{
		get 
		{ 
			return books.Length; 
		}
	}
	// переборщик коллекции
	public IEnumerator GetEnumerator()
	{
		int len = books.Length;
		for (int i = 0; i<len; i++)
		{
			yield return books[i];
		}
	}
}
// реализация в программе
public class Program
{
	public static void Main()
	{
		Library lib = new Library();
		foreach (Book b in lib)
		{
			Console.WriteLine(b.Name);
		}
	}
}


// Метод GetEnumerator() представляет итератор. И когда мы будем осуществлять перебор в объекте Library в цикле foreach, то будет идти обращение к вызову yield return books[i];.
// !!! При обращении к оператору yield return будет сохраняться текущее местоположение. И когда метод foreach перейдет к следующей итерации для получения нового объекта, итератор начнет выполнения с этого местоположения !!!

// Хотя при реализации итератора в методе GetEnumerator() применялся перебор массива в цикле for, но это необязательно делать. Мы можем просто определить несколько вызовов оператора yield return:
IEnumerator IEnumerable.GetEnumirator()
{
	yield return books[0];
	yield return books[1];
	yield return books[2];
}
// В этом случае при каждом вызове оператора yield return итератор также будет запоминать текущее местоположение и при последующих вызовах начинать с него.

//													Именованный итератор
// Выше для создания итератора мы использовали метод GetEnumerator. Но оператор yield можно использовать внутри любого метода, только такой метод должен возвращать объект интерфейса IEnumerable. Подобные методы еще называют именованными итераторами.

// класс объекта для библиотеки
class Book
{
	public Book(string name)
	{
		this.Name = name;
	}
	public string Name { get; set; }
}
// коллекция для объектов типа "Книга"
class Library
{
	// массив объектов
	private Book[] books;
	// конструктор
	public Library()
	{
		books = new Book[] { new Book("Отцы и slaves"), new Book("Война и ass"), new Book ("Billy Онегин") };
	}
	// получение длинны коллекции
	public int Length 
	{
		get 
		{ 
			return books.Length; 
		}
	}
	// именованный переборщик коллекции
	public IEnumerable GetBooks(int max)
	{
		int len = books.Length;
		for (int i = 0; i<max; i++)
		{
			if (i == len)
				yield break;
			else
				yield return books[i];
		}
	}
}
// реализация в программе
// Вызов library.GetBooks(5) будет возвращать набор из не более чем 5 объектов Book. Но так как у нас всего три таких объекта, то в методе GetBooks после трех операций сработает оператор yield break.
public class Program
{
	public static void Main()
	{
		Library lib = new Library();
		foreach (Book b in lib.GetBooks(5))
		{
			Console.WriteLine(b.Name);
		}
	}
}
// Определенный здесь итератор - метод IEnumerable GetBooks(int max) в качестве параметра принимает количество выводимых объектов. В процессе работы программы может сложиться, что его значение будет больше, чем длина массива books. И чтобы не произошло ошибки, используется оператор yield break. Этот оператор прерывает выполнение итератора.
// !! Не именованный быстрее