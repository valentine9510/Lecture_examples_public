using System;
using System.Diagnostics;
using System.Threading;


public class NonStaticMethodMultiThreading
{
    public static void Main()
    {
        Console.WriteLine("Main thread (Thread {0}) started...",
                                Thread.CurrentThread.ManagedThreadId);
        SomeClass obj = new SomeClass();
        var th = new Thread(obj.SomeWork);
        th.Start();
        Console.WriteLine("Main thread (Thread {0}) sleeing for 1(s)...",
                                Thread.CurrentThread.ManagedThreadId);
        Thread.Sleep(1000);
        Console.WriteLine("Main thread (Thread {0}) waiting...",
                                Thread.CurrentThread.ManagedThreadId);
        th.Join();
        Console.WriteLine("Main thread (Thread {0}) exiting...",
                                Thread.CurrentThread.ManagedThreadId);
    }
}

public class SomeClass
{
    public void SomeWork()
    {
        var sw = Stopwatch.StartNew();

        do
        {
            Console.WriteLine("Thread {0}: Elapsed {1:N2}(s)",
                                Thread.CurrentThread.ManagedThreadId,
                                sw.ElapsedMilliseconds / 1000.0);
            Thread.Sleep(500);
        } while (sw.ElapsedMilliseconds <= 5000);
        sw.Stop();
        Console.WriteLine("Thread ({0}) finished.",
                               Thread.CurrentThread.ManagedThreadId);
    }
}
