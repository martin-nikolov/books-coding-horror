namespace ProgrammingInCSharp.Actions
{
    using System;

    public class Actions
    {
        private static void Main()
        {
            var pub = new Pub();

            pub.Raise();

            //Actions.SimpleActionsResult();
        }

        private static void SimpleActionsResult()
        {
            Action<int, int> calculator = (x, y) => { Console.WriteLine("Substract: {0}", x - y); };
            calculator += (x, y) => { Console.WriteLine("Multiply: {0}", x * y); };

            var delegates = calculator.GetInvocationList();
            foreach (var @delegate in delegates)
            {
                @delegate.DynamicInvoke(5, 6);
            }
        }
    }

    public class Pub
    {
        // Not-nullable property
        private Action OnChange { get; set; } = delegate { };

        public void Subscribe(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException($"{nameof(action)} cannot be null.");
            }

            this.OnChange += action;
        }

        public void Raise()
        {
            this.OnChange.Invoke();
        }
    }
}