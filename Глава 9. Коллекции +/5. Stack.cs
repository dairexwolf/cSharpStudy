/* Класс Stack<T> представляет коллекцию, которая использует алгоритм LIFO ("последний вошел - первый вышел"). При такой организации каждый следующий добавленный элемент помещается поверх предыдущего. Извлечение из коллекции происходит в обратном порядке - извлекается тот элемент, который находится выше всех в стеке.

//В классе Stack можно выделить два основных метода, которые позволяют управлять элементами:
* Push: добавляет элемент в стек на первое место
* Pop: извлекает и возвращает первый элемент из стека
* Peek: просто возвращает первый элемент из стека без его удаления
*/


using System;
using System.Collections.Generic;

namespace CumZone
{
	class Program
	{
		static void Main()
		{
			Stack<int> numbers = new Stack<int>();
			
			numbers.Push(3); 	// в стеке 3
			numbers.Push(6);	// в стеке 6, 3
			numbers.Push(12);	// в стеке 12, 6, 3
			// Так как вверху стека будет находится числа 12, то оно извлекается
			
			int stackElement = numbers.Pop();	// в стеке 6, 3
			Console.WriteLine("Взятое число: " + stackElement);
			Console.WriteLine("Числа в стеке:");
			// Перебор стека
			foreach (int i in numbers)
			{
				Console.WriteLine(i);
			}
			
			Stack<Person> persons = new Stack<Person>();
			persons.Push(new Person() { Name = "Gachi" });
			persons.Push(new Person() { Name = "Muchi" });
			persons.Push(new Person() { Name = "Vichi" });
			persons.Push(new Person() { Name = "Guchi" });
			
			Console.WriteLine("Осталось людей в стеке:");
			//Перебор стека
			foreach (Person p in persons)
			{
				Console.WriteLine(p.Name);
			}
			
			//Выгружаем элемент из стека
			Person person = persons.Pop();
			Console.WriteLine("Выгруженный: " + person.Name);
			Console.WriteLine("Осталось людей в стеке:");
			//Перебор стека
			foreach (Person p in persons)
			{
				Console.WriteLine(p.Name);
			}
			
		}
	}
	class Persons
	{
		public string Name {get; set; }
	}
}
