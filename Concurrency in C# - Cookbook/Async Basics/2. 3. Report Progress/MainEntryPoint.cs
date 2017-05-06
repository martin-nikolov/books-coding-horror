namespace ReportProgress
{
    using System;
    using System.Threading.Tasks;

    public class MainEntryPoint
    {
        internal static void Main()
        {
            CallMyMethodAsync().Wait();
        }

        private static async Task CallMyMethodAsync()
        {
            var progress = new Progress<int>();

            progress.ProgressChanged += (sender, args) =>
            {
                Console.WriteLine("{0}%", args);
            };

            await MyMethodAsync(progress);
        }

        private static async Task MyMethodAsync(IProgress<int> progress = null)
        {
            int percentComplete = 0;

            while (percentComplete < 100)
            {
                progress?.Report(++percentComplete);

                await Task.Delay(10);
            }
        }
    }
}
