using System;
using System.Threading;
using System.Threading.Tasks;

namespace NestedLock
{
    class Program
    {
        public static void Main()
        {
            object lock1 = new object();
            object lock2 = new object();
            Console.WriteLine("Starting...");


            var task1 = Task.Run(() =>
            {
                bool deposited = false;
               
                while (!deposited)
                {
                    try
                    {
                        bool entered = Monitor.TryEnter(lock1, TimeSpan.FromMilliseconds(100));
                        if (!entered) continue;
                        Thread.Sleep(1000);
                        Console.WriteLine("Task1: Entered Lock1 ");
                        entered = Monitor.TryEnter(lock2, TimeSpan.FromMilliseconds(100));
                        if (!entered) continue;
                        Console.WriteLine("Task1: Entered Lock2 ");
                        Thread.Sleep(1000);
                        //todo: Lets say you have transferred from account A ot B
                        deposited = true;
                        break;
                           
                    }
                    finally
                    {
                        
                        if (Monitor.IsEntered(lock1)) Monitor.Exit(lock1);
                        if (Monitor.IsEntered(lock2)) Monitor.Exit(lock2);
                        Thread.Sleep(200);

                    }
                }
            });

            var task2 = Task.Run(() =>
            {
                bool deposited = false;
                while (!deposited)
                {
                    try
                    {
                        bool entered = Monitor.TryEnter(lock2, TimeSpan.FromMilliseconds(100));
                        if (!entered) continue;
                        Thread.Sleep(1000);
                        Console.WriteLine("Task2: Entered Lock2 ");
                        entered = Monitor.TryEnter(lock1, TimeSpan.FromMilliseconds(100));
                        if (!entered) continue;
                        Console.WriteLine("Task2: Entered Lock1 ");
                        Thread.Sleep(1000);
                        //todo: Lets say you have transferred from account A ot B
                        deposited = true;
                        break;

                    }
                    finally
                    {
                        
                        if (Monitor.IsEntered(lock1)) Monitor.Exit(lock1);
                        if (Monitor.IsEntered(lock2)) Monitor.Exit(lock2);
                        Thread.Sleep(200);

                    }
                }
            });

            Task.WaitAll(task1, task2);
            Console.WriteLine("Finished...");
        }
    }
}
