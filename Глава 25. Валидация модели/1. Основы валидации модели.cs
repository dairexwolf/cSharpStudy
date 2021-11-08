// Большую роль в приложении играет валидация модели или проверка вводимых данных на корректность. Например, у нас есть класс пользователя, в котором определено свойство для хранения возраста. И нам было бы нежелательно, чтобы пользователь вводил какое-либо отрицательное число или заведомо невозможный возвраст, например, миллион лет.

// Например, пусть у нас есть проект консольного приложения, в котором есть клас User

using System;

public class User
{
    public string ID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}

class Program
{
    // В главном классе программы мы можем проверять вводимые данные с помощью условных конструкций
    static void Main()
    {
        User user = new User();
        Console.WriteLine("Введите имя:");
        string name = Console.ReadLine();
        Console.WriteLine("Введите возраст");
        try
        {
            int age = Int32.Parse(Console.ReadLine());
            if (!String.IsNullOrEmpty(name) && name.Length > 1)
                user.Name = name;
            else Console.WriteLine("Тебе не нравится? Пошел нахер отсюда");

            if (age >= 1 && age <= 150)
                user.Age = age;
            else Console.WriteLine("Да ты заебал");
        }
        catch
        {
            Console.WriteLine("Надо было вводить цифры, сука");
        }
        Console.Read();
    }
}

// Здесь предполагается, что имя должно иметь больше 1 символа, а возраст должен быть в диапазоне от 1 до 100. Однако в классе может быть гораздо больше свойств, для которых надо осуществлять проверки. А это привет к тому, что увеличится значительно код программы за счет проверок. К тому же задача валидации данных довольно часто встречается в приложениях. Поэтому фреймворк .NET предлагает гораздо более удобный функционал в виде атрибутов из пространства имен System.ComponentModel.DataAnnotations.

// Изменим касс User следующим образом

using System;
using System.ComponentModel.DataAnnotations;

namespace StudyApp
{
	public class User
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [Range(1, 150)]
        public int Age { get; set; }
    }

}

// Все правила валидации модели в System.ComponentModel.DataAnnotations определяются в виде атрибутов. В данном случае используются три атрибута: классы RequiredAttribute, StringLengthAttribute и RangeAttribute. В коде необязательно использовать суффикс Attribute, поэтому он, как правило, отбрасывается. Атрибут Required требует обзательного наличия значения. Атрибут StringLength устанавливает максимальную и минимальную длину строки, а атрибут Range устанавливает диапазон приемлимых значений.

// Теперь изменим код главного класса программы:

using System;
using System.Collections.Generic;
using System.ComponentNodel.DataAnnotations;

namespace StudyApp
{
	class Program
	{
		static void Main()
        {
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();

            Console.WriteLine("Введите возраст:");
            int age = Int32.Parse(Console.ReadLine());

            User user = new User { Name = name, Age = age };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);
            if (!Validator.TryValidateObject(user, context, results, true))
            {
                foreach (var error in results)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            Console.Read();
        }
	}
}

// Здесь используются классы ValidationResult, Validator и ValidationContext, которые предоставляются пространством имен System.ComponentModel.DataAnnotations и которые управляют валидацией.

// Вначале мы создаем контекст валидации - объект ValidationContext. В качестве первого параметра в конструктор этого класса передается валидируемый объект, то есть в данном случае объект User.

// Собственно валидацию производит класс Validator и его метод TryValidateObject(). В этот метод передается валидируемый объект (в данном случае объект user), контекст валидации, список объектов ValidationResult и булевый параметр, указывающий, надо ли валидировать все свойства.

// Если метод Validator.TryValidateObject() возвращает false, значит объект не проходит валидацию. Если модель не проходит валидацию, то список объектов ValidationResult оказывается заполенным. А каждый объект ValidationResult содержит информацию о возникшей ошибке. Класс ValidationResult имеет два ключевых свойства: MemberNames - список свойств, для которых возникла ошибка, и ErrorMessage - собственно сообщение об ошибке.

// Если мы введем недопустимые значения, поскольку объект не имеет Id, но для данного свойства установлен атрибут Required, то соответственно мы получаем ошибку "The Id field is required". И также здесь отображаются ошибки для свойств Name и Age.

// Таким образом, вместо кучи условных конструкций для проверки значений свойств модели мы можем использовать один метод Validator.TryValidateObject(), а все правила валидации определить в виде атрибутов.

// Однако, как видно из консольного вывода, мы можем столкнуться с проблемой локализации сообщений. В .NET Core по умолчанию используются встроенные сообщения, которые могут быть не совсем уместными, и далее мы рассмотрим, как настроить эти сообщения.
