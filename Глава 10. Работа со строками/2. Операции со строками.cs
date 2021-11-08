// Основная функциональность класса String раскрывается через его методы, среди которых можно выделить следующие:
/*
	Compare: сравнивает две строки с учетом текущей культуры (локали) пользователя
	CompareOrdinal: сравнивает две строки без учета локали
	Contains: определяет, содержится ли подстрока в строке
	Concat: соединяет строки
	CopyTo: копирует часть строки, начиная с определенного индекса в массив
	EndsWith: определяет, совпадает ли конец строки с подстрокой
	Format: форматирует строку
	IndexOf: находит индекс первого вхождения символа или подстроки в строке
	Insert: вставляет в строку подстроку
	Join: соединяет элементы массива строк
	LastIndexOf: находит индекс последнего вхождения символа или подстроки в строке
	Replace: замещает в строке символ или подстроку другим символом или подстрокой
	Split: разделяет одну строку на массив строк
	Substring: извлекает из строки подстроку, начиная с указанной позиции
	ToLower: переводит все символы строки в нижний регистр
	ToUpper: переводит все символы строки в верхний регистр
	Trim: удаляет начальные и конечные пробелы из строки
*/

// 													Конкатенация
// Конкатенация строк или объединение может производиться как с помощью операции +, так и с помощью метода Concat:
string s1 = "hello";
string s2 = "world";
string s3 = s1 + " " + s2;				//результат: hello world
string s4 = String.Concat(s3, "!!!");	//результат: hello world!!!
// Метод Concat является статическим методом класса String, принимающим в качестве параметров две строки. Также имеются другие версии метода, принимающие другое количество параметров.
Console.WriteLine(s3+"\n"+s4);

// Для объединения строк также может использоваться метод Join:
string s5 = "apple";
string s6 = "a day";
string s7 = "keeps";
string s8 = "a doctor";
string s9 = "away";
string[] values = new string[] { s5, s6, s7, s8, s9 };
String s10 = String.Join(" ", values); 	// результат: "apple a day keeps a doctor away"
Console.WriteLine(s10);
// Метод Join также является статическим. Использованная выше версия метода получает два параметра: строку-разделитель (в данном случае пробел) и массив строк, которые будут соединяться и разделяться разделителем.

//													Сравнение
// Для сравнения строк применяется статический метод Compare
// Данная версия метода Compare принимает две строки и возвращает число. Если первая строка по алфавиту стоит выше второй, то возвращается число меньше нуля. В противном случае возвращается число больше нуля. И третий случай - если строки равны, то возвращается число 0.

string s1 = "hello";
string s2 = "fellos";

int result = String.Compare(s1, s2);
if (result < 0)
	Console.WriteLine("Строка s1 перед строкой s2");
else if( result > 0)
	Console.WriteLine("Строка s1 стоит после строки s2");
else 
	Console.WriteLine("Строки s1 и s2 идентичны");
// результат: "Строка s1 стоит после строки s2"

// В данном случае так как символ h по алфавиту стоит ниже символа f, то и вторая строка будет стоять выше.

//													Поиск в строке
// С помощью метода IndexOf мы можем определить индекс первого вхождения отдельного символа или подстроки в строке:
string s1 = "hello policeman";
char ch = 'o';
int indexOfChar = s1.IndexOf(ch);	//равно 4
Console.WriteLine(indexOfChar);
string subString = "pol";
int indexOfSubstring = s1.IndexOf(subString); // равно 6
Console.WriteLine(indexOfSubstring);
string subString2 = "man";
int lastIndexOfSubstring = s1.LastIndexOf(subString2);
Console.WriteLine(lastIndexOfSubstring);	//равно 12

// Подобным образом действует метод LastIndexOf, только находит индекс последнего вхождения символа или подстроки в строку.

// Еще одна группа методов позволяет узнать начинается или заканчивается ли строка на определенную подстроку. Для этого предназначены методы StartsWith и EndsWith. Например, у нас есть задача удалить из папки все файлы с расширением exe:

string path = @"C:\Users\vyatkin\Documents\c#\Глава 10. Работа со строками\NeedDir";
string[] files = Directory.GetFiles(path);
int ifiles = files.Length;
for (int i = 0;i < ifiles; i++)
{
	if (files[i].EndsWith(".exe"))
		File.Delete(files[i]);
}

//													Разделение строки
// С помощью функции Split мы можем разделить строку на массив подстрок. В качестве параметра функция Split принимает массив символов или строк, которые и будут служить разделителями. Например, подсчитаем количество слов в сроке, разделив ее по пробельным символам:
string text = "И почему же все так произошло?";
string[] words = text.Split(new char[] { ' ' });
foreach (string s in words)
	Console.WriteLine(s);
// Это не лучший способ разделения по пробелам, так как во входной строке у нас могло бы быть несколько подряд идущих пробелов и в итоговый массив также бы попадали пробелы, поэтому лучше использовать другую версию метода:
string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
// Второй параметр StringSplitOptions.RemoveEmptyEntries говорит, что надо удалить все пустые подстроки.

//												 	Вставка одной строки в другую
//Для вставки одной строки в другую применяется функция Insert:
string txt = "Хороший день";
string txt2 = ", Алег";
txt = txt.Insert(12, txt2);
Console.WriteLine(txt);
//Первым параметром в функции Insert является индекс, по которому надо вставлять подстроку, а второй параметр - собственно подстрока.

//													Удаление строк
//Удалить часть строки помогает метод Remove:

string txt = "Здарова отец";
// индекс последнего символа
int ind = txt.Length-1;
// вырезаем последний символ
txt = txt.Remove(ind);
Console.WriteLine(txt);
// вырезаем первые 2 символа
txt = txt.Remove(0,2);

//													Замена
//Чтобы заменить один символ или подстроку на другую, применяется метод Replace:

string txt = "Good day, sir";
txt = txt.Replace("Good", "bad");
Console.WriteLine(txt);
// результат: bad day, sir
txt = txt.Replace("a","");
Console.WriteLine(txt);
// результат: bd dy, sir
//Он заменяет все символы

//													Смена регистра
//Для приведения строки к верхнему и нижнему регистру используются соответственно функции ToUpper() и ToLower()

string txt = "Deus wult!";

Console.WriteLine(txt.ToLower()); // deus wult!
Console.WriteLine(txt.ToUpper()); // DEUS WULT!