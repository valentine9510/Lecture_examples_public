using System;
using System.Threading;


namespace concurrency
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Starts!");

            for (int i = 0; i < 2; i++)
            {
                Thread thread = new Thread(MyProcess);

                // worker threads
                thread.Start();
            }

            Console.WriteLine("A process that takes 4s!");
            Thread.Sleep(4000);
            Console.WriteLine("Main Thread {0} is Done!", Thread.CurrentThread.ManagedThreadId);

        }

        private static void MyProcess(object obj)
        {
            Console.WriteLine("A process that takes 4s!");
            Thread.Sleep(4000);
            Console.WriteLine("Thread {0} Done!", Thread.CurrentThread.ManagedThreadId);

        }
    }
}
