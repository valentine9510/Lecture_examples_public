using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

class DataflowProducerConsumer
{
    static void Produce(ITargetBlock<byte[]> target)
    {
        var rand = new Random();

        for (int i = 0; i < 100; ++i)
        {
            var buffer = new byte[1024];
            rand.NextBytes(buffer);
            target.Post(buffer);
        }

        target.Complete();
    }

    //static async Task<int> ConsumeAsync(ISourceBlock<byte[]> source)
    //{
    //    int bytesProcessed = 0;

    //    while (await source.OutputAvailableAsync())
    //    {
    //        byte[] data = await source.ReceiveAsync();
    //        bytesProcessed += data.Length;
    //    }

    //    return bytesProcessed;
    //}


    static async Task<int> ConsumeAsync(IReceivableSourceBlock<byte[]> source)
    {
        int bytesProcessed = 0;
        while (await source.OutputAvailableAsync())
        {
            while (source.TryReceive(out byte[] data))
            {
                bytesProcessed += data.Length;
            }
        }
        return bytesProcessed;
    }


    static async Task Main()
    {
        var buffer = new BufferBlock<byte[]>();
        var consumerTask1 = ConsumeAsync(buffer);
        var consumerTask2 = ConsumeAsync(buffer);

        Produce(buffer);

        var bytesProcessed1 = await consumerTask1;
        var bytesProcessed2 = await consumerTask2;
        Console.WriteLine($"Processed {bytesProcessed1:#,#} bytes and {bytesProcessed2:#,#} bytes");
    }
}

// Sample  output:
//     Processed 102,400 bytes.