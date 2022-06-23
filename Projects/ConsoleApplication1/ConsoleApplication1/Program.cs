using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static int i;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string start = "Начало взлома всех виртуальных банков";
            Console.WriteLine(start);
            Task[] tasks = new Task[99];


            for (i=0; i < 99; i++)
            {
                 tasks[i] = new Task(vzlom);
            }

            foreach (Task t in tasks)
                t.Start();

            Task.WaitAll(tasks);
            Console.WriteLine("Все взломано! Теперь тебя посадят");
            Console.ReadKey();
        }
        static void vzlom()
        {
            int i = Program.i;
            Console.Write("Взлом вируального банка номер {0}", i);
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(500);
            Console.Write(".");
            Thread.Sleep(2000);
            Console.WriteLine(".");
            Thread.Sleep(100);
            Console.WriteLine("Готово!");

        }
    }
}
