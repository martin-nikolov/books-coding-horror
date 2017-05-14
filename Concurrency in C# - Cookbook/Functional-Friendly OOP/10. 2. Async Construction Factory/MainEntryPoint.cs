namespace AsyncConstructionFactory
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
            var instance = await MyAsyncClass.CreateAsync();

            Console.WriteLine($"Instance of type {instance} was created successfully.");
        }
    }
}