namespace AsyncInitializationPattern.Imp
{
    using System;
    using System.Threading.Tasks;
    using AsyncInitializationPattern.Abstract;

    internal class MyFundamentalType : IMyFundamentalType, IAsyncInitialization
    {
        private readonly ILogger logger;

        public MyFundamentalType(ILogger logger)
        {
            this.logger = logger;

            this.Initialization = this.InitializeAsync();
        }

        /// <summary>
        /// The result of the asynchronous initialization of this instance.
        /// </summary>
        public Task Initialization { get; }

        /// <summary>
        /// Asynchronously initialize this instance.
        /// </summary>
        /// <returns></returns>
        private async Task InitializeAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            this.logger.WriteLine($"Object of type {this.GetType().FullName} was initialized successfully.");
        }
    }
}