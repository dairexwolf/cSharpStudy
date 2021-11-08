// Для работы с датами и временем в .NET предназначена структура DateTime. Она представляет дату и время от 00:00:00 1 января 0001 года до 23:59:59 31 декабря 9999 года.
// Для создания нового объекта DateTime также можно использовать конструктор. Пустой конструктор создает начальную дату:

DateTime date = new DateTime();
Console.WriteLine(date);		// 01.01.0001 0:00:00

// То есть мы получим минимально возможное значение, которое также можно получить следующим образом:
Console.WriteLine(DateTime.MinValue);

// Чтобы задать конкретную дату, нужно использовать один из конструкторов, принимающих параметры:
DateTime date = new DateTime(2021, 3, 11); 	// год - месяц - день
Console.WriteLine(date);					// 11.03.2021

//Добавим время
DateTime day = new DateTime(2021, 3, 11, 11,45,23);	// год - месяц - день - час - минута - секунда
Console.WriteLine(day);								// 11.03.21 11:45:23

//Если необходимо получить текущую время и дату, то можно использовать ряд свойств DateTime:

Console.WriteLine(DateTime.Now);		// время и дата на устройстве
Console.WriteLine(DateTime.UtcNow);		// время и дата по Гринвичу (UTC\GMT)
Console.WriteLine(DateTime.Today);		// сегодняшний день
//11.03.2021 05:49:13
//11.03.2021 04:49:13
//11.03.2021 00:00:00

/*										Краткая историческая и техническая справка
// При работе с датами надо учитывать, что по умолчанию для представления дат применяется григорианский календарь. Но что будет, если мы захотим получить день недели для 5 октября 1582 года:

DateTime someDate = new DateTime(1582, 10, 5);
Console.WriteLine(someDate.DayOfWeek);
// 
*/

// 											Операции с DateTime
/*
 Основные операции со структурой DateTime связаны со сложением или вычитанием дат. Например, надо к некоторой дате прибавить или, наоборот, отнять несколько дней.

 Для добавления дат используется ряд методов:

	Add(TimeSpan value): добавляет к дате значение TimeSpan

	AddDays(double value): добавляет к текущей дате несколько дней

	AddHours(double value): добавляет к текущей дате несколько часов

	AddMinutes(double value): добавляет к текущей дате несколько минут

	AddMonths(int value): добавляет к текущей дате несколько месяцев

	AddYears(int value): добавляет к текущей дате несколько лет
*/

// Например, добавим к некоторой дате 3 часа:

DateTime now = DateTime.Now;
Console.WriteLine(now.AddHours(3));

// Для вычитания дат используется метод Substract(DateTime date):
DateTime date1 = DateTime.Now;		//свойство
DateTime date2 = date1.AddHours(-10);
Console.WriteLine($"Дата 1: {date1},\nДата 2: {date2}");
Console.WriteLine("Вычитание из даты 1 даты 2: " + date1.Subtract(date2));	//Этот метод возвращает TimeSpan, так что там нет годов, месяцев

// Другие методы форматирования дат:

DateTime date = new DateTime(2021, 3, 11, 12, 18, 47);
Console.WriteLine(date.ToLocalTime()); 			// ToLocalTime() преобразует время UTC в локальное время, добавляя смещение относительно времени по Гринвичу.
Console.WriteLine(date.ToUniversalTime()); 		// ToUniversalTime(), наоборот, преобразует локальное время во время UTC, то есть вычитает смещение относительно времени по Гринвичу.
Console.WriteLine(date.ToLongDateString());		// Расширенно оказывает дату, словами
Console.WriteLine(date.ToShortDateString());	// Короткая запись даты
Console.WriteLine(date.ToLongTimeString());		// Запись времени с секундами
Console.WriteLine(date.ToShortTimeString());	// Запись времени без секунд