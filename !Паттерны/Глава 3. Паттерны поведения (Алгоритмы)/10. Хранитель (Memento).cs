 // Паттерн Хранитель (Memento) позволяет выносить внутреннее состояние объекта за его пределы для последующего возможного восстановления объекта без нарушения принципа инкапсуляции.
 
 /*
 Когда использовать Memento?

- Когда нужно сохранить состояние объекта для возможного последующего восстановления

- Когда сохранение состояния должно проходить без нарушения принципа инкапсуляции
 */
 
// То есть ключевыми понятиями для данного паттерна являются сохранение внутреннего состояния и инкапсуляция, и важно соблюсти баланс между ними. Ведь, как правило, если мы не нарушаем инкапсуляцию, то состояние объекта хранится в объекте в приватных переменных. И не всегда для доступа к этим переменным есть методы или свойства с сеттерами и геттерами. Например, в игре происходит управление героем, все состояние которого заключено в нем самом - оружие героя, показатель жизней, силы, какие-то другие показатели. И нередко может возникнуть ситуация, сохранить все эти показатели во вне, чтобы в будущем можно было откатиться к предыдущему уровню и начать игру заново. В этом случае как раз и может помочь паттерн Хранитель.

// Формальная структура паттерна на языке C#:

class Memento
{
	public string State { get; private set; }
	public Memento (string state)
	{
		this.State = state;
	}
}

Caretaker
{
	public Memento memento { get; set; }
}

class Originator
{
	public string State { get; set; }
	public void SetMemento (Memento memento)
	{
		State = memento.State;
	}
	public Memento CreateMemento()
	{
		return new Memento(State);
	}
}

/*
Участники

- Memento: хранитель, который сохраняет состояние объекта Originator и предоставляет полный доступ только этому объекту Originator

- Originator: создает объект хранителя для сохранения своего состояния

- Caretaker: выполняет только функцию хранения объекта Memento, в то же время у него нет полного доступа к хранителю и никаких других операций над хранителем, кроме собственно сохранения, он не производит
*/

// Теперь рассмотрим реальный пример: нам надо сохранять состояние игрового персонажа в игре:

using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Hero hero = new Hero();
        hero.Shoot();   // делаем выстрел
        GameHistory game = new GameHistory();

        game.History.Push(hero.SaveState());    // сохраняем игру

        hero.Shoot();   // делаем выстрел, осталось 8 патронов
        hero.RestoreState(game.History.Pop());

        hero.Shoot();   // делаем выстрел

        Console.Read();
    }
}

// organisator
class Hero
{
    private int bullets = 10;   // количество патронов
    private int lives = 5;      // кол-во жизней

    public void Shoot()
    {
        if (bullets > 0)
        {
            bullets--;
            Console.WriteLine("Производим вытрел.Осталось {0} патронов", bullets);
        }
        else Console.WriteLine("Кончились патроны");
    }
    // сохранение состояния
    public HeroMemento SaveState()
    {
        Console.WriteLine("Сохранение игры. Осталось {0} патронов и {1} жизней", bullets, lives);
        return new HeroMemento(bullets, lives);
    }

    // загрузка прошлого состояния
    public void RestoreState(HeroMemento memento)
    {
        this.bullets = memento.Bullets;
        this.lives = memento.Lives;
        Console.WriteLine($"Загрузка прошлого состояния. Параметры: {bullets} патронов, {lives} жизней");
    }
}

//Memento
class HeroMemento
{
    public int Bullets { get; private set; }
    public int Lives { get; private set; }
    public HeroMemento(int bullets, int lives)
    {
        this.Bullets = bullets;
        this.Lives = lives;
    }
}

// Caretaker
class GameHistory
{
    public Stack<HeroMemento> History { get; private set; }
    public GameHistory()
    {
        History = new Stack<HeroMemento>();
    }

}

// Здесь в роли Originator выступает класс Hero, состояние которого описывается количество патронов и жизней. Для хранения состояния игрового персонажа предназначен класс HeroMemento. С помощью метода SaveState() объект Hero может сохранить свое состояние в HeroMemento, а с помощью метода RestoreState() - восстановить.

// Для хранения состояний предназначен класс GameHistory, причем все состояния хранятся в стеке, что позволяет с легкостью извлекать последнее сохраненное состояние.

/*
Использование паттерна Memento дает нам следующие преимущества:

- Уменьшение связанности системы

- Сохранение инкапсуляции информации

- Определение простого интерфейса для сохранения и восстановления состояния
*/

// В то же время мы можем столкнуться с недостатками, в частности, если требуется сохранение большого объема информации, то возрастут издержки на хранение всего объема состояния.