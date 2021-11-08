// В прошлых темах было рассмотрено как сохранять и считывать информацию с текстовых и бинарных файлов с помощью классов из пространства System.IO. Но .NET также предоставляет еще один механизм для удобной работы с бинарными файлами и их данными - !!! бинарную сериализацию.

// Сериализация представляет процесс преобразования какого-либо объекта в поток байтов. После преобразования мы можем этот поток байтов или записать на диск или сохранить его временно в памяти.

// А при необходимости можно выполнить обратный процесс - десериализацию, то есть получить из потока байтов ранее сохраненный объект.

// 												Атрибут Serializable
// Чтобы объект определенного класса можно было сериализовать, надо этот класс пометить атрибутом Serializable:
[Serializable]
class Person
{
	public string Name { get; set; }
	public int Year { get; set; }
	
	public Person (string name, int year)
	{
		Name = name;
		Year = year;
	}
}

// При отстутствии данного атрибута объект Person не сможет быть сериализован, и при попытке сериализации будет выброшено исключение SerializationException.

// Сериализация применяется к свойствам и полям класса. Если мы не хотим, чтобы какое-то поле класса сериализовалось, то мы его помечаем атрибутом NonSerialized:

[Serializable]
class Person
{
	public string Name { get; set; }
	public int Year { get; set; }
	
	[NonSerialized]
	public string accNumber;
	
	public Person (string name, int year, string acc)
	{
		Name = name;
		Year = year;
		accNumber = acc;
	}
}

// При наследовании подобного класса, следует учитывать, что атрибут Serializable автоматически не наследуется. И если мы хотим, чтобы производный класс также мог бы быть сериализован, то опять же мы применяем к нему атрибут:

[Serializable]
class Worker : Person


// Для бинарной сериализации применяется класс BinaryFormatter:

using System;
using System.IO;
// Так как класс BinaryFormatter определен в пространстве имен System.Runtime.Serialization.Formatters.Binary, то в самом начале подключаем его.
using System.Runtime.Serialization.Formatters.Binary;

namespace StudySerialization
{
	[Serializable]
	class Person
	{
		public string Name { get; set; }
		public int Age { get; set; }
		
		public Person (string name, int age)
		{
			Name = name;
			Age = age;
		}
	}
	
	class Program
	{
		static void Main()
		{
			// объект для сериализации
			Person person = new Person("Tom", 29);
			Console.WriteLine("Объект создан");
			
			// создаем объект BinaryFormatter
			BinaryFormatter formatter = new BinaryFormatter();
			// получаем поток, куда будем записывать сериализованный объект
			using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
			{
				formatter.Serialize(fs, person);
				
				Console.WriteLine("Объект сериализирован");
			}
			
			// десериализация из файла people.dat
			using (FileStream fs = new FileStream("people.dat", FileMode.OpenOrCreate))
			{
				Person newPerson = (Person)formatter.Deserialize(fs);
				
				Console.WriteLine("Объект десериализован");
				Console.WriteLine($"Имя: {newPerson.Name} --- Возраст: {newPerson.Age}");
			}
			Console.ReadLine();
		}
	}
}

// У нас есть простенький класс Person, который объявлен с атрибутом Serializable. Благодаря этому его объекты будут доступны для сериализации.

// Далее создаем объект BinaryFormatter: BinaryFormatter formatter = new BinaryFormatter();

// Затем последовательно выполняем сериализацию и десериализацию. Для обоих операций нам нужен поток, в который либо сохранять, либо из которого считывать данные. Данный поток представляет объект FileStream, который записывает нужный нам объект Person в файл people.dat.

// Сериализация одним методом formatter.Serialize(fs, person) добавляет все данные об объекте Person в файл people.dat.

// При десериализации нам нужно еще преобразовать объект, возвращаемый функцией Deserialize, к типу Person: (Person)formatter.Deserialize(fs).

// Как вы видите, сериализация значительно упрощает процесс сохранения объектов в бинарную форму по сравнению, например, с использованием связки классов BinaryWriter/BinaryReader.

// Хотя мы взяли лишь один объект Person, но равным образом мы можем использовать и массив подобных объектов, список или иную коллекцию, к которой применяется атрибут Serializable. Посмотрим на примере массива:

Person person1 = new Person("Billy", 45);
Person person2 = new Person("Van", 39);

// массив для сериализации
Person[] people = new Person[] { person1, person2 };

BinaryFormatter formatter = new BinaryFormatter();

using (FileStream fs = new FileStream("people2.dat", FileMode.OpenOrCreate))
{
	// сериализуем весь массив people
	formatter.Serialize(fs, people);
	
	Console.WriteLine("Объект сериализован");
}

// десериализация
using (FileStream fs = new FileStream("people2.dat", FileMode.OpenOrCreate))
{
	Person[] deserilizePeople = (Person[])formatter.Deserialize(fs);
	
	foreach (Person p in deserilizePeople)
	{
		Console.WriteLine($"Имя: {p.Name}\tВозраст: {p.Age}");
	}
}




