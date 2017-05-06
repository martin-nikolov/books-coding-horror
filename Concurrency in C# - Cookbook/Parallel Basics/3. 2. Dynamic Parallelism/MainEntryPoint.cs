namespace DynamicParallelism
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Nito.AsyncEx;

    public class MainEntryPoint
    {
        private static void Main()
        {
            try
            {
                AsyncContext.Run(async () => await MainAsync());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        private static async Task MainAsync()
        {
            await UsingAttachedToParentTask();
            await UsingContinuationTask();
        }

        private static async Task UsingAttachedToParentTask()
        {
            var parentTask = Task.Factory.StartNew(ParentWithContinuationTaskAction,
                                                   CancellationToken.None,
                                                   TaskCreationOptions.None,
                                                   TaskScheduler.Default);

            await parentTask;
        }

        private static async Task UsingContinuationTask()
        {
            var parentTask = Task.Factory.StartNew(ParentTaskAction,
                                                   CancellationToken.None,
                                                   TaskCreationOptions.None,
                                                   TaskScheduler.Default);

            var continuationTask = parentTask.ContinueWith(t => ContinuationTaskAction(),
                                                           CancellationToken.None,
                                                           TaskContinuationOptions.AttachedToParent,
                                                           TaskScheduler.Default);

            await continuationTask;
        }

        private static void ParentTaskAction()
        {
            Console.WriteLine("\nParent task started...");

            Thread.Sleep(1000);

            Console.WriteLine("Parent task was completed successfully. Managed thread id: {0}", Thread.CurrentThread.ManagedThreadId);
        }

        private static void ParentWithContinuationTaskAction()
        {
            Console.WriteLine("Parent task started...");

            Thread.Sleep(1000);

            Task.Factory.StartNew(ContinuationTaskAction, CancellationToken.None, TaskCreationOptions.AttachedToParent, TaskScheduler.Default);
            Task.Factory.StartNew(ContinuationTaskAction, CancellationToken.None, TaskCreationOptions.AttachedToParent, TaskScheduler.Default);
            Task.Factory.StartNew(ContinuationTaskAction, CancellationToken.None, TaskCreationOptions.AttachedToParent, TaskScheduler.Default);
            Task.Factory.StartNew(ContinuationTaskAction, CancellationToken.None, TaskCreationOptions.AttachedToParent, TaskScheduler.Default);

            Console.WriteLine("Parent task was completed successfully. Managed thread id: {0}", Thread.CurrentThread.ManagedThreadId);
        }

        private static void ContinuationTaskAction()
        {
            Console.WriteLine("Child task started...");

            Thread.Sleep(3000);

            Console.WriteLine("Child task was completed succesfully. Managed thread id: {0}", Thread.CurrentThread.ManagedThreadId);
        }
    }
}