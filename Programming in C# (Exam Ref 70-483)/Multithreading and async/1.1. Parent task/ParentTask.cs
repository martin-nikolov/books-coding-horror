namespace ProgrammingInCSharp.ParentTask
{
    using System;
    using System.Threading.Tasks;

    public class ParentTask
    {
        private static void Main()
        {
            var task = ParentTask.RunNewTask();
            task.Wait();
        }

        private static Task RunNewTask()
        {
            Task<int[]> parentTask = Task.Run(() =>
                                              {
                                                  var results = new int[3];

                                                  new Task(() => results[0] = 1, TaskCreationOptions.AttachedToParent).Start();
                                                  new Task(() => results[1] = 2, TaskCreationOptions.AttachedToParent).Start();
                                                  new Task(() => results[2] = 3, TaskCreationOptions.AttachedToParent).Start();

                                                  return results;
                                              });

            Task finalTask = parentTask.ContinueWith(tasks =>
            {
                var totalSum = 0;

                foreach (var taskResult in tasks.Result)
                {
                    totalSum += taskResult;
                    Console.WriteLine(taskResult);
                }

                Console.WriteLine("Sum: {0}", totalSum);
            });

            return finalTask;
        }
    }
}