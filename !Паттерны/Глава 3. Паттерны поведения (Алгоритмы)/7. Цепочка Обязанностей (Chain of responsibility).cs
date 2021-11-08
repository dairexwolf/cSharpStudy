// Цепочка Обязанностей (Chain of responsibility) - поведенческий шаблон проектирования, который позволяет избежать жесткой привязки отправителя запроса к получателю. Все возможные обработчики запроса образуют цепочку, а сам запрос перемещается по этой цепочке. Каждый объект в этой цепочке при получении запроса выбирает, либо закончить обработку запроса, либо передать запрос на обработку следующему по цепочке объекту.

/*
Когда применяется цепочка обязанностей?

- Когда имеется более одного объекта, который может обработать определенный запрос

- Когда надо передать запрос на выполнение одному из нескольких объект, точно не определяя, какому именно объекту

- Когда набор объектов задается динамически
*/

// Формальное определение на языке C#

class Client 
{
	void Main()
	{
		Handler h1 = new ConcreteHandler1();
		Handler h2 = new ConcreteHandler2();
		h1.Successor = h2;
		h1.HandleRquest(2);
	}
}
abstract class Handler 
{
	public Handler Successor{ get; set; }
	public abstract void HandleRequest(int condition);
}

class ConcreteHandler1 : Handler
{
	public override void HandlerRequest(int condition)
	{
		// обработка запроса
		
		if (condition == 1)
		{
			// завершение выполнения запроса
		}
		// передача запроса дальше по цепи при наличии в ней обработчиков
		else if (Successor != null)
		{
			Successor.HandleRequest(condition);
		}
	}
}

class ConcreteHandler1 : Handler
{
	public override void HandlerRequest(int condition)
	{
		// обработка запроса
		
		if (condition == 2)
		{
			// завершение выполнения запроса
		}
		// передача запроса дальше по цепи при наличии в ней обработчиков
		else if (Successor != null)
		{
			Successor.HandleRequest(condition);
		}
	}
}

/*
Участники

- Handler: определяет интерфейс для обработки запроса. Также может определять ссылку на следующий обработчик запроса

- ConcreteHandler1 и ConcreteHandler2: конкретные обработчики, которые реализуют функционал для обработки запроса. При невозможности обработки и наличия ссылки на следующий обработчик, передают запрос этому обработчику
В данном случае для простоты примера в качестве параметра передается некоторое число, сначала обработчик обрабатывает запрос и, если передано соответствующее число, завершает его обработку. Иначе передает запрос на обработку следующем в цепи обработчику при его наличии.

- Client: отправляет запрос объекту Handler
*/

/*
Использование цепочки обязанностей дает нам следующие преимущества:

- Ослабление связанности между объектами. Отправителю и получателю запроса ничего не известно друг о друге. Клиенту неизветна цепочка объектов, какие именно объекты составляют ее, как запрос в ней передается.

- В цепочку с легкостью можно добавлять новые типы объектов, которые реализуют общий интерфейс.
*/

// В то же время у паттерна есть недостаток: никто не гарантирует, что запрос в конечном счете будет обработан. Если необходимого обработчика в цепочки не оказалось, то запрос просто выходит из цепочки и остается необработанным.

// Использование паттерна довольно часто встречается в нашей жизни. Достаточно распространена ситуация, когда чиновники перекладывают друг на друга по цепочке выполнения какого-нибудь дела, и оно в конце концов оказывается не выполненным. Или когда мы идем в поликлинику, но при этом точно не знаем характер заболевания. В этом случае мы идем к терапевту, а он в зависимости от заболевания уже может либо сам лечить, либо отправить на лечение другим специализированным врачам.

// Рассмотрим конкретный пример. Допустим, необходимо послать человеку определенную сумму денег. Однако мы точно не знаем, какой способ отправки может использоваться: банковский перевод, системы перевода типа WesternUnion и Unistream или система онлайн-платежей PayPal. Нам просто надо внести сумму, выбрать человека и нажать на кнопку. Подобная система может использоваться на сайтах фриланса, где все отношения между исполнителями и заказчиками происходят опосредованно через функции системы и где не надо знать точные данные получателя.

using System;

class Program
{
    static void Main()
    {
        // устанавливаем, что через банк переводов мы не делаем
        Receiver receiver = new Receiver(false, true, true);

        // инициализация всех переводов
        PaymentHandler bankPaymentHandler = new BankPaymentHandler();
        PaymentHandler moneyPaymentHandler = new MoneyPaymentHandler();
        PaymentHandler paypalPaymentHandler = new PayPalPaymentHandler();
        // задаем цепочку. bank->paypal->money
        bankPaymentHandler.Successor = paypalPaymentHandler;
        paypalPaymentHandler.Successor = moneyPaymentHandler;

        // банк не пройдет,, перейдет в paypal
        bankPaymentHandler.Handle(receiver);
        // пройдет
        paypalPaymentHandler.Handle(receiver);
        // пройдет
        moneyPaymentHandler.Handle(receiver);

        Console.Read();

    }
}

class Receiver
{
    // банковские переводы
    public bool BankTransfer { get; set; }
    // денежные переводы - WesternUnion, Unistream
    public bool MoneyTransfer { get; set; }
    // перевод через PayPal
    public bool PayPalTransfer { get; set; }
    public Receiver(bool bt, bool mt, bool ppt)
    {
        BankTransfer = bt;
        MoneyTransfer = mt;
        PayPalTransfer = ppt;
    }
}

abstract class PaymentHandler
{
    public PaymentHandler Successor { get; set; }
    public abstract void Handle(Receiver receiver);
}

class BankPaymentHandler : PaymentHandler
{
    public override void Handle(Receiver receiver)
    {
        if (receiver.BankTransfer == true)
            Console.WriteLine("Выполняем банковский перевод");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}

class PayPalPaymentHandler : PaymentHandler
{
    public override void Handle(Receiver receiver)
    {
        if (receiver.PayPalTransfer == true)
            Console.WriteLine("Выполняем перевод через PayPal");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}

class MoneyPaymentHandler : PaymentHandler
{
    public override void Handle(Receiver receiver)
    {
        if (receiver.MoneyTransfer == true)
            Console.WriteLine("Выполняем перевод через систему денежных переводов");
        else if (Successor != null)
            Successor.Handle(receiver);
    }
}

// Класс Receiver с помощью конструктора и передаваемых в него параметров устанавливает возможные используемые системы платежей. При осуществлении платежа каждый отдельный объект PaymentHandler будет проверять установку у получателя определенного типа платежей. И если произойдет сопоставление типа платежей у получателя объекту PaymentHandler, то данный объект выполняет платеж. Если же необходимого способа платежей не будет определено, то деньги остаются в системе.

// При этом преимуществом цепочки является и то, что она позволяет расположить последовательность объектов-обработчиков в ней в зависимости от их приоритета.

