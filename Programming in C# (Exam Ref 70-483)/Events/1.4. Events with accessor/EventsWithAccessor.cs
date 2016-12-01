namespace ProgrammingInCSharp.EventsWithAccessor
{
    using System;

    public class EventsWithAccessor
    {
        private static void Main()
        {
            var pub = new Pub();

            pub.OnChange += RaiseError;
            pub.OnChange += RaiseError;
            pub.OnChange += RaiseWarning;

            pub.Raise(new MyEventArgs("Exception occurs 1.", Status.Error));
            pub.Raise(new MyEventArgs("Type not defined 1.", Status.Warning));

            pub.OnChange -= RaiseError;
            pub.OnChange -= RaiseError;
            pub.OnChange -= RaiseError;

            pub.Raise(new MyEventArgs("Exception occurs 2.", Status.Error));
            pub.Raise(new MyEventArgs("Type not defined 2.", Status.Warning));
        }

        private static void RaiseError(object sender, MyEventArgs args)
        {
            if (args.Status == Status.Error)
            {
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("Status: {0} | Message: {1} | Sender: {2}", args.Status, args.Message, sender);

                Console.ForegroundColor = defaultColor;
            }
        }

        private static void RaiseWarning(object sender, MyEventArgs args)
        {
            if (args.Status == Status.Warning)
            {
                var defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine("Status: {0} | Message: {1} | Sender: {2}", args.Status, args.Message, sender);

                Console.ForegroundColor = defaultColor;
            }
        }
    }

    public class MyEventArgs : EventArgs
    {
        public MyEventArgs(string message, Status status)
        {
            this.Message = message;
            this.Status = status;
        }

        public string Message { get; set; }

        public Status Status { get; set; }
    }

    public enum Status
    {
        Error,
        Warning
    }

    public class Pub
    {
        private readonly object obj = new object();

        // Not-nullable property
        private EventHandler<MyEventArgs> onChange = delegate { };

        public event EventHandler<MyEventArgs> OnChange
        {
            add
            {
                lock (this.obj)
                {
                    if (value != null)
                    {
                        this.onChange += value;
                    }
                }
            }

            remove
            {
                lock (this.obj)
                {
                    if (value != null)
                    {
                        // Delegate subtraction has unpredictable result
                        this.onChange -= value;
                    }
                }
            }
        }

        public void Raise(MyEventArgs args)
        {
            this.onChange(this, args);
        }
    }
}