namespace ImmutableQueue
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
            var queue = ImmutableQueue<int>.Empty;

            queue = queue.Enqueue(1);
            queue = queue.Enqueue(7);

            Console.WriteLine("Queue: {0}", string.Join(", ", queue));

            int nextItem;
            queue = queue.Dequeue(out nextItem);

            Console.WriteLine("Next item: {0}", nextItem);
            Console.WriteLine("Count: {0}", queue.Count());
        }
    }
}