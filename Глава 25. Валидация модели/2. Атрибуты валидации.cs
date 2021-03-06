// Для валидации модели мы можем использовать большой набор встроенных атрибутов. Каждый атрибут может иметь свои собственные свойства, которые позволяют конкретизировать правило валидации. Но также атрибуты имееют и ряд общих свойств. Наиболее используемым из которых является свойство ErrorMessage. При выводе ошибок валидации .NET использует встроенные локализованные сообщение. А данное свойство как раз и позволяет переопределить сообщение об ошибке:

using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp
{
	public class User
	{
		[Required(ErrorMessage = "Идентификатор пользователя не установлен")]
		public string Id { get; set; }
		[Required(ErrorMessage = "Не указано имя пользователя")]
		[StringLength(50, MinimumLength=3, ErrorMessage = "недопустимая длинна имени")]
		public string Name { get; set; }
		[Required]
		[Range(1,150,ErrorMessage = "Недопустимый возраст")]
		public int Age { get; set; }
	}
}

// И если id не будет указано, то отобразится определенное в атрибуте сообщение об ошибке.

// Имеется довольно большое количество атрибутов. Основные из них:

// 1. Required: данный атрибут указывает, что свойство должно быть обязательно установлено, обязательно должно иметь какое-либо значение.
// Из его свойств следует отметить свойство AllowEmptyStrings. Если оно имеет значение true, то для строковых свойств разрешено использовать пустые строки.
// Например, пусть у нас есть следующее создание объекта User:
User user = new User {Id = "", Name = "Egor", Age = 20 };

// Со следующим атрибутом никакой ошибки не будет возникать:
[Required(ErrorMessage = "Идентифиактор пользователя не установлен", AllowEmptyStrings = true) ]
public string Id { get; set; }

// Если же поменять true на false, тогда возникнет ошибка.

// 2. RegularExpression: указывает на регулярное выражение, которому должно соответствовать значение свойства. Например, пусть у пользователя определено свойство Phone, хранящее номер телефона:

[Requried]
[RegularExpression(@"^\+[1-9]\d{3}-\d{4}$)", ErrorMessage = "Номер телефона должен иметь формат: +xxxx-xxx-xxxx")]
public string Phone { get; set; }

// В этом случае пользователь должен будет обязательно ввести номер наподобие +1111-111-2345.

// 3. StringLength: определяет допустимую длину для строковых свойств. В качестве первого параметра он принимает максимально допустимую длину строки. С помощью дополнительного свойства MinimumLength можно установить минимально допустимую длину строки

// 4. Range: задает диапазон допустимых числовых значений. В качестве первых двух параметров он принимает минимальное и максимальное значения

// 5. Compare: позволяет сравнить значение текущего свойства со значением другого свойства, которое передается в этот атрибут.

// Например, нередко при регистрации пользователя ему предоставляется два поля для ввода пароля, чтобы не ошибиться. В этом случае мы могли бы определить ля регистрации следующую модель:

public class RegisterModel
{
	[Required]
	public  string Login { get; set; }
	[Required]
	public string Password { get; set; }
	[Required]
	[Compare("Password")]
	public string ConfirmPassword { get; set; }	
}

// И если значения свойств Password и ConfirmPassword не будут совпадать, тогда мы получим ошибку валидации.

// 6. Phone: данный атрибут автоматически валидирует значение свойства, является ли оно телефонным номером. Фактически это встроенная альтренатива использованию регулярного выражения, как было показано выше

// 7. EmailAddress: определяет, является ли значение свойства электронным адресом

// 8. CreditCard: определяет, является ли значение свойства номером кредитной карты

// 9. Url: определяет, является ли значение свойства гиперссылкой


