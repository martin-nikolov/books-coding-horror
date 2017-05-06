namespace SimpleTimeout
{
    using System;
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
                Console.Error.WriteLine(ex.Message);
            }
        }

        private static async Task MainAsync()
        {
            /*
             * 1. Completed task within specified timeout. 
             */

            var task1 = GetSampleTask(runtimeInSeconds: 2);
            var completedTask = await RunWithTimeout(task1, 3);

            Console.WriteLine("TASK 1 ---> Result: {0}\n", await completedTask);

            /*
             * 2. Task exceeded timeout interval. 
             */

            var task2 = GetSampleTask(runtimeInSeconds: 3);
            var exceededTimeoutTask = await RunWithTimeout(task2, 2);

            // Exception is thrown
            Console.WriteLine("TASK 2 ---> Result: {0}", await exceededTimeoutTask);
        }

        private static async Task<int> GetSampleTask(int runtimeInSeconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(runtimeInSeconds));

            return 1;
        }

        private static async Task<Task<T>> RunWithTimeout<T>(Task<T> task, int timeoutInSeconds)
        {
            var timeoutTask = Task.Delay(TimeSpan.FromSeconds(timeoutInSeconds));

            var completedTask = await Task.WhenAny(task, timeoutTask);
            if (completedTask == timeoutTask)
            {
                throw new TimeoutException("Timeout interval was exceeded.");
            }

            return completedTask as Task<T>;
        }
    }
}