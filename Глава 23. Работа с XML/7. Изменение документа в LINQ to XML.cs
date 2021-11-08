// Возьмем xml-файл из прошлой темы и отредактируем его содержимое

using System;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace
{
	class Program
	{
		static void Main()
		{
			XDocument xdoc = XDocument.Load("phones.xml");
			XElement root = xdoc.Element("phones");
			
			foreach (XElement xe in root.Elements("phone").ToList())
			{
				// изменяем название и цену
				if (xe.Attribute("name").Value== "Samsung Galaxy S5")
				{
					xe.Attribute("name").Value = "Samsung Galaxy Note 4";
					xe.Element("price").Value = "31000";
				}
				// если iphone - удаляем его
				else if (xe.Attribute("name").Value == "IPhone 6")
					xe.Remove();
				
			}
			// добавляем новый элемент
			root.Add(new XElement("phone",
						new XAttribute("name", "Nokia Lumia 930"),
						new XElement("company", "Nokia"),
						new XElement ("price", "19500")));
			xdoc.Save("phones1.xml");
			// выводим xml-док на консоль
			Console.WriteLine(xdoc);
			
			Console.Read();
		}
	}
}

// Для изменения содержимого простых элементов и атрибутов достаточно изменить их свойство Value: xe.Element("price").Value = "31000"

// Если же нам надо редактировать сложный элемент, то мы можем использовать комбинацию методов Add/Remove для добавления и удаления вложенных элементов.

// В результате сформируется и сохранится на диск новый документ








