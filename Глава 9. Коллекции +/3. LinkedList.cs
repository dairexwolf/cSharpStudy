/*Класс LinkedList<T> представляет двухсвязный список, в котором каждый элемент хранит ссылку одновременно на следующий и на предыдущий элемент.

//Если в простом списке List<T> каждый элемент представляет объект типа T, то в LinkedList<T> каждый узел представляет объект класса LinkedListNode<T>. Этот класс имеет следующие свойства:
* Value: само значение узла, представленное типом T
* Next: ссылка на следующий элемент типа LinkedListNode<T> в списке. Если следующий элемент отсутствует, то имеет значение null
* Previous: ссылка на предыдущий элемент типа LinkedListNode<T> в списке. Если предыдущий элемент отсутствует, то имеет значение null
//Используя методы класса LinkedList<T>, можно обращаться к различным элементам, как в конце, так и в начале списка:
* AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode): вставляет узел newNode в список после узла node.
* AddAfter(LinkedListNode<T> node, T value): вставляет в список новый узел со значением value после узла node.
* AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode): вставляет в список узел newNode перед узлом node.
* AddBefore(LinkedListNode<T> node, T value): вставляет в список новый узел со значением value перед узлом node.
* AddFirst(LinkedListNode<T> node): вставляет новый узел в начало списка
* AddFirst(T value): вставляет новый узел со значением value в начало списка
* AddLast(LinkedListNode<T> node): вставляет новый узел в конец списка
* AddLast(T value): вставляет новый узел со значением value в конец списка
* RemoveFirst(): удаляет первый узел из списка. После этого новым первым узлом становится узел, следующий за удаленным
* RemoveLast(): удаляет последний узел из списка
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
			LinkedList<int> numbers = new LinkedList<int>();
			
			// Вставляем узел со значением 1 на последнее место. 
			// Т.к. в сеписке нет узлов, он будет самым первым
			numbers.AddLast(1);
			// Вставляем узел со значением 2 на первое место
			numbers.AddFirst(2);
			// Вставляем после последнего узла новый узел со значением 3
			numbers.AddAfter(numbers.Last, 3);
			// Теперь у нас список имеет следующую последовательность: 2, 1, 3
			// Прогон списка
			foreach (int i in numbers)
			{
				Console.WriteLine(i);
			}
			
			LinkedList<Person> persons = new LinkedList<Person>();
			
			// Добавляем person в список и получим объект LinkedListNode<Person>, в котором хранится имя Tom
			LinkedListNode<Person> tom = persons.AddLast(new Person() { Name = "Tom"});
			persons.AddLast(new Person() { Name = "Jhon" });
			persons.AddFirst(new Person() { Name = "Billy" });
			
			// Получаем узел перед Томом и его значение
			Console.WriteLine(tom.Previous.Value.Name);
			// Получаем узел после тома и его значение
			Console.WriteLine(tom.Next.Value.Name);
			
		}
	}
}

//Здесь создаются и используются два списка: для чисел и для объектов класса Person. С числами, наверное, все более менее понятно. Разберем работу с классом Person.

//Методы вставки (AddLast, AddFirst) при добавлении в список возвращают ссылку на добавленный элемент LinkedListNode<T> (в нашем случае LinkedListNode<Person>). Затем управляя свойствами Previous и Next, мы можем получить ссылки на предыдущий и следующий узлы в списке.