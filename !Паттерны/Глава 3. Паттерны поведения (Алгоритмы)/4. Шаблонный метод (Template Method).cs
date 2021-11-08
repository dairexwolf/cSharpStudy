// Шаблонный метод (Template Method) определяет общий алгоритм поведения подклассов, позволяя им переопределить отдельные шаги этого алгоритма без изменения его структуры.

/*
Когда использовать шаблонный метод?

- Когда планируется, что в будущем подклассы должны будут переопределять различные этапы алгоритма без изменения его структуры

- Когда в классах, реализующим схожий алгоритм, происходит дублирование кода. Вынесение общего кода в шаблонный метод уменьшит его дублирование в подклассах.
*/

// Формальная реализация паттерна на C#:

abstract class AbstractClass
{
	public void TemplateMethod()
	{
		Operation1();
		Operation2();
	}
	
	public abstract void Operation1();
	public abstract void Operation2();
}

class ConcreteClass : AbstractClass
{
	public override void Operation1()
	{
		// код
	}
	public override void Operation2()
	{
		
	}
}

/*
Участники

- AbstractClass: определяет шаблонный метод TemplateMethod(), который реализует алгоритм. Алгоритм может состоять из последовательности вызовов других методов, часть из которых может быть абстрактными и должны быть переопределены в классах-наследниках. При этом сам метод TemplateMethod(), представляющий структуру алгоритма, переопределяться не должен.

- ConcreteClass: подкласс, который может переопределять различные методы родительского класса.

*/

// Рассмотрим применение на конкретном примере. Допустим, в нашей программе используются объекты, представляющие учебу в школе и в вузе:

class School
{
    // идем в первый класс
    public void Enter() { }
    // обучение
    public void Study() { }
    // сдаем выпускные экзамены
    public void PassExams() { }
    // получение аттестата об окончании
    public void GetAttestat() { }
}
 
class University
{
    // поступление в университет
    public void Enter() { }
    // обучение
    public void Study() { }
    // проходим практику
    public void Practice() { }
    // сдаем выпускные экзамены
    public void PassExams() { }
    // получение диплома
     public void GetDiplom() { }
}

// Как видно, эти классы очень похожи, и самое главное, реализуют примерно общий алгоритм. Да, где-то будет отличаться реализация методов, где-то чуть больше методов, но в целом мы имеем общий алгоритм, а функциональность обоих классов по большому счету дублируется. Поэтому для улучшения структуры классов мы могли бы применить шаблонный метод:

class Program
{
	static void Main()
	{
		School sch = new School();
		University uni = new University();
		
		sch.Learn();
		uni.Learn();
	}
}

abstract class Education
{
	// TemplateMethod
	pubilc void Learn()
	{
		Enter();
		Study();
		PassExams();
		GetDocument();
	}
	
	public abstract void Enter();
	public abstract void Study();
	public virtual void PassExams()
	{
		Console.WriteLine("Сдаем выпускные экзамены");
	}
	public abstract void GetDocument();
}

class School : Education
{
	public override void Enter()
	{
		Console.WriteLine("Идем в первый класс");
	}
	public override void Study()
	{
		Console.WriteLine("Посещаем уроки, делаем домашние задания");
	}
	public override void GetDocument()
	{
		Console.WriteLine("Получаем аттестат");
	}
}

class University : Education
{
	public override void Enter()
	{
		Console.WriteLine("Сдаем выпускные экзамены");
	}
	
	public override void Study()
	{
		Console.WriteLine("Посещаем лекции, семенары, лабораторные и практики, сдаем работы");
	}
	
	public override PassExams()
	{
		Console.WriteLine("Сдаем экзамен по специальности и защищаем диплом");
	}
	public override void GetDocument()
	{
		Console.WriteLine("Получаем дип лом");
	}
}



