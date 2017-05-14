namespace AsyncLazyCachedProperty
{
    using System;
    using System.Threading.Tasks;
    using Nito.AsyncEx;

    internal class MyAsyncClass
    {
        public AsyncLazy<int> Data { get; } = new AsyncLazy<int>(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(3));

            return 13;
        });
    }
}