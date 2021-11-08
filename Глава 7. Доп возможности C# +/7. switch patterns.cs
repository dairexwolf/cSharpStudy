class Program 
{
	/*
	*Обычная ситуация
	*static int Select (int o, int a, int b)
	*{
	*	switch (o) 
	*	{
	*		case 1: return a+b;
	*		case 2: return a-b;
	*		case 3: return a*b;
	*		default: throw new ArgumentException("Недопустимый код операции");
	*	}
	*}
	*/
	//Паттерны
	
	/*static int Select (int o, int a, int b)
	*{
	*	return op switch
	*	{
	*		1 => a + b,
	*		2 => a - b,
	*		3 => a * b,
	*		_ => throw new ArgumentException("Недопустимый код операции");
	*	};
	*	return result;
	*}
	*/
	//Теперь не требуется оператор case, а после сравниваемого значения ставится стрелка =>. Значение справа от стрелки выступает в качестве возвращаемоего значения. Кроме того, вместо оператора default используется почерк _. В итоге результат конструкции switch будет присвиваиваться переменной result.
	//Однако, все можно укоротить еще больше
	
	static int Select(int op, int a, int b) => o switch
	{
		1 => a + b,
		2 => a - b,
		3 => a * b,
		_ => throw new ArgumentException("Недопустимый код операции");
	};
	
	static void Main()
	{
		try
		{
		int x = Select(1,4,10);
		Console.WriteLine(x);
		
		x = Select(10,4,10);
		Console.WriteLine(x);
		
		}
	}
}

