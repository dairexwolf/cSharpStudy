using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    // главный метод
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
        // показать логи
        mPult.LogCommands();

        Console.WriteLine();
        // создадим множество комманд и убедимся, что ограничение кеша на логи работают
        mPult.PressButton(0);
        mPult.PressButton(1);
        mPult.PressButton(1);
        mPult.PressButton(1);
        mPult.PressButton(1);
        mPult.PressButton(1);
        mPult.PressButton(1);
        mPult.PressButton(1);
        mPult.PressButton(1);
        mPult.PressUndoButton(1);
        mPult.PressUndoButton(1);
        mPult.PressUndoButton(1);
        mPult.PressUndoButton(1);
        mPult.LogCommands();

        Console.Read();
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

// Пустая команда
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
	bool TVon;
    // секция состояния телевизора
    public void On()
    {
		if (TVon)
		{
			Console.WriteLine("Телевизор уже включен");
		}
		else
		{
			Console.WriteLine("Включил телек!");
			TVon = true;
		}
    }
    public void Off()
    {
		if (TVon)
		{	
			Console.WriteLine("Выключил телек...");
			TVon = false;
		}
		else 
			Console.WriteLine("Телевизор уже выключен");
		
    }

    // секция звука
    public const int OFF = 0;
    public const int HIGH = 20;
    private int level;

    public TV()
    {
        level = OFF;
		TVon = false;
    }

    public void RaiseLevel()
    { 
		if (TVon)
		{
			if (level < HIGH)
				level++;
			Console.WriteLine("Уровень звука: " + level);
		}
    }
    public void DropLevel()
    {
		if (TVon)
		{
			if (level > OFF)
				level--;
			Console.WriteLine("Уровень звука: " + level);
		}
    }
}

// Реализация команды - включение\выключение телевизора
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

// Реализаиця команды - увеличение\уменьшение звука
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


// Invoker - сам пульт
class MultiPult
{
    // Массив команд
    protected Command[] buttons;
    // Класс логов
    protected CommandsLog commandsHistory;

    // при инициализации заполняем все нуливыми команадами и инициализация логов
    public MultiPult()
    {
        buttons = new Command[2];
        for (int i = 0; i < 2; i++)
            buttons[i] = new NoCommand();
        commandsHistory = new CommandsLog();
    }

    // назначение команды на кнопку
    public void SetCommand(int numver, Command com)
    {
        if (numver < 2)
            buttons[numver] = com;
    }

    // моделирование нажатия на кнопку
    public void PressButton(int number)
    {
        if (number < 2)
        {
            // моделирование задержки при нажатии на кнопку
            Thread.Sleep(100);
            buttons[number].Execute();
            commandsHistory.Enqueue(buttons[number], (byte)1);
        }
    }
    public void PressUndoButton(int number)
    {
        if (number < 2)
        {
            // моделирование задержки при нажатии на кнопку
            Thread.Sleep(100);
            buttons[number].Undo();
            // добавляем использанную команду в лог команд
            commandsHistory.Enqueue(buttons[number], (byte)0);
        }
    }

    public void LogCommands()
    {
        // моделирование работы прибора
        Thread.Sleep(100);
        commandsHistory.Show();
    }
}

// класс лога
class CommandsLog
{
    // пускай будет как лог, который мы в конце посмотрим. Лог состоит из 2 очередей, 
	//связанных между собой индексами. В одном - комнада, во втором - какая команда.
    Queue<Command> commandsHistory;
    Queue<byte> log;

    public CommandsLog()
    {
        commandsHistory = new Queue<Command>();
        log = new Queue<byte>();
    }

    public void Enqueue(Command com, byte flag)
    {
        if (log.Count < 10)
        {
            // добавляем использанную команду в лог команд
            log.Enqueue(flag);
            commandsHistory.Enqueue(com);
        }
        else
        {
            log.Dequeue();
            log.Enqueue(flag);
            commandsHistory.Dequeue();
            commandsHistory.Enqueue(com);
        }
    }

    public void Show()
    {
        Queue<Command> oldCommandHistory = new Queue<Command>();
        Command oldCom;
        Queue<byte> oldLogHistory = new Queue<byte>();
        int oldLog;

        int x = commandsHistory.Count;
        Console.WriteLine();
        if (x < 10)
            Console.WriteLine("Просмотр логов. Записано {0} команд.", x);
        else
            Console.WriteLine("Просмотр логов. Лог команд переполнен!", x);
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



