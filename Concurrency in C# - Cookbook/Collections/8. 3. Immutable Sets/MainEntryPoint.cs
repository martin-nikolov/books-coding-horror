namespace ImmutableSets
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
            ImmutableHashSetDemo();
            ImmutableSortedSetDemo();
        }

        private static void ImmutableHashSetDemo()
        {
            var hashSet = ImmutableHashSet<int>.Empty;

            hashSet = hashSet.Add(4);
            hashSet = hashSet.Add(1);
            hashSet = hashSet.Add(10);

            // Displays "1, 4, 10" in an unpredictable order
            Console.WriteLine("HashSet: {0}", string.Join(", ", hashSet));

            hashSet = hashSet.Remove(4);

            Console.WriteLine("HashSet: {0}", string.Join(", ", hashSet));
            Console.WriteLine("Count: {0}", hashSet.Count);
            Console.WriteLine("{0}", new string('-', 30));
        }

        private static void ImmutableSortedSetDemo()
        {
            var sortedSet = ImmutableSortedSet<int>.Empty;

            sortedSet = sortedSet.Add(4);
            sortedSet = sortedSet.Add(1);
            sortedSet = sortedSet.Add(10);

            Console.WriteLine("SortedSet: {0}", string.Join(", ", sortedSet));

            sortedSet = sortedSet.Remove(4);

            Console.WriteLine("SortedSet: {0}", string.Join(", ", sortedSet));
            Console.WriteLine("Count: {0}", sortedSet.Count());
            Console.WriteLine("{0}", new string('-', 30));
        }
    }
}