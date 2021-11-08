// XPath представляет язык запросов в XML. Он позволяет выбирать элементы, соответствующие определенному селектору.

/*
.

	выбор текущего узла

..

	выбор родительского узла

*

	выбор всех дочерних узлов текущего узла

user

	выбор всех узлов с определенным именем, в данном случае с именем "user"

@name

	выбор атрибута текущего узла, после знака @ указывается название атрибута (в данном случае "name")

@+

	выбор всех атрибутов текущего узла

element[3]

	выбор определенного дочернего узла по индексу, в данном случае третьего узла

//user

	выбор в документе всех узлов с именем "user"

user[@name='Bill Gates']

	выбор элементов с определенным значением атрибута. В данном случае выбираются все элементы "user" с атрибутом name='Bill Gates'

user[company='Microsoft']

	выбор элементов с определенным значением вложенного элемента. В данном случае выбираются все элементы "user", у которых дочерний элемент "company" имеет значение 'Microsoft'

//user/company

	выбор в документе всех узлов с именем "company", которые находятся в элементах "user"
*/

/*
Действие запросов XPath основано на применении двух методов класса XmlElement:

- SelectSingleNode(): выбор единственного узла из выборки. Если выборка по запросу содержит несколько узлов, то выбирается первый

- SelectNodes(): выборка по запросу коллекции узлов в виде объекта XmlNodeList
*/

//Для запросов возьмем xml-документ из прошлых тем.
// Теперь выберем все узлы корневого элемента, то есть все элементы user:

XmlDocument xDoc = new XmlDocument();
xDoc.Load("users.xml");
XmlElement xRoot = xDoc.DocumentElement;

// выбор всех дочерних узлов
XmlNodeList childNodes = xRoot.SelectNodes("*");
foreach (XmlNode n in childNodes)
Console.WriteLine(n.OuterXml);

// Выберем все узлы <user>
XmlNodeList childNodes = xRoot.SelectNodes("user");

// Выведем на консоль значения атрибутов name у элементов user
foreach (XmlNodes n in childNodes)
	Console.WriteLine(n.SelectSingleNode("@name").Value);

// Выберем узел, у которого атрибут name имеет значение "Billy":
XmlNode childNode = xRoot.SelectSingleNode("user[@name = 'Billy']");
if (childNode != null)
	Console.WriteLine(childNode.OuterXml);

// Выберем узел, у которого вложенный элемент "company" имеет значение "Microsoft":
XmlNode childNode = xRoot.SelectSingleNode("user[company='Niko-niko-doga']");
if (childNode !=null)
	Console.WriteLine(childNode.OuterXml);

// Допустим, нам надо получить только компании. Для этого надо осуществить выборку вниз по иерархии элементов:
XmlNodeList childNodes = xRoot.selectNodes("//user/company");
foreach (XmlNode n in childNodes)
	Console.Writeline(n.InnerText);

// InnerText - текст внутри простого узла. Пример: <company>Dungeon</company>
// Value - значение аттрибута узла. Пример: <user name = "Billy">
                                  название узла | название аттрибута | значение аттрибута