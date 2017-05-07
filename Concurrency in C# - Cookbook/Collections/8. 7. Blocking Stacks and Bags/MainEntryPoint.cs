namespace BlockingStacksAndBags
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    public class MainEntryPoint
    {
        private const int NumberOfConsumers = 5;

        private static readonly BlockingCollection<int> blockingQueue = new BlockingCollection<int>(new ConcurrentStack<int>(),
                                                                                                    boundedCapacity: NumberOfConsumers);

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
            Task.Factory.StartNew(() =>
            {
                var counter = 0;

                while (++counter <= 50)
                {
                    blockingQueue.Add(counter);

                    Thread.Sleep(TimeSpan.FromSeconds(0.1));
                }

                blockingQueue.CompleteAdding();
            });
        }

        private static void StartConsumer(string consumerName)
        {
            Task.Factory.StartNew(() =>
            {
                foreach (var item in blockingQueue.GetConsumingEnumerable())
                {
                    Console.WriteLine("[{0}] Action executed: GetConsumingEnumerable | Managed thread Id: {1} | Item: {2}",
                                      consumerName,
                                      Thread.CurrentThread.ManagedThreadId,
                                      item);

                    Thread.Sleep(TimeSpan.FromSeconds(0.5));
                }
            });
        }
    }
}