namespace AsyncLocks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class MyAsyncLockClass
    {
        private readonly SemaphoreSlim mutex = new SemaphoreSlim(1);

        private int value;

        public async Task DelayAndIncrement(int id)
        {
            this.Log(id, "Waiting the mutex.");

            await this.mutex.WaitAsync();

            this.Log(id, "Awaited the mutex.");

            try
            {
                this.Log(id, "Entered the try-block.");

                var oldValue = this.value;

                await Task.Delay(TimeSpan.FromSeconds(oldValue));

                this.Log(id, $"Awaited the delay of {oldValue} seconds.");

                this.value = oldValue + 1;

                this.Log(id, $"Updated the value to {this.value}.");
            }
            finally
            {
                this.Log(id, "Released the mutex.");

                this.mutex.Release();
            }
        }

        public Task<int> GetValue()
        {
            return Task.FromResult(this.value);
        }

        private void Log(int id, string messsage)
        {
            Console.WriteLine($"[{id}]: {messsage}.");
        }
    }
}