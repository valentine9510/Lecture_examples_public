using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeadlockAvoidanceAllLocj
{
    class Program
    {
        public static void Main()
        {
            object lock1 = new object();
            Console.WriteLine("Starting...");
            var task1 = Task.Run(() =>
            {
                lock (lock1)
                {
                    Console.WriteLine("Thread 1 Lock both Account A and Account B");
                    Thread.Sleep(5000);
                    Console.WriteLine("Done");

                }
            });

            var task2 = Task.Run(() =>
            {
                  
                lock (lock1)
                {
                    Console.WriteLine("Thread 2 Lock both Account A and Account B");
                    Thread.Sleep(2000);
                    Console.WriteLine("Done");
                }
               
            });

            Task.WaitAll(task1, task2);
            Console.WriteLine("Finished...");
        }
    }
}
