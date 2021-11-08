// Паттерн Посредник (Mediator) представляет такой шаблон проектирования, который обеспечивает взаимодействие множества объектов без необходимости ссылаться друг на друга. Тем самым достигается слабосвязанность взаимодействующих объектов.
/*
Когда используется паттерн Посредник?

- Когда имеется множество взаимосвязаных объектов, связи между которыми сложны и запутаны.

- Когда необходимо повторно использовать объект, однако повторное использование затруднено в силу сильных связей с другими объектами.
*/

// Формальная структура классов и связей между ними с применением паттерна на языке C#:

abstract class Mediator
{
	public abstract void Send(string msg, Colleague colleague);
}

abstract class Colleague
{
	protected Mediator mediator;
	
	public Colleague(Mediator mediator)
	{
		this.Mediator = mediator;
	}
}

class CocreteColleague1 : Colleague
{
	public ConcreteColleague1(Mediator mediator)
		: base (mediator)
		{ }
	
	public void Send(string message)
	{
		mediator.Send(message, this);
	}
	
	public void Notify(string message)
	{ }
}

class ConcreteColleague2 : Colleague
{
	public ConcreteColleague2(Mediator mediator)
		: base (mediator)
		{ }
	
	public void Send(string message)
	{
		mediator.Send(message, this);
	}
	
	public void Notify(string message)
	{ }
}

class ConcreteMediator : Mediator
{
	public ConcreteColleague1 Colleague1 { get; set; }
	public ConcreteColleague2 Colleague2 { get; set; }
	public override void Send(string msg, Colleague colleague)
	{
		if (Colleague1 == colleague)
			Colleague1.Notify(msg);
		else
			Colleague2.Notify(msg);
	}
}

/*
Участники

- Mediator: представляет интерфейс для взаимодействия с объектами Colleague

- Colleague: представляет интерфейс для взаимодействия с объектом Mediator

- ConcreteColleague1 и ConcreteColleague2: конкретные классы коллег, которые обмениваются друг с другом через объект Mediator

- ConcreteMediator: конкретный посредник, реализующий интерфейс типа Mediator
*/

// Рассмотрим реальный пример. Система создания программных продуктов включает ряд акторов: заказчики, программисты, тестировщики и так далее. Но нередко все эти акторы взаимодействуют между собой не непосредственно, а опосредованно через менеджера проектов. То есть менеджер проектов выполняет роль посредника. В этом случае процесс взаимодействия между объектами мы могли бы описать так:

using System;

class Program
{
    static void Main()
    {
        ManagerMediator mediator = new ManagerMediator();
        Colleague customer = new CustomerColleague(mediator);
        Colleague programmer = new ProgrammerColleague(mediator);
        Colleague tester = new TesterColleague(mediator);
        mediator.Customer = customer;
        mediator.Programmer = programmer;
        mediator.Tester = tester;
        customer.Send("Есть заказ, надо сделать прогу");
        programmer.Send("Программа готова, пора тестить");
        tester.Send("Все норм");
        Console.Read();
    }
}

abstract class Mediator
{
    public abstract void Send(string msg, Colleague colleague);
}

abstract class Colleague
{
    protected Mediator mediator;

    public Colleague(Mediator mediator)
    {
        this.mediator = mediator;
    }

    public virtual void Send(string message)
    {
        mediator.Send(message, this);
    }
    public abstract void Notify(string msg);
}
// класс заказчика
class CustomerColleague : Colleague
{
    public CustomerColleague(Mediator mediator)
        : base(mediator)
    { }

    public override void Notify(string msg)
    {
        Console.WriteLine("Сообщение заказчику: " + msg);
    }
}
// класс программиста
class ProgrammerColleague : Colleague
{
    public ProgrammerColleague(Mediator mediator)
        : base(mediator)
    { }

    public override void Notify(string msg)
    {
        Console.WriteLine("Сообщение программисту: " + msg);
    }
}
// класс тестера
class TesterColleague : Colleague
{
    public TesterColleague(Mediator mediator)
        : base(mediator)
    { }

    public override void Notify(string msg)
    {
        Console.WriteLine("Сообщение тестеру: " + msg);
    }
}

class ManagerMediator : Mediator
{
    public Colleague Customer { get; set; }
    public Colleague Programmer { get; set; }
    public Colleague Tester { get; set; }

    public override void Send(string msg, Colleague colleague)
    {
        // если отправитель заказчик, значит есть новый заказ
        // отправляем сообщение программисту
        if (Customer == colleague)
            Programmer.Notify(msg);
        // если отправитель программист, то можно приступать к тестированию
        // отправляем сообщение тестеру
        else if (Programmer == colleague)
            Tester.Notify(msg);
        // если отправитель тестер, значит продукт готов
        // отправляем сообщение заказчику
        else if (Tester == colleague)
            Customer.Notify(msg);
    }
}

// Класс менеджера - ManagerMediator в методе Send() проверяет, от кого пришло сообщение, и в зависимости от отправителя перенаправляет его другому объекту с помощью методов Notify(), определенных в классе Colleague.

/*
В итоге применение паттерна Посредник дает нам следующие преимущества:

- Устраняется сильная связанность между объектами Colleague

- Упрощается взаимодействие между объектами: вместо связей по типу "все-ко-всем" применяется связь "один-ко-всем"

- Взаимодействие между объектами абстрагируется и выносится в отдельный интерфейс

- Централизуется управления отношениями между объектами(!)
*/

