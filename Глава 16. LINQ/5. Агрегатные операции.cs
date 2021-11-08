// К агрегатным операциям относят различные операции над выборкой, например, получение числа элементов, получение минимального, максимального и среднего значения в выборке, а также суммирование значений.

// 														Метод Aggregate 
// Метод Aggregate выполняет общую агрегацию элементов коллекции в зависимости от указанного выражения. Например:

int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

int query = nums.Aggregate((x,y) => x - y);
Console.WriteLine(query);

// Переменная query будет представлять результат агрегации массива. В качестве условия агрегации используется выражение (x,y)=> x - y, то есть вначале из первого элемента вычитается второй, потом из получившегося значения вычитается третий и так далее. То есть будет эквивалентно выражению:

query = 1 - 2 - 3 - 4 - 5 - 6 - 7 - 8 - 9 - 10;
Console.WriteLine(query);

// Соответственно мы бы могли использовать любые другие операции, например, сложение:

int query = nums.Aggregate((x, y) => x + y);

query = 1+2+3+4+5+6+7+8+9+10;

// 												Получение размера выборки. Метод Count

// Для получения числа элементов в выборке используется метод Count():

int[] nums = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
int size = (from i in nums where i%2 ==0 && i>10 select i).Count();
Console.WriteLine(size);

// Метод Count() в одной из версий также может принимать лямбда-выражение, которое устанавливает условие выборки. Поэтому мы можем в данном случае не использовать выражение Where:

int size = nums.Count( i => i % 2 == 0 && i > 10);
Console.WriteLine(size);

// 														Получение суммы

// Для получения суммы значений применяется метод Sum:
int[] nums = { 1, 2, 3, 4, 10, 34, 55, 66, 77, 88 };
List<User> users = new List<User>()
{
	new User { Name = "Tom", Age = 23 },
	new User { Name = "Sam", Age = 27 },
	new User { Name = "Bill", Age = 35 },
	new User { Name = "Tom", Age = 54 }
};

int min1 = nums.Min();
Console.WriteLine(min1);
int min2 = users.Min( n => n.Age );
Console.WriteLine(min2);

int max1 = nums.Max();
Console.WriteLine(max1);
int max2 = users.Max( n => n.Age);
Console.WriteLine(max2);

double avg1 = nums.Average();
Console.WriteLine(avg1);
double avg2 = users.Average(n => n.Age);
Console.WriteLine(avg2);

// Один из самых популярных вопросов. Как имея агрегатную функцию min() или max() , получить из коллекции тех же юзеров, не только к примеру максимальный возраст (его значение), но и сам объект юзера?
users.Where(x => x.Age == users.Max(n => n.Age))
// или
var maxUsersAge = users.Max(n => n.Age);
var selectedUsers = users.Where(x => x.Age == maxUsersAge);

