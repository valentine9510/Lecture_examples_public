// Reference https://www.codeproject.com/Articles/1112510/TPL-Producer-Consumer-Pattern-Thread-Safe-Queue-Co

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;



class Program
{
    static void Main(string[] args)
    {
        var t1 = Task.Factory.StartNew(() => ProduceTasks());
        var t2 = Task.Factory.StartNew(() => ConsumeTasks());
        var t3 = Task.Factory.StartNew(() => ConsumeTasks());
        Task.WaitAll(t1, t2, t3);
        Console.ReadLine();
    }

    public static void ProcessQueue(QueuedObject queue)
    {
        string reversedString = new string(Enumerable.Range(1, queue.RandomString.Length).Select(i => queue.RandomString[queue.RandomString.Length - i]).ToArray());

        Console.WriteLine
            (
            "Dequeued: " + queue.QueueID +
            "\t" + "Consumer ThreadID :" + Thread.CurrentThread.ManagedThreadId +
            "\t" + DateTime.Now.ToLongTimeString() +
            "\t" + "ReversedString :" + reversedString
            );
    }

    public static void ProduceTasks()
    {
        Random random = new Random();
        for (int i = 1; i <= 1; i++)
        {
            var queue = new QueuedObject
            {
                QueueID = i,
                ProducerThreadID = Thread.CurrentThread.ManagedThreadId,
                EnqueueDateTime = DateTime.Now,
                // Used to Generate Random String
                RandomString = new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 5).Select(s => s[random.Next(s.Length)]).ToArray())
            };

            #region Queueing using Queue Collection
            // Uncomment QueueService.Enqueue(() => { ProcessQueue(queue); }) to use .Net Queue Object to Queue Tasks.
            // And Comment ConcurrentQueueService.Enqueue
            QueueService.Enqueue(() => { ProcessQueue(queue); });
            #endregion

            #region Queueing using ConcurrentQueue
            //ConcurrentQueueService.Enqueue(() => { ProcessQueue(queue); });
            #endregion

            Console.WriteLine
                (
                "Enqueued: " + queue.QueueID +
                "\t" + "Producer ThreadID :" + queue.ProducerThreadID +
                "\t" + queue.EnqueueDateTime.ToLongTimeString() +
                "\t" + "RandomString   :" + queue.RandomString
                );
        }
    }

    public static void ConsumeTasks()
    {
        //QueueService.Dequeue();
        ConcurrentQueueService.Dequeue();
    }


    public class QueuedObject
    {
        public int QueueID { get; set; }
        public int ConsumerThreadID { get; set; }
        public int ProducerThreadID { get; set; }
        public string RandomString { get; set; }
        public DateTime EnqueueDateTime { get; set; }
        public DateTime DequeueDateTime { get; set; }
    }

    public static class QueueService
    {
        static Queue<Task> _queue;

        static QueueService() { _queue = new Queue<Task>(); }
        public static void Enqueue(Action action, CancellationToken cancelToken = default(CancellationToken))
        {
            Task task = new Task(action, cancelToken);
            _queue.Enqueue(task);
        }

        public static void Dequeue()
        {
            while (true)
            {
                try
                {
                    Task task = _queue.Dequeue();
                    task.RunSynchronously();
                }
                catch (NullReferenceException ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

    }

    class ConcurrentQueueService
    {
        static ConcurrentQueue<Task> _queue;

        static ConcurrentQueueService()
        {
            _queue = new ConcurrentQueue<Task>();
        }

        public static void Enqueue(Action action, CancellationToken cancelToken = default(CancellationToken))
        {
            Task task = new Task(action, cancelToken);
            _queue.Enqueue(task);
        }

        public static void Dequeue()
        {
            while (true)
            {
                try
                {
                    Task task;
                    if (_queue.TryDequeue(out task)) { task.RunSynchronously(); }
                }
                catch (NullReferenceException ex)
                {
                    string w = ex.Message;
                    Debug.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }

}