using System;
using System.Threading;

namespace SemaphoreSlimSpace
{
    class Program
    {
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3);
        static void Main()
        {
            Console.WriteLine("Main Begins");
            for (int i = 0; i < 10; i++)
            {
                new Thread(EnterSemaphore).Start(i + 1);
            }
            Console.WriteLine("Main is Done");
    
        }

        private static void EnterSemaphore(object id)
        {
            Console.WriteLine(id + " is waiting to be part of the club");
            semaphoreSlim.Wait();
            Console.WriteLine(id + " part of the club");
            Thread.Sleep(1000);
            //Thread.Sleep(1000/ Convert.ToInt32(id));
            Console.WriteLine(id + " left the club");
            semaphoreSlim.Release();
        }
    }
}