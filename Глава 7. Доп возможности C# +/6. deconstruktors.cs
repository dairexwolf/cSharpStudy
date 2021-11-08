class Person
{
	public string Name {get; set;}
	public int Age {get; set;}
	//Деконструктор
	public void Deconstruct (out string name, out int age) //метод Deconstruct должен принимать как минимум два выходных параметра.
	{
		name=this.name;
		age = this.Age;
	}
}

class Program 
{
	Person p = new Person {Name = "Tom", Age = 33 };
	(string name, int age) = p;
	Console.WriteLine(name);    // Tom
	Console.WriteLine(age);     // 33
}
//По сути деконструкторы это не более,чем синтаксический сахар. Это все равно, что если бы мы написали в предыдущих версиях C# следующий набор выражений:
/*
Person person = new Person { Name = "Tom", Age = 33 };
 
string name; int age;
person.Deconstruct(out name, out age);
*/