// В прошлой теме, где рассматривалась реализация метода Dispose, говорилось, что для его вызова можно использовать следующую конструкцию try..catch:

Person p = null;
try
{
	p = new Person();
}
finally
{
	if (p != null)
		p.Dispose();
}

// Однако синтаксис C# также предлагает синонимичную конструкцию для автоматического вызова метод Dispose - конструкцию using:
using (Person p = new Person())
{
	//........
}

// Конструкция using оформляет блок кода и создает объект некоторого класса, который реализует интерфейс IDisposable, в частности, его метод Dispose. При завершении блока кода у объекта вызывается метод Dispose.

// Важно, что данная конструкция применяется только для классов, которые реализуют интерфейс IDisposable.

using System;
static void Main()
{
	Test();
}

private static void Test()
{
	using (Person p = new Person { Name = "Tom" })
		{
			// переменная p доступна только в блоке using
			Console.WriteLine("Некторые действия с объектом Person. Получим его имя: {0}", p.Name);
		}
		Console.WriteLine("Конец метода Test");
}

public class Person : IDisposable
{
	public string Name{ get; set; }
	public void Dispose()
	{
		Console.WriteLine("Disposed method");
	}
	~Person
	{
		Console.WriteLine("Disposed Destructor");
	}
}

// Здесь мы видим, что по завершении блока using у объекта Person вызывается метод Dispose. Вне блока кода using объект p типа Person не существует.

// Начиная с версии C# 8.0 мы можем задать в качестве области действия всю окружающую область видимости, например, метод:

private static void Test()
{
	using Person p = new Person { Name = "Tom" };
	// переменная p доступна до конца метода Test
    Console.WriteLine($"Некоторые действия с объектом Person. Получим его имя: {p.Name}");
             
    Console.WriteLine("Конец метода Test");
}

// В данном случае using сообщает компилятору, что объявляемая переменная должна быть удалена в конце области видимости - то есть в конце метода Test. 

// 													Освобождение множества ресурсов
// Для освобождения множества ресурсов мы можем применять вложенные конструкции using. Например:
private static void Test()
{
	using (Person tom = new Person { Name = "Tom" })
		{
			using (Person bob = new Person { Name = "Bob" })
				{
					Console.WriteLine($"Person1: {tom.Name}	\t Person2: {bob.Name}");
				} // Вызов метода Dispose() для объекта bob
		}// Вызов метода Dispose() для объекта tom
		Console.WriteLine("Конец метода Test()");
}

// В данном случае обе конструкции using создают объекты одного и того же типа, но это могут быть и разные типы данных, главное, чтобы они реализовали интерфейс IDisposable.

// Мы можем сократить это определение:

private static void Test()
{
    using (Person tom = new Person { Name = "Tom" })
    using(Person bob = new Person { Name = "Bob" })
    {
        Console.WriteLine($"Person1: {tom.Name}    Person2: {bob.Name}");
    } // вызов метода Dispose для объектов bob и tom
    Console.WriteLine("Конец метода Test");
}

// И, как уже было выше сказано, в C# мы можем задать в качестве области действия для объектов, создаваемых в конструкции using, весь метод:

private static void Test()
{
    using Person tom = new Person { Name = "Tom" };
    using Person bob = new Person { Name = "Bob" };
     
    Console.WriteLine($"Person1: {tom.Name}    Person2: {bob.Name}");
     
    Console.WriteLine("Конец метода Test");
} // вызов метода Dispose для объектов bob и tom