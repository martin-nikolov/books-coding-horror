namespace AsynchonousQueues
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    public class MainEntryPoint
    {
        private const int NumberOfConsumers = 5;

        private static readonly BufferBlock<int> asyncQueue = new BufferBlock<int>(new DataflowBlockOptions() { BoundedCapacity = NumberOfConsumers });

        internal static void Main()
        {
            StartProducer();

            for (int i = 1; i <= NumberOfConsumers; i++)
            {
                StartConsumer($"Consumer {i}");
            }

            Console.ReadLine();
        }

        private static void StartProducer()
        {
            Task.Factory.StartNew(async () =>
            {
                var counter = 0;

                while (++counter <= 50)
                {
                    await asyncQueue.SendAsync(counter);
                    await asyncQueue.SendAsync(++counter);

                    Thread.Sleep(TimeSpan.FromSeconds(0.1));
                }

                asyncQueue.Complete();
            });
        }

        // For more than 1 consumer
        private static void StartConsumer(string consumerName)
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("[{0}] Action executed: OutputAvailableAsync | Managed thread Id: {1} | Item: {2}",
                                   consumerName,
                                   Thread.CurrentThread.ManagedThreadId,
                                   await asyncQueue.ReceiveAsync());

                        Thread.Sleep(TimeSpan.FromSeconds(0.2));
                    }
                    catch (InvalidOperationException)
                    {
                        break;
                    }
                }
            });
        }

        // For one consumer
        //private static void StartConsumer(string consumerName)
        //{
        //    Task.Factory.StartNew(async () =>
        //    {
        //        while (await asyncQueue.OutputAvailableAsync())
        //        {
        //            Console.WriteLine("[{0}] Action executed: OutputAvailableAsync | Managed thread Id: {1} | Item: {2}",
        //                           consumerName,
        //                           Thread.CurrentThread.ManagedThreadId,
        //                           await asyncQueue.ReceiveAsync());

        //            Thread.Sleep(TimeSpan.FromSeconds(0.2));
        //        }
        //    });
        //}
    }
}