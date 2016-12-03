namespace DataAccess.AsyncParallelOperations
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class AsyncParallelOperations
    {
        private static void Main()
        {
            AsyncParallelOperations.ExecuteMultipleRequests().Wait();
        }

        private static async Task ExecuteMultipleRequests()
        {
            HttpClient httpClient = new HttpClient();

            var task1 = httpClient.GetStringAsync("http://blogs.msdn.com")
                                  .ContinueWith(r => Console.WriteLine("1. Blogs MSDN's response length: {0}", r.Result.Length));

            var task2 = httpClient.GetStringAsync("http://msdn.microsoft.com")
                                  .ContinueWith(r => Console.WriteLine("2. MSDN Microsoft's response length: {0}", r.Result.Length));

            var task3 = httpClient.GetStringAsync("http://microsoft.com")
                                  .ContinueWith(r => Console.WriteLine("3. Microsoft's response length: {0}", r.Result.Length));

            await Task.WhenAll(task1, task2, task3);
        }
    }
}
