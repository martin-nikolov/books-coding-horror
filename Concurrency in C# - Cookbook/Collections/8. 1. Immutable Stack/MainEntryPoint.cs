namespace ImmutableStack
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;

    /*
     * This package provides collections that are thread safe and guaranteed to never change their contents, 
     * also known as immutable collections. Like strings, any methods that perform modifications will not change 
     * the existing instance but instead return a new instance. For efficiency reasons, the implementation uses 
     * a sharing mechanism to ensure that newly created instances share as much data as possible with the previous 
     * instance while ensuring that operations have a predictable time complexity.
     */

    public class MainEntryPoint
    {
        internal static void Main()
        {
            FirstDemo();
            SecondDemo();
        }

        private static void FirstDemo()
        {
            var stack = ImmutableStack<int>.Empty;

            stack = stack.Push(1);
            stack = stack.Push(7);

            Console.WriteLine("Stack: {0}", string.Join(", ", stack));

            int lastItem;
            stack = stack.Pop(out lastItem);

            Console.WriteLine("Last item: {0}", lastItem);
            Console.WriteLine("Count: {0}", stack.Count());
            Console.WriteLine("{0}", new string('-', 30));
        }

        private static void SecondDemo()
        {
            var stack = ImmutableStack<int>.Empty;
            stack = stack.Push(1);

            var biggerStack = stack.Push(7);

            Console.WriteLine("Stack: {0}", string.Join(", ", stack));
            Console.WriteLine("Bigger stack: {0}\n", string.Join(", ", biggerStack));
        }
    }
}