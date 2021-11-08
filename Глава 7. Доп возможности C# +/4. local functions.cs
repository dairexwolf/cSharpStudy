class Program 
{
	static void Main()
	{
		var result = GetResult(new int[] {-3,-2,-1,0,1,2,3});
		Console.WriteLine(result); //6
		Console.Read();
	}
	
	static int GetResult (int[] numbers)
	{
		int limit = 0;
		//локальная функция
		bool IsMoreThan (int number)
		{
			return (number>limit);
		}
		
		int result = 0;
		int l = numbers.Length;
		for (int i = 0; i<l;i++)
		{
			if IsMoreThen(numbers[i]))
			{
				result +=numbers[i];
			}
		}
		return result; 
	}


	//Статическая локальная функция: Их особенностью является то, что они не могут обращаться к переменным окружения, то есть метода, в котором статическая функция определена.
	static int GetResult(int[] numbers)
	{
		//статическая локальная функция
		static bool IsMoreThan (int number)
		{
			int limit = 0;					//т.к. она не может обратиться к переменным функции, в которой была сделана
			return number > limit;
		}
		
		int result = 0;
		int l = numbers.Length;
		for (int i=0;i<l;i++)
		{
			if (IsMoreThan(numbers[i]))
			result += numbers[i];
		}
		
		return result;
	}
}