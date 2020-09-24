using System;
using System.Threading;
using System.Collections.Generic;

namespace LectureMutexExample
{
    class Program
    {
        static void Main(string[] args)
        {
            bool usage_flag = false;
            int counter = 0;
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 10; i++)
            {
                
                Thread thread = new Thread(() => threadinc(ref usage_flag, ref counter));
                thread.Name = string.Format("Thread{0}", i + 1);
                thread.Start();
                threads.Add(thread);

            }
            //join all the threads
            foreach (Thread thread in threads)
                thread.Join();
            
            Console.WriteLine("Counter: {0}", counter);

            void threadinc(ref bool usage_flag, ref int counter)
            {
                Console.WriteLine("{0}", Thread.CurrentThread.Name);
                // increment
                for (int i = 0; i < 5000000; ++i)
                {
                    
                    // wait until not being used
                    while (usage_flag == true) {}
                    usage_flag = true; //atomic operation

                    counter += 1;

                    // let next thread use counter
                    usage_flag = false;
                }
            }

        }
    }
}





