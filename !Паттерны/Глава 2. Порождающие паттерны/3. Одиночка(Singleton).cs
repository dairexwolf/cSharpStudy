// Одиночка (Singleton, Синглтон) - порождающий паттерн, который гарантирует, что для определенного класса будет создан только один объект, а также предоставит к этому объекту точку доступа.

// Когда надо использовать Синглтон? Когда необходимо, чтобы для класса существовал только один экземпляр

// Синглтон позволяет создать объект только при его необходимости. Если объект не нужен, то он не будет создан. В этом отличие синглтона от глобальных переменных.

// Классическая реализация данного шаблона проектирования на C# выглядит следующим образом:

class Singleton
{
	// В классе определяется статическая переменная - ссылка на конкретный экземпляр данного объекта и приватный конструктор.
	private static Singleton instance;	
	private Singleton()
	{}
	
	public static getInstance()
	{
		if (instance == null)
			instance = new Singleton();
		return instance;
	}
}

// Для применения паттерна Одиночка создадим небольшую программу. Например, на каждом компьютере можно одномоментно запустить только одну операционную систему. В этом плане операционная система будет реализоваться через паттерн синглтон:


class Program
{
    static void Main()
    {
        Computer comp = new Computer();
        comp.Launch("Windows 10");
        Console.WriteLine(comp.OS.Name);

        // у нас не получится изменить ОС, т.к. объект создан
        comp.OS = OS.getInstance("Linux");
        Console.WriteLine(comp.OS.Name);

        Console.ReadLine();
    }
}

class Computer
{
    public OS OS { get; set; }
    public void Launch(string osName)
    {
        OS = OS.getInstance(osName);
    }
}
class OS
{
    private static OS instance;
    public string Name { get; private set; }

    protected OS(string name)
    {
        this.Name = name;
    }

    public static OS getInstance(string name)
    {
        if (instance == null)
            instance = new OS(name);
        return instance;
    }
}

// 														Синглтон и многопоточность
// При применении паттерна синглтон в многопоточным программах мы можем столкнуться с проблемой, которую можно описать следующим образом:

using Threading;

static void Main()
{
	(new Thread(() =>
	{
		Computer comp2 = new Computer();
		comp2.OS = OS.getInstance("Windows 10");
		Console.WriteLine(comp2.OS.Name);
	})).Start();
	
	Computer comp = new Computer();
	comp.Launch("Linux");
	Console.WriteLine(comp.OS.Name);
	Console.ReadLine();
}

// Здесь запускается дополнительный поток, который получает доступ к синглтону. Параллельно выполняется тот код, который идет запуска потока и кторый также обращается к синглтону. Таким образом, и главный, и дополнительный поток пытаются инициализровать синглтон нужным значением - "Windows 10", либо "Windows 8.1". Какое значение сиглтон получит в итоге, пресказать в данном случае невозможно.

// В итоге мы сталкиваемся с проблемой инициализации синглтона, когда оба потока одновременно обращаются к коду:

if (instance == null)
    instance = new OS(name);

// Чтобы решить эту проблему, перепишем класс синглтона следующим образом:

class OS
{
    private static OS instance;
    public string Name { get; private set; }
	private static object syncRoot = new Object();

    protected OS(string name)
    {
        this.Name = name;
    }

    public static OS getInstance(string name)
    {
        if (instance == null)
		{
			lock (syncRoot)
			{
				if (instance == null)
					instance = new OS(name);
			}
		}
        return instance;
    }
}
// Чтобы избежать одновременного доступа к коду из разных потоков критическая секция заключается в блок lock.
// Даже то, что объекта 2, все равно означает, что объект у синглтона может быть только 1. Ссылка на него хранится в статистической ячейке памяти

// 													Другие реализации синглтона
// Выше были рассмотрены общие стандартные реализации: потоконебезопасная и потокобезопасная реализации паттерна. Но есть еще ряд дополнительных реализаций, которые можно рассмотреть.

//	Потокобезопасная реализация без использования lock

public class Singleton
{
	private static readonly Singleton instance = new Singleton();
	
	public string Date { get; private set; }
	
	private Singleton()
	{
		Date = System.DateTime.Now.TimeOfDay.ToString();
	}
	
	public static Singleton GetInstance()
	{
		return instance;
	}
}

// Данная реализация также потокобезопасная, то есть мы можем использовать ее в потоках так:

(new Thread(() =>
{
	Singleton singleton1 = Singleton.GetInstance();
	Console.WriteLine(singleton1.Date);
})).Start();

Singleton singleton2 = Singleton.GetInstance();
Console.WriteLine(singleton2.Date);

// Потокобезопасность этой реализации основывается на том, что статический конструктор (или, другими словами, инициализатор типа) в одном домене гарантировано вызывается не более одного раза. и было бы хорошо добавить о неявном статическом конструкторе и его вызове перед конструктором.

// 													Lazy-реализация
// Определение объекта синглтона в виде статического поля класса открывает нам дорогу к созданию Lazy-реализации паттерна Синглтон, то есть такой реализации, где данные будут инициализироваться только перед непосредственным использованием. Поскольку статические поля инициализируются перед первым доступом к статическому членам класса и перед вызовом статического конструктора (при его наличии). Однако здесь мы можем столкнуться с двумя трудностями.

// Во-первых, класс синглтона может иметь множество статических переменных. Возможно, мы вообще не будем обращаться к объекту синглтона, а будем использовать какие-то другие статические переменные:

public class Singleton
{
	private static readonly Singleton instance = new Singleton();
	public static string text = "hello";
	public string Date { get; private set; }
	
	private Singleton()
	{
		Console.WriteLine($"Singleton ctor {DateTime.Now.TimeOfDay}");
		Date = DateTime.Now.TimeOfDay.ToString();
	}
	
	public static Singleton GetInstance()
	{
		Console.WriteLine($"GetInstance {DateTime.Now.TimeOfDay}");
		Thread.Sleep(200);
		return instance;
	}
}
class Program
{
	static void Main()
	{
		Console.WriteLine($"Main {DateTime.Now.TimeOfDay}");
		Console.WriteLine(Singleton.text);
		Console.ReadLine();
	}
}

// В данном случае идет только обращение к переменной text, однако статическое поле instance также будет инициализировано.
// В данном случае мы видим, что статическое поле instance инициализировано. Для решения этой проблемы выделим отдельный внутренний класс в рамках класса синглтона:

public class Singleton
{
	public static string text = "hello";
	public string Date { get; private set; }
	
	private Singleton()
	{
		Console.WriteLine($"Singleton ctor {DateTime.Now.TimeOfDay}");
		Date = DateTime.Now.TimeOfDay.ToString();
	}
	
	public static Singleton GetInstance()
	{
		Console.WriteLine($"GetInstance {DateTime.Now.TimeOfDay}");
		Thread.Sleep(200);
		return Nested.instance;
	}
	
	private class Nested
	{
		internal static readonly Singleton instance = new Singleton();
	}
}

// Теперь статическая переменная, которая представляет объект синглтона, определена во вложенном классе Nested. Чтобы к этой переменной можно было обращаться из класса синглтона, она имеет модификатор internal, в то же время сам класс Nested имеет модификатор private, что позволяет гарантировать, что данный класс будет доступен только из класса Singleton.

// Теперь объект синглтона не инициализируется.

// Далее мы сталкиваемся со второй проблемой: статические поля инициализируются перед первым доступом к статическому членам класса и перед вызовом статического конструктора (при его наличии). Но когда именно? Если класс содержит статические поля, не содержит статического конструктора, то время инициализации статических полей зависит от реализации платформы. Нередко это непосредственно перед первым использованием, но тем не менее момент точно не определен - это может быть происходить и чуть раньше. Однако если класс содержит статический конструктор, то статические поля будут инициализироваться непосредственно либо при создании первого экземпляра класса, либо при первом обращении к статическим членам класса.

static void Main(string[] args)
{
    Console.WriteLine($"Main {DateTime.Now.TimeOfDay}");
    Console.WriteLine(Singleton.text);
 
    Singleton singleton1 = Singleton.GetInstance();
    Console.WriteLine(singleton1.Date);
    Console.Read();
}

// Мы видим, что код метода GetInstance, который идет до вызова конструктора класса Singleton, выполняется после выполнения этого конструктора. Поэтому добавим в выше определенный класс Nested статический конструктор:

private class Nested
    {
        static Nested() { }
        internal static readonly Singleton instance = new Singleton();
    }
	
// 														Реализация через Lazy<T>
// Еще один способ создания синглтона представляет использование класса Lazy<T>:

public class Singleton
{
	private static readonly Lazy<Singleton> lazy =
		new Lazy<Singleton>(() => new Singleton());
	
	public string Name { get; private set; }
	private Singleton ()
	{
		Name = System.Guid.NewGuid().ToString();
	}
	
	public static Singleton GetInstance()
	{
		return lazy.Value;
	}
}


