namespace ProgrammingInCSharp.AsyncLinearOperations
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class AsyncLinearOperations
    {
        private static void Main()
        {
            AsyncLinearOperations.ExecuteMultipleRequests().Wait();
        }

        private static async Task ExecuteMultipleRequests()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                await httpClient.GetStringAsync("http://blogs.msdn.com")
                                .ContinueWith(r => Console.WriteLine("1. Blogs MSDN's response length: {0}", r.Result.Length));

                await httpClient.GetStringAsync("http://msdn.microsoft.com")
                                .ContinueWith(r => Console.WriteLine("2. MSDN Microsoft's response length: {0}", r.Result.Length));

                await httpClient.GetStringAsync("http://microsoft.com")
                                .ContinueWith(r => Console.WriteLine("3. Microsoft's response length: {0}", r.Result.Length));
            }
        }
    }
}
