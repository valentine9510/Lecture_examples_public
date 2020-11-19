using System;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
namespace ProducerConsumerPattern {  
    class Program
        {
            static void Main(string[] args)
            {
                var pc = new ProducerConsumer();
                pc.StartChannel();
                Console.ReadKey();
            }
        }
    public class ProducerConsumer
    {
        static int messageLimit = 5;
        Channel<string> channel = Channel.CreateBounded<string>(messageLimit);

        public void StartChannel()
        {
            List<string> names = new List<string>();
            names.Add("John Smith");
            names.Add("Jane Smith");
            names.Add("John Doe");
            names.Add("Jane Doe");
            Task producer = Task.Factory.StartNew(() => {
                foreach (var name in names)
                {
                    channel.Writer.TryWrite(name);
                }
                channel.Writer.Complete();
            });
            Task[] consumer = new Task[2];
            for (int i = 0; i < consumer.Length; i++)
            {
                consumer[i] = Task.Factory.StartNew(async () => {
                    while (await channel.Reader.WaitToReadAsync())
                    {
                        if (channel.Reader.TryRead(out
                                var data))
                        {
                            Console.WriteLine(" Data read from Consumer No.{0} is {1}", Task.CurrentId, data);
                        }
                    }
                });
            }
            producer.Wait();
            Task.WaitAll(consumer);
        }
    }
}