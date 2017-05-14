namespace AsyncCompletion
{
    using System;
    using System.Threading.Tasks;
    using AsyncCompletion.Impl;
    using AsyncCompletion.Infrastructure;
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
            await AsyncHelpers.Using(() => new CompletionClass(), async resource =>
            {
                Console.WriteLine("Waiting for the task completetion...");
            });
        }
    }
}