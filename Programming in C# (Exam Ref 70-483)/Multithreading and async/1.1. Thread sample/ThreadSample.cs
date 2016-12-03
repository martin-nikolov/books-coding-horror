namespace ProgrammingInCSharp.ThreadSample
{
    using System;
    using System.Threading;

    public class ThreadSample
    {
        private static readonly object syncObj = new object();

        private static void Main()
        {
            new Thread(() => ThreadSample.ThreadMethod(ConsoleColor.White, "ThreadMethod 1: {0}")) { Priority = ThreadPriority.Lowest }.Start();
            new Thread(() => ThreadSample.ThreadMethod(ConsoleColor.Red, "ThreadMethod 2: {0}")).Start();
            new Thread(() => ThreadSample.ThreadMethod(ConsoleColor.Green, "ThreadMethod 3: {0}")).Start();
            new Thread(() => ThreadSample.ThreadMethod(ConsoleColor.Cyan, "ThreadMethod 4: {0}")) { Priority = ThreadPriority.Highest }.Start();

            //ThreadSample.ThreadMethod(ConsoleColor.Yellow, "Main: {0}");
        }

        private static void ThreadMethod(ConsoleColor color, string message)
        {
            for (int i = 1; i <= 1000; i++)
            {
                lock (syncObj)
                {
                    ThreadSample.PrintMessage(color, string.Format(message, i));

                    //Thread.Sleep(0);
                }
            }

            Console.WriteLine("\r--------------------------" + string.Format(message, "Finished --------------------------"));
        }

        private static void PrintMessage(ConsoleColor color, string message)
        {
            var prevColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write("\r" + message);
            Console.ForegroundColor = prevColor;
        }
    }
}