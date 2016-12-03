namespace ProgrammingInCSharp.UsingExceptionDispatchInfo
{
    using System;
    using System.Runtime.ExceptionServices;

    public class UsingExceptionDispatchInfo
    {
        private static void Main()
        {
            ExceptionDispatchInfo possibleException = null;

            try
            {
                var text = Console.ReadLine();
                int.Parse(text);
            }
            catch (FormatException ex)
            {
                possibleException = ExceptionDispatchInfo.Capture(ex);
            }

            possibleException?.Throw();
        }
    }
}