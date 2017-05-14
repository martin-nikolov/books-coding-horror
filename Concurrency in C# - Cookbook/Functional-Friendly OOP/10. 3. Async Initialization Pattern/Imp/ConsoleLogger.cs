namespace AsyncInitializationPattern.Imp
{
    using System;
    using AsyncInitializationPattern.Abstract;

    public class ConsoleLogger : ILogger
    {
        public void WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
        }
    }
}