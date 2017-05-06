namespace ConcurrentProcessing
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Nito.AsyncEx;

    public class MainEntryPoint
    {
        internal static void Main()
        {
            try
            {
                AsyncContext.Run(async () => await MainAsync());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        private static async Task MainAsync()
        {
            /*
             * 1. Linear processing
             */

            await LinearProcessing();

            /*
             * 2. Concurrent processing
             */

            await ConcurrentProcessing();
        }

        private static async Task LinearProcessing()
        {
            var stopWatch = Stopwatch.StartNew();
            {
                var task1 = DelayAndReturnAsync(5);
                var task2 = DelayAndReturnAsync(2);
                var task3 = DelayAndReturnAsync(5);

                var tasks = new List<Task<int>>() { task1, task2, task3 };

                // Await each task in order
                foreach (var task in tasks)
                {
                    var result = await task;

                    Console.WriteLine(result);
                }
            }

            Console.WriteLine("LINEAR PROCESSING ---> Elapsed time: {0}\n", stopWatch.Elapsed);
        }

        private static async Task ConcurrentProcessing()
        {
            var stopWatch = Stopwatch.StartNew();
            {
                var task1 = DelayAndReturnAsync(5);
                var task2 = DelayAndReturnAsync(2);
                var task3 = DelayAndReturnAsync(5);

                var tasks = new List<Task<int>>() { task1, task2, task3 };

                var processingTasks = tasks.Select(async t =>
                {
                    var result = await t;

                    Console.WriteLine(result);
                })
                .ToArray();

                // Await all processing to complete
                await Task.WhenAll(processingTasks);
            }

            Console.WriteLine("CONCURRENT PROCESSING ---> Elapsed time: {0}\n", stopWatch.Elapsed);
        }

        private static async Task<int> DelayAndReturnAsync(int seconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds));

            return seconds;
        }
    }
}