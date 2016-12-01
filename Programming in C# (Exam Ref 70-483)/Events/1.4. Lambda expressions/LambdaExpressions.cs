namespace ProgrammingInCSharp.LambdaExpressions
{
    using System;

    public class LambdaExpressions
    {
        private static void Main()
        {
            Calculate calculator = (x, y) =>
            {
                Console.WriteLine("Substract: {0}", x - y);
                return x - y;
            };

            calculator += (x, y) =>
            {
                Console.WriteLine("Multiply: {0}", x * y);
                return x * y;
            };

            var delegates = calculator.GetInvocationList();
            foreach (var @delegate in delegates)
            {
                var result = (int)@delegate.DynamicInvoke(5, 6);
                Console.WriteLine("Result: {0}\n", result);
            }
        }

        delegate int Calculate(int x, int y);
    }
}