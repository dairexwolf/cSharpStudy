using System;

abstract class Card
{
    public bool active;

    protected string firm;

    public abstract int Sum();

    public abstract void SumPlus(int sum);

    public abstract void SumMinus(int sum);
}

abstract class Bankomat : IDisposable
{
    public string firm;

    public bool[] operations;

    public Card card;

    public abstract void CashOut(int sum);

    public abstract void CashIn(int sum);

    public abstract int Balance();

    public void Dispose()
    { }
}

class CreditCard : Card
{
    public CreditCard(string firm) : base()
    {
        this.firm = firm;
        active = true;
    }

    private int dolg;

    public override int Sum()
    {
        return dolg;
    }

    public override void SumPlus(int sum)
    {
        dolg -= sum;
    }

    public override void SumMinus(int sum)
    {
        dolg += sum;
    }
}

class DebitCard : Card
{
    public DebitCard(string firm) : base()
    {
        this.firm = firm;
        active = true;
    }

    private int balance;

    public override int Sum()
    {
        return balance;
    }

    public override void SumPlus(int sum)
    {
        balance += sum;
    }

    public override void SumMinus(int sum)
    {
        int x = balance - sum;
        if (0 < x)
        {
            balance = x;
        }
        else
        {
            throw new Exception("Недостаточно средств");
        }
    }
}

class BankomatAll : Bankomat
{
    public BankomatAll(string firm, Card card) : base()
	{
        this.firm = firm;
        operations = new bool[] { true, true, true };
        this.card = card;
    }

    public override void CashOut(int sum)
    {
        card.SumMinus(sum);
    }

    public override void CashIn(int sum)
    {
        card.SumPlus(sum);
    }

    public override int Balance()
    {
        return card.Sum();
    }
}

class Program
{
    static void Main()
    {
        
        Console.WriteLine("Введите тип вашей карты:\n1. Кредитная\n2. Дебетовая");
        Card card;
        int i = Int32.Parse(Console.ReadLine());
        string con;
        switch (i)
        {
            case 1:
                card = new CreditCard("-");
                break;

            case 2:
                card = new DebitCard("-");
                break;
            default:
                Console.WriteLine("Завести карту не удалось");
                throw new Exception("Неверная карта");
        }

        Bankomat bankomat = new BankomatAll("FiberStrike", card);

        for (;;)
        {
            Console.WriteLine("Добро пожаловать в программу банкомат!");
            
            Console.WriteLine("Банкомат способен и выдавать, и принимать банкноты");
            Console.WriteLine("Введите цифру желаемой операции");
            Console.WriteLine("1. Внести сумму на счет");
            Console.WriteLine("2. Снять сумму со счета");
            Console.WriteLine("3. Посмотреть баланс");
            Console.WriteLine("4. Выход");

            con = Console.ReadLine();
            if (con != "")
                i = Int32.Parse(con);
            else break;

            switch (i)
            {
                case 1:
                    Console.Write("Введите сумму:");
                    con = Console.ReadLine();
                    if (con != "")
                    {
                        i = Int32.Parse(con);
                        bankomat.CashIn(i);
                    }

                    else
                    {
                        Console.WriteLine("Ошибка операции");
                        break;
                    }
                    break;

                case 2:
                    Console.Write("Введите сумму:");
                    con = Console.ReadLine();
                    if (con != "")
                    {
                        i = Int32.Parse(con);
                        bankomat.CashOut(i);
                    }

                    else
                    {
                        Console.WriteLine("Ошибка операции");
                        break;
                    }
                    break;
                case 3:
                    Console.WriteLine("Баланс равен {0} рублей", bankomat.Balance());
                    break;
            }

            Console.WriteLine("Желаете провести еще одну операцию? 1 - да, остальное - нет");
            con = Console.ReadLine();
            if (con != "1")
            {
                break;
            }
        }
    }
}