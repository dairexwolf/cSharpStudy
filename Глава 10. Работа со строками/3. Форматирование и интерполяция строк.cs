//												Формирование строк
//При выводе строк в консоли с помощью метода Console.WriteLine мы можем применять форматирование вместо конкатенации:

class Program
{
	static void Main(string args[])
	{
		Person person = new Person { Name = "Tom", Age = 23};
		
		Console.WriteLine("Имя: {0} Возраст: {1}", person.Name, person.Age);
		
		// тоже самое с помощью метода Format
		string output = String.Format("Имя: {0}, Возраст: {1}", person.Name, person.Age);
		Console.WriteLine(output);
	}
}
class Person
{
	public string Name {get; set; }
	public int Age { get; set; }
}

//Метод Format принимает строку с плейсхолдерами типа {0}, {1} и т.д., а также набор аргументов, которые вставляются на место данных плейсхолдеров. В итоге генерируется новая строка.

//В методе Format могут использоваться различные спецификаторы и описатели, которые позволяют настроить вывод данных. Рассмотрим основные описатели:
//										Форматирование валюты
//Для форматирования валюты используется описатель "C":

double number = 23.7;
string result = String.Format("{0:C}", number);
Console.WriteLine(result);	// $ 23.7
result = String.Format("{0:C2}", number);
Console.WriteLine(result);	// $ 23.70

// Число после описателя указывает, сколько чисел будет использоваться после разделителя между целой и дробной частью. При выводе также добавляется обозначение денежного знака для текущей культуры компьютера.

//											Форматирование целых чисел
// Для форматирования целочисленных значение применяется описатель "d":

int num = 23;
string result = String.Format("{0:d}", num);
Console.WriteLine(result);
result = String.Format("{0:d4}", num);
Console.WriteLine(result);

//Число после описателя указывает, сколько цифр будет в числовом значении. Если в исходном числе цифр меньше, то к нему добавляются нули.

//										Форматирование дробных чисел
// Для форматирования дробны чисел используется описатель F, число после которого указывает, сколько знаков будет использоваться после разделителя между целой и дробной частью. Если исходное число - целое, то к нему добавляются разделитель и нули.

int num = 23;
string result = String.Format("{0:f}", num);
Console.WriteLine(result);
// результат: 23,00

double dnum = 12.345;
result = String.Format("{0:f4}", dnum);
Console.WriteLine(result);
// результат: 12,3450

double dnum2 = 25.07;
result = String.Format("{0:f1}", dnum2);
Console.WriteLine(result);
// результат: 25,1

//										Формат процентов
// Описатель "P" задает отображение процентов. Используемый с ним числовой спецификатор указывает, сколько знаков будет после запятой:
decimal number = 0.12354m;
Console.WriteLine("{0:P1}", number);	// 12.4 %

//										Настраиваемые форматы (маска)
// Используя знак #, можно настроить формат вывода. Например, нам надо вывести некоторое число в формате телефона +х (ххх)ххх-хх-хх:
long number = 78005553535;
string result = String.Format("{0:+# (###) ###-##-##}", number);
Console.WriteLine(result);	// +7 (800) 555-35-35

//										ToString
//Метод ToString() не только получает строковое описание объекта, но и может осуществлять форматирование. Он поддерживает те же описатели, что используются в методе Format:

long number = 78005553535;
Console.WriteLine(number.ToString("+# (###) ###-##-##"));	//+7 (800) 555-35-35

double money = 24.8;
Console.WriteLine(money.ToString("C2"));	// $ 24,80

//										Интерполяция строк
//Интерполяция строк — это процесс замены заполнителей в строке значениями строковой переменной.
//Начиная с версии языка C# 6.0 была добавлена такая функциональность, как интерполяция строк. Эта функциональность призвана заменить форматирование строк. Так, перепишем пример с выводом значений свойств объекта Person:

class Program
{
	static void Main(string args[])
	{
		Person person = new Person { Name = "Tom", Age = 23};
		
		//Знак доллара перед строкой указывает, что будет осуществляться интерполяция строк.
		//Внутри строки опять же используются плейсхолдеры {...}, только внутри фигурных скобок уже можно напрямую писать те выражения, которые мы хотим вывести.
		Console.WriteLine($"Имя: {person.Name} Возраст: {person.Age}");
	}
}
class Person
{
	public string Name {get; set; }
	public int Age { get; set; }
}

//Интерполяция по сути представляет более лаконичное форматирование. При этом внутри фигурных скобок мы можем указывать не только свойства, но и различные выражения языка C#:

int x = 8;
int y = 7;
string result = $"{x} + {y} = {x + y}";
Console.WriteLine(result);	// 8 + 7 = 15

//В следующем примере проверяем, не равен ли person значению null. Если не равен, то выводим его имя, иначе выводим какое-нибудь имя по умолчанию:


class Program
{
	static void Main(string args[])
	{
		Person person = new Person { Name = "Tom", Age = 23};
		
		Console.WriteLine("Имя: {0} Возраст: {1}", person.Name, person.Age);
		
		// тоже самое с помощью метода Format
		string output = String.Format("Имя: {0}, Возраст: {1}", person.Name, person.Age);
		Console.WriteLine(output);
		person = null;
		output = $"{person?.Name??"Нет имени"}";
		Console.WriteLine(output);
	}
}
class Person
{
	public string Name {get; set; }
	public int Age { get; set; }
}

//Уже внутри строки можно применять форматирование. В этом случае мы можем применять все те же описатели, что и в методе Format. Например, выведем номер телефона в формате +x xxx-xxx-xx-xx:

string number = "78005553535";
Console.WriteLine($"{number:+# (###) ### ##-##}");

// Добавляем пространство до и после форматируемого вывода:
class Program
{
	static void Main(string args[])
	{
		Person person = new Person { Name = "Tom", Age = 23};
		
		Console.WriteLine($"Имя: {person.Name, -5}"); 
		Console.WriteLine($"Возраст: {person.Age, 5}");

	}
}
class Person
{
	public string Name {get; set; }
	public int Age { get; set; }
}