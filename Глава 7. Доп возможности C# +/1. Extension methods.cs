class Program 
{
	static void Main()
	{
		string s = "Привет мир";
		char c = 'и';
		int i = = s.Char.Count(c); //в строке s считывается количество символов c и записывается в i
		Console.WriteLine(i);
		Console.Read();
	}
}

public static class StringExtension
{
	public static int CharCount(this string str,char c) //this string str - Собственно метод расширения - 
	//это обычный статический метод, который в качестве первого параметра всегда принимает такую конструкцию: 
	//this имя_типа название_параметра, то есть в нашем случае this string str. 
	//Так как наш метод будет относиться к типу string, то мы и используем данный тип.
	{
		int count = 0;
		int l = str.Length;
		for (int i=0;i<l;i++)
		{
			if(str[i]==c) count++;
		}
		return count;
	}
}
		