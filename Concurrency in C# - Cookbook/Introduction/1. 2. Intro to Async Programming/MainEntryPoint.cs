namespace IntroToAsyncProgramming
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
            var task1 = DoSomeWorkAsync();
            var task2 = DoSomeWorkAsync();

            await Task.WhenAll(task1, task2);
        }

        private static async Task DoSomeWorkAsync()
        {
            Console.WriteLine(1);

            await Task.Delay(3000);

            Console.WriteLine(2);

            await Task.Delay(1000);

            Console.WriteLine(3);
        }
    }
}