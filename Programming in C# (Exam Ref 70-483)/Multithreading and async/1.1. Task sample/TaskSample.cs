namespace ProgrammingInCSharp.TaskSample
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class TaskSample
    {
        private static void Main()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            var completedTask = TaskSample.RunNewTask(cancellationToken);

            Console.ReadLine();
            cancellationTokenSource.Cancel();

            completedTask.Wait(cancellationToken);
        }

        private static Task RunNewTask(CancellationToken cancellationToken)
        {
            var task = Task.Run(() =>
                                {
                                    for (int i = 0; i < 10000; i++)
                                    {
                                        if (cancellationToken.IsCancellationRequested)
                                        {
                                            cancellationToken.ThrowIfCancellationRequested();
                                        }

                                        Console.WriteLine(i);
                                    }
                                },
                                cancellationToken);

            task.ContinueWith(i => { Console.WriteLine("Task: OnlyOnCanceled"); }, TaskContinuationOptions.OnlyOnCanceled);
            task.ContinueWith(i => { Console.WriteLine("Task: OnlyOnFaulted"); }, TaskContinuationOptions.OnlyOnFaulted);

            var completedTask = task.ContinueWith(i => { Console.WriteLine("Task: OnlyOnRanToCompletion"); }, TaskContinuationOptions.OnlyOnRanToCompletion);
            return completedTask;
        }
    }
}