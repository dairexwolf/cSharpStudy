//Для определения локальной переменной-ссылки (ref local) перед ее типом ставится ключевое слово ref:
int x = 5;
ref int xRef = ref x; 	//Здесь переменная xRef указывает не просто на значение переменной x, а на область в памяти, где располагается эта переменная. Для этого перед x также указывается ref.
ref int xRef2; 			//Ошибка. Мы не можем просто определить переменную-ссылку, нам обязательно надо присвоить ей некоторое значение.

//Получив ссылку, мы можем манипулировать значением по этой ссылке. Например:
static void Main()
{
	int x =5;
	ref int xRef = ref x;
	Console.WriteLine(x);	//5
	xRef = 125;
	Console.WriteLine(x);	//125
	x=652;
	Console.WriteLine(xRef);//652
	
	Console.ReadLine();
}

//Стоит учитывать, возможность переназначить ссылку появилась только с версии 7.3:

int a = 5;
int b = 8;
Console.WriteLine("a = "+a); 
Console.WriteLine("b = "+b);
ref int pointer = ref a;
pointer = 34;		// a = 34
pointer = ref b;	// до версии 7.3 так делать было нельзя
pointer = 6;		// b = 6 
Console.WriteLine("a = "+a); 
Console.WriteLine("b = "+b);

//Для возвращения из функции ссылки в сигнатуре функции перед возвращаемым типом, а также после оператора return следует указать ключевое слово ref:
static ref int Find(int number, int[] numbers)
{
	int k = numbers.Length;
	for(int i=0; i<k;i++)
	{
		if (numbers[i] == number)
			return ref numbers[i];	//возвращаем ссылку на адрес, а не само значение
	}
	throw new IndexOutOfRangeException("Число не найдено");
}

static void Main()
{
	int[] numbers = { 1, 2 ,3, 4, 5, 6, 7 };
	ref int numberRef = ref Find(4, numbers);	//Ищем число 4 в массиве
	numberRef = 9;	//заменяем его на 9
	Console.WriteLine(numbers[3]);	//9
}