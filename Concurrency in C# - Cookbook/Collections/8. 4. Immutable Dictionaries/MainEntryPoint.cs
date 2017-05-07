namespace ImmutableDictionaries
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

    public class MainEntryPoint
    {
        internal static void Main()
        {
            ImmutableDictionaryDemo();
            ImmutableSortedDictionaryDemo();
        }

        private static void ImmutableDictionaryDemo()
        {
            var dictionary = ImmutableDictionary<int, string>.Empty;

            dictionary = dictionary.Add(4, "Four");
            dictionary = dictionary.Add(10, "Four");
            dictionary = dictionary.Add(1, "One");
            dictionary = dictionary.SetItem(10, "Diez");

            Console.WriteLine("Dictionary: {0}", string.Join(", ", dictionary));

            dictionary = dictionary.Remove(10);

            Console.WriteLine("Dictionary: {0}", string.Join(", ", dictionary));
            Console.WriteLine("Count: {0}", dictionary.Count);
            Console.WriteLine("{0}", new string('-', 30));
        }

        private static void ImmutableSortedDictionaryDemo()
        {
            var dictionary = ImmutableSortedDictionary<int, string>.Empty;

            dictionary = dictionary.Add(4, "Four");
            dictionary = dictionary.Add(10, "Four");
            dictionary = dictionary.Add(1, "One");
            dictionary = dictionary.SetItem(10, "Diez");

            Console.WriteLine("SortedDictionary: {0}", string.Join(", ", dictionary));

            dictionary = dictionary.Remove(10);

            Console.WriteLine("SortedDictionary: {0}", string.Join(", ", dictionary));
            Console.WriteLine("Count: {0}", dictionary.Count);
            Console.WriteLine("{0}", new string('-', 30));
        }
    }
}