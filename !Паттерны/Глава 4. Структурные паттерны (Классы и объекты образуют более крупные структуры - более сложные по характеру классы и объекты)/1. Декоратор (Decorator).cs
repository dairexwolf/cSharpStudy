// Декоратор (Decorator) представляет структурный шаблон проектирования, который позволяет динамически подключать к объекту дополнительную функциональность.

// Для определения нового функционала в классах нередко используется наследование. Декораторы же предоставляет наследованию более гибкую альтернативу, поскольку позволяют динамически в процессе выполнения определять новые возможности у объектов.

/*
Когда следует использовать декораторы?

- Когда надо динамически добавлять к объекту новые функциональные возможности. При этом данные возможности могут быть сняты с объекта

- Когда применение наследования неприемлемо. Например, если нам надо определить множество различных функциональностей и для каждой функциональности наследовать отдельный класс, то структура классов может очень сильно разрастись. Еще больше она может разрастись, если нам необходимо создать классы, реализующие все возможные сочетания добавляемых функциональностей.
*/

// Формальная организация паттерна в C# могла бы выглядеть следующим образом:

abstract class Component
{
	public abstract void Operation();
}

class ConcreteComponent
{
	public override Operation()
	{ }
}

abstract class Decorator : Component
{
	protected Component comp;
	
	public void SetComponent(Component component)
	{
		this.comp = component;
	}
	
	public override void Operation()
	{
		if (comp != null)
			comp.Operation();		
	}
}

class ConcreteDecoratorA : Decorator
{
	public override void Operation()
	{
		base.Operation();
	}
}

class ConcreteDecoratorB : Decorator
{
	public override void Operation()
	{
		base.Operation();
	}
}

/*
Участники

- Component: абстрактный класс, который определяет интерфейс для наследуемых объектов

- ConcreteComponent: конкретная реализация компонента, в которую с помощью декоратора добавляется новая функциональность

- Decorator: собственно декоратор, реализуется в виде абстрактного класса и имеет тот же базовый класс, что и декорируемые объекты. Поэтому базовый класс Component должен быть по возможности легким и определять только базовый интерфейс.
Класс декоратора также хранит ссылку на декорируемый объект в виде объекта базового класса Component и реализует связь с базовым классом как через наследование, так и через отношение агрегации.

- Классы ConcreteDecoratorA и ConcreteDecoratorB представляют дополнительные функциональности, которыми должен быть расширен объект ConcreteComponent.
*/

// Рассмотрим пример. Допустим, у нас есть пиццерия, которая готовит различные типы пицц с различными добавками. Есть итальянская, болгарская пиццы. К ним могут добавляться помидоры, сыр и т.д. И в зависимости от типа пицц и комбинаций добавок пицца может иметь разную стоимость. Теперь посмотрим, как это изобразить в программе на C#:

using System;

class Program
{
    static void Main()
    {
        Pizza pizza1 = new ItalianPizza();
        pizza1 = new TomatoPizza(pizza1);   // итальянская пицца с томатами
        Console.WriteLine("Название: " + pizza1.Name);
        Console.WriteLine("Цена: " + pizza1.GetCost());

        Pizza pizza2 = new ItalianPizza();
        pizza2 = new CheesePizza(pizza2);   // итальянская пицца с сыром
        Console.WriteLine("Название: " + pizza2.Name);
        Console.WriteLine("Цена: " + pizza2.GetCost());

        Pizza pizza3 = new BulgerianPizza();
        pizza3 = new TomatoPizza(pizza3);
        pizza3 = new CheesePizza(pizza3);   // болгарская пицца с томатами и сыром
        Console.WriteLine("Название: " + pizza3.Name);
        Console.WriteLine("Цена: " + pizza3.GetCost());

        Console.Read();
    }
}

abstract class Pizza
{
    public Pizza(string n)
    {
        this.Name = n;
    }

    public string Name { get; protected set; }
    public abstract int GetCost();
}

class ItalianPizza : Pizza
{
    public ItalianPizza() : base("Итальянская пицца")
    { }
    public override int GetCost()
    {
        return 550;
    }
}

class BulgerianPizza : Pizza
{
    public BulgerianPizza() : base("Болгарская пицца")
	{ }
	
	public override int GetCost()
    {
        return 635;
    }
}

abstract class PizzaDecorator : Pizza
{
    protected Pizza pizza;
    public PizzaDecorator(string n, Pizza pizza) : base(n)
    {
        this.pizza = pizza;
    }
}

class TomatoPizza : PizzaDecorator
{
    public TomatoPizza(Pizza pizza) : base(pizza.Name + ", с томатами", pizza)
	{ }
	
	// рекурсия получается. Этот объект хранит объект старой пиццы и вызывает ее метод GetCost();
	public override int GetCost()
    {
        return pizza.GetCost() + 30;
    }
}

class CheesePizza : PizzaDecorator
{
    public CheesePizza(Pizza pizza) : base(pizza.Name + ", с сыром", pizza)
	{ }
	public override int GetCost()
    {
        return pizza.GetCost() + 50;
    }
}

// В качестве компонента здесь выступает абстрактный класс Pizza, который определяет базовую функциональность в виде свойства Name и метода GetCost(). Эта функциональность реализуется двумя подклассами ItalianPizza и BulgerianPizza, в которых жестко закодированы название пиццы и ее цена.

// Декоратором является абстрактный класс PizzaDecorator, который унаследован от класса Pizza и содержит ссылку на декорируемый объект Pizza. В отличие от формальной схемы здесь установка декорируемого объекта происходит не в методе SetComponent, а в конструкторе.

// Отдельные функциональности - добавление томатов и сыры к пиццам реализованы через классы TomatoPizza и CheesePizza, которые обертывают объект Pizza и добавляют к его имени название добавки, а к цене - стоимость добавки, то есть переопределяя метод GetCost и изменяя значение свойства Name.

// Благодаря этому при создании пиццы с добавками произойдет ее обертывание декоратором:
Pizza pizza3 = new BulgerianPizza();
 pizza3 = new TomatoPizza(pizza3);
 pizza3 = new CheesePizza(pizza3);

// Сначала объект BulgerianPizza обертывается декоратором TomatoPizza, а затем CheesePizza. И таких обертываний мы можем сделать множество. Просто достаточно унаследовать от декоратора класс, который будет определять дополнительный функционал.

// А если бы мы использовали наследование, то в данном случае только для двух видов пицц с двумя добавками нам бы пришлось создать восемь различных классов, которые бы описывали все возможные комбинации. Поэтому декораторы являются более предпочтительным в данном случае методом.

