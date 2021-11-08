// Паттерн "Команда" (Command) позволяет инкапсулировать запрос на выполнение определенного действия в виде отдельного объекта. Этот объект запроса на действие и называется командой. При этом объекты, инициирующие запросы на выполнение действия, отделяются от объектов, которые выполняют это действие.

// Команды могут использовать параметры, которые передают ассоциированную с командой информацию. Кроме того, команды могут ставиться в очередь и также могут быть отменены.

/*
Когда использовать команды?

- Когда надо передавать в качестве параметров определенные действия, вызываемые в ответ на другие действия. То есть когда необходимы функции обратного действия в ответ на определенные действия.

- Когда необходимо обеспечить выполнение очереди запросов, а также их возможную отмену.

- Когда надо поддерживать логгирование изменений в результате запросов. Использование логов может помочь восстановить состояние системы - для этого необходимо будет использовать последовательность запротоколированных команд.
*/

// Формальное определение на языке C# может выглядеть следующим образом:

abstract class Command
{
	public abstract void Execut();
	public abstract void Undo();
}

// конкретная команда
class ConcreteCommand : Command
{
	Receiver receiver;
	public ConcreteCommand(Reciver r)
	{
		receiver = r;
	}
	
	public override void Execute()
	{
		receiver.Operation();
	}
	
	public override void Undo()
	{}
}
// получатель команды
class Receiver
{
	public void Operation()
	{}
}
// инициатор команды
class Invoker
{
	Command command;
	public void SetCommand(Commanad c)
	{
		command = c;
	}
	public void Run()
	{
		command.Execute();
	}
	public void Cancel()
	{
		command.Undo();
	}
}
// клиент - создает команду и устанавливает ее получателя
class Client
{
	void Main()
	{
		Invoker invoker = new Invoker();
		Receiver receiver = new Receiver();
		ConcreteCommand command = new ConcreteCommand(receiver);
		invoker.SetCommand(command);
		invoker.Run();
	}
}

/*
Участники

- Command: интерфейс, представляющий команду. Обычно определяет метод Execute() для выполнения действия, а также нередко включает метод Undo(), реализация которого должна заключаться в отмене действия команды

- ConcreteCommand: конкретная реализация команды, реализует метод Execute(), в котором вызывается определенный метод, определенный в классе Receiver

- Receiver: получатель команды. Определяет действия, которые должны выполняться в результате запроса.

- Invoker: инициатор команды - вызывает команду для выполнения определенного запроса

- Client: клиент - создает команду и устанавливает ее получателя с помощью метода SetCommand()
*/

// Таким образом, инициатор, отправляющий запрос, ничего не знает о получателе, который и будет выполнять команду. Кроме того, если нам потребуется применить какие-то новые команды, мы можем просто унаследовать классы от абстрактного класса Command и реализовать его методы Execute и Undo.

// В программах на C# команды находят довольно широкое применение. Так, в технологии WPF и других технологиях, которые используют XAML и подход MVVM, на командах во многом базируется взаимодействие с пользователем. В некоторых архитектурах, например, в архитектуре CQRS, команды являются одним из ключевых компонентов.

// Нередко в роли инициатора команд выступают панели управления или кнопки интерфейса. Самая простая ситуация - надо программно организовать включение и выключение прибора, например, телевизора. Решение данной задачи с помощью команд могло бы выглядеть так:

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Pult pult = new Pult();
        TV tv = new TV();
        pult.SetCommand(new TVOnCommand(tv));
        pult.PressButton();
        pult.PressUndo();

        Console.Read();
    }
}

// абстрактный класс команды
abstract class Command
{
    public abstract void Execute();
    public abstract void Undo();
}

// Reciever - получатель
class TV
{
    public void On()
    {
        Console.WriteLine("Включил телек!");
    }
    public void Off()
    {
        Console.WriteLine("Выключил телек...");
    }

}

class TVOnCommand : Command
{
    TV tv;
    public TVOnCommand(TV tvSet)
    {
        tv = tvSet;
    }
    public override void Execute()
    {
        tv.On();
    }
    public override void Undo()
    {
        tv.Off();
    }
}

// Invoker - инициатор
class Pult
{
    Command command;

    public Pult() { }

    public void SetCommand(Command com)
    {
        command = com;
    }

    public void PressButton()
    {
        if (command != null)
            command.Execute();
        else
            Console.WriteLine("Нет команды");
    }
    public void PressUndo()
    {
		if (command != null)
			command.Undo();
		else
            Console.WriteLine("Нет команды");
    }
}

// Итак, в этой программе есть интерфейс команды - ICommand, есть ее реализация в виде класса TVOnCommand, есть инициатор команды - класс Pult, некий прибор - пульт, управляющий телевизором. И есть получатель команды - класс TV, представляющий телевизор. В качестве клиента используется класс Program.

// При этом пульт ничего не знает об объекте TV. Он только знает, как отправить команду. В итоге мы получаем гибкую систему, в которой мы легко можем заменять одни команды на другие, создавать последовательности команд. Например, в нашей программе кроме телевизора появилась микроволновка, которой тоже неплохо было бы управлять с помощью одного интерфейса. Для этого достаточно добавить соответствующие классы и установить команду:

using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Pult pult = new Pult();
        TV tv = new TV();
		pult.PressButton();
        pult.SetCommand(new TVOnCommand(tv));
        pult.PressButton();
        pult.PressUndo();

		Microwave mic1 = new Microwave();
		// 5000 - время нагрева пищи
		pult.SetCommand(new MicrowaveCommand(microwave, 5000));
		pult.PressButton();
        Console.Read();
    }
}

// ... ранее описаные классы

// Чтобы бороться с тем, что мы запускаем команду без ее назначения, можно не только создать проверку на пустоту команды, но и можно определить класс пустой команды, которая будет устанавливаться по умолчанию

class NoCommand : Command
{
	public override void Execute()
	{
		Console.WriteLine("Пустая команда. Ничего не произошло");
	}
	public override void Undo()
	{
		Console.WriteLine("Отмена пустой команды. Все еще ничего не произошло");
	}
}

/*
class Pult
{
	Command command;

    public Pult() 
	{
		command = new NoCommand();			// по умолчанию
	}

    public void SetCommand(Command com)
    {
        command = com;
    }

    public void PressButton()
    {
        if (command != null)
            command.Execute();
        else
            Console.WriteLine("Нет команды");
    }
    public void PressUndo()
    {
		if (command != null)
			command.Undo();
		else
            Console.WriteLine("Нет команды");
    }
}

*/

class Microwave
{
	public StartCooking(int time)
	{
		Console.WriteLine("Подогревам еду");
		// имитация работы с помощью асинхронного метода Task.Delay
		Task.Delay(time).GetAwaiter().GetResult();
	}
	
	public void StopCooking()
	{
		Console.WriteLine("Еда подогрета!");
	}
}

class MicrowaveCommand : Command
{
	Microwave microwave;
	int time;
	
	public MicrowaveCommand(Microwave m, int t)
	{
		microwave = m;
		time = t;
	}
	public override void Execute()
	{
		microwave.StartCooking(time);
		microwave.StopCooking();
	}
	public override void Undo()
	{
		microwave.StopCooking();
	}
}

// При этом инициатор необязательно указывает на одну команду. Он может управлять множеством команд. Например, на пульте от телевизора есть как кнопка для включения, так и кнопки для регулировки звука:

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        TV tv = new TV();
        Volume volume = new Volume();
        MultiPult mPult = new MultiPult();
        mPult.SetCommand(0, new TVOnCommand(tv));
        mPult.SetCommand(1, new VolumeCommand(volume));
        // включаем телевизор
        mPult.PressButton(0);
        // увеличиваем громкость
        mPult.PressButton(1);
        mPult.PressButton(1);
        mPult.PressButton(1);
        // действие отмены
        mPult.PressUndoButton();
        mPult.PressUndoButton();
        mPult.PressUndoButton();
        mPult.PressUndoButton();
    }
}

// абстрактный класс команды
abstract class Command
{
    public abstract void Execute();
    public abstract void Undo();
}

class NoCommand : Command
{
    public override void Execute()
    {
        Console.WriteLine("Пустая команда. Ничего не произошло");
    }
    public override void Undo()
    {
        Console.WriteLine("Отмена пустой команды. Все еще ничего не произошло");
    }
}
// Reciever - получатель
class TV
{
    public void On()
    {
        Console.WriteLine("Включил телек!");
    }
    public void Off()
    {
        Console.WriteLine("Выключил телек...");
    }

}

class TVOnCommand : Command
{
    TV tv;
    public TVOnCommand(TV tvSet)
    {
        tv = tvSet;
    }
    public override void Execute()
    {
        tv.On();
    }
    public override void Undo()
    {
        tv.Off();
    }
}

class Volume
{
    public const int OFF = 0;
    public const int HIGH = 20;
    private int level;

    public Volume()
    {
        level = OFF;
    }

    public void RaiseLevel()
    {
        if (level < HIGH)
            level++;
        Console.WriteLine("Уровень звука: " + level);
    }
    public void DropLevel()
    {
        if (level > OFF)
            level--;
        Console.WriteLine("Уровень звука: " + level);
    }
}

class VolumeCommand : Command
{
    Volume vol;
    public VolumeCommand(Volume v)
    {
        vol = v;
    }
    public override void Execute()
    {
        vol.RaiseLevel();
    }

    public override void Undo()
    {
        vol.DropLevel();
    }

}

// Invoker
class MultiPult
{
    Command[] buttons;
    Stack<Command> commandsHistory;

    public MultiPult()
    {
        buttons = new Command[2];
        for (int i = 0; i < 2; i++)
            buttons[i] = new NoCommand();
        commandsHistory = new Stack<Command>();
    }

    public void SetCommand(int numver, Command com)
    {
        if (numver < 2)
            buttons[numver] = com;
    }

    public void PressButton(int number)
    {
        if (number < 2)
            buttons[number].Execute();
        // добавляем использанную команду в лог команд
        commandsHistory.Push(buttons[number]);
    }
    public void PressUndoButton()
    {
        // подсчет объектов в стеке и проверка на их присутсвие
        if (commandsHistory.Count > 0)
        {
            // удаление 
            Command undoCommand = commandsHistory.Pop();
            undoCommand.Undo();
        }
    }
}

// Здесь два получателя команд - классы TV и Volume. Volume управляет уровнем звука и сохраняет текущий уровень в переменной level. Также есть две команды TVOnCommand и VolumeCommand.

// Инициатор - MultiPult имеет две кнопки в виде массива buttons: первая предназначена для TV, а вторая - для увеличения уровня звука. Чтобы сохранить историю команд используется стек. При отправке команды в стек добавляется новый элемент, а при ее отмене, наоборот, происходит удаление из стека. В данном случае стек выполняет роль примитивного лога команд.

// мое прим. Программа написана с возможностью показать, что есть несколько объектов, а инициатор может включать в себя несколько команд, но логическая схема программы отвратительна. Звук является отдельным объектом от телевизора и никак с ним не связан, но выключить телевизор не выйдет, пока не уберешь звук, зато прекрасно показали хранение команд в стеке. Попробую переписать данную программу и сделать ее более интересной.
// бля пусть это будет практикой

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        TV tv = new TV();
        MultiPult mPult = new MultiPult();
        mPult.SetCommand(0, new TVOnCommand(tv));
        mPult.SetCommand(1, new VolumeCommand(tv));
        // включаем телевизор
        mPult.PressButton(0);
        // увеличиваем громкость
        mPult.PressButton(1);
        mPult.PressButton(1);
        mPult.PressButton(1);
        // действие отмены
        mPult.PressUndoButton(1);
        mPult.PressUndoButton(1);
        mPult.PressUndoButton(1);
        mPult.PressUndoButton(0);
        mPult.LogCommands();
    }
}

// абстрактный класс команды
abstract class Command
{
    public abstract void Execute();
    public abstract void Undo();
    // добавим обязательный метод для лога
    public abstract void Show(int i);
}

class NoCommand : Command
{
    public override void Execute()
    {
        Console.WriteLine("Пустая команда. Ничего не произошло");
    }
    public override void Undo()
    {
        Console.WriteLine("Отмена пустой команды. Все еще ничего не произошло");
    }
    public override void Show(int i)
    {
    }
}
// Reciever - получатель
class TV
{
    // секция состояния телевизора
    public void On()
    {
        Console.WriteLine("Включил телек!");
    }
    public void Off()
    {
        Console.WriteLine("Выключил телек...");
    }

    // секция звука
    public const int OFF = 0;
    public const int HIGH = 20;
    private int level;

    public TV()
    {
        level = OFF;
    }

    public void RaiseLevel()
    {
        if (level < HIGH)
            level++;
        Console.WriteLine("Уровень звука: " + level);
    }
    public void DropLevel()
    {
        if (level > OFF)
            level--;
        Console.WriteLine("Уровень звука: " + level);
    }
}

class TVOnCommand : Command
{
    TV tv;

    public TVOnCommand(TV tvSet)
    {
        tv = tvSet;
    }
    public override void Execute()
    {
        tv.On();
    }
    public override void Undo()
    {
        tv.Off();
    }
    public override void Show(int i)
    {
        Console.Write("Включение телевизора: ");
        if (i == 1)
            Console.WriteLine("Телевизор был включен!");
        else
            Console.WriteLine("Телевизор был выключен...");
    }
}


class VolumeCommand : Command
{
    TV tv;


    public VolumeCommand(TV v)
    {
        tv = v;
    }
    public override void Execute()
    {
        tv.RaiseLevel();
    }

    public override void Undo()
    {
        tv.DropLevel();
    }
    public override void Show(int i)
    {
        Console.Write("Регулировка звука: ");
        if (i == 1)
            Console.WriteLine("Звук был увеличен!");
        else
            Console.WriteLine("Звук был уменьшен...");
    }
}

// Invoker
class MultiPult
{
    Command[] buttons;
    // пускай будет как лог, который мы в конце посмотрим. Лог состоит из 2 очередей, связанных между собой индексами. В одном - комнада, во втором - какая команда.
    Queue<Command> commandsHistory;
    Queue<byte> log;

    public MultiPult()
    {
        buttons = new Command[2];
        for (int i = 0; i < 2; i++)
            buttons[i] = new NoCommand();
        log = new Queue<byte>();
        commandsHistory = new Queue<Command>();
    }

    public void SetCommand(int numver, Command com)
    {
        if (numver < 2)
            buttons[numver] = com;
    }

    public void PressButton(int number)
    {
        if (number < 2)
        {
            buttons[number].Execute();
            // добавляем использанную команду в лог команд
            log.Enqueue((byte)1);
            commandsHistory.Enqueue(buttons[number]);
        }
    }
    public void PressUndoButton(int number)
    {
        if (number < 2)
        {
            buttons[number].Undo();
            // добавляем использанную команду в лог команд
            log.Enqueue((byte)0);
            commandsHistory.Enqueue(buttons[number]);
        }
    }

    public void LogCommands()
    {
        Queue<Command> oldCommandHistory = new Queue<Command>();
        Command oldCom;
        Queue<byte> oldLogHistory = new Queue<byte>();
        int oldLog;

        int x = commandsHistory.Count;
        for (int i = 0; i < x; i++)
        {
            oldCom = commandsHistory.Dequeue();
            oldLog = (int)log.Dequeue();
            oldCom.Show(oldLog);
            oldCommandHistory.Enqueue(oldCom);
            oldLogHistory.Enqueue((byte)oldLog);
        }
        commandsHistory = oldCommandHistory;
        log = oldLogHistory;
    }
}

// 													Макрокоманды
// 	Для управления набором команд используются макрокоманды. Макрокоманда должна реализовать тот же интерфейс, что и другие команды, при этом макрокоманда инкапсулирует в одной из своих переменных весь набор используемых команд. Рассмотрим на примере.

// Для создания и развития программного продукта необходимо несколько исполнителей, выполняющих различные функции: программист пишет код, тестировщик выполняет тестирование продукта, а маркетолог пишет рекламные материалы и проводит кампании по рекламированию продукта. Управляет всем процессом менеджер. Программа на C#, описывающая создание программного продукта с помощью паттерна команд, могла бы выглядеть следующим образом:

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Programmer prog = new Programmer();
        Tester tester = new Tester("Tig");
        Marketolog marketolog = new Marketolog();

        List<ICommand> commands = new List<ICommand>
        {
            new CodeCommand(prog),
            new TestCommand(tester),
            new AdvertizeCommand(marketolog)
        };

        Manager manager = new Manager();
        manager.SetCommand(new MacroCommand(commands));
        manager.StartProject();
        manager.StopProject();

        tester = new Tester("Leo");
        manager.StartProject();
        manager.StopProject();
    }
}
interface ICommand
{
    void Execut();
    void Undo();
}
// класс макрокоманды
class MacroCommand : ICommand
{
    List<ICommand> commands;
    public MacroCommand(List<ICommand> coms)
    {
        commands = coms;
    }

    public void Execut()
    {
        foreach (ICommand c in commands)
            c.Execut();
    }

    public void Undo()
    {
        foreach (ICommand c in commands)
            c.Undo();
    }
}

class Programmer
{
    public void StartCoding()
    {
        Console.WriteLine("Программист начинает писать код!");
    }
    public void StopCoding()
    {
        Console.WriteLine("Программист заканчивает писать код...");
    }
}

class Tester
{
    string Name;
    public Tester (string n)
    {
        Name = n;
    }
    public void StartTest()
    {
        Console.WriteLine("Тестировщик начинает тестирование!");
    }
    public void StopTest()
    {
        Console.WriteLine($"Тестировщик {Name} заканчивает тестирование...");
    }
}

class Marketolog
{
    public void StartAdvertize()
    {
        Console.WriteLine("Маркетолог начинает рекламировать продукт!");
    }

    public void StopAdvertize()
    {
        Console.WriteLine("Маркетолог заканчивает рекламировать продукт...");

    }
}

class CodeCommand : ICommand
{
    Programmer programmer;
    public CodeCommand(Programmer p)
    {
        programmer = p;
    }
    public void Execut()
    {
        programmer.StartCoding();
    }
    public void Undo()
    {
        programmer.StopCoding();
    }
}

class TestCommand : ICommand
{
    Tester tester;
    public TestCommand(Tester t)
    {
        tester = t;
    }
    public void Execut()
    {
        tester.StartTest();
    }
    public void Undo()
    {
        tester.StopTest();
    }
}

class AdvertizeCommand : ICommand
{
    Marketolog marketolog;
    public AdvertizeCommand(Marketolog m)
    {
        marketolog = m;
    }
    public void Execut()
    {
        marketolog.StartAdvertize();
    }
    public void Undo()
    {
        marketolog.StopAdvertize();
    }
}

class Manager
{
    ICommand command;
    public void SetCommand(ICommand c)
    {
        command = c;
    }
    public void StartProject()
    {
        if (command != null)
            command.Execut();
    }
    public void StopProject()
    {
        if (command != null)
            command.Undo();
    }
}

// В роли инициатора здесь выступает менеджер, а в роли получателей запросов - программист, маркетолог и тестеровщик. Запуская проект, менеджер тем самым запускает макрокоманду, которая содержит ряд отдельных команд. Выполнение этих команд делегируется получателям.
