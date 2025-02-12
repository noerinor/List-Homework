using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ParallelProcessing
{
    public class ArrayProcessor
    {
        private readonly int[] array;
        private readonly int threadCount;

        public ArrayProcessor(int[] array, int threadCount)
        {
            this.array = array;
            this.threadCount = threadCount;
        }

        public async Task<int> FindMinParallel()
        {
            var partitionSize = array.Length / threadCount;
            var tasks = new List<Task<int>>();

            for (int i = 0; i < threadCount; i++)
            {
                var start = i * partitionSize;
                var end = (i == threadCount - 1) ? array.Length : (i + 1) * partitionSize;
                tasks.Add(Task.Run(() => FindMinInRange(start, end)));
            }

            var results = await Task.WhenAll(tasks);
            return results.Min();
        }

        private int FindMinInRange(int start, int end)
        {
            return array[start..end].Min();
        }

        public async Task<int> FindMaxParallel()
        {
            var partitionSize = array.Length / threadCount;
            var tasks = new List<Task<int>>();

            for (int i = 0; i < threadCount; i++)
            {
                var start = i * partitionSize;
                var end = (i == threadCount - 1) ? array.Length : (i + 1) * partitionSize;
                tasks.Add(Task.Run(() => FindMaxInRange(start, end)));
            }

            var results = await Task.WhenAll(tasks);
            return results.Max();
        }

        private int FindMaxInRange(int start, int end)
        {
            return array[start..end].Max();
        }

        public async Task<long> CalculateSumParallel()
        {
            var partitionSize = array.Length / threadCount;
            var tasks = new List<Task<long>>();

            for (int i = 0; i < threadCount; i++)
            {
                var start = i * partitionSize;
                var end = (i == threadCount - 1) ? array.Length : (i + 1) * partitionSize;
                tasks.Add(Task.Run(() => CalculateSumInRange(start, end)));
            }

            var results = await Task.WhenAll(tasks);
            return results.Sum();
        }

        private long CalculateSumInRange(int start, int end)
        {
            return array[start..end].Sum(x => (long)x);
        }

        public async Task<double> CalculateAverageParallel()
        {
            var sum = await CalculateSumParallel();
            return (double)sum / array.Length;
        }

        public int[] CopyArrayPart(int startIndex, int length)
        {
            var result = new int[length];
            Array.Copy(array, startIndex, result, 0, length);
            return result;
        }
    }

    public class TextProcessor
    {
        private readonly string text;
        private readonly int threadCount;

        public TextProcessor(string text, int threadCount)
        {
            this.text = text;
            this.threadCount = threadCount;
        }

        public async Task<Dictionary<char, int>> GetCharacterFrequencyParallel()
        {
            var partitionSize = text.Length / threadCount;
            var tasks = new List<Task<Dictionary<char, int>>>();

            for (int i = 0; i < threadCount; i++)
            {
                var start = i * partitionSize;
                var end = (i == threadCount - 1) ? text.Length : (i + 1) * partitionSize;
                tasks.Add(Task.Run(() => GetCharacterFrequencyInRange(start, end)));
            }

            var results = await Task.WhenAll(tasks);
            return MergeDictionaries(results);
        }

        private Dictionary<char, int> GetCharacterFrequencyInRange(int start, int end)
        {
            return text[start..end]
                .GroupBy(c => c)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        public async Task<Dictionary<string, int>> GetWordFrequencyParallel()
        {
            var words = Regex.Split(text.ToLower(), @"\W+")
                            .Where(w => !string.IsNullOrEmpty(w))
                            .ToArray();

            var partitionSize = words.Length / threadCount;
            var tasks = new List<Task<Dictionary<string, int>>>();

            for (int i = 0; i < threadCount; i++)
            {
                var start = i * partitionSize;
                var end = (i == threadCount - 1) ? words.Length : (i + 1) * partitionSize;
                tasks.Add(Task.Run(() => GetWordFrequencyInRange(words, start, end)));
            }

            var results = await Task.WhenAll(tasks);
            return MergeDictionaries(results);
        }

        private Dictionary<string, int> GetWordFrequencyInRange(string[] words, int start, int end)
        {
            return words[start..end]
                .GroupBy(w => w)
                .ToDictionary(g => g.Key, g => g.Count());
        }

        private Dictionary<TKey, int> MergeDictionaries<TKey>(IEnumerable<Dictionary<TKey, int>> dictionaries)
        {
            return dictionaries
                .SelectMany(dict => dict)
                .GroupBy(kvp => kvp.Key)
                .ToDictionary(g => g.Key, g => g.Sum(kvp => kvp.Value));
        }
    }

    public class PerformanceTester
    {
        public static async Task TestArrayProcessing(int[] array, int[] threadCounts)
        {
            Console.WriteLine($"Testing array of size: {array.Length}");

            foreach (var threadCount in threadCounts)
            {
                Console.WriteLine($"\nThread count: {threadCount}");
                var processor = new ArrayProcessor(array, threadCount);
                var sw = new Stopwatch();

                sw.Restart();
                var min = await processor.FindMinParallel();
                sw.Stop();
                Console.WriteLine($"Find Min: {sw.ElapsedMilliseconds}ms");

                sw.Restart();
                var max = await processor.FindMaxParallel();
                sw.Stop();
                Console.WriteLine($"Find Max: {sw.ElapsedMilliseconds}ms");

                sw.Restart();
                var sum = await processor.CalculateSumParallel();
                sw.Stop();
                Console.WriteLine($"Calculate Sum: {sw.ElapsedMilliseconds}ms");

                sw.Restart();
                var avg = await processor.CalculateAverageParallel();
                sw.Stop();
                Console.WriteLine($"Calculate Average: {sw.ElapsedMilliseconds}ms");
            }
        }

        public static async Task TestTextProcessing(string text, int[] threadCounts)
        {
            Console.WriteLine($"Testing text of length: {text.Length}");

            foreach (var threadCount in threadCounts)
            {
                Console.WriteLine($"\nThread count: {threadCount}");
                var processor = new TextProcessor(text, threadCount);
                var sw = new Stopwatch();

                sw.Restart();
                var charFreq = await processor.GetCharacterFrequencyParallel();
                sw.Stop();
                Console.WriteLine($"Character Frequency: {sw.ElapsedMilliseconds}ms");

                sw.Restart();
                var wordFreq = await processor.GetWordFrequencyParallel();
                sw.Stop();
                Console.WriteLine($"Word Frequency: {sw.ElapsedMilliseconds}ms");
            }
        }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Enter number of threads to test (comma-separated): ");
            var threadCounts = Console.ReadLine()
                .Split(',')
                .Select(int.Parse)
                .ToArray();

            // Тест масивив
            var arraySizes = new[] { 1000000, 10000000, 100000000 };
            var random = new Random();

            foreach (var size in arraySizes)
            {
                var array = Enumerable.Range(0, size)
                    .Select(_ => random.Next(1000000))
                    .ToArray();

                await PerformanceTester.TestArrayProcessing(array, threadCounts);
            }

            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sample.txt");
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Error: file not found {filePath}");
                    return;
                }

                var text = File.ReadAllText(filePath);
                await PerformanceTester.TestTextProcessing(text, threadCounts);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error when reding file {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error {ex.Message}");
            }
        }
    }
}