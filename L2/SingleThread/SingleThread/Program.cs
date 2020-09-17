using System;
using System.Threading;

namespace concurrency
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Do something that takes 4 seconds to process!");
                Thread.Sleep(4000);
                Console.WriteLine("Done!");
            }
        }
    }
}
