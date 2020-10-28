using System;
using System.Threading;

public class Example
{
    //  ManualReset synchronization events.
    //
    private static EventWaitHandle ewh;

    // A counter to make sure all threads are started and
    // blocked before any are released. A Long is used to show
    // the use of the 64-bit Interlocked methods.
    //
    private static long threadCount = 0;

    public static void Main()
    {
        
        // Create a ManualReset EventWaitHandle.
        //
        ewh = new EventWaitHandle(false, EventResetMode.ManualReset);

        // Create and start five more numbered threads.
        //
        for (int i = 0; i <= 4; i++)
        {
            Thread t = new Thread(
                new ParameterizedThreadStart(ThreadProc)
            );
            t.Start(i);
        }

        // Wait until all the threads have started and blocked.
        //
        while (Interlocked.Read(ref threadCount) < 5)
        {
            Thread.Sleep(500);
        }

        // Because the EventWaitHandle was created with
        // ManualReset mode, signaling it releases all the
        // waiting threads.
        //
        Console.WriteLine("Press ENTER to release the waiting threads.");
        Console.ReadLine();
        ewh.Set();
    }

    public static void ThreadProc(object data)
    {
        int index = (int)data;

        Console.WriteLine("Thread {0} blocks.", data);
        // Increment the count of blocked threads.
        Interlocked.Increment(ref threadCount);

        // Wait on the EventWaitHandle.
        ewh.WaitOne();

        Console.WriteLine("Thread {0} exits.", data);
        // Decrement the count of blocked threads.
        Interlocked.Decrement(ref threadCount);
    }
}