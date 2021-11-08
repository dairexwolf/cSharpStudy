// Создадим свой пример. Создание магазина игрушек

class Toy
{
	public string Name { get; private set; }
	public string Price { get; private set; }
	public Toy (name, price)
	{
		this.Name = name;
		this.Price = price;
	}
}

class ToyStore
{
	List<Toy> toys = new List<Toys>();
	
	public IToyReader reader;
	public IToyBinder binder;
	public IToyValidator validator;
	public IToySaver saver;
	
	public ToyStore(IToyReader reader, IToyBinder binder, IToyValidator validator, IToySaver saver)
	{
		this.reader = reader;
		this.binder = binder;
		this.validator = validator;
		this.saver = saver;
	}
	
	public void Add()
	{
		string[] data = reader.GetInputData();
		Toy toy = binder.CreateToy(data);
		if (validator.IsValid(toy))
		{
			toys.Add(toy);
			saver.Save(toy, "toys.txt");
			Console.WriteLine("Успешно");
		}
		else
		{
			Console.WriteLine("Некорректные данные");
		}
	}
}

interface IToyReader
{
	string[] GetInputData();
}

class ConsoleToyReader : IToyReader
{
	public string[] GetInputData()
	{
		Console.WriteLine("Введите название игрушки:");
		strng name = Console.ReadLine();
		Console.WriteLine("Введите цену");
		string price = Console.ReadLine();
		return new string[] { name, price };
	}
}

interface IToyBinder
{
	Toy CreateToy(string[] data);
}

class GeneralToyBinder : IToyBinder
{
	public Toy CreateToy(string[] data)
	{
		if(data.Length>=2)
		{
			int price;
			if ( Int32.TryParse(data[1], out price))
				return new Toy (data[0], price);
			else 
				throw new Exception("Ошибка привязчика модели игрушки. Некорректные данные для аттрибута Price");
		}
		else
			throw new Exception("Ошибка привязчика модели игрушки. Недостаточно данных для создания игрушки");
	}
}

interface IToyValidator
{
	bool IsValid(Toy toy);
}

class GeneralToyValidator : IToyValidator
{
	public bool IsValid(Toy toy)
	{
		if (String.IsNullOrEmpty(toy.Name) || toy.Price <=0 )
			return false;
		
		return true;
	}
}

interface IToySaver
{
	void Save(Toy toy, string fileName)
	{
		using (System.IO.StreamWriter writer = new System.IO.StreamWriter(fileName, true))
		{
			writer.WriteLine(toy.Name);
			writer.WriteLine(toy.Price);
		}
	}
}
