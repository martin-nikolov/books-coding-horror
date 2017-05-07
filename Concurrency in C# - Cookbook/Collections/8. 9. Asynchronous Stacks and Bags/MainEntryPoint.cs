namespace AsynchronousStacksAndBags
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using Nito.AsyncEx;

    public class MainEntryPoint
    {
        private const int NumberOfConsumers = 5;

        private static readonly AsyncCollection<int> asyncCollection = new AsyncCollection<int>(new ConcurrentBag<int>(), NumberOfConsumers);

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
                    await asyncCollection.AddAsync(counter);
                    await asyncCollection.AddAsync(++counter);

                    Thread.Sleep(TimeSpan.FromSeconds(0.1));
                }

                asyncCollection.CompleteAdding();
            });
        }

        // For more than 1 consumer
        private static void StartConsumer(string consumerName)
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    var takeResult = await asyncCollection.TryTakeAsync();
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