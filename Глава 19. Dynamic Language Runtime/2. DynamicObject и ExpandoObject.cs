// Интересные возможности при разработке в C# и .NET с использованием DLR предоставляет пространство имен System.Dynamic и в частности класс ExpandoObject. Он позволяет создавать динамические объекты, наподобие тех, что используются в javascript:

using System;
using System.Collections.Generic;

namespace MyApp
{
	class Program
	{
		static void Main()
		{
			dynamic viewbag = new System.Dynamic.ExpandoObject();
			viewbag.Name = "Tom";
			viewbag.Age = 22;
			viewbag.Languages = new List<string> { "english", "german", "french" };
			Console.WriteLine($"{viewbag.Name} - {viewbag.Age}");
			foreach (var lang in viewbag.Languages)
				Console.WriteLine(lang);
			
			// объявим анонимный метод
			viewbag.IncrementAge = ( Action<int>) (delegate (int x)
			{
				viewbag.Age += x;
			}
			);
			// объявим лямбда-метод
			/*
			viewbag.IncrementAge = (Action<int>) (x => viewbag.Age += x);
									делегат, который применится для метода
													сам метод
			*/
			
			viewbag.IncrementAge(6);
			Console.WriteLine($"{viewbag.Name} - {viewbag.Age}");
		}
	}
}

// У динамического объекта ExpandoObject можно объявить любые свойства, например, Name, Age, Languages, которые могут представлять самые различные объекты. Кроме того, можно задать методы с помощью делегатов.


// 												DynamicObject
// На ExpandoObject по своему действию похож другой класс - DynamicObject. Он также позволяет задавать динамические объекты. Только в данном случае нам надо создать свой класс, унаследовав его от DynamicObject и реализовав его методы:

/*
TryBinaryOperation(): выполняет бинарную операцию между двумя объектами. Эквивалентно стандартным бинарным операциям, например, сложению x + y)

TryConvert(): выполняет преобразование к определенному типу. Эквивалентно базовому преобразованию в C#, например, (SomeType) obj

TryCreateInstance(): создает экземпляр объекта

TryDeleteIndex(): удаляет индексатор

TryDeleteMember(): удаляет свойство или метод

TryGetIndex(): получает элемент по индексу через индексатор. В C# может быть эквивалентно следующему выражению int x = collection[i]

TryGetMember(): получаем значение свойства. Эквивалентно обращению к свойству, например, string n = person.Name

TryInvoke(): вызов объекта в качестве делегата

TryInvokeMember(): вызов метода

TrySetIndex(): устанавливает элемент по индексу через индексатор. В C# может быть эквивалентно следующему выражению collection[i] = x;

TrySetMember(): устанавливает свойство. Эквивалентно присвоению свойству значения person.Name = "Tom"

TryUnaryOperation(): выполняет унарную операцию подобно унарным операциям в C#: x++

*/
// Каждый из этих методов имеет одну и ту же модель определения: все они возвращают логическое значение, показывающее, удачно ли прошла операция. В качестве первого параметра все они принимают объект связывателя или binder. Если метод представляет вызов индексатора или метода объекта, которые могут принимать параметры, то в качестве второго параметра используется массив object[] - он хранит переданные в метод или индексатор аргументы.

// Почти все операции, кроме установки и удаления свойств и индексаторов, возвращают определенное значение (например, если мы получаем значение свойства). В этом случае применяется третий параметр out object vaue, который предназначен для хранения возвращаемого объекта.
// Например, определение метода TryInvokeMember():

public virtual bool TryInvokemember(InvokeMemberBinder binder, object[] args, out object result);

// Параметр InvokeMemberBinder binder является связывателем - получает свойства и методы объекта, object[] args хранит передаваемые аргументы, out object result предназначен для хранения выходного результата.

// Рассмотрим на примере. Создадим класс динамического объекта:

using System.Dynamic;

// Класс наследуется от DynamicObject, так как непосредственно создавать объекты DynamicObject мы не можем. И также здесь переопределяется три унаследованных метода:
class PersonObject : DynamycObject
{
	// Для хранения всех членов класса, как свойств, так и методов, определен словарь Dictionary<string, object> members. Ключами здесь являются названия свойств и методов, а значениями - значения этих свойств.
	Dictionary<string, object> members = new Dictionary<string, object>();
	
	// В методе TrySetMember() производится установка свойства:
	// установка свойства
	public override bool TrySetMember(SetMemberBinder binder, object value)
	{
		members[binder.Name] = value;
		return true;
	}
	// Параметр binder хранит названием устанавливаемого свойства (binder.Name), а value - значение, которое ему надо установить.

	// Для получения значения свойства переопределен метод TryGetMember:
	// получение свойства
	public override bool TryGetMember(GetMemberBinder binder, out object result)
	{
		result = null;
		if (members.ContainsKey(binder.Name))
		{
			result = members[binder.Name];
			return true;
		}
		return false;
	}
	// Опять же binder содержит название свойства, а параметр result будет содержать значение получаемого свойства.
	
	// Для вызова методов определен метод TryInvokeMember:
	// вызов метода
	public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
	{
		dynamic method = members[binder.Name];
		// method представляет ссылку на некоторый метод. (int)args[0] преобразует переданный объект к типу int (мы ожидаем объект типа int). В итоге выражение method ((int)args[0]); выполняет метод и передает ему аргумент типа int
		result = method((int)args[0]);
		return result != null;
	}
	// Сначала с помощью binder получаем метод и затем передаем ему аргумент args[0], предварительно приведя его к типу int, и результат метода устанавливаем в параметре result. То есть в данном случае подразумевается, что метод будет принимать один параметр типа int и возвращать какой-то результат.
}

// Теперь применим класс в программе:

static void Main()
{
	dynamic person = new PersonObject();
	// Выражение person.Name = "Tom" будет вызывать метод TrySetMember, в который в качестве второго параметра будет передаваться строка "Tom".
	person.Name = "Tom";
	// Выражение return person.Age; вызывает метод TryGetMember.
	person.Age = 23;
	Func<int, int> Incr = delegate (int x) 
	{
		person.Age +=x;
		return person.Age;
	};
	person.IncrementAge = Incr;
	Console.WriteLine($"{person.Name} - {person.Age}");	// Tom - 23
	person.IncrementAge(4);
	Console.WriteLine($"{person.Name} - {person.Age}");	// Tom - 27
}

// Также у объекта person определен метод IncrementAge, который представляет действия анонимного делегата delegate (int x) { person.Age+=x; return person.Age; }. Делегат принимает число x, увеличивает на это число свойство Age и возвращает новое значение person.Age. И при вызове этого метода будет происходить обращение к методу TryInvokeMember. И, таким образом, произойдет приращение значения свойства person.Age.