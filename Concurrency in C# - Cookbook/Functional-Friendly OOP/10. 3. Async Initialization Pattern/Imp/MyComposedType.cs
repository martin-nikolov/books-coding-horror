namespace AsyncInitializationPattern.Imp
{
    using System.Threading.Tasks;
    using AsyncInitializationPattern.Abstract;
    using AsyncInitializationPattern.Infrastructure;

    internal class MyComposedType : IMyComposedType, IAsyncInitialization
    {
        private readonly IMyFundamentalType fundamental;
        private readonly ILogger logger;

        public MyComposedType(IMyFundamentalType fundamental, ILogger logger)
        {
            this.fundamental = fundamental;
            this.logger = logger;

            this.Initialization = this.InitializeAsync();
        }

        /// <summary>
        /// The result of the asynchronous initialization of this instance.
        /// </summary>
        public Task Initialization { get; }

        private async Task InitializeAsync()
        {
            // Asynchronously wait for the fundamental instance to initialize, if necessary.
            await AsyncInitialization.WhenAllInitializedAsync(this.fundamental);

            // Do our own initialization (synchronous or asynchronous).
            // ...

            this.logger.WriteLine($"Object of type {this.GetType().FullName} was initialized successfully.");
        }
    }
}