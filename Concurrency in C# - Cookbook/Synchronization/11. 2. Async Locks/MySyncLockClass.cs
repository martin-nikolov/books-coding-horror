namespace AsyncLocks
{
    using System;
    using System.Threading.Tasks;

    public class MySyncLockClass
    {
        private readonly object mutex = new object();

        private int value;

        public void DelayAndIncrement(int id)
        {
            this.Log(id, "Waiting the lock.");

            lock (this.mutex)
            {
                this.Log(id, "Entered the lock.");

                var oldValue = this.value;

                Task.Delay(TimeSpan.FromSeconds(oldValue)).Wait();

                this.Log(id, $"Awaited the delay of {oldValue} seconds.");

                this.value = oldValue + 1;

                this.Log(id, $"Updated the value to {this.value}.");
            }

            this.Log(id, "Leaved the lock.");
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