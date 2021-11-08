class Employee
{
    public virtual void Work()
    {
        Console.WriteLine("Да работаю я, работаю");
    }
}
 
class Manager : Employee
{
    public override void Work()
    {
        Console.WriteLine("Отлично, работаем дальше");
    }
    public bool IsOnVacation { get; set; }
}

class Program 
{
	static void Main()
	{
		Employee emp = new Manager(); //Employee();
		UseEmployee(emp);
		
		Console.Read();
	}
	
	static void UseEmployee (Employee emp)
	{
		if (emp is Manager manager && manager.IsOnVacation==false) //вот эта штука
	/*
		//При обычном случае это было бы: 
		if (emp is Manager)
    {
        Manager manager = (Manager)emp;
        if(!manager.IsOnVacation)
            manager.Work();
    }
	*/
	//Pattern matching фактически выполняет сопоставление с некоторым шаблоном. Здесь выполняется сопоставление с типом Manager. То есть в данном случае речь идет о type pattern - в качестве шаблона выступает тип Manager. Если сопоставление прошло успешно, в переменной manager оказывается объект emp. И далее мы можем вызвать у него методы и свойства.
		{
			manager.Work();
		}
		else
		{ 
			Console.WriteLine("Никакого праздника");
		}
	}
	
	//Также мы можем использовать constant pattern - сопоставление с некоторой константой. Например, можно проверить, имеет ли ссылка значение:
	static void UseEmployee1 
	{
		if (!(emp is null))
		{
			emp.Work();
		}
	}
	
	//Кроме выражения if pattern matching может применяться в конструкции switch:
	static void SwitchEmployee (Employee emp)
	{
		switch (emp)
		{
			case Manager manager:
				manager.Work();
				break;
			case null:
				Console.WriteLine("Объект не задан");
				break;
			default:
				Console.WriteLine("Объект не менеджер");
				break;
		}
	}
	
	//С помощью выражения when можно вводить дополнительные условия в конструкцию case. В этом случае опять же преобразуем объект emp в объект типа Manager и в случае удачного преобразования смотрим на значение свойства IsOnVacation: если оно равно false, то выполняется данный блок case.
	static void WhenEmployee (Employee emp)
	{
		swith (emp)
		{
			case Manager manager when manager.IsOnVacation==false:
				manager.Work();
				break;
			case null:
		Console.WriteLine("Объект не задан");
				break;
			default:
				Console.WriteLine("Объект не менеджер");
				break;
		}
	}
}
	