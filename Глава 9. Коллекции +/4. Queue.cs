/*Класс Queue<T> представляет обычную очередь, работающую по алгоритму FIFO ("первый вошел - первый вышел").

// У класса Queue<T> можно отметить следующие методы:
* Dequeue: извлекает и возвращает первый элемент очереди
* Enqueue: добавляет элемент в конец очереди
* Peek: просто возвращает первый элемент из начала очереди без его удаления
*/

using System;
using System.Collections.Generic;

namespace CumColl
{
	class Person
	{
		public string Name { get; set; }
	}
	class Program
	{
		static void Main()
		{
			Queue<int> numbers = new Queue<int>();
			
			numbers.Enqueue(3);
			numbers.Enqueue(5);
			numbers.Enqueue(10);
			// Очередь: 3, 6, 12
			
			// Получаем первый элемент очереди
			int queueElement = numbers.Dequeue();
			// Теперь очередь 5, 8
			Console.WriteLine("Достанный элемент: " + queueElement);
			Console.WriteLine("В очереди остались:");
			foreach (int i in numbers)
			{
				Console.WriteLine(i);
			}
			
			Queue<Person> persons = new Queue<Person>();
			persons.Enqueue(new Person() { Name = "Gachi" });
			persons.Enqueue(new Person() { Name = "Muchi" });
			persons.Enqueue(new Person() { Name = "Vichi" });
			persons.Enqueue(new Person() { Name = "Guchi" });
			
			//Получаем первый элемент без его извлечения
			Person man = persons.Peek();
			Console.WriteLine("Первый в очереди: " + man.Name);
			
		Console.WriteLine("Сейчас в очереди {0} человек", persons.Count);
		foreach (Person p in persons)
		{
			Console.WriteLine(p.Name);
		}
		
		//Извлекаем человека из очереди
		Person person = persons.Dequeue();
		//Теперь в очереди: Muchi, Vichi, Guchi
		Console.WriteLine("Первый пошел: " + person.Name);
		Console.WriteLine("Сейчас в очереди {0} человек", persons.Count);
        foreach (Person p in persons)
		{
			Console.WriteLine(p.Name);
		}
		}
	}
}

