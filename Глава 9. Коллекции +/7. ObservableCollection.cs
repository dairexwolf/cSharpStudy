//Во-первых, класс ObservableCollection находится в пространстве имен System.Collections.ObjectModel, кроме того, также понадобятся ряд объектов из пространства System.Collections.Specialized, поэтому в начале подключаем эти пространства имен.

//Класс ObservableCollection определяет событие CollectionChanged, подписавшись на которое, мы можем обработать любые изменения коллекции.

//В обработчике этого события Users_CollectionChanged для получения всей информации о событии используется объект NotifyCollectionChangedEventArgs e. Его свойство Action позволяет узнать характер изменений. Оно хранит одно из значений из перечисления NotifyCollectionChangedAction.

//Свойства NewItems и OldItems позволяют получить соответственно добавленные и удаленные объекты. Таким образом, мы получаем полный контроль над обработкой добавления, удаления и замены объектов в коллекции.

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace HelloApp
{
	class User
	{
		public string Name {get; set; }
	}
	
	class Program
	{
		private static void Users_CollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				//если добавление
				case NotifyCollectionChangedAction.Add:		
				User newUser = e.NewItems[0] as User;
				Console.WriteLine($"Добавлен новый объект: {newUser.Name}");
				break;
				//если удаление
				case NotifyCollectionChangedAction.Remove:
				User oldUser = e.OldItems[0] as User;
				Console.WriteLine($"Удален объект: {oldUser.Name}");
				break;
				//если замена
				case NotifyCollectionChangedAction.Replace:
				User replacedUser = e.OldItems[0] as User;
				User replacingUser = e.NewItems[0] as User;
				Console.WriteLine($"Объект {replacedUser.Name} заменен объектом {replacingUser.Name}");
				break;
			}
		}
		static void Main()
		{
			//Инициализация коллекции
			ObservableCollection<User> users = new ObservableCollection<User>
			{
				new User { Name= "Bill" },
				new User { Name = "Tom" },
				new User { Name = "Alice" }
			};
			users.CollectionChanged += Users_CollectionChanged;
			
			users.Add(new User { Name = "Bob" });
			users.RemoveAt(1);
			users[0] = new User{ Name = "Anders" };
			
			foreach(User user in users)
			{
				Console.WriteLine(user.Name);
			}
			
			//Console.Read();
		}
	}
}