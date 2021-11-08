//Итак, класс ArrayList представляет коллекцию объектов. И если надо сохранить вместе разнотипные объекты - строки, числа и т.д., то данный класс как раз для этого подходит.
/*
Основные методы класса:
* int Add(object value): добавляет в список объект value
* void AddRange(ICollection col): добавляет в список объекты коллекции col, которая представляет интерфейс ICollection - интерфейс, реализуемый коллекциями.
* void Clear(): удаляет из списка все элементы
* bool Contains(object value): проверяет, содержится ли в списке объект value. Если содержится, возвращает true, иначе возвращает false
* void CopyTo(Array array): копирует текущий список в массив array.
* ArrayList GetRange(int index, int count): возвращает новый список ArrayList, который содержит count элементов текущего списка, начиная с индекса index
* int IndexOf(object value): возвращает индекс элемента value
* void Insert(int index, object value): вставляет в список по индексу index объект value
* void InsertRange(int index, ICollection col): вставляет в список начиная с индекса index коллекцию ICollection
* int LastIndexOf(object value): возвращает индекс последнего вхождения в списке объекта value
* void Remove(object value): удаляет из списка объект value
* void RemoveAt(int index): удаляет из списка элемент по индексу index
* void RemoveRange(int index, int count): удаляет из списка count элементов, начиная с индекса index
* void Reverse(): переворачивает список
* void SetRange(int index, ICollection col): копирует в список элементы коллекции col, начиная с индекса index
* void Sort(): сортирует коллекцию

Кроме того, с помощью свойства Count можно получить количество элементов в списке.
*/

using System;
using System;Collections;

namespace Collections
{
	class Program
	{
		static void Main()
		{
			ArrayList list = new ArrayList();
			list.Add(2.3);	// заносим в список объект типа double
			list.Add(3);	// заносим в список объект типа int
			list.AddRange(new string[] {"Hello", "world!" });	// заносим в списк строковый массив
			
			//перебор значений
			foreach (object o in list)
			{
				Console.WriteLine(o);
			}
			
			list.RemoveAt(0);	// удаляем первый элемент
			list.Reverse();		// переворачиваем список
			Console.WriteLine(list[0]);	//получение элемента по индексу
			// перебор списка
			for (int i = 0; i < list.Count; i++)
			{
				Console.WriteLine(list[i]);
			}

			
		}
	}
}