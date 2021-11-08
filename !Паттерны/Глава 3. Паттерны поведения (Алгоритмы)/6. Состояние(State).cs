// Состояние (State) - шаблон проектирования, который позволяет объекту изменять свое поведение в зависимости от внутреннего состояния.

/*
Когда применяется данный паттерн?

- Когда поведение объекта должно зависеть от его состояния и может изменяться динамически во время выполнения

- Когда в коде методов объекта используются многочисленные условные конструкции, выбор которых зависит от текущего состояния объекта
*/

// Формальное определение паттерна на C#:

class Program
{
	static void Main()
	{
		Context context = new Context(new StateA());
		context.Request();	// Переход в состояние StateB
		context.Request(); 	// Переход в состояние StateA
	}
}

abstract class State
{
	public abstract void Handle(Context context);
}
class StateA : State
{
	public override void Handle(Context context)
	{
		context.State = new StateB();
	}
}

class StateB : State
{
	public override void Handle(Context context)
	{
		context.State = new StateA();
	}
}

class Context
{
	public State State { get; set;}
	public Context(State state)
	{
		this.State = state;
	}
	public void Request()
	{
		this.State.Handle(this);
	}
}

/*
Участники паттерна

- State: определяет интерфейс состояния

- Классы StateA и StateB - конкретные реализации состояний

- Context: представляет объект, поведение которого должно динамически изменяться в соответствии с состоянием. Выполнение же конкретных действий делегируется объекту состояния
*/

// Например, вода может находиться в ряде состояний: твердое, жидкое, парообразное. Допустим, нам надо определить класс Вода, у которого бы имелись методы для нагревания и заморозки воды. Без использования паттерна Состояние мы могли бы написать следующую программу:

using System;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        Water water = new Water(WaterState.LIQUID);
        water.Heat();
        water.Frost();
        water.Frost();

        Console.Read();
    }
}

enum WaterState
{
    SOLID,
    LIQUID,
    GAS
}
class Water
{
    public WaterState State { get; set; }
    public Water(WaterState ws)
    {
        State = ws;
    }
    public void Heat()
    {
        if (State == WaterState.SOLID)
        {
            Console.WriteLine("Лед превращается в жидкость");
            State = WaterState.LIQUID;
        }
        else if (State == WaterState.LIQUID)
        {
            Console.WriteLine("Вода превращается в пар");
            State = WaterState.GAS;
        }
        else if (State == WaterState.GAS)
            Console.WriteLine("Повышаем температуру водяного пара");
    }
    public void Frost()
    {
        if (State == WaterState.LIQUID)
        {
            Console.WriteLine("Жидкость превращается в лед");
            State = WaterState.SOLID;
        }
        else if (State == WaterState.GAS)
        {
            Console.WriteLine("Водяной пар конденсирует в жидкость");
            State = WaterState.LIQUID;
        }
    }
}

// Вода имеет три состояния, и в каждом методе нам надо смотреть на текущее состояние, чтобы произвести действия. В итоге с трех состояний уже получается нагромождение условных конструкций. Да и самим методов в классе Вода может также быть множество, где также надо будет действовать в зависимости от состояния. Поэтому, чтобы сделать программу более гибкой, в данном случае мы можем применить паттерн Состояние:

using System;

class Program
{
	static void Main()
	{
		Water water = new Water(new LiquidWaterState());
		water.Heat();
		water.Frost();
		water.Frost();
		
		Console.Read();
	}
}

class Water
{
	public IWaterState State { get; set; }
	
	public Water (IWaterState ws)
	{
		State = ws;
	}
	
	public void Heat()
	{
		State.Heat(this);
	}
	public void Frost()
	{
		State.Frost(this);
	}
}

interface IWaterState
{
	void Heat(Water water);
	void Frost(Water water);
}

class SolidWaterState : IWaterState
{
	public void Heat(Water water)
	{
		Console.WriteLine("Лед превращается в жидкость");
		water.State = new LiquidWaterState();
	}
	public void Frost(Water water)
	{
		Console.writeLine("Продолжаем заморозку льда");
	}
}

class LiquidWaterState : IWaterState
{
	public void Heat(Water water)
	{
		Console.WriteLine("Жидкость превращается в пар");
		water.State = new GasWaterState();
	}
	public void Frost(Water water)
	{
		Console.WriteLine("Жидкость превращается в лед");
		water.State = new SolidWaterState();
	}
}

class GasWaterState : IWaterState
{
	public void Frost(Water water)
	{
		Console.WriteLine("Водяной пар превращается в жидкую воду");
		water.State = new LiquidWaterState();
	}
	public void Heat(Water water)
	{
		Console.WriteLine("Пар нагревается");
	}
}

// Таким образом, реализация паттерна Состояние позволяет вынести поведение, зависящее от текущего состояния объекта, в отдельные классы, и избежать перегруженности методов объекта условными конструкциями, как if..else или switch. Кроме того, при необходимости мы можем ввести в систему новые классы состояний, а имеющиеся классы состояний использовать в других объектах.