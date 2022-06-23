using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace GeneratorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PropertyGenerator newProps = new PropertyGenerator(10000);
            SaveXML(newProps.PropertyList);
            Console.ReadLine();
        }

        private static void SaveXML(List<Property> props)
        {
            // передаем в конструктор тип класса Person
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Property>));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("data.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, props);
            }
        }
    }

    [Serializable]
    public class Property
    {
        // требуемые переменные
        private string id;
        private string name;
        private string company;
        private int price;
        private string responsible;
        private string objectType;

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Company
        {
            get
            {
                return company;
            }
            set
            {
                company = value;
            }
        }
        public string Responsible
        {
            get
            {
                return responsible;
            }
            set
            {
                responsible = value;
            }
        }
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public string ObjectType
        {
            get
            {
                return objectType;
            }
            set
            {
                objectType = value;
            }
        }

        
    }

    public class ListOfObject
    {
        List<string> companyList = new List<string> { "ООО Мебель", "ООО Крутая мебель", "ООО Никс", "ЗАО ДНС", "ОАО е2е4" };
        List<string> mebelList = new List<string> { "Стол", "Стул", "Табуретка", "Компьютерный стол", "Шкаф", "Стойки", "Тумбочки" };
        List<int> mebelPriceList = new List<int> { 7000, 3000, 1500, 8450, 11299, 2700, 1300 };
        List<string> compList = new List<string> { "Компьютер учебный", "Компьютер преподавателя", "Ноутбук", "Проектор", "Телевизор" };
        List<int> compPriceList = new List<int> { 30000, 50000, 60000, 25000, 50000 };
        public Property GenerateProp()
        {
            Random rand = new Random();
            string company = companyList[rand.Next(0, companyList.Count())];
            Property prop = new Property();
            int i;
            switch (company)
            {
                case "ООО Мебель":
                    prop.Company = companyList[0];
                    i = rand.Next(0, mebelList.Count());
                    prop.ObjectType = mebelList[i];
                    prop.Name = prop.ObjectType + " деревянный";
                    prop.Price = mebelPriceList[i];
                    return prop;
                case "ООО Крутая мебель":
                    prop.Company = companyList[1];
                    i = rand.Next(0, mebelList.Count());
                    prop.ObjectType = mebelList[i];
                    prop.Name = prop.ObjectType + " деревянный c металлическим каркасом";
                    prop.Price = mebelPriceList[i] * 2;
                    return prop;
                case "ООО Никс":
                    prop.Company = companyList[2];
                    i = rand.Next(0, compList.Count());
                    prop.ObjectType = compList[i];
                    prop.Name = prop.ObjectType;
                    prop.Price = compPriceList[i];
                    return prop;
                case "ЗАО ДНС":
                    i = rand.Next(0, 1);
                    if (i>0)
                    {
                        prop.Company = companyList[3];
                        i = rand.Next(0, mebelList.Count());
                        prop.ObjectType = mebelList[i];
                        prop.Name = prop.ObjectType + " деревянный";
                        prop.Price = mebelPriceList[i];
                    }
                    else
                    {
                        prop.Company = companyList[3];
                        i = rand.Next(0, compList.Count());
                        prop.ObjectType = compList[i];
                        prop.Name = prop.ObjectType;
                        prop.Price = compPriceList[i];
                    }
                    return prop;
                case "ОАО е2е4":
                    i = rand.Next(0, 1);
                    if (i > 0)
                    {
                        prop.Company = companyList[4];
                        i = rand.Next(0, mebelList.Count());
                        prop.ObjectType = mebelList[i];
                        prop.Name = prop.ObjectType + " деревянный";
                        prop.Price = mebelPriceList[i];
                    }
                    else
                    {
                        prop.Company = companyList[4];
                        i = rand.Next(0, compList.Count());
                        prop.ObjectType = compList[i];
                        prop.Name = prop.ObjectType;
                        prop.Price = compPriceList[i];
                    }
                    return prop;
                default: i = 0;
                    return prop;
            }
        }
    }

    public class PropertyGenerator
    {
        private List<Property> pl = new List<Property>();
        public List<Property> PropertyList { get { return pl; } set { pl = value; } }
        private List<string> ResponsibleList = new List<string> { "Иваныч Г.Г.", "Серегин С.В.", "Лежачко К.В.", "Бабаклавский А.К.", "Бондарь М.В." };
        public PropertyGenerator(int x)
        {
            Random rnd = new Random();
            ListOfObject lo = new ListOfObject();
            int i = 0;
            while (i < x)
            {
                PropertyList.Add(lo.GenerateProp());
                PropertyList[i].ID = "P" + i.ToString();

                PropertyList[i].Responsible = ResponsibleList[rnd.Next(0, ResponsibleList.Count())];
                Console.WriteLine(PropertyList[i].ID);
                Console.WriteLine(PropertyList[i].Name);
                Console.WriteLine(PropertyList[i].ObjectType);
                Console.WriteLine(PropertyList[i].Company);
                Console.WriteLine(PropertyList[i].Responsible);
                Console.WriteLine(PropertyList[i].Price);
                Console.WriteLine("----------------------------------------");
                i++;
            }
        }
    }
}
