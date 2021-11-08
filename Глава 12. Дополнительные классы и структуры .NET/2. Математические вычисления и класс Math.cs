// Для выполнения различных математических операций в библиотеке классов .NET предназначен класс Math. Он является статическим, поэтому все его методы также являются статическими.
// 												Рассмотрим методы класса Math:
 
 int i = 1;	// счетчик
 double result = Math.Abs(-12.4);	// 12.4 
 Console.WriteLine(i + " Абсолютное значение -12.4: " + result);
 //Abs(double value): возвращает абсолютное значение для аргумента value
 
 i++;
 double res = Math.Acos(1);			// 0
 Console.WriteLine(i + " Арккосинус 1: " + res);
 //Acos(double value): возвращает арккосинус value. Параметр value должен иметь значение от -1 до 1
 
i++;
res = Math.Asin(-1);
 Console.WriteLine(i + " Арксинус -1: " + res);
 // Asin(double value): возвращает арксинус value. Параметр value должен иметь значение от -1 до 1

i++;
res = Math.Atan(34);
Console.WriteLine(i + " Арктангенс 34: " + res);
// Atan(double value): возвращает арктангенс value

i++;
res = Math.BigMul(100, 9340);
Console.WriteLine(i + " Произведение 100 * 9340 в виде объекта long: " + res);
//BigMul(int x, int y): возвращает произведение x * y в виде объекта long

i++;
res = Math.Ceiling(2.34);	// 3
Console.WriteLine(i + " Наименьшее целое число с плавающей точкой, которое не меньше 2.34: " + res);
// Ceiling(double value): возвращает наименьшее целое число с плавающей точкой, которое не меньше value

i++;
res = Math.Cos(180);
Console.WriteLine(i + " Косинус угла 180: " + res);
// Cos(double d): возвращает косинус угла d

i++;
res = Math.Cosh(180);
Console.WriteLine(i + " Гиперболический косинус угла 180: " + res);
// Cosh(double d): возвращает гиперболический косинус угла d

i++;
int ost;								// 5
int div = Math.DivRem(27, 5, out ost);	// 2
Console.WriteLine(i + " Результат 27/5 = {0}, а остаток = {1}", div, ost);
// возвращает результат от деления a/b, а остаток помещается в параметр result

i++;
res = Math.Exp(4);
Console.WriteLine(i + " Основание натурального логарифма, возведенное в степень 4: " + res);
// Exp(double d): возвращает основание натурального логарифма, возведенное в степень d

i++;
res = Math.Floor(3.95);			// 3
Console.WriteLine(i + " Наибольшее целое число, которое не больше 3.95: " + res);
// Floor(decimal d): возвращает наибольшее целое число, которое не больше d

i++;
res = Math.IEEERemainder(34, 5);	// 34-30=4
Console.WriteLine(i + " Остаток от деления 34 на 5: " + res);
// IEEERemainder(double a, double b): возвращает остаток от деления a на b
// - предназначена для каких-то специфических задач в электронике. Вас ждут очень весёлые результаты, если вы будете его использовать дабы получить остаток от деления. Используйте % для таких вычислений.

i++;
res = 

i++;
res = Math.Log(10);			// 
Console.WriteLine(i + " Натуральный логарифм числа 10: " + res);
// Log(double d): возвращает натуральный логарифм числа d

i++;
 res = Math.Log(16, 2);			// 4
Console.WriteLine(i + " Логарифм числа 16 по основанию 2 = "+ res);
// Log(double a, double newBase): возвращает логарифм числа a по основанию newBase

i++;
res = Math.Log10(20);
Console.WriteLine(i + " Десятичный логарифм числа 20 = "+ res);
// Log10(double d): возвращает десятичный логарифм числа d

i++;
res = Math.Max(5, 10);
Console.WriteLine(i + " Максимальное число из 5 и 10 = "+ res);
// Max(double a, double b): возвращает максимальное число из a и b

i++;
res = Math.Min(10, 34);
Console.WriteLine(i + " Минимальное число из 10 и 34 = "+ res);
// Min(double a, double b): возвращает минимальное число из a и b

i++;
res = Math.Pow(2, 10);
Console.WriteLine(i + " Возвращает число 2, возведенное в степень 10: "+ res);
//Pow(double a, double b): возвращает число a, возведенное в степень b

res = Math.Round(3.4);
Console.Write(i + " Округление 3.4 = " + res);

res = Math.Round(3.7);
Console.WriteLine("; Округление 3.7 = " + res);
// Round(double d): возвращает число d, округленное до ближайшего целого числа

i++;
res = Math.Round(20.567, 2);
Console.Write(i + " Округление 20.567 с 2 знаками после запятой: " + res);
res = Math.Round(20.543, 1);
Console.WriteLine("; Округление 20.543 с 1 знаком после запятой: " + res);
//Round(double a, round b): возвращает число a, округленное до определенного количества знаков после запятой, представленного параметром b

i++;
res = Math.Sign(15);						// 1
Console.Write(i + " Метод Sign(15): " + res);
res = Math.Sign(0);							// 0
Console.Write(". Метод Sign(0): " + res);
res = Math.Sign(-15);						// -1
Console.WriteLine(". Метод Sign(-15): {0}.", res);
// Sign(double value): возвращает число 1, если число value положительное, и -1, если значение value отрицательное. Если value равно 0, то возвращает 0

i++;
res = Math.Sin(90);
Console.WriteLine(i + " Синус 90 = " + res);
// Sin(double value): возвращает синус угла value

i++;
res = Math.Sinh(90);
Console.WriteLine(i + " Гиперболический синус 90 = " + res);
// Sinh(double value): возвращает гиперболический синус угла value

i++;
res = Math.Sqrt(25); 						// 5
Console.WriteLine(i + " Квадратный корень числа 25 = " + res);
// Sqrt(double value): возвращает квадратный корень числа value

i++;
res = Math.Tan(90);			
Console.WriteLine(i + " Тангенс угла 90 = " + res);
// Tan(double value): возвращает тангенс угла value

i++;
res = Math.Tanh(90);
Console.WriteLine(i + " Гиперболический тангенс угла 90 = " + res);
// Tanh(double value): возвращает гиперболический тангенс угла value

i++;
res = Math.Truncate(3.14);
Console.WriteLine(i + " Отбрасывание дробной части числа 3.14, целое значение: " + res);
//Truncate(double value): отбрасывает дробную часть числа value, возвращаяя лишь целое значние

//												Константы Math
// Также класс Math определяет две константы: Math.E и Math.PI. Например, вычислим площадь круга:

double radius = 15;
Console.WriteLine("Радиус круга = "+ radius);
double area = Math.PI * Math.Pow(radius, 2);
Console.WriteLine($"Площадь круга с радиусом {radius} равна {area}");





