// Для работы с XML в C# можно использовать несколько подходов. В первых версиях фреймворка основной функционал работы с XML предоставляло пространство имен System.Xml. 

/*
В нем определен ряд классов, которые позволяют манипулировать xml-документом:

- XmlNode: представляет узел xml. В качестве узла может использоваться весь документ, так и отдельный элемент

- XmlDocument: представляет весь xml-документ

- XmlElement: представляет отдельный элемент. Наследуется от класса XmlNode

- XmlAttribute: представляет атрибут элемента

- XmlText: представляет значение элемента в виде текста, то есть тот текст, который находится в элементе между его открывающим и закрывающим тегами

- XmlComment: представляет комментарий в xml

- XmlNodeList: используется для работы со списком узлов
*/

/*
Ключевым классом, который позволяет манипулировать содержимым xml, является XmlNode, поэтому рассмотрим некоторые его основные методы и свойства:

- Свойство Attributes возвращает объект XmlAttributeCollection, который представляет коллекцию атрибутов

- Свойство ChildNodes возвращает коллекцию дочерних узлов для данного узла

- Свойство HasChildNodes возвращает true, если текущий узел имеет дочерние узлы

- Свойство FirstChild возвращает первый дочерний узел

- Свойство LastChild возвращает последний дочерний узел

- Свойство InnerText возвращает текстовое значение узла

- Свойство InnerXml возвращает всю внутреннюю разметку xml узла

- Свойство Name возвращает название узла. Например, <user> - значение свойства Name равно "user"

- Свойство ParentNode возвращает родительский узел у текущего узла
*/

// Применим эти классы и их функционал. И вначале для работы с xml создадим новый файл. Назовем его users.xml
// Теперь пройдемся по этому документу и выведем его данные на консоль:


using System;
using System.Xml;

class Program
{
    static void Main()
    {

        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("users.xml");
        // получим корневой элемент
        XmlNode xRoot = xDoc.DocumentElement;
        // обход всех узлов в корневом элементе
        foreach (XmlNode xnode in xRoot)
        {
            // получаем аттрибут name
            if (xnode.Attributes.Count > 0)
            {
                XmlNode attr = xnode.Attributes.GetNamedItem("name");
                if (attr != null)
                    Console.WriteLine(attr.Value);
            }

            // обходим все дочерние узлы элемента user
            foreach (XmlNode childnode in xnode.ChildNodes)
            {
                // если узел - company
                if (childnode.Name == "company") ;
                Console.WriteLine($"Компания: {childnode.InnerText}");

                // если узел - age
                if (childnode.Name == "age")
                    Console.WriteLine($"Возраст: {childnode.InnerText}");
            }

            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine();
        }

        Console.Read();
    }
}

// Чтобы начать работу с документом xml, нам надо создать объект XmlDocument и затем загрузить в него xml-файл: xDoc.Load("users.xml");
// При разборе xml для начала мы получаем корневой элемент документа с помощью свойства xDoc.DocumentElement. Далее уже происходит собственно разбор узлов документа.
// В цикле foreach(XmlNode xnode in xRoot) пробегаемся по всем дочерним узлам корневого элемента. Так как дочерние узлы представляют элементы <user>, то мы можем получить их атрибуты: XmlNode attr = xnode.Attributes.GetNamedItem("name"); и вложенные элементы: foreach(XmlNode childnode in xnode.ChildNodes)
// Чтобы определить, что за узел перед нами, мы можем сравнить его название: if(childnode.Name=="company")

// Подобным образом мы можем создать объекты User по данным из xml:

using System;
using System.Xml;
using System.Collections.Generic;

class User
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Company { get; set; }
}

class Program
{
    static void Main()
    {
        List<User> users = new List<User>();

        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("users.xml");
        XmlElement xRoot = xDoc.DocumentElement;
        foreach (XmlElement xnode in xRoot)
        {
            User user = new User();
            XmlNode attr = xnode.Attributes.GetNamedItem("name");
            if (attr != null)
                user.Name = attr.Value;

            foreach (XmlNode childnode in xnode.ChildNodes)
            {
                if (childnode.Name == "company")
                    user.Company = childnode.InnerText;
                if (childnode.Name == "age")
                    user.Age = Int32.Parse(childnode.InnerText);
            }
            users.Add(user);

        }

        foreach (User u in users)
            Console.WriteLine($"{u.Name}\n{u.Company}\n{u.Age}\n--------------------------\n");
        Console.Read();
    }
}

// Комментарии:
// Перебор XML документа следует делать рекурсивной процедурой. Перебирать элементы - циклом по Childs, а не поиском по конкретному имени. Так универсально.
// Автор, добавьте, пожалуйста, уточнение в статью: "XmlNode является базовым классом, от которого наследуются все остальные классы для работы с XML", а то роль XmlNode иначе непонятна. Именно базовым, именно наследуются. Так программистам сразу же будет понятно. Спасибо.
