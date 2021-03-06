// При создании приложения для него определяется набор сборок, которые будут использоваться. В проекте указываются ссылки на эти сборки, и когда приложение выполняется, при обращении к функционалу этих сборок они автоматически подгружаются.
// Но также мы можем сами динамически подгружать другие сборки, на которые в проекте нет ссылок.

// Для управления сборками в пространстве имен System.Reflection имеется класс Assembly. С его помощью можно загружать сборку, исследовать ее.
// Чтобы динамически загрузить сборку в приложение, надо использовать статические методы Assembly.LoadFrom() или Assembly.Load().

// Метод LoadFrom() принимает в качестве параметра путь к сборке. Например, исследуем сборку на наличие в ней различных типов:

static void Main()
{
	Assembly asm = Assembly.LoadFrom("myApp.dll");
	
	Console.WriteLine(asm.FullName);
	// Получаем все типы из сборки myApp.dll
	Type[] types = asm.GetTypes();
	foreach(Type t in types)
		Console.WriteLine(t.Name);
	
	
}

// dll мне лень создавать, так что сделаем вид, что все круто и работает

// В данном случае для исследования указывается сборка MyApp.dll. Здесь использован относительный путь, так как сборка находится в одной папке с приложением - в проекте в каталоге bin/Debug/netcoreapp3.x. Можно в принципе в качестве имени указать и имя текущего приложение. В этом случае программа будет исследовать саму себя. В любом случае стоит учитывать, что загрузке подлежат (по крайней мере в .NET Core 3.0) сборки с расширением dll, но не exe.
// Метод Load() действует аналогично, только в качестве его параметра передается дружественное имя сборки, которое нередко совпадает с именем приложения: 

Assembly asm = Assembly.Load("MyApp");


// Дополнение: Assembly.Load и Assembly.LoadFrom используют разные контексты загрузки. Assembly.Load загружает assembly в контексте хоста assembly ( контекст по умолчанию ). для правильной загрузки все зависимые сборки должны быть доступны из пути приложения хоста, GAC или должны быть загружены вручную заранее. Assembly.LoadFrom не использует контекст хоста assembly. Он создает собственный контекст загрузки ( load-from context ) и способен автоматически разрешать сборки depandant, отсутствующие в контексте по умолчанию. Эти сборки из пути, который не находится в пути главного приложения.

// Получив все типы сборки с помощью метода GetTypes(), мы опять же можем применить к каждому типу все те методы, которые были рассмотрены в прошлой теме.

// 															Позднее связывание
// С помощью динамической загрузки мы можем реализовать технологию позднего связывания. Позднее связывание позволяет создавать экземпляры некоторого типа, а также использовать его во время выполнения приложения.
// Использование позднего связывания менее безопасно в том плане, что при жестком кодировании всех типов (ранее связывание) на этапе компиляции мы можем отследить многие ошибки. В то же время позднее связывание позволяет создавать расширяемые приложения, когда дополнительный функционал программы неизвестен, и его могут разработать и подключить сторонние разработчики.
// Ключевую роль в позднем связывании играет класс System.Activator. С помощью его статического метода Activator.CreateInstance() можно создавать экземпляры заданного типа.
// Например, динамически загрузим сборку и вызовем у ней некоторый метод. Допустим, загружаемая сборка MyApp.exe представляет следующую программу:

class Program
{
	public static double GetResult(int percent, double capital, int year)
	{
		for (int i =0; i < year; i++)
			capital += capital / 100 * percent;
		return capital;
	}
	
	static void Main()
	{
		Console.WriteLine(GetResult(4, 100, 2));
	}
}

// Итак, у нас стандартный класс Program с двумя методами. Теперь динамически подключим сборку с этой программой в другой программе и вызовем ее методы.

// Пусть наша основная программа будет выглядеть так:

static void Main()
{
	try
	{
		Assembly asm = Assmebly.LoadFrom("MyApp.dll");
		Type t = asm.GetType("MyApp.Program", true, true);
		
		// создаем экземпляр класса Program
		object obj = Activator.CreateInstance(t);
		
		// получаем метод GetResult
		MethodInfo method = t.GetMethod("GetResult");
		
		// вызываем метод, передаем ему значения для параметров и получаем результат
		object result = method.Invoke(obj, new object[] { 4, 100, 3 });
		// Если бы метод не принимал параметров, то вместо массива объектов использовалось бы значение null: method.Invoke(obj, null)
		Console.WriteLine(result);
	}
	catch(Exception ex)
	{
		Console.WriteLine(ex.Message);
	}
}

// Предположим, что исследуемая сборка находится в проекте в каталоге bin/Debug/netcoreapp3.x, тогда ее можно получить с помощью выражения Assembly asm = Assembly.LoadFrom("MyApp.dll").
// Затем с помощью метода GetType получаем тип - класс Program, который находится в сборке MyApp.dll: Type t = asm.GetType("MyApp.Program", true, true);
// Получив тип, создаем его экземпляр: object obj = Activator.CreateInstance(t). Результат создания - объект класса Program представляет собой переменную obj.
// И в конце остается вызвать метод. Во-первых, получаем сам метод: MethodInfo method = t.GetMethod("GetResult");. И потом с помощью метода Invoke вызываем его: object result = method.Invoke(obj, new object[] { 6, 100, 3 }). Здесь первый параметр представляет объект, для которого вызывается метод, а второй - набор параметров в виде массива object[].
// Так как метод GetResult возвращает некоторое значение, то мы можем его получить из метода в виде объекта типа object.
// В сборке MyApp.exe в классе Program также есть и другой метод - метод Main. Вызовем теперь его:

Console.WriteLine("Вызов метода Main");
method = t.GetMethod("Main", BindingFlags.DeclaredOnly | BindingFlags.Instance 
							| BindingFlags.NonPublic | BindingFlags.Static);

method.Invoke(obj, null);
// Так как метод Main является статическим и не публичным, то к нему используется соответствующая битовая маска BindingFlags.NonPublic | BindingFlags.Static. И поскольку он в качестве параметра принимает массив строк, то при вызове метода передается соответствующее значение: method.Invoke(obj, new object[]{new string[]{}}) (если использовать string[] args как параметр)


