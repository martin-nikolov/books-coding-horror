namespace HijackedNestedTaskUsingWait
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// This is article from StackOverFlow - "Is Task.Factory.StartNew() guaranteed to use 
    /// another thread than the calling thread?"
    /// See <see cref="http://stackoverflow.com/questions/12245935/is-task-factory-startnew-guaranteed-to-use-another-thread-than-the-calling-thr" />
    /// </summary>
    public class MainEntryPoint
    {
        internal static void Main()
        {
            for (var i = 0; i < 10; i++)
            {
                Task.Factory.StartNew(Launch).Wait();
            }
        }

        internal static void Launch()
        {
            Console.WriteLine("Launch thread: {0}", Thread.CurrentThread.ManagedThreadId);

            Task.Factory.StartNew(Nested).Wait();
        }

        internal static void Nested()
        {
            Console.WriteLine("Nested thread: {0}\n", Thread.CurrentThread.ManagedThreadId);
        }
    }
}