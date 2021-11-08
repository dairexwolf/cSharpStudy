//Анонимные типы позволяют создать объект с некоторым набором свойств без определения класса. Анонимный тип определяется с помощью ключевого слова var и инициализатора объектов:
var user = new { Name = "Tom", Age = 34 };
//В данном случае user - это объект анонимного типа, у которого определены два свойства Name и Age. И мы также можем использовать его свойства, как и у обычных объектов классов. Однако тут есть ограничение - свойства анонимных типов доступны только для чтения.
//При этом во время компиляции компилятор сам будет создавать для него имя типа и использовать это имя при обращении к объекту. Нередко анонимные типы имеют имя наподобие "<>f__AnonymousType0'2".

//Кроме исользованной выше формы инициализации, когда мы присваиваем свойствам некоторые значения, также можно использовать инициализаторы с проекцией (projection initializers), когда мы можем передать в инициализатор некоторые идиентификаторы, имена которых будут использоваться как названия свойств:

class User 
{
	public string Name;
}
class Program 
{
	static void Main()
	{
		User tom = new User {Name = "Tom"};
		int age = 34;
		var student = new {tom.Name, age}; //иницициализатор с проекцией
		Console.WriteLine(student.Name);
		Console.WriteLine(student.age);
		Console.Read();
		
		/*
		var user = new { Name = "Tom", Age = 34 };
		var student = new { Name = "Alice", Age = 21 };
		var manager = new { Name = "Bob", Age = 26, Company = "Microsoft" };
		 
		Console.WriteLine(user.GetType().Name); // <>f__AnonymousType0'2
		Console.WriteLine(student.GetType().Name); // <>f__AnonymousType0'2
		Console.WriteLine(manager.GetType().Name); // <>f__AnonymousType1'3
		*/
	}
}

//Короткий вариант
var student = new {Name = tom.Name, age=age);

//Массивы объектов анонимных типов
var people = new[]
{
	new {Name="Tom"}, new {Name="Bob"}
};
foreach (var p in people)
{
	Console.WriteLine(p.Name);
}

