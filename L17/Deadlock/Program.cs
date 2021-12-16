using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Deadlocks
{
    class Program
    {

        static void Main(string[] args)
        {
            object LockA = new object();
            object LockB = new object();
            new Thread(() =>
            {
                lock (LockA)
                {
                    Console.WriteLine("Lock A obtained");
                    Thread.Sleep(2000);
                    lock (LockB)
                    {
                        Console.WriteLine("Lock B obtained");
                    }
                }
            }).Start();
            lock (LockB)
            {
                Console.WriteLine("Main Thread obtained Lock B");
                Thread.Sleep(1000);
                lock (LockA)
                {
                    Console.WriteLine("Main Thread obtained Lock B");
                }
            }
        }
    }
}
