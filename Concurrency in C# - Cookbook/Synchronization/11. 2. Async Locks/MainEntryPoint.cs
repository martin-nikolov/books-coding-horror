namespace AsyncLocks
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Nito.AsyncEx;

    public class MainEntryPoint
    {
        private const int TaskCountToCreate = 3;

        internal static void Main()
        {
            try
            {
                AsyncContext.Run(async () => await MainAsync());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private static async Task MainAsync()
        {
            await ExampleWithSynchronousLock();

            await ExampleWithAsynchronousLock();
        }

        private static async Task ExampleWithSynchronousLock()
        {
            var sw = Stopwatch.StartNew();

            Console.WriteLine("----------------> Synchronous lock started...");

            var syncLockInstance = new MySyncLockClass();

            var tasks = new List<Task>();

            for (int i = 1; i <= TaskCountToCreate; i++)
            {
                var taskId = i;
                tasks.Add(Task.Factory.Run(() => { syncLockInstance.DelayAndIncrement(taskId); }));
            }

            await Task.WhenAll(tasks);

            Console.WriteLine("Result: {0} | Elapsed: {1}\n\n", await syncLockInstance.GetValue(), sw.Elapsed);
        }

        private static async Task ExampleWithAsynchronousLock()
        {
            var sw = Stopwatch.StartNew();

            Console.WriteLine("----------------> Asynchronous lock started...");

            var asyncLockInstance = new MyAsyncLockClass();

            var tasks = new List<Task>();

            for (int i = 1; i <= TaskCountToCreate; i++)
            {
                var taskId = i;
                tasks.Add(Task.Factory.Run(async () => { await asyncLockInstance.DelayAndIncrement(taskId); }));
            }

            await Task.WhenAll(tasks);

            Console.WriteLine("Result: {0} | Elapsed: {1}, ", await asyncLockInstance.GetValue(), sw.Elapsed);
        }
    }
}