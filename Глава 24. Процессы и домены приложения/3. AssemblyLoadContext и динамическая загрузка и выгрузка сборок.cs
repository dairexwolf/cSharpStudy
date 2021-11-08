// В .NET Framework можно было создавать вторичные домены и загружать в них различные сборки. В .NET Core можно использовать только один домен. А для загрузки и выполнения сборок применяется класс AssemblyLoadContext из пространства имен System.Runtime.Loader, который представляет контекст загрузки и выгрузки сборок. Рассмотрим, как его использовать.

// Допустим, у нас есть следующая программа:

using System;
 
namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = 5;
            int result = Factorial(number);
 
            Console.WriteLine($"Факториал числа {number} равен {result}");
        }
 
        public static int Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}

// !!!Эта программа содержит метод вычисления факториала, и, допустим, она компилируется в сборку MyApp.dll. Загрузим эту сборку, чтобы использовать ее метод Factorial.

// Прежде всего нам потребуется реализовать класс AssemblyLoadContext, который является абстрактным. Простейшая реализация будет выглядеть следующим образом:

public class CustomAssemblyLoadContext : AssemblyLoadContext
{
	public CustomAssemblyLoadContext() : base(isCollectible: true)
	{ }
	
	protected override Assembly Load(AssemblyName assemblyName)
	{
		return null;
	}
}

// При наследовании абстрактного класса AssemblyLoadContext нам необходимо реализовать метод Load, который должен загружать сборку. Но в данном случае мы его не будем использовать, поэтому просто возвращаем null из метода.

// В конструкторе при обращении к конструктору базового класса устанавливается параметр isCollectible = true. Значение true указывает, что загруженные сборки можно выгружать.

/* Для загрузки сборок класс AssemblyLoadContext предоставляет ряд методов. Некоторые из них:

Assembly LoadFromAssemblyName (AssemblyName assemblyName): загружает определенную сборку по имени, которое представлено типом System.Reflection.AssemblyName

Assembly LoadFromAssemblyPath (string assemblyPath): загружает сборку по определенному пути (путь должен быть абсолютным)

Assembly LoadFromStream (System.IO.Stream stream): загружает определенную сборку из потока Stream

*/

// Использовав один из этих методов, мы можем получить доступ к сборке через тип Assembly и обращаться к ее функционалу.

// После завершения работы со сборкой мы можем вызвать у AssemblyLoadContext метод Unload() и выгрузить контекст со всеми загруженными сборками и тем самым снизить потребление памяти и увеличить общую производительность.

// Рассмотрим полный пример:

using System;
using System.Reflection;
using System.Runtime.Loader;
using System.IO;

namespace HelloApp
{
	class Program
	{
		static void Main()
		{
			LoadAssembly(6);
			// очистка
			GC.Collect();
			GC.WaitForPendingFinalizers();
			
			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
				Console.WriteLine(asm.GetName().Name);
			
			Console.Read();
		}
		
		private static void LoadAssembly(int number);
		{
			var context = new CustomAssemblyLoadContext();
			
			// установка обработчика выгрузки
			context.Unloading += Context_Unloading;
			// получаем путь к сборке MyApp
			var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "MyApp.dll");
			// загружаем сборку
			Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);
			// получаем тип Program из сборки MyApp.dll
			var type = assembly.GetType("MyApp.Program");
			// получаем метод Factorial
			var greetMethod = type.GetMethod("Factorial");
			
			// вызываем метод
			var instance = Actovator.CreateInstance(type);
			int result = (int)greetmethod.Invoke(instance, new object[] { number });
			// выводим результат метода на консоль
			Console.WriteLine("Факториал числа {0} = {1}", number, result);
			
			// смотрим, какие сборки у нас загружены
			Console.WriteLine();
			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
				Console.WriteLine(asm.GetName().Name);
			
			Console.WriteLine();
			
			// выгружаем контекст
			context.Unload();
		}
		
		// обработчик выгрузки контекста
		private static void Context_Unloading(AssemblyLoadContext obj)
		{
			Console.WriteLine("Библиотека MyApp выгружена\n");
		}
		
	}
	
	// реализация AssemblyLoadContext
	public class CustomAssemblyLoadContext : AssemblyLoadContext]
	{
		public CustomAssemblyLoadContext() : base(isCollectible: true) { }
		
		protected override Assembly Load(AssemblyName assemblyName)
		{
			return null;
		}
	}	
}

// В данном случае мы имеем простейшую реализацию класса AssemblyLoadContext в виде типа CustomAssemblyLoadContext. Абстрактный класс определяет событие Unloadig, благодар чему мы можем повесить обработчик и определить момент выгрузки контекста.

context.Unloading += Context_Unloading;

// Далее используется метод LoadFromAssemblyPath для загузки сборки MyApp.dll по абсолютному пути. В данном случае предполагается, что файл сборки находится в одой папке с текущим приложением.

Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);

// Получив сборку, обращаемся к методу Factorial и получаем факториал числа.

// Затем смотрим, какие сборки загружены в текущий домен. Среди них мы сможем найти и MyApp.dll. И в конце выгружаем контекст:

context.Unload();

// Все эти действия оформляются в виде отдельного метода, который вызывается в методе Main:

LoadAssembly(6);
// очистка
GC.Collect();
GC.WaitForPendingFinalizers();

// Но обратите внимание, что выгрузка контекста сама по себене означает немедленной очистки памяти. Вызов метода Unload только инициирует процесс выгрузки, реальная выгрузка произойдет лишь тогда, когда в дело вступит автоматический сборщик мусора и удалит соответствующие объекты. Поэтому для более быстрой очистки в конце вызываются методы GC.Collect() и GC.WaitForPendingFinalizers().


// https://metanit.com/sharp/tutorial/18.3.php

// Зачем нужен домен и загрузка сборок:
Представьте, что у вас есть некая программа по обработке изображений. Существуют встроенные фильтры, но, например, в папке UserFilters есть множество фильтров из других источников (dll файлы). Теперь вы можете зайти в пункт меню UserFilters, где будут перечислены сотни сборок и методов для конвертации изображений, выбрать нужный и нажать применить. После этого будет найдена и загружена нужная сборка, применен фильтр, получен результат, выгружена сборка (можно и не выгружать). А так пришлось бы прописывать сотни сборок в проекте, и это если у вас есть доступ к исходникам.
Пример может и не очень удачный, но, надеюсь, смысл сказанного понятен.

Иерархический порядок: Винда запускает процесс->каждый процесс один\несколько домен.приложений-> каждое домен.прилож -> один\несколько процессов->каждый процесс один\несколько приложений->каждое приложение один\несколько процессов-> каждый процесс один\несколько потоков
