namespace AsyncDisposalWithCancellationToken
{
    using System;
    using System.Threading;
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
            Task<int> task;

            using (var resource = new MyClass())
            {
                var disposeCancellationToken = new CancellationTokenSource();

                task = resource.CalculateValueAsync(disposeCancellationToken.Token);

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            Console.WriteLine($"Result: {await task}");
        }
    }
}