// Паттерн Компоновщик (Composite) объединяет группы объектов в древовидную структуру по принципу "часть-целое" и позволяет клиенту одинаково работать как с отдельными объектами, так и с группой объектов.

// Образно реализацию паттерна можно представить в виде меню, которое имеет различные пункты. Эти пункты могут содержать подменю, в которых, в свою очередь, также имеются пункты. То есть пункт меню служит с одной стороны частью меню, а с другой стороны еще одним меню. В итоге мы однообразно можем работать как с пунктом меню, так и со всем меню в целом.

/*
Когда использовать компоновщик?

- Когда объекты должны быть реализованы в виде иерархической древовидной структуры

- Когда клиенты единообразно должны управлять как целыми объектами, так и их составными частями. То есть целое и его части должны реализовать один и тот же интерфейс
*/

class Client 
{
	public void Main()
	{
		Component root = new Composite("Root");
		Component leaf = new Leaf("Leaf");
		Composite subtree = new Composite("Subtree");
		root.Add(leaf);
		root.Add(subtree);
		root.Display();
	}
}
abstract class Component
{
	protected string name;
	
	public Component(string name)
	{
		this.name = name;
	}
	
	public abstract void Display();
	public abstract void Add(Component c);
	public abstract void Remove(Component c);
}
class Composite : Component
{
	List<Component> children = new List<Component>();
	
	public Composite(string name) : base(name)
	{ }
	
	public override void Add(Component component)
	{
		children.Add(component);
	}
	
	public override void Remove(Component component)
	{
		children.Remove(component);
	}
	
	public override void Display()
	{
		Console.WriteLine(name);
		Console.WriteLine("---");
		foreach (Component component in children)
			component.Display();
	}
}

class Leaf : Component
{
	public Leaf(string name)
		: base(name)
		{ }
	
	public override void Display()
	{
		Console.WriteLine(name);
	}
	
	public override void Add(Component component)
	{
		throw new NotImplementedException();
	}
	
	public override Remove(Component component)
	{
		throw new NotImplementedException();
	}
}

/*
Участники

- Component: определяет интерфейс для всех компонентов в древовидной структуре

- Composite: представляет компонент, который может содержать другие компоненты и реализует механизм для их добавления и удаления

- Leaf: представляет отдельный компонент, который не может содержать другие компоненты

- Client: клиент, который использует компоненты
*/

// Рассмотрим простейший пример. Допустим, нам надо создать объект файловой системы. Файловую систему составляют папки и файлы. Каждая папка также может включать в себя папки и файлы. То есть получается древовидная иерархическая структура, где с вложенными папками нам надо работать также, как и с папками, которые их содержат. Для реализации данной задачи и воспользуемся паттерном Компоновщик:

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Component fileSystem = new Directory("Файловая система");
        // Определяем новый диск
        Component diskC = new Directory("Диск C");
        // новые файлы
        Component pngFile = new File("123.png");
        Component docxFile = new File("Doc.docx");
        // новый диск
        Component diskD = new Directory("Диск D");
        // новые файлы
        Component jpgFile = new File("456.jpg");
        Component docFile = new File("Docx.doc");
        // добавляем файлы в диск C
        diskC.Add(pngFile);
        diskC.Add(docxFile);
        // добавляем файлы в диск D
        diskD.Add(jpgFile);
        diskD.Add(docFile);
        // добавляем диски в файловую систему
        fileSystem.Add(diskC);
        fileSystem.Add(diskD);
        // выводим все данные
        fileSystem.Print();
        Console.WriteLine();
        // удаляем с диска C файл
        diskC.Remove(pngFile);
        // создаем новую папку
        Component folder = new Directory("Новая папка");
        // создадим файлы
        Component txtFile = new File("readme.txt");
        Component csFile = new File("4. Компоновщик(Composite).cs");
        // добавляем в нее файлы
        folder.Add(txtFile);
        folder.Add(csFile);
        diskD.Add(folder);

        fileSystem.Print();

        Console.Read();
    }
}

abstract class Component
{
    protected string name;

    public Component(string name)
    {
        this.name = name;
    }

    public virtual void Add(Component component)
    { }
    public virtual void Remove(Component component)
    { }
    public virtual void Print()
    {
        Console.WriteLine(name);
    }
}

class Directory : Component
{
    private List<Component> components = new List<Component>();

    public Directory(string name)
        : base(name)
    { }

    public override void Add(Component component)
    {
        components.Add(component);
    }
    public override void Remove(Component component)
    {
        components.Remove(component);
    }

    public override void Print()
    {
        Console.WriteLine("Узел " + name);
        Console.WriteLine("Подузлы:");
        foreach (Component component in components)
            component.Print();
    }
}

class File : Component
{
    public File(string name) : base(name)
    { }
}

// В итоге подобная система обладает неплохой гибкостью: если мы захотим добавить новый вид компонентов, нам достаточно унаследовать новый класс от Component.

// И также применяя компоновщик, мы легко можем обойти все узлы древовидной структуры.