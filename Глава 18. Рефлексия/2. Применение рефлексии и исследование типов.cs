// Применим метод GetMembers и выведем всю информацию о типе:

using System;
using System.Reflection;

namespace MyApp
{
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public User(string n, int a)
        {
            Name = n;
            Age = a;
        }
        public void Display()
        {
            Console.WriteLine($"Имя: {Name}, возраст {Age}");
        }
        public int Payment(int hours, int perhour)
        {
            return hours * perhour;
        }

    }
    class Program
    {
        static void Main()
        {
            Type myType = Type.GetType("MyApp.User", false, true);

            foreach (MemberInfo mi in myType.GetMembers())
                Console.WriteLine($"{mi.DeclaringType} {mi.MemberType} {mi.Name}");
			// Свойство DeclaringType возвращает полное название типа.
			// Свойство MemberType возвращает значение из перечисления MemberTypes, в котором определены различные типы:
			/*
			MemberTypes.Constructor

			MemberTypes.Method

			MemberTypes.Field

			MemberTypes.Event

			MemberTypes.Property

			MemberTypes.NestedType
			*/
			// Вместо получения всех отдельных частей типа через метод GetMembers() можно по отдельности получать различные методы, свойства и т.д. через специальные методы.

        }
    }
}

// В данном случае мы получим все общедоступные!!!! члены класса User.

//             							Получение информации о методах
// При получении информации о методах нам могут быть полезны методы GetMethods() и GetParameters():

Type myType = Type.GetType("MyApp.User", false, true);

Console.WriteLine("Методы:");
foreach (MethodInfo method in myType.GetMethods())
{
	string modificator = "";
	if (method.IsStatic)
		modificator += "static ";
	if (method.IsVirtual)
		modificator += "virtual";
	Console.Write($"{modificator} {method.ReturnType.Name} {method.Name} (");
	// получаем все параметры
	ParameterInfo[] pars = method.GetParameters();
	for (int i=0; i<pars.Length; i++)
	{
		Console.Write($"{pars[i].ParameterType.Name} {pars[i].Name}");
		if(i+1<pars.Length)
			Console.Write(", ");
	}
	Console.WriteLine(");");
}

// В данном случае использовалась простая форма метода GetMethods(), которая извлекает все общедоступные публичные методы. Но мы можем использовать и другую форму метода: MethodInfo[] GetMethods(BindingFlags).
/*
Перечисление BindingFlags может принимать различные значения:

	DeclaredOnly: получает только методы непосредственно данного класса, унаследованные методы не извлекаются

	Instance: получает только методы экземпляра

	NonPublic: извлекает не публичные методы

	Public: получает только публичные методы

	Static: получает только статические методы

*/
// Объединяя данные значения с помощью побитовой операции ИЛИ можно комбинировать вывод. Например, получим только методы самого класса без унаследованных, как публичные, так и все остальные:

MethodInfo[] methods = myType.GetMethods(BindingFlags.DeclaredOnly 
						| BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);


// 												Получение конструкторов
// Для получения конструкторов применяется метод ConstructorInfo[] GetConstructors():
Type myType = Type.GetType("MyApp.User", false, true);

            Console.WriteLine("Конструкторы:");
            foreach (ConstructorInfo ctor in myType.GetConstructors())
            {
                Console.Write(myType.Name + " (");
                // получаем параметры конструктора
                ParameterInfo[] paramiters = ctor.GetParameters();
                for (int i = 0; i < paramiters.Length; i++)
                {
                    Console.Write(paramiters[i].ParameterType.Name + " " + paramiters[i].Name);
                    if (i + 1 < paramiters.Length)
                        Console.Write(", ");
                }
                Console.WriteLine(")");
            }

// 											Получение информации о полях и свойствах
// Для извлечения полей и свойств применяются соответственно методы GetFields() и GetProperties()

Type myType = Type.GetType("MyApp.User", false, true);

Console.WriteLine("Поля");
foreach (FieldInfo field in myType.GetFields())
	Console.WriteLine($"{field.FieldType} {field.Name}");
Console.WriteLine();
Console.WriteLine("Свойства:");
foreach (PropertyInfo prop in myType.GetProperties())
	Console.WriteLine($"{prop.PropertyType} {prop.Name}");

// 											Поиск реализованных интерфейсов
// Чтобы получить все реализованные типом интерфейсы, надо использовать метод GetInterfaces(), который возвращает массив объектов Type:

Type myType = Type.GetType("MyApp.User", false, true);

Console.WriteLine("Реализованные интерфейсы:");
foreach (Type i in myType.GetInterfaces())
	Console.WriteLine(i.Name);

// Так как каждый интерфейс представляет объект Type, то для каждого полученного интерфейса можно также применить выше рассмотренные методы для извлечения информации о свойствах и методах

// Объединив все выше рассмотренные методы, можно провести комплексное изучение определенного типа и получить все его составные части.