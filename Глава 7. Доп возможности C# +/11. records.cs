//Records представляют новый ссылочный тип, который появился в C#9. Ключевая особенность records состоит в том, что они могут представлять неизменяемый (immutable) тип, который по умолчанию обладает рядом дополнительных возможностей по сравнению с классами. Зачем нам нужны неизменяемые типы? Такие типы более безопасны в тех ситуациях, когда нам надо гарантировать, что данные объекта не будут изменяться. В .NET в принципе уже есть неизменяемые типы, например, String.

public record Person
{
	public string Name { get; set; }
	public int Age {get; set; }
}

//Все records являются ссылочными типами. На уровне промежуточного языка IL, в который компилируется код C#, для record фактически создается класс.
//Стоит отметить, что records являются неизменяемыми (immutable) только при определенных условиях. Например, мы можем использовать выше определенный тип Person следующим образом:

class Program
{
	static void Main()
	{
		var person = new Person() { Name = "Tom" };
		Person.Name = "Bob";
		Console.WriteLine(person.Name);	//Bob
		//При выполнении этого кода не возникнет никакой ошибки, мы спокойно сможем изменять значения свойств объекта Person. Чтобы сделать его действительно неизменяемым, надо использовать модификатор init вместо обычных сеттеров.
	}
}

class record Person
{
	public string Name {get; init; }
	public string Age {get; init; }
}
class Program
{
	static void Main()
	{
		var person = new Person() { Name = "Tom" };
		person.Name = "Bob"; //Ошибка
		Console.WriteLine(person.Name);
	}
}

//В данном случае мы получим ошибку при попытке изменить значение свойств объекта Person.

//Во многим records похожи на обычные классы, например, они могут абстрактными, их также можно наследовать либо запрещать наследование с помощью оператора sealed. Тем не менее есть и ряд отличий. Рассмотрим некоторые основные отличия records от классов.

//При определении record компилятор генерирует метод Equals() для сравнения с другим объектом. При этом сравнение двух records производится на основе их значений. Например, рассмотрим следующий пример:

using System;

namespace TestNet5ConsoleApp1
{
	public record Person
	{
		public string Name (get; init; }
		public int Age {get; init; }
	}
	public class User
	{
		public string Name {get; init; }
		public int Age {get; init; }
	}
	class Program
	{
		static void Main()
		{
			var person1 = new Person() { Name = "Tom" };
			var person2 = new Person() { Name = "Tom" };
			Console.WriteLine(person1.Equals(person2)); //true
			
			var user1 = new User() { Name = "Tom" };
			var user2 = new User() { Name = "Tom" };
			Console.WriteLine(user1.Equals(user2)); //false
		}
	}
}

//В данном случае при сравнении двух объектов record Person мы увидим, что они равны, так как их значения (значения свойств Name и Age) равны. Однако в случае с объектами класса User, которые имеют те же одинаковые значения мы увидим, что они не равны. Так как сравнение records производится по значению.
//Кроме того, для record уже по умолчанию реализованы операторы == и !=, которые также сравнивают две record по значению:
			var person1 = new Person() { Name = "Tom" };
			var person2 = new Person() { Name = "Tom" };
			Console.WriteLine(person1 == person2); //true
			
			var user1 = new User() { Name = "Tom" };
			var user2 = new User() { Name = "Tom" };
			Console.WriteLine(user1 == user2); //false
			
//В отличие от классов records поддерживают инициализацию с помощью оператора with. Он позволяет создать одну record на основе другой record:

var person1 = new Person() { Name = "Tom", Age = 36 };
var person2 = person1 with {Name = "Sam" };
Console.WriteLine($"{person2.Name} - {person2.Age}"); 	// Sam - 36

//После record, значения которой мы хотим скопировать, указывается оператор with, после которого в фигурных скобках указываются значения для тех свойств, которые мы хотим изменить. Так, в данном случае person2 получает для свойства Age значение из person1, а свойство Name изменяется. Эта возможность может быть особенно актуальна, если в record, которую мы хотим скопировать, множество свойств, из которых мы хотим поменять одно-два.

//Records могут принимать данные для свойств через конструктор, и в этом случае мы можем сократить их определение. Например, пусть у нас есть следующая record Person:

public record Person
{
	public string Name { get; init; }
	public int Age { get; init; }
	public Person (string str, int age)
	{
		Name = str; Age = age;
	}
	public void Deconstruct(out string personName, out int personAge) => (personName, personAge) = (Name, Age);
	//Кроме конструктора здесь реализован деконструктор, который позволяет разложить объект Person на кортеж значений.
}
//Применение:
class Program
{
	static void Main()
	{
		var person = new Person ("Tom", 36);
		Console.WriteLine(person.Name);	// Tom
		var (userName, userAge) = person;
		Console.WriteLine(userAge);		// 36
		Console.WriteLine(userName);	// Tom
	}
}
//Выше определенную record Person можно сократить до позиционной record:
public record Person(string Name, int Age);
//Это все определение типа. То есть мы говорим, что для типа Person будет создаваться конструктор, который принимает два параметра и присваивает их значения соответственно свойствам Name и Age, и что также автоматически будет создаваться деконструктор. Ее использование будет аналогично:

using System;
namespace HelloApp
{
	public record Person(string Name, int Age);
	class Program
	{
		static void Main()
		{
			var person = new Person ("Tom, 36);
			Console.WriteLine(person.Name);	// Tom
			var (userName, userAge) = person;			
			Console.WriteLine(userAge);		// 36
			Console.WriteLine(userName);	// Tom
		}
	}
}
