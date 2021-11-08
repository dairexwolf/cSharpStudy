// Мост (Bridge) - структурный шаблон проектирования, который позволяет отделить абстракцию от реализации таким образом, чтобы и абстракцию, и реализацию можно было изменять независимо друг от друга.

// Даже если мы отделим абстракцию от конкретных реализаций, то у нас все равно все наследуемые классы будут жестко привязаны к интерфейсу, определяемому в базовом абстрактном классе. Для преодоления жестких связей и служит паттерн Мост.

/*
Тогда использовать данный паттерн?

- Когда надо избежать постоянной привязки абстракции к реализации

- Когда наряду с реализацией надо изменять и абстракцию независимо друг от друга. То есть изменения в абстракции не должно привести к изменениям в реализации
*/

// Общая реализация паттерна состоит в объявлении классов абстракций и классов реализаций в отдельных параллельных иерархиях классов.

// Связь агрегации между классами Abstraction и Implementor фактически и представляет некоторый мост между двумя параллельными иерархиями классов. Собственно поэтому паттерн получил название Мост.

// Формальное описание паттерна на языке C#:

class Client
{
	static void Main()
	{
		Abstraction abstraction;
		abstraction = new RefinedAbstraction(new ConcreteImplementorA());
		abstraction.Operation();
		abstraction.Implementor = new ConcreteImplementorB();
		abstraction.Operation();
	}
}


abstract class Abstraction
{
	protected Implementor implemetor;
	public Implementor Implementor
	{
		set { implemetor = value; }
	}
	public Abstraction (Implementor implemetor)
	{
		this.implemetor = implemetor;
	}
	public virtual void Operation()
	{
		implemetor.OperationImp();
	}
}

abstract class Implementor
{
	public abstract void OperationImp(); 
}

class RefinedAbstraction : Abstraction
{
	public RefinedAbstraction(Implementor implemetor)
		: base (implemetor)
	{ }
	
	public override void Operation()
	{
	}
}

class ConcreteImplementorA : Implementor
{
	public override void OperationImp()
	{
	}
}

class ConcreteImplementorB : Implementor
{
	public override void OperationImp()
	{ }
}

/*
Участники

- Abstraction: определяет базовый интерфейс и хранит ссылку на объект Implementor. Выполнение операций в Abstraction делегируется методам объекта Implementor

- RefinedAbstraction: уточненная абстракция, наследуется от Abstraction и расширяет унаследованный интерфейс

- Implementor: определяет базовый интерфейс для конкретных реализаций. Как правило, Implementor определяет только примитивные операции. Более сложные операции, которые базируются на примитивных, определяются в Abstraction

- ConcreteImplementorA и ConcreteImplementorB: конкретные реализации, которые унаследованы от Implementor

- Client: использует объекты Abstraction
*/

// Теперь рассмотрим реальное применение. Существует множество программистов, но одни являются фрилансерами, кто-то работает в компании инженером, кто-то совмещает работу в компании и фриланс. Таким образом, вырисовывается иерархия различных классов программистов. Но эти программисты могут работать с различными языками и технологиями. И в зависимости от выбранного языка деятельность программиста будет отличаться. Для решения описания данной задачи в программе на C# используем паттерн Мост:

using System;

class Program
{
	static void Main()
	{
		// создаем программиста C++
		Programmer freelancer = new FreelanceProgrammer(new CPPLanguage());
		freelancer.DoWork();
		freelancer.EarnMoney();
		// пришел новый заказ, но теперь нужен на C#
		freelancer.Language = new CSharpLanguage();
		freelancer.DoWork();
		freelancer.EarnMoney();
		
		Console.Read();
	}
}

interface ILanguage
{
	void Build();
	void Execute();
}

class CPPLanguage : ILanguage
{
	public void Build()
	{
		Console.WriteLine("С помощью компилятора С++ компилируем программу в бинарный код");
	}
	
	public void Execute()
	{
		Console.WriteLine("Запускаем исполняемый файл программы, написанную на С++");
	}
}

class CSharpLanguage : ILanguage
{
	public void Build()
	{
		Console.WriteLine("С помощью компилятора Roslyn компилируем исходный код в файл exe");
	}
	public void Execute()
	{
		Console.WriteLine("JIT компилирует программу в бинарный код");
		Console.WriteLine("CLR выполняет скомпилированный бинарный код");
	}
}

abstract class Programmer
{
	protected ILanguage language;
	public ILanguage Language
	{
		set { language = vlaue; }
	}
	
	public Programmer (ILanguage language)
	{
		this.language = language;
	}
	
	public virtual void DoWork()
	{
		language.Build();
		language.Execute();
	}
	public abstract void EarnMoney();	
}

class FreelanceProgrammer : Programmer
{
	public FreelanceProgrammer(ILanguage language) : base (language)
	{
	}
	
	public override void EarnMoney()
	{
		Console.WriteLine("Получаем оплату за выполненный заказ");
	}
}

class CorporateProgrammer : Programmer
{
	public CorporateProgrammer(ILanguage language) : base (language);
	{ }
	
	public override void EarnMoney()
	{
		Console.WriteLine("Получаем зарплату за месяц");
	}
}

// В роли Abstraction выступает класс Programmer, а в роли Implementor - интерфейс ILanguage, который представляет язык программирования. В методе DoWork() класса Programmer вызываются методы объекта ILanguage.

// Языки CPPLanguage и CSharpLanguage определяют конкретные реализации, а классы FreelanceProgrammer и CorporateProgrammer представляют уточненные абстракции.

// Таким образом, благодаря применению паттерна реализация отделяется от абстракции. Мы можем развивать независимо две параллельные иерархии(!). Устраняются зависимости между реализацией и абстракцией во время компиляции, и мы можем менять конкретную реализацию во время выполнения.