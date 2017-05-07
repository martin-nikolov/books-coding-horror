namespace ImmutableLists
{
    using System;
    using System.Collections.Immutable;

    /*
     * This package provides collections that are thread safe and guaranteed to never change their contents, 
     * also known as immutable collections. Like strings, any methods that perform modifications will not change 
     * the existing instance but instead return a new instance. For efficiency reasons, the implementation uses 
     * a sharing mechanism to ensure that newly created instances share as much data as possible with the previous 
     * instance while ensuring that operations have a predictable time complexity.
     */

    /*
     * List: Complexity for operations - Add -> amortized O(1) / Insert - O(N) / RemoveAt - O(N) / Index O(1)
     * ImmutableList: Complexity for operations -> Add - O(logN) / Insert - O(logN) / RemoveAt - O(logN) / Index - O(logN) / Foreach - O(N) / For - O(N * logN)
     */

    public class MainEntryPoint
    {
        internal static void Main()
        {
            var list = ImmutableList<int>.Empty;

            list = list.Insert(0, 1);
            list = list.Insert(0, 2);
            list = list.Insert(0, 3);

            Console.WriteLine("List: {0}", string.Join(", ", list));

            list = list.RemoveAt(1);

            Console.WriteLine("List: {0}", string.Join(", ", list));
            Console.WriteLine("{0}", new string('-', 30));

            // The best way to iterate over an ImmutableList - O(N) complexity
            foreach (var value in list)
                Console.WriteLine(value);

            // This will also work, but it will be much slower - O(N * logN) complexity
            for (var i = 0; i < list.Count; i++)
                Console.WriteLine(list[i]);
        }
    }
}