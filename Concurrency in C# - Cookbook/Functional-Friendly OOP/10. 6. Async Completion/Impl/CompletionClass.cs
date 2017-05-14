namespace AsyncCompletion.Impl
{
    using System;
    using System.Threading.Tasks;
    using AsyncCompletion.Abstract;

    internal class CompletionClass : IAsyncCompletion
    {
        private readonly TaskCompletionSource<object> completion = new TaskCompletionSource<object>();

        private Task taskCompleting;

        public Task Completion => this.completion.Task;

        public void Complete()
        {
            if (this.taskCompleting == null)
            {
                this.taskCompleting = this.CompleteAsync();
            }
        }

        private async Task CompleteAsync()
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
            }
            catch (Exception ex)
            {
                this.completion.TrySetException(ex);
            }
            finally
            {
                this.completion.TrySetResult(null);
            }
        }
    }
}