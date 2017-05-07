namespace BlockingAndAsynchronousQueues
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Nito.AsyncEx;

    public class MainEntryPoint
    {
        private const int NumberOfConsumers = 5;

        private static readonly AsyncProducerConsumerQueue<int> asyncQueue = new AsyncProducerConsumerQueue<int>(NumberOfConsumers);

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
                    // Asynchronous producer
                    await asyncQueue.EnqueueAsync(counter);
                    await asyncQueue.EnqueueAsync(++counter);

                    Thread.Sleep(TimeSpan.FromSeconds(0.05));

                    // Synchronous producer
                    asyncQueue.Enqueue(++counter);
                    asyncQueue.Enqueue(++counter);

                    Thread.Sleep(TimeSpan.FromSeconds(0.05));
                }

                asyncQueue.CompleteAdding();
            });
        }

        // For more than 1 consumer
        private static void StartConsumer(string consumerName)
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var takeResult = await asyncQueue.TryDequeueAsync();
                    if (!takeResult.Success)
                    {
                        break;
                    }

                    Console.WriteLine("[{0}] Action executed: OutputAvailableAsync | Managed thread Id: {1} | Item: {2}",
                                consumerName,
                                Thread.CurrentThread.ManagedThreadId,
                                takeResult.Item);

                    Thread.Sleep(TimeSpan.FromSeconds(0.15));
                }
            });
        }
    }
}