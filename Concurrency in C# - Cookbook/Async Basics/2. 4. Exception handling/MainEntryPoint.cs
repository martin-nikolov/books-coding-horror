namespace ExceptionHandling
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class MainEntryPoint
    {
        internal static void Main()
        {
            ObserveOneExceptionAsync().Wait();
            ObserveAllExceptionsAsync().Wait();
        }

        private static async Task ObserveOneExceptionAsync()
        {
            var task1 = ThrowNotImplementedExceptionAsync();
            var task2 = ThrowInvalidOperationExceptionAsync();

            Console.WriteLine("Waiting the tasks...");

            try
            {
                await Task.WhenAll(task1, task2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}\n", ex.GetType().Name);
            }
        }

        private static async Task ObserveAllExceptionsAsync()
        {
            var task1 = ThrowNotImplementedExceptionAsync();
            var task2 = ThrowInvalidOperationExceptionAsync();

            Console.WriteLine("Waiting the tasks...");

            var allTasks = Task.WhenAll(task1, task2);

            try
            {
                await allTasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exceptions: {0}\n", string.Join(", ", allTasks.Exception.InnerExceptions.Select(x => x.GetType().Name)));
            }
        }

        /// !IMPORTANT: if we skip the 'async' keyword the exception is thrown immediately after method invocation (line 17 and line 34)
        private static async Task ThrowNotImplementedExceptionAsync()
        {
            throw new NotImplementedException();
        }

        /// !IMPORTANT: if we skip the 'async' keyword the exception is thrown immediately after method invocation (line 18 and line 35)
        private static async Task ThrowInvalidOperationExceptionAsync()
        {
            throw new InvalidOperationException();
        }
    }
}