using CustomCollections;
using System.Collections.Generic;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new List();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            Console.WriteLine("List Contents:");
            foreach (var item in list.ToArray())
                Console.WriteLine(item);

            Console.WriteLine($"Contents 2? {list.Contains(2)}");
            list.Remove(2);
            Console.WriteLine($"Contents 2 after deleting? {list.Contains(2)}");

            Console.WriteLine("Reverse list:");
            list.Reverse();
            foreach (var item in list.ToArray())
                Console.WriteLine(item);
        }
    }
}
