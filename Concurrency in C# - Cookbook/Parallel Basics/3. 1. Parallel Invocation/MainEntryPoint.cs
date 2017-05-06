namespace ParallelInvocation
{
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    public class MainEntryPoint
    {
        internal static void Main()
        {
            var array = new int[1000];

            // CPU intensive processing
            ProcessArray(array);
            ProcessArrayInParallel(array);
        }

        private static void ProcessArray(int[] array)
        {
            var stopWatch = Stopwatch.StartNew();
            {
                ProcessPartialArray(array, 0, array.Length/2);
                ProcessPartialArray(array, array.Length/2, array.Length);
            }

            Console.WriteLine("Elapsed time: {0}\n", stopWatch.Elapsed);
        }

        private static void ProcessArrayInParallel(int[] array)
        {
            var stopWatch = Stopwatch.StartNew();
            {
                Parallel.Invoke(
                    () => ProcessPartialArray(array, 0, array.Length / 2),
                    () => ProcessPartialArray(array, array.Length / 2, array.Length));
            }

            Console.WriteLine("Elapsed time: {0}", stopWatch.Elapsed);
        }

        private static void ProcessPartialArray(int[] array, int begin, int end)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Managed thread id: {0}", threadId);

            for (int i = begin, sum = 0; i < end; i++)
            {
                Thread.Sleep(10);

                sum += array[i];
            }
        }
    }
}
