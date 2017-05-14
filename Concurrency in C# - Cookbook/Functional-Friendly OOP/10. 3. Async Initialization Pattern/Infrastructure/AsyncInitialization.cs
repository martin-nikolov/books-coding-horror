namespace AsyncInitializationPattern.Infrastructure
{
    using System.Linq;
    using System.Threading.Tasks;
    using AsyncInitializationPattern.Abstract;

    public static class AsyncInitialization
    {
        public static Task WhenAllInitializedAsync(params object[] instances)
        {
            return Task.WhenAll(instances.OfType<IAsyncInitialization>().Select(x => x.Initialization));
        }
    }
}