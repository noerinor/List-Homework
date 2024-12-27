using CustomCollections.Interfaces;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new ObservableList<int>();

            list.ItemAdded += (sender, e) => Console.WriteLine($"Element added: {e.Item}");
            list.ItemInserted += (sender, e) => Console.WriteLine($"Element inserted: {e.Item}");
            list.ItemRemoved += (sender, e) => Console.WriteLine($"Element remved: {e.Item}");

            list.Add(1);
            list.Add(2);
            list.Insert(1, 3);
            list.Remove(2);

            Console.WriteLine("list:");
            foreach (var item in list.ToArray())
                Console.WriteLine(item);
        }
    }
}