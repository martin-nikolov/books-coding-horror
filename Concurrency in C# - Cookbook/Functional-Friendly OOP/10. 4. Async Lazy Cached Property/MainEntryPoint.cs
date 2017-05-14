namespace AsyncLazyCachedProperty
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
            var instance = new MyAsyncClass();

            Console.WriteLine("Retrieving the value...");
            Console.WriteLine($"Value: {await instance.Data}");
            Console.WriteLine($"Cached value: {await instance.Data}");
        }
    }
}