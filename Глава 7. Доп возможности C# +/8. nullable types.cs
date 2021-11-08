//Значение null по умолчанию могут принимать только объекты ссылочных типов. Однако в различных ситуациях бывает удобно, чтобы объекты числовых типов данных имели значение null, то есть были бы не определены. Стандартный пример - работа с базой данных, которая может содержать значения null. И мы можем заранее не знать, что мы получим из базы данных - какое-то определенное значение или же null. Для этого надо использовать знак вопроса ? после типа значений. Например:

int? z = null;
bool? enabled = null;

//Но фактически запись ? является упрощенной формой использования структуры System.Nullable<T>. Параметр T в угловых скобках представляет универсальный параметр, вместо которого в конкретной задача уже подставляется конкретный тип данных. Следующие виды определения переменных будут эквивалентны:

int? z1 = 5;
bool? enabled1 = null;
Double? d1 = 3.3;

Nullable<int> z2 = 5;
Nullable<bool> enabled 2 = null;
Nullable<System.Double> d2 = 3.3;

//Для всех типов Nullable определено два свойства: Value, которое представляет значение объекта, и HasValue, которое возвращает true, если объект Nullable хранит некоторое значение. Причем свойство Value хранит объект того типа, которым типизируется Nullable:

class Program
{
	static void Main()
	{
		int? x = 7;
		Console.WriteLine(x.Value); 			//7
		Nullasble<State> state = new State() {Name = "Naenia" };
		Console.WriteLine(state.Value.Name); 	//Narnia
		Console.ReadLine();
	}
}
struct State
{
	public string Name{ get; set; }
}

//Однако если мы попробуем получить значение переменной, которая равна null, то мы столкнемся с ошибкой:

int? x = null;
Console.WriteLine(x.Value); 			//Ошибка
State? state = null;
Console.WriteLine(state.Value.Name);	//Ошибка

//В этом случае необходимо выполнять проверку на наличие значения с помощью еще одного свойства HasValue:

int? x = null;
if(x.HasValue)	//если имеет значение, значит HasValue вернет true, выполниться условие
	Console.WriteLine(x.Value);
else 			//если не имеет значения, значит HasValue вернет false, выполнится условие
	Console.WriteLine("x is equal to null");

State? state = null;
if(state.HasValue)	//если имеет значение, значит HasValue вернет true, выполниться условие
	Console.WriteLine(state.Value.Name);
else				//если не имеет значения, значит HasValue вернет false, выполнится условие
	Console.WriteLine("state is equal to null");
	
//Также надо учитывать, что структура Nullable применяется только для типов значений, поскольку ссылочные типы итак могут иметь значение null. Т.е. классы не подойдут.

class Country
{
	public string Name {get; set; }
}
struct State
{
	public string Name {get; set; }
}

class Program
{
	static void Main()
	{
		Nullabe<State> state = null; 		//Значимый тип, не ссылочный, поэтому можно
		Nullable<Country> country = null; 	//Ошибка - Country - ссылчоный тип
		
	Console.ReadLine();
	}
}

//Также структура Nullable с помощью метода GetValueOrDefault() позволяет использовать значение по умолчанию (для числовых типов это 0), если значение не задано:

int? figure = null;
Console.WriteLine(figure.GetValueOrDefault());	//По умолчанию используется 0
Console.WriteLine(figure.GetValueOrDefault(10));	//По умолчанию используется 10

													//Равенство объектов
//При проверке объектов на равенство следует учитывать, что они равны не только, когда они имеют ненулевые значения, которые совпадают, но и когда оба объекта равны null. Но структуры должны быть равны

int? x1 = null;
int? x2 = null;

if (x1==x2)
	Console.WriteLine("Объекты равны");		//Эти будут равны
else 
	Console.WriteLine("Объекты не равны");
	x1 = 52;
if (x1==x2)
	Console.WriteLine("Объекты равны");		//Эти будут равны
else 
	Console.WriteLine("Объекты не равны");

													//Преобразование типов
//Рассмотрим возможные преобразования:
//явное преобразование от T? к T
int? x1 = 12;
if(x1.HasValue)
{
	int x2 = (int)x1;
	Console.WriteLine(x2);
}
//Неявное преобразование от T к T?
int x1 = 4;
int? x2 = x1;
Console.WriteLine(x2);

//неявные расширяющие преобразования от V к T?
int x1 = 6;
long? x2 = (int?)x1;
Console.WriteLine(x2);

//Явные сужающие преобразования от V к T?
long x1 = 514;
int? x2 = (int?)x1;
Console.WriteLine(x2);

//Подобным образом работают преобразования от V? к T?
long? x1 = 14;
int? x2 = (int?)x1;
Console.WriteLine(x2);

//И явные преобразования от V? к T
long? x1 = 8;
int x2 = (int)x1;
Console.WriteLine(x2);


