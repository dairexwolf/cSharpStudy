/*
Для редактирования xml-документа (изменения, добавления, удаления элементов) мы можем воспользоваться методами класса XmlNode:

- AppendChild: добавляет в конец текущего узла новый дочерний узел

- InsertAfter: добавляет новый узел после определенного узла

- InsertBefore: добавляет новый узел до определенного узла

- RemoveAll: удаляет все дочерние узлы текущего узла

- RemoveChild: удаляет у текущего узла один дочерний узел и возвращает его
*/

/*
Класс XmlDocument добавляет еще ряд методов, которые позволяют создавать новые узлы:

- CreateNode: создает узел любого типа

- CreateElement: создает узел типа XmlDocument

- CreateAttribute: создает узел типа XmlAttribute

- CreateTextNode: создает узел типа XmlTextNode

- CreateComment: создает комментарий
*/

using System;
using System.Xml;

class Program
{
    static void Main()
    {
		XmlDocument xDoc = new XmlDocument();
        xDoc.Load("users.xml");
        XmlElement xRoot = xDoc.DocumentElement;
        // создаем новый элемент users
        XmlElement userElem = xDoc.CreateElement("user");
        // создаем аттрибут name
        XmlAttribute nameAttr = xDoc.CreateAttribute("name");
        // создаем элементы company и age
        XmlElement companyElem = xDoc.CreateElement("company");
        XmlElement ageElem = xDoc.CreateElement("age");
        // создаем текстовые значения для элементов и аттрибута
        XmlText nameText = xDoc.CreateTextNode("RipSkinner");
        XmlText companyText = xDoc.CreateTextNode("Gachibass");
        XmlText ageText = xDoc.CreateTextNode("30");

        // добавляем узлы
        nameAttr.AppendChild(nameText);
        companyElem.AppendChild(companyText);
        ageElem.AppendChild(ageText);
        userElem.Attributes.Append(nameAttr);
        userElem.AppendChild(companyElem);
        userElem.AppendChild(ageElem);
        xRoot.AppendChild(userElem);
        xDoc.Save("users.xml");
	}
}

// Добавление элементов происходит по одной схеме. Сначала создаем элемент (xDoc.CreateElement("user")). Если элемент сложный, то есть содержит в себе другие элементы, то создаем эти элементы. Если элемент простой, содержащий внутри себя некоторое текстовое значение, то создаем этот текст (XmlText companyText = xDoc.CreateTextNode("Text");).
// Затем все элементы добавляются в основной элемент user, а тот добавляется в корневой элемент (xRoot.AppendChild(userElem);).
// Чтобы сохранить измененный документ на диск, используем метод Save: xDoc.Save("users.xml")

// Удаление узлов

// Удалим первый узел xml-документа:

XmlDocument xDoc = new XmlDocument();
xDoc.Load("users.xml");
XmlElement xRoot = xDoc.DocumentElement;

XmlNode firstNode = xRoot.FirstChild;
xRoot.RemoveChild(firstNode);
xDoc.Save("users");

// Удалим последний узел
 xDoc = new XmlDocument();
        xDoc.Load("users.xml");
        xRoot = xDoc.DocumentElement;

        XmlNode lastNode = xRoot.LastChild;
        xRoot.RemoveChild(lastNode);
        xDoc.Save("users.xml");
