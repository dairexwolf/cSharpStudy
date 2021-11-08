// Паттерн "Абстрактная фабрика" (Abstract Factory) предоставляет интерфейс для создания семейств взаимосвязанных объектов с определенными интерфейсами без указания конкретных типов данных объектов.

/*
Когда использовать абстрактную фабрику

- Когда система не должна зависеть от способа создания и компоновки новых объектов

- Когда создаваемые объекты должны использоваться вместе и являются взаимосвязанными
*/

// Формальное определение паттерна на языке C# может выглядеть следующим образом:

abstract class AbstractFactory
{
    public abstract AbstractProductA CreateProductA();
    public abstract AbstractProductB CreateProductB();
}
class ConcreteFactory1: AbstractFactory
{
    public override AbstractProductA CreateProductA()
    {
        return new ProductA1();
    }
         
    public override AbstractProductB CreateProductB()   
    {
        return new ProductB1(); 
    }
}
class ConcreteFactory2: AbstractFactory
{
    public override AbstractProductA CreateProductA()
    {
        return new ProductA2();
    }
         
    public override AbstractProductB CreateProductB()
    {
        return new ProductB2();
    }
}
 
abstract class AbstractProductA
{}
             
abstract class AbstractProductB     
{}
                 
class ProductA1: AbstractProductA   
{}
     
class ProductB1: AbstractProductB   
{}
 
class ProductA2: AbstractProductA   
{}
                 
class ProductB2: AbstractProductB       
{}      
             
class Client
{
    private AbstractProductA abstractProductA;
    private AbstractProductB abstractProductB;
 
    public Client(AbstractFactory factory)
    {
        abstractProductB = factory.CreateProductB();
        abstractProductA = factory.CreateProductA();
    }
    public void Run()
    { }
}

/*
Паттерн определяет следующих участников:

- Абстрактные классы AbstractProductA и AbstractProductB определяют интерфейс для классов, объекты которых будут создаваться в программе.

Конкретные классы ProductA1 / ProductA2 и ProductB1 / ProductB2 представляют конкретную реализацию абстрактных классов

Абстрактный класс фабрики AbstractFactory определяет методы для создания объектов. Причем методы возвращают абстрактные продукты, а не их конкретные реализации.

Конкретные классы фабрик ConcreteFactory1 и ConcreteFactory2 реализуют абстрактные методы базового класса и непосредственно определяют какие конкретные продукты использовать

Класс клиента Client использует класс фабрики для создания объектов. При этом он использует исключительно абстрактный класс фабрики AbstractFactory и абстрактные классы продуктов AbstractProductA и AbstractProductB и никак не зависит от их конкретных реализаций
*/

// Посмотрим, как мы можем применить паттерн. Например, мы делаем игру, где пользователь должен управлять некими супергероями, при этом каждый супергерой имеет определенное оружие и определенную модель передвижения. Различные супергерои могут определяться комплексом признаков. Например, эльф может летать и должен стрелять из арбалета, другой супергерой должен бегать и управлять мечом. Таким образом, получается, что сущность оружия и модель передвижения являются взаимосвязанными и используются в комплексе. То есть имеется один из доводов в пользу использования абстрактной фабрики.

// И кроме того, наша задача при проектировании игры абстрагировать создание супергероев от самого класса супергероя, чтобы создать более гибкую архитектуру. И для этого применим абстрактную фабрику:

using System;

class Program
{
	static void Main()
	{
		Hero elf = new Hero(new ElfFactory());
		elf.Hit();
		elf.Run();
		
		Hero human = new Hero(new HumanFactory());
		human.Hit();
		human.Run();
	}	
}
// абстрактный класс оружие
abstract class Weapon
{
	public abstract void Hit();
}
// абстрактный класс движение
abstract class Movement
{
	public abstract void Move();
}

// класс арбалет
class Arbalet : Weapon
{
	public override void Hit()
	{
		Console.WriteLine("Выстрел");
	}
}
// класс меч
class Sword : Weapon
{
	public override void Hit()
	{
		Console.WriteLine("Удар");
	}
}

// движение полета
class FlyMovement : Movement
{
	public override void Move()
	{
		Console.WriteLine("Летим");
	}
}
// движение пешком
class RunMovement : Movement
{
	public override void Move()
	{
		Console.WriteLine("Бежим");
	}
}
// класс абстрактной фабрики
abstract class HeroFactory
{
	public abstract Movement CreateMovement();
	public abstract Weapon CraeteWeapon();
}
// фабрика создания летящего героя с арбалетом
class ElfFactory : HeroFactory
{
	public override Movement CreateMovment()
	{
		return new FlyMovement();
	}
	public override Weapon CreateWeapon()
	{
		return new Arbalet();
	}
}
// фабрика бегущего героя с мечом
class HumanFactory : HeroFactory
{
	public override Movement CreateMovement()
	{
		return new RunMovement();
	}
	public override Weapon CreateWeapon()
	{
		return new Sword();
	}
}
// клиент - сам супергерой
class Hero
{
	private Weapon weapon;
	private Movement movement;
	public Hero(HeroFactory factory)
	{
		weapon = factory.CreateWeapon();
		movement = factory.CreateMovement();
	}
	public void Run()
	{
		movement.Move();
	}
	public void Hit()
	{
		weapon.Hit();
	}
}

// Таким образом, создание супергероя абстрагируется от самого класса супергероя. В то же время нельзя не отметить и недостатки шаблона. В частности, если нам захочется добавить в конфигурацию супергероя новый объект, например, тип одежды, то придется переделывать классы фабрик и класс супергероя. Поэтому возможности по расширению в данном паттерне имеют некоторые ограничения.

