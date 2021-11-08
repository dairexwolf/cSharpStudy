// Не работает, и говорят, не актуально, поэтому буду копипастить.

// Основная функциональность по работе с JSON сосредоточена в пространстве имен System.Text.Json.

// Ключевым типом является класс JsonSerializer, который и позволяет сериализовать объект в json и, наоборот, десериализовать код json в объект C#.

/*
Для сохранения объекта в json в классе JsonSerializer определен статический метод Serialize(), который имеет ряд перегруженных версий. Некоторые из них:

- string Serialize(Object obj, Type type, JsonSerializerOptions options): сериализует объект obj типа type и возвращает код json в виде строки. Последний необязательный параметр options позволяет задать дополнительные опции сериализации

- string Serialize<T>(T obj, JsonSerializerOptions options): типизированная версия сериализует объект obj типа T и возвращает код json в виде строки.

- Task SerializeAsync(Object obj, Type type, JsonSerializerOptions options): сериализует объект obj типа type и возвращает код json в виде строки. Последний необязательный параметр options позволяет задать дополнительные опции сериализации

- Task SerializeAsync<T>(T obj, JsonSerializerOptions options): типизированная версия сериализует объект obj типа T и возвращает код json в виде строки.

- object Deserialize(string json, Type type, JsonSerializerOptions options): десериализует строку json в объект типа type и возвращает десериализованный объект. Последний необязательный параметр options позволяет задать дополнительные опции десериализации

- T Deserialize<T>(string json, JsonSerializerOptions options): десериализует строку json в объект типа T и возвращает его.

- ValueTask<object> DeserializeAsync(Stream utf8Json, Type type, JsonSerializerOptions options, CancellationToken token): десериализует текст UTF-8, который представляет объект JSON, в объект типа type. Последние два параметра необязательны: options позволяет задать дополнительные опции десериализации, а token устанавливает CancellationToken для отмены задачи. Возвращается десериализованный объект, обернутый в ValueTask

- ValueTask<T> DeserializeAsync<T>(Stream utf8Json, JsonSerializerOptions options, CancellationToken token): десериализует текст UTF-8, который представляет объект JSON, в объект типа T. Возвращается десериализованный объект, обернутый в ValueTask
*/

// Рассмотрим применение класса на простом примере. Сериализуем и десериализуем простейший объект:

using System;
using System.Text.Json;

namespace JsonSerializer
{
	class Person
	{
		public string Name { get; set; }
		public int Age { get; set; }
	}
	class Program
	{
		static void Main()
		{
			Person tom = new Person { Name= "Tom", Age = 22 };
			string json = JsonSerializer.Serialize<Person>(tom);
			Console.WriteLine(json);
			person restoredPerson = JsonSerializer.Deserialize<Person>(json);
			Console.WriteLine(restoredperson.Name);
		}
	}
}

// Здесь вначале сериализуем с помощью метода JsonSerializer.Serialize() объект типа Person в стоку с кодом json. Затем обратно получаем из этой строки объект Person посредством метода JsonSerializer.Deserialize().












