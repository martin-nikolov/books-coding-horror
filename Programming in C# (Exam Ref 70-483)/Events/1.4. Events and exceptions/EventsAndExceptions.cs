namespace ProgrammingInCSharp.EventsAndExceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EventsAndExceptions
    {
        private static void Main()
        {
            var pub = new Publisher();
            var subscriber = new Subscriber();

            pub.OnChange += subscriber.Invoke;
            pub.OnChange += StaticInvokeMethod;
            pub.OnChange += subscriber.Invoke;
            pub.OnChange += StaticInvokeMethod;

            try
            {
                pub.Raise(new MyEventArgs("Sample message..."));
            }
            catch (AggregateException ex)
            {
                // Process exceptions
                Console.WriteLine("Number of exceptions: {0}", ex.InnerExceptions.Count);
            }
        }

        private static void StaticInvokeMethod(object sender, MyEventArgs args)
        {
            Console.WriteLine("Static invoker: {0}", args.Message);
        }
    }

    public class MyEventArgs : EventArgs
    {
        public MyEventArgs(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }

    public class Publisher
    {
        // Not-nullable property
        public event EventHandler<MyEventArgs> OnChange = delegate { };

        public void Raise(MyEventArgs args)
        {
            var exceptions = new List<Exception>();

            var delegates = this.OnChange.GetInvocationList();
            foreach (var @delegate in delegates)
            {
                try
                {
                    @delegate.DynamicInvoke(this, args);
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }
        }
    }

    public class Subscriber
    {
        public void Invoke(object sender, MyEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}