// Еще один подход к работе с Xml представляет технология LINQ to XML. Вся функциональность LINQ to XML содержится в пространстве имен System.Xml.Linq. Рассмотрим основные классы этого пространства имен:
/*
- XAttribute: представляет атрибут xml-элемента

- XComment: представляет комментарий

- XDocument: представляет весь xml-документ

- XElement: представляет отдельный xml-элемент
*/

//Ключевым классом является XElement, который позволяет получать вложенные элементы и управлять ими. Среди его методов можно отметить следующие:
/*
- Add(): добавляет новый атрибут или элемент

- Attributes(): возвращает коллекцию атрибутов для данного элемента

- Elements(): возвращает все дочерние элементы данного элемента

- Remove(): удаляет данный элемент из родительского объекта

- RemoveAll(): удаляет все дочерние элементы и атрибуты у данного элемента
*/

// Итак, используем функциональность LINQ to XML и создадим новый XML-документ:

using System;
using System.Xml;
using System.Xml.Linq;

class Program
{
    static void Main()
    {
        XDocument xDoc = new XDocument();
        // создадим первый элемент
        XElement iphone6 = new XElement("phone");
        // создадим атрибут
        XAttribute iphoneNameAttr = new XAttribute("name", "iPhone");   // первое - название элемента, второе - значение
        XElement iphoneCompanyElem = new XElement("company", "Apple");
        XElement iphonePriceElem = new XElement("price", "40000");
        //добавляем атрибут и элементы в первый элемент
        iphone6.Add(iphoneNameAttr);
        iphone6.Add(iphoneCompanyElem);
        iphone6.Add(iphonePriceElem);

        // создадим второй элемент
        XElement galaxys5 = new XElement("phone");
        XAttribute galaxysNameAttr = new XAttribute("name", "Samsung Galaxy S5");
        XElement galaxysCompanyElem = new XElement("company", "Samsung");
        XElement galaxysPriceElem = new XElement("price", "33000");
        galaxys5.Add(galaxysNameAttr);
        galaxys5.Add(galaxysCompanyElem);
        galaxys5.Add(galaxysPriceElem);
        // создаем корневой элемент
        XElement phones = new XElement("phones");
        // добавляем в корневой элемент
        phones.Add(iphone6);
        phones.Add(galaxys5);
        // добавляем корневой элемент в документ
        xDoc.Add(phones);
        // сохраняем документ
        xDoc.Save("phones.xml");
    }
}

// Чтобы создать документ, нам нужно создать объект класса XDocument. Это объект самого верхнего уровня в хml-документе.
// Элементы создаются с помощью конструктора класса XElement. Конструктор имеет ряд перегруженных версий. Первый параметр конструктора передает название элемента, например, phone. Второй параметр передает значение этого элемента.

// Создание атрибута аналогично созданию элемента. Затем все атрибуты и элементы добавляются в элементы phone с помощью метода Add().

// Так как документ xml должен иметь один корневой элемент, то затем все элементы phone добавляются в один контейнер - элемент phones.

// В конце корневой элемент добавляется в объект XDocument, и этот объект сохраняется на диске в xml-файл с помощью метода Save().

// Но можно интересней и быстрее
// Конструктор класса XElement позволяют задать набор объектов, которые будут входить в элемент. И предыдущий пример мы могли бы сократить следующим способом:

XDocument xDoc = new XDocument( 
	new XElement ("phones",
		new XElement("phone",
			new XAttribute("name", "iPhone 6"),
			new XElement("company", "Apple"),
			new XElement("price", "40000")),
		new XElement("phone",
			new XAttribute("name", "Samsung Galaxy S5"),
			new XElement("company", "Samsung"),
			new XElement("price","33000")
					)
				)
	);
xDoc.Save("phones2.xml");

