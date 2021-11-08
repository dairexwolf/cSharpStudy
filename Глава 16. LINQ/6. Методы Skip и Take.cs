// Метод Skip() пропускает определенное количество элементов, а метод Take() извлекает определенное число элементов. Нередко данные методы применяются вместе для создания постраничного вывода.

// Извлечем три первых элемента:

int[] nums = { -3, -2, -1, 0, 1, 2, 3 };
var result = nums.Take(3);

foreach (int i in result)
	Console.WriteLine(i);

// Выберем все элементы, кроме первых трех:

var result = nums.Skip(3);

// Совмещая оба метода, мы можем выбрать определенное количество элементов начиная с определенного элемента. Например, выберем три элемента, начиная с пятого (то есть пропустив четыре элемента):

int[] nums = { -3, -2, -1, 0, 1, 2, 3 };
var result = numbers.Skip(3).Take(3);
foreach (int i in result)
    Console.WriteLine(i);

// Похожим образом работают методы TakeWhile и SkipWhile.
// Метод TakeWhile выбирает цепочку элементов, начиная с первого элемента, пока они удовлетворяют определенному условию. Например:

string[] teams = { "Бавария", "Боруссия", "Рил Нигга", "Починки из май сити", "ПМЖ", "Бурия" };
foreach (var t in teams.TakeWhile(x => x.StartsWith("Б")))
	Console.WriteLine(t);

// Согласно условию мы выбираем те команды, которые начинаются с буквы Б. В массиве есть три таких команды. Однако в цикле будут выведены только две первых:
// Потому что цепочка обрывается на третьей команде - "Реал Мадрид" - она не соответствует условию, и после этого выборка уже не идет.
// Если бы первой командой в массиве стояла бы команда, начинающаяся не с буквы Б, например, "Реал Мадрид", то в этом случае метод возвратил бы нам 0 элементов.
// В подобном русле действует метод SkipWhile. Он пропускает цепочку элементов, начиная с первого элемента, пока они удовлетворяют определенному условию. Например:

string[] teams = { "Бавария", "Боруссия", "Рил Нигга", "Починки из май сити", "ПМЖ", "Бурия" };
foreach (var t in teams.SkipWhile(x => x.StartsWith("Б")));
	Console.WriteLine(t);

// Первые две команды, которые начинаются с буквы Б и соответствуют условию, будут пропущены. На третьей команде цепочка обрывается, поэтому последняя команда, начинающаяся с буквы Б, будет включена в выходной список
// И опять же если в массиве первый элемент не начинался бы с буквы Б, то цепочка пропускаемых элементов прервалась бы уже на первом элементе, и поэтому метод SkipWhile возвратил бы все элементы массива.
