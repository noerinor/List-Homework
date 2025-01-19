namespace LINQ;

using System;
using System.Linq;

internal class Program
{
    static void Main(string[] args)
    {
        // 1. 
        Console.WriteLine("1. " + string.Join(",", Enumerable.Range(10, 41)));

        // 2. 
        Console.WriteLine("2. " + string.Join(",", Enumerable.Range(10, 41).Where(x => x % 3 == 0)));

        // 3. 
        Console.WriteLine("3. " + string.Join(" ", Enumerable.Repeat("Linq", 10)));

        // 4. 
        Console.WriteLine("4. " + string.Join(";", "aaa;abb;ccc;dap".Split(';').Where(word => word.Contains('a'))));

        // 5. 
        Console.WriteLine("5. " + string.Join(",", "aaa;abb;ccc;dap"
            .Split(';')
            .Where(word => word.Contains('a'))
            .Select(word => word.Count(c => c == 'a'))));

        // 6. 
        Console.WriteLine("6. " + "aaa;xabbx;abb;ccc;dap".Split(';').Any(word => word == "abb"));

        // 7.
        Console.WriteLine("7. " + "aaa;xabbx;abb;ccc;dap".Split(';').OrderByDescending(word => word.Length).First());

        // 8.
        Console.WriteLine("8. " + "aaa;xabbx;abb;ccc;dap".Split(';').Average(word => word.Length));

        // 9. 
        Console.WriteLine("9. " + new string("aaa;xabbx;abb;ccc;dap;zh"
            .Split(';')
            .OrderBy(word => word.Length)
            .First()
            .Reverse()
            .ToArray()));

        // 10.
        Console.WriteLine("baaa;aabb;aaa;xabbx;abb;ccc;dap;zh"
            .Split(';')
            .FirstOrDefault(word => word.StartsWith("aa"))?
            .Skip(2)
            .All(c => c == 'b') ?? false);

        // 11.
        Console.WriteLine("11. " + Enumerable.Range(10, 41)
            .Select(x => $"x{x}bb")
            .SkipWhile(x => !x.EndsWith("bb"))
            .Skip(2)
            .LastOrDefault());

    }
}

