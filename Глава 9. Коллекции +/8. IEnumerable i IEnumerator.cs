// Как мы увидели, основной для большинства коллекций является реализация интерфейсов IEnumerable и IEnumerator. Благодаря такой реализации мы можем перебирать объекты в цикле foreach
// Перебираемая коллекция должна реализовать интерфейс IEnumerable.
// Интерфейс IEnumerable имеет метод, возвращающий ссылку на другой интерфейс - перечислитель:
public interface IEnumerable
{
	IEnumerator GetEnumerator();	
}
// А интерфейс IEnumerator определяет функционал для перебора внутренних объектов в контейнере:
public interface IEnumerator
{
	bool MoveNext();			//перемещение на одну позицию вперед в контейнере элементов
	object Current { get; }		//текущий элемент в контейнере
	void Reset();				//перемещение в начало контейнера
}
// Метод MoveNext() перемещает указатель на текущий элемент на следующую позицию в последовательности. Если последовательность еще не закончилась, то возвращает true. Если же последовательность закончилась, то возвращается false.
// Свойство Current возвращает объект в последовательности, на который указывает указатель.
// Метод Reset() сбрасывает указатель позиции в начальное положение.
// Каким именно образом будет осуществляться перемещение указателя и получение элементов зависит от реализации интерфейса. В различных реализациях логика может быть построена различным образом.

// Например, без использования цикла foreach перебирем коллекцию с помощью интерфейса IEnumerator:

using System;
using System.Collections;

class Program
{
	static void Main()
	{
		int[] numbers = { 0, 2, 4, 6, 8, 10 };
		
		IEnumerator ie = numbers.GetEnumerator();	// получаем IEnumerator
		while (ie.MoveNext())						// пока не будет возвращено false
		{
			int item = (int)ie.Current;				// берем элемент на текущей позиции
			Console.WriteLine(item);
		}
		ie.Reset();									// сбрасываем указатель в начало массива
		//Console.Read();
	}
}

										Реализация IEnumerable и IEnumerator
//Рассмотрим простешую реализацию IEnumerable на примере:
using System;
using System.Collections;

//В данном случае класс Week, который представляет неделю и хранит все дни недели, реализует интерфейс IEnumerable. Однако в данном случае мы поступили очень просто - вместо реализации IEnumerator мы просто возвращаем в методе GetEnumerator объект IEnumerator для массива.
class Week : IEnumerable
{
	string [] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
	
	public IEnumerator GetEnumerator()
	{
		return days.GetEnumerator();
	}
	
}
//Благодаря этому мы можем перебрать все дни недели в цикле foreach.
class Program 
{
	static void Main()
	{
		Week week = new Week();
		foreach(var day in week) 
			Console.WriteLine(day);
		//Console.Read();
	}
}

//В то же время стоит отметить, что для перебора коллекции через foreach в принципе необязательно реализовать интерфейс IEnumerable. Достаточно в классе определить публичный метод GetEnumerator, который бы возвращал объект IEnumerator. Например:
class Week	//Реализация не обязательна
{
    string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", 
                        "Friday", "Saturday", "Sunday" };
 
    public IEnumerator GetEnumerator()
    {
        return days.GetEnumerator();
    }
}

//Однако это было довольно просто - мы просто используем уже готовый перчислитель массива. Однако, возможно, потребуется задать свою собственную логику перебора объектов. Для этого реализуем интерфейс IEnumerator:

using System;
using System.Collections;

namespace Hey
{
	// класс, позволяющий нам перечислять основной класс
	class WeekEn : IEnumerator
	{
		string[] days;
		int position = -1;
		public WeekEn (string[] days)
		{
			this.days = days;
		}
		public object Current
		{
			get
			{
				if (position == -1 || position >= days.Length)
					throw new InvalidOperationException();
				return days[position];
			}
		}
		public bool MoveNext()
		{
			if (position < days.Length - 1)
			{
				position++;
				return true;
			}
			else 
				return false;
		}
		public void Reset()
		{
			position = -1;
		}
	}
	//основной класс
	class Week
	{
		string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", 
                        "Friday", "Saturday", "Sunday" };
		public IEnumerator GetEnumerator()
		{
			return new WeekEn(days);
		}
	}
	class Program
	{
		static void Main()
		{
			Week week = new Week();
			foreach (var day in week)
			{
				Console.WriteLine(day);
			}
			//Console.Read();
		}
	}
}

//Здесь теперь класс Week использует не встроенный перечислитель, а WeekEnumerator, который реализует IEnumerator.

//Ключевой момент при реализации перечислителя - перемещения указателя на элемент. В классе WeekEnumerator для хранения текущей позиции определена переменная position. Следует учитывать, что в самом начале (в исходном состоянии) указатель должен указывать на позицию условно перед первым элементом. Когда будет производиться цикл foreach, то данный цикл вначале вызывает метод MoveNext и фактически перемещает указатель на одну позицию в перед и только затем обращается к свойству Current для получения элемента в текущей позиции.

//В примерах выше использовались необобщенные версии интерфейсов, однако мы также можем использовать их обобщенные двойники:

using System;
using System.Collections;
using System.Collections.Generic;

class Week 
{
	string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", 
                        "Friday", "Saturday", "Sunday" };
	public IEnumerator<string> GetEnumerator()
	{
		return new WeekEn(days);
	}
}

class WeekEn : IEnumerator<string>
{
	string[] days;
	int position= -1;
	public WeekEn (string[] days)
	{
		this.days = days; 
	}
	
	public string Current
	{
		get
		{
			if (position == -1 || position >= days.Length)
				throw new InvalidOperationException();
			return days[position];
		}
	}
	
	//сокращенная запись свойства
	//Происходит явная реализация свойства интерфейса IEnumerator, в которой нет реализации). Тем самым мы говорим, что не хотим реализовывать свойство Сurrent, которое возвращало бы тип Object. Вместо этого мы предоставляем реализацию свойства Current для string, чтобы избежать приведений типа.
	object IEnumerator.Current => throw new NotImplementedException();
	
	public bool MoveNext()
	{
		if (position < days.Length - 1)
		{
			position++;
			return true;
		}
		else
			return false;
	}
	public void Reset()
	{
		position=-1;
	}
	public void Dispose() { }
}
class Program
{
	static void Main()
	{
		Week week = new Week();
		foreach (var day in week)
			Console.WriteLine(day);
		//Console.Read();
	}
}