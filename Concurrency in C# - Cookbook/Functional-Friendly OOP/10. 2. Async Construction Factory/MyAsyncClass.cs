namespace AsyncConstructionFactory
{
    using System;
    using System.Threading.Tasks;

    internal class MyAsyncClass
    {
        private MyAsyncClass()
        {
        }

        private async Task<MyAsyncClass> InitializeAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            return this;
        }

        public static Task<MyAsyncClass> CreateAsync()
        {
            var result = new MyAsyncClass();

            return result.InitializeAsync();
        }
    }
}