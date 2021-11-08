// Несмотря на то, что .NET предоставляет нам большой набор встроенных атрибутов валидации, их может не хватать, нам могут потребоваться более изощренные в плане логики атрибуты. И в этом случае мы можем определить свои классы атрибутов.

// При создании атрибута надо понимать, к чему именно он будет применяться - к свойству модели или ко всей модели в целом. Поэтому создадим два атрибута.

// Для создания атрибута нам надо унаследовать свой класс от класса ValidationAttribute и реализовать его метод IsValid():

public class UserNameAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                string userName = value.ToString();
                if (!userName.StartsWith("T"))
                    return true;
                else
                    this.ErrorMessage = "Имя не должно начинаться с буквы \"T\"";
            }
            return false;
        }
    }

// Данный атрибут будет применяться к строковому свойству, поэтому в метод IsValid(object value) в качестве value будет передаваться строка. Поэтому в ходе программы мы можем преобразовать значение value к строке: value.ToString() или так string userName = value as string.

// Подобным образом определим еще один атрибут, который будет применяться ко всей модели:

public class UserValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            User user = value as User;
            if (user.Name == "Alice" && user.Age == 33)
            {
                this.ErrorMessage = "Имя не должно быть Alice и возраст одновременно не может быть равен 33";
                return false;
            }
            return true;
        }
    }

// Поскольку атрибут будет применяться ко всей модели, то в метод IsValid в качестве параметра value будет передаваться объект User. Как правило, атрибуты, которые применяются ко всей модели, валидируют сразу комбинацию свойств класса. В данном случае смотрим, чтобы имя и возраст одновременно не были равны "Alice" и 33.

// Теперь применим эти атрибуты:

[UserValidation]
    public class User
    {
        [Required(ErrorMessage = "Требуется поле Id")]
	public string Id { get; set; }
        [Required]
        [UserName]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }

    }

// Также, как и другие атрибуты, мы можем использовать эти атрибуты без суффикса Attribute. И теперь применим класс User в программе:

class Program
    {
        static void Main()
        {
            User user1 = new User { Id = "", Name = "Tom", Age = 512 };
            Validate(user1);
            Console.WriteLine();
            User user2 = new User { Id = "dio", Name = "Alice", Age = 33 };
            Validate(user2);
            Console.Read();
        }

        private static void Validate(User user)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(user);

            if (!Validator.TryValidateObject(user, context, results, true))
            {
                foreach (var error in results)
                    Console.WriteLine(error.ErrorMessage);
            }
            else Console.WriteLine("Пользователь прошел проверку");
        }
    }
	
	