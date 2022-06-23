using System;
using System.Threading;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            for (;;)
            {
                Console.Write("Analysis..");
                for (int i = 0; i < 7; i++)
                {
                    Console.Write(".");
                    Thread.Sleep(1000);
                }
                Console.WriteLine("\t Done!");
                Console.Write("Work in Progress...");
                Console.WriteLine(" ...");
                for (int n = 5; n < 100;)
                {
                    Console.WriteLine($"........ {n}");
                    Thread.Sleep(500);
                    n += 3;
                }
                Console.WriteLine("This part done!");
            }
        }
    }
}
