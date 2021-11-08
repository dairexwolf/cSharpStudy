// Для удобного сохранения и извлечения объектов из файлов xml может использоваться класс XmlSerializer.

// Во-первых, XmlSerializer предполагает некоторые ограничения. Например, класс, подлежащий сериализации, должен иметь стандартный конструктор без параметров. Также сериализации подлежат только открытые члены. Если в классе есть поля или свойства с модификатором private, то при сериализации они будут игнорироваться.

// Во-вторых, XmlSerializer требует указания типа:

using System;
using System.IO;
using System.Xml.Serialization;

namespace Serialization
{
	// класс и члены объявлены как public
	[Serializable]
	public class Person
	{
		public string Name { get; set; }
		public int Age { get; set; }
		
		// стандартный конструктор без параметров
		public Person()
		{ }
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
			Person person = new Person("Neit",510);
			Console.WriteLine("Объект создан");
			
			// передаем в конструктор тип класса
			XmlSerializer formatter = new XmlSerializer(typeof(Person));
			
			// получаем поток, куда будем записывать сериализованный объект
			using (FileStream fs = new FileStream("persons.xml", FileMode.OpenOrCreate))
			{
				formatter.Serialize(fs, person);
				
				Console.WriteLine("Объект сериализован");
			}
			
			// десериализация
			using (FileStream fs = new FileStream("persons.xml", FileMode.OpenOrCreate))
			{
				Person newPerson = (Person)formatter.Deserialize(fs);
				
				Console.WriteLine("Объект десериализован");
				Console.WriteLine($"{newPerson.Name} --- {newPerson.Age}");
			}
			Console.ReadLine();
		}
	}
}

// Итак, класс Person общедоступный и имеет общедоступные свойства, поэтому он может сериализоваться. При создании объекта XmlSerializer передаем в конструктор тип класса. Метод Serialize добавляет данные в файл persons.xml. А метод Deserialize извлекает их оттуда.
// Если мы откроем файл persons.xml, то увидим содержание нашего объекта

// Равным образом мы можем сериализовать массив или коллекцию объектов, но главное требование состоит в том, чтобы в них был определен стандартный конструктор:

Person[] people = new Person[] { new Person("Duardin", 34), new Person("Neit", 32) };

XmlSerializer formatter = new XmlSerializer(typeof(Person[]));

FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate);
formatter.Serialize(fs, people);
fs.Close();

fs = new FileStream("people.xml", FileMode.OpenOrCreate);
Person[] newpeople = (Person[])formatter.Deserialize(fs);

fs.Close();
int length = newpeople.Length;
for (int i = 0; i < length; i++)
{
	Console.WriteLine($"Имя: {newpeople[i].Name}\t-\tВозраст: {newpeople[i].Age}");
}
Console.ReadLine();

// Но это был простой объект. Однако с более сложными по составу объектами работать так же просто. Например:

using System;
using System.IO;
using System.Xml.Serialization;

namespace Serialization
{
    // класс и члены объявлены как public
    [Serializable]
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Company Company { get; set; }

        // стандартный конструктор без параметров
        public Person()
        { }
        public Person(string name, int age, Company comp)
        {
            Name = name;
            Age = age;
            Company = comp;
        }
    }

    [Serializable]
    public class Company
    {
        public string Name { get; set; }

        // стандартный конструктор без параметров
        public Company() { }

        public Company(string name)
        {
            Name = name;
        }
    }

    class Program
    {
        static void Main()
        {
            Person person1 = new Person("Duardin", 121, new Company("Demonoebateli"));
            Person person2 = new Person("Neit", 510, new Company("Mage Guild"));
            Person[] people = new Person[] { person1, person2 };

            XmlSerializer formatter = new XmlSerializer(typeof(Person[]));

            using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
                formatter.Serialize(fs, people);

            using (FileStream fs = new FileStream("people.xml", FileMode.OpenOrCreate))
            {
                Person[] newpeople = (Person[])formatter.Deserialize(fs);
                int length = newpeople.Length;
                for (int i = 0; i < length; i++)
                {
                    Console.WriteLine($"Имя: {newpeople[i].Name}\t-\tВозраст: {newpeople[i].Age}\t-\tКомпания: {newpeople[i].Company.Name}");
                }
            }
            Console.ReadLine();
        }
    }
}

// Класс Person содержит свойство Company, которое будет хранить объект класса Company. Члены класса Company объявляются с модификатором public, кроме того также присутствует стандартный конструктор без параметров. 


