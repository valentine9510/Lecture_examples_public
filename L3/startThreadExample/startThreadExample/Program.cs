using System;
using System.Diagnostics;
using System.Threading;

public class Example
{
    public static void Main()
    {
        Console.WriteLine("Main thread (Thread {0}) starts...",
                          Thread.CurrentThread.ManagedThreadId);

        var th = new Thread(ExecuteInForeground);
        th.Start();
        Thread.Sleep(1000);
        Console.WriteLine("Main thread (Thread {0}) waiting to join...",
                          Thread.CurrentThread.ManagedThreadId);
        th.Join();
        Console.WriteLine("Main thread (Thread {0}) exiting...",
                          Thread.CurrentThread.ManagedThreadId);
    }

    private static void ExecuteInForeground()
    {
        var sw = Stopwatch.StartNew();
        Console.WriteLine("Thread {0}: {1}, Priority {2}",
                          Thread.CurrentThread.ManagedThreadId,
                          Thread.CurrentThread.ThreadState,
                          Thread.CurrentThread.Priority);
        do
        {
            Console.WriteLine("Thread {0}: Elapsed {1:N2} seconds",
                              Thread.CurrentThread.ManagedThreadId,
                              sw.ElapsedMilliseconds / 1000.0);
            Thread.Sleep(500);
        } while (sw.ElapsedMilliseconds <= 5000);
        sw.Stop();
        Console.WriteLine("Thread {0} exit...",
                         Thread.CurrentThread.ManagedThreadId);
        Console.WriteLine("Thread {0} waiting to join...",
                         Thread.CurrentThread.ManagedThreadId);
    }
}