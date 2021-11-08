
//Применение данного модификатора означает, что для установки значений свойств с модификатором init можно использовать только инициализатор либо конструктор. После инициализации значений подобных свойств они доступны только для чтения, и соответственно в дальнейшем их значения мы изменить не сможем.

using System;

namespace HelloApp
{
	public class Person
{
	public string Name { get; init; }
	public int Age {get; set; }
}
	class Program
	{
		static void Main()
		{
			/* В данном случае класс Person для свойства Name вместо сеттера использует оператор init. В итоге на след. строке предполагается создание объекта с инициализацией всех его свойств.
			*var person = new Person(); 
			// Однако поскольку инициализация свойства уже произошла, то на след. строке мы получим ошибку.
			* person.Name = "Jhon Smith"; //ошибка
			* Console.WriteLine(person.Name);
			*/
			// Как можно установить подобное свойство? Через инициализатор:
			var person = new Person() { Name="Tom" };
			Console.WriteLine(person.Name);	//Tom
		}
	}
}

//Через конструктор:
class Car
{
	public Car (string name)
	{
		Name=name;
	}
	public string Name {get; init; }
	public string Age { get; set; }
}
class Program 
{
	static void Main()
	{
		var car = new Car("Audi");
		Console.WriteLine(car.Name); //Audi
	}
}

//Через инициализатор свойства с модификатором init. В данном случае в init-свойстве Name разворачивается в полное свойство, которое управляет полем для чтения name. Благодаря этому перед установкой значения свойства мы можем произвести некоторую предобработку. Кроме того, в выражении init установливается другое init-свойство - Email, которое для установки значения использует значение свойства Name - из имени получаем значение для электронного адреса.
using System;

namespace HelloApp
{
	public class Person
	{
		readonly string name;
		public string Name { 
		get 
		{ 
			return name; 
		}
		init
		{
			name = value;
			Email = $"{value}@gmail.com";
		}
		}
	}
	class Program
	{
		static void Main()
		{
			var person = new Person() { Name="Sam" };
			Console.WriteLine(person.Name);
			Console.WriteLine(person.Email);
		}
	}
}
						//Сравнение с readonly-свойствами
//Идея создания свойств, значения которых нельзя изменять после создания, не нова. Уже в прошлых версиях C# мы могли использовать свойства для чтения:

public class Person
{
    public Person(string n)
    {
        Name = n;
    }
    public string Name { get; }
}

//В данном случае установить свойство можно только из конструктора. Но при таком подходе мы не сможем использовать инициализаторы:

var person = new Person() { Name="Sam" }; // ! Ошибка
//init-свойства снимают это ограничение.