// Возьмем xml-файл, созданный в прошлой теме. Переберем его элементы и выведем их значения на консоль:
using System;
using System.Xml;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        XDocument xdoc = XDocument.Load("phones.xml");
        foreach (XElement phoneElement in xdoc.Element("phones").Elements("phone"))

        {
            XAttribute nameAttribute = phoneElement.Attribute("name");
            XElement companyElement = phoneElement.Element("company");
            XElement priceElement = phoneElement.Element("price");

            if (nameAttribute != null && companyElement != null && priceElement != null)
            {
                Console.WriteLine($"Смартфон: {nameAttribute.Value}");
                Console.WriteLine($"Компания: {companyElement.Value}");
                Console.WriteLine($"Цена: {priceElement.Value}");
            }
            Console.WriteLine("-----------------------------------------------------------------------------");
        }
        Console.Read();
    }
}

// Чтобы начать работу с имеющимся xml-файлом, надо сначала загрузить его с помощью статического метода XDocument.Load(), в который передается путь к файлу.

// Поскольку xml хранит иерархически выстроенные элементы, то и для доступа к элементам надо идти начиная с высшего уровня в этой иерархии и далее вниз. Так, для получения элементов phone и доступа к ним надо сначала обратиться к корневому элементу, а через него уже к элементам phone: xdoc.Element("phones").Elements("phone")

// Метод Element("имя_элемента") возвращает первый найденный элемент с таким именем. Метод Elements("имя_элемента") возвращает коллекцию одноименных элементов. В данном случае мы получаем коллекцию элементов phone и поэтому можем перебрать ее в цикле.

// Спускаясь дальше по иерархии вниз, мы можем получить атрибуты или вложенные элементы, например, XElement companyElement = phoneElement.Element("company")

// Значение простых элементов, которые содержат один текст, можно получить с помощью свойства Value: string company = phoneElement.Element("company").Value

// Сочетая операторы Linq и LINQ to XML можно довольно просто извлечь из документа данные и затем обработать их. Например, имеется следующий класс и создадим на основании данных в xml объекты этого класса:

class Phone
{
    public string Name { get; set; }
    public int Price { get; set; }
}

class Program
{
    static void Main()
    {
        XDocument xdoc = XDocument.Load("phones.xml");
        var items = from xe in xdoc.Element("phones").Elements("phone")
                    where xe.Element("company").Value == "Samsung"
                    select new Phone
                    {
                        Name = xe.Attribute("name").Value,
                        Price = Int32.Parse(xe.Element("price").Value)

                    };
        foreach (var item in items)
            Console.WriteLine($"{item.Name} - {item.Price}");
    }
}

// выборка всех телефонов:

items = from xe in xdoc.Element("phones").Elements("phone")
select new Phone
{
	Name = xe.Attribute("name").Value,
	Price = Int32.Parse(xe.Element("price").Value)
};
foreach (var item in items)
    Console.WriteLine($"{item.Name} - {item.Price}");






