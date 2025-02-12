using CustomList = CustomCollections.List<int>;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new CustomList();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            Console.WriteLine("List contains:");
            foreach (var item in list.ToArray())
                Console.WriteLine(item);

            Console.WriteLine($"Contains 2? {list.Contains(2)}");
            list.Remove(2);
            Console.WriteLine($"Contains 2 after remove? {list.Contains(2)}");

            Console.WriteLine("Reversed:");
            list.Reverse();
            foreach (var item in list.ToArray())
                Console.WriteLine(item);
        }
    }
}
