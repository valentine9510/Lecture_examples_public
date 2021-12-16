using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;


class CQ_EnqueueDequeuePeek
{
    // Demonstrates:
    // ConcurrentQueue<T>.Enqueue()
    // ConcurrentQueue<T>.TryPeek()
    // ConcurrentQueue<T>.TryDequeue()
    static void Main()
    {
        // Construct a Queue<>.
        //Queue<int> cq = new Queue<int>();

        // Construct a Queue<>.
        ConcurrentQueue<int> cq = new ConcurrentQueue<int>();

        // Populate the queue.
        for (int i = 0; i < 10000; i++)
        {
            cq.Enqueue(i);
        }

        // Peek at the first element.
        int result;
        if (!cq.TryPeek(out result))
        {
            Console.WriteLine("CQ: TryPeek failed when it should have succeeded");
        }
        else if (result != 0)
        {
            Console.WriteLine("CQ: Expected TryPeek result of 0, got {0}", result);
        }

        int outerSum = 0;
        // An action to consume the ConcurrentQueue.
        Action action = () =>
        {
            int localSum = 0;
            while (cq.TryDequeue(out int localValue))
            {
                localSum += localValue;
            }
            Interlocked.Add(ref outerSum, localSum);
        };

        /***** Method 1 to Start 4 concurrent Consuming Action ***/
        //Start 4 concurrent consuming actions.
        //Parallel.Invoke(action, action, action, action);

        /***** Method 2 to Start 4 concurrent Consuming Action ***/
        //var task1 = Task.Factory.StartNew(action);
        //task1.Wait();
        //var task2 = Task.Factory.StartNew(action);
        //task2.Wait();
        //var task3 = Task.Factory.StartNew(action);
        //task3.Wait();
        //var task4 = Task.Factory.StartNew(action);
        //task4.Wait();

        /***** Method 3 to Start 4 concurrent Consuming Action ***/
        Task task1 = new Task(action);
        task1.Start();
        Task task2 = new Task(action);
        task2.Start();
        Task task3 = new Task(action);
        task3.Start();
        Task task4 = new Task(action);
        task4.Start();

        task1.Wait();
        task2.Wait();
        task3.Wait();
        task4.Wait();


        Console.WriteLine("outerSum = {0}, should be 49995000", outerSum);
    }
}