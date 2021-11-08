// Нам необязательно определять правила валидации модели в виде атрибутов. Мы можем применить к классу интерфейс IValidatableObject и реализовать его метод Validate(). В этом случае класс будет сам себя валидировать.

// Итак, применим этот интерфейс к классу User:

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudyApp
{
	public class User : IValidateObject
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			List<ValidationResult> errors = new List<ValidationResult>();
			
			if (string.IsNullOrWhiteSpace(this.Name))
				errors.Add(new ValidationResult("Не указано имя"));
			
			if (string.IsNullOrWhiteSpace(this.Id))
				errors.Add(new ValidationResult("Не указан идентификатор пользователя");
			
			if (this.Age < 1 || this.Age > 150)
				errors.Add(new ValidationResult("Недопустимый возраст"));
			
			return errors;
		}
	}
}

// Здесь роль атрибутов фактически выполняет логика из метода Validate(). И в основной части программы мы также можем применять валидацию к объекту User:

User user = new User { Id = "", Name = "", Age = -22};
var result = new List<ValidationResult>();
var context = new ValidationContext(user);
if (!Validator.tryValidateObject(user, context, results, true))
{
	foreach (var error in result)
	{
		Console.WriteLine(error.ErrorMessage);
	}
}
else Console.WriteLine("Пользователь прошел валидацию");
