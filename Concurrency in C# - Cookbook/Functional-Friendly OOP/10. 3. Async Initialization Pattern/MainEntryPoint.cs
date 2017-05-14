namespace AsyncInitializationPattern
{
    using System;
    using System.Threading.Tasks;
    using AsyncInitializationPattern.Abstract;
    using AsyncInitializationPattern.Infrastructure;
    using Nito.AsyncEx;

    /*
     * You are coding a type that requires some asynchronous work to be done in its constructor, 
     * but you cannot use the asynchronous factory pattern because the instance is created via 
     * reflection (e.g., a Dependency Injection/Inversion of Control library, data binding, Activator.CreateInstance, etc.).
     * 
     * When you have this scenario, you have to return an uninitialized instance, but you can 
     * mitigate this by applying a common pattern: the asynchronous initialization pattern.
     */

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
            DependencyInjection.RegisterDependencies();

            var fundamentalTypeInstance = DependencyInjection.Get<IMyFundamentalType>();
            var composedTypeInstance = DependencyInjection.Get<IMyComposedType>();

            await AsyncInitialization.WhenAllInitializedAsync(fundamentalTypeInstance, composedTypeInstance);
        }
    }
}