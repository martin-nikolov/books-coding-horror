namespace AsyncDisposalWithCancellationToken
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class MyClass : IDisposable
    {
        private readonly CancellationTokenSource disposeCancellationToken = new CancellationTokenSource();

        public async Task<int> CalculateValueAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var combinedCancellationToken = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, this.disposeCancellationToken.Token))
            {
                await Task.Delay(TimeSpan.FromSeconds(3), combinedCancellationToken.Token);

                return 13;
            }
        }

        public void Dispose()
        {
            this.disposeCancellationToken.Cancel();
        }
    }
}