namespace ThreadsafeDictionaries
{
    using System;
    using System.Collections.Concurrent;

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
            var dictionary = new ConcurrentDictionary<int, string>();
            Console.WriteLine("Dictionary: {0}", dictionary.Count > 0 ? string.Join(", ", dictionary) : "no values");

            var newValue = dictionary.AddOrUpdate(0, key => "Zero", (key, oldValue) => "Zero1");

            Console.WriteLine("New value: {0}", newValue);
            Console.WriteLine("Dictionary: {0}", dictionary.Count > 0 ? string.Join(", ", dictionary) : "no values");

            var modifiedValue = dictionary.AddOrUpdate(0, key => "Zero", (key, oldValue) => "Zero1");

            Console.WriteLine("\nNew value: {0}", modifiedValue);
            Console.WriteLine("Dictionary: {0}", dictionary.Count > 0 ? string.Join(", ", dictionary) : "no values");
            Console.WriteLine("{0}", new string('-', 30));
        }
    }
}