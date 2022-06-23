using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            IProperty prop = new Furniture();
            prop.SetInfo();
            prop.Info();

        }
    }

    class Client
    {

    }

    interface IProperty
    {
        IProperty Create();
        void Info();
        void SetInfo();
    }

    class Furniture : IProperty
    {
        string id;
        public string Name { get; set; }
        string lenght;
        string height;
        string width;
        string weight;
        string cost;

        public void SetInfo()
        {
            Console.WriteLine("Start");
            id = "I";
            lenght = "L";
            height = "H";
            width = "Wid";
            weight = "Wei";
            cost = "100 rub";
            Console.WriteLine("Ready");
        }

        public IProperty Create()
        {
            return new Furniture();
        }

        public void Info()
        {
            Console.WriteLine(id + " " + lenght + " " + width + " " + height + " " + weight + " " + cost);
        }

    }
    
}
