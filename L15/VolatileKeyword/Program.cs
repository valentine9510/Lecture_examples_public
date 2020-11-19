using System;
using System.Threading;
namespace VolatileInThreading
{
    public class Worker
    {
        private volatile bool shouldStop;
        public void DoWork()
        {
            while (!shouldStop)
            {
                Console.WriteLine("Worker thread: working...");
            }
            Console.WriteLine("Worker thread: terminating gracefully.");
        }
        public void RequestStop()
        {
            shouldStop = true;
        }
    }
    public class WorkerThreadExample
    {
        static void Main()
        {
            Console.WriteLine("***Main Starts Here!!!");
            Worker workerObject = new Worker();
            Thread workerThread = new Thread(workerObject.DoWork);
            workerThread.Start();

            Console.WriteLine("Main thread: starting worker thread...");

            while (!workerThread.IsAlive) ;
            Thread.Sleep(1000);
            workerObject.RequestStop();
            workerThread.Join();
            Console.WriteLine("Main thread: worker thread has terminated.");
            
        }
    }
}