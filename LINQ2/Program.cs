using System;
using System.Collections.Generic;
using System.Linq;

class Actor
{
    public string Name { get; set; }
    public DateTime Birthdate { get; set; }
}

abstract class ArtObject
{
    public string Author { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
}

class Film : ArtObject
{
    public int Length { get; set; }
    public IEnumerable<Actor> Actors { get; set; }
}

class Book : ArtObject
{
    public int Pages { get; set; }
}

class Program
{
    static void Main()
    {
        var data = new List<object>() {
            "Hello",
            new Book() { Author = "Terry Pratchett", Name = "Guards! Guards!", Pages = 810 },
            new List<int>() {4, 6, 8, 2},
            new string[] {"Hello inside array"},
            new Film() { Author = "Martin Scorsese", Name= "The Departed", Actors = new List<Actor>() {
                new Actor() { Name = "Jack Nickolson", Birthdate = new DateTime(1937, 4, 22)},
                new Actor() { Name = "Leonardo DiCaprio", Birthdate = new DateTime(1974, 11, 11)},
                new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)}
            }},
            new Film() { Author = "Gus Van Sant", Name = "Good Will Hunting", Actors = new List<Actor>() {
                new Actor() { Name = "Matt Damon", Birthdate = new DateTime(1970, 8, 10)},
                new Actor() { Name = "Robin Williams", Birthdate = new DateTime(1951, 8, 11)},
            }},
            new Book() { Author = "Stephen King", Name="Finders Keepers", Pages = 200},
            "Leonardo DiCaprio"
        };

        // 1.
        Console.WriteLine("\n1.___________\n");
        Console.WriteLine(string.Join(", ", data.Where(x => !(x is ArtObject))));

        // 2. 
        Console.WriteLine("\n2.___________\n");
        Console.WriteLine(string.Join(", ", data.OfType<Film>().SelectMany(f => f.Actors).Select(a => a.Name).Distinct()));

        // 3. 
        Console.WriteLine("\n3.___________\n");
        Console.WriteLine(data.OfType<Film>().SelectMany(f => f.Actors).Count(a => a.Birthdate.Month == 8));

        // 4. 
        Console.WriteLine("\n4.___________\n");
        Console.WriteLine(string.Join(", ", data.OfType<Film>().SelectMany(f => f.Actors).OrderBy(a => a.Birthdate).Take(2).Select(a => a.Name)));

        // 5.
        Console.WriteLine("\n5.___________\n");
        Console.WriteLine(string.Join(", ", data.OfType<Book>().GroupBy(b => b.Author).Select(g => $"{g.Key}: {g.Count()}")));

        // 6.
        Console.WriteLine("\n6.___________\n");
        Console.WriteLine(string.Join(", ", data.OfType<ArtObject>().GroupBy(a => a.Author).Select(g => $"{g.Key}: {g.Count()}")));

        // 7. 
        Console.WriteLine("\n7.___________\n");
        Console.WriteLine(data.OfType<Film>().SelectMany(f => f.Actors).SelectMany(a => a.Name).Distinct().Count());

        // 8.
        Console.WriteLine("\n8.___________\n");
        Console.WriteLine(string.Join(", ", data.OfType<Book>().OrderBy(b => b.Author).ThenBy(b => b.Pages).Select(b => b.Name)));

        // 9. 
        Console.WriteLine("\n9.___________\n");
        Console.WriteLine(string.Join("\n", data.OfType<Film>().SelectMany(f => f.Actors, (f, a) => new { a.Name, Film = f.Name })
            .GroupBy(x => x.Name)
            .Select(g => $"{g.Key}: {string.Join(", ", g.Select(f => f.Film))}")));

        // 10. 
        Console.WriteLine("\n10.___________\n");
        Console.WriteLine(data.OfType<Book>().Sum(b => b.Pages) + data.OfType<IEnumerable<int>>().SelectMany(i => i).Sum());

        // 11. 
        Console.WriteLine("\n11.___________\n");
        var bookDictionary = data.OfType<Book>().GroupBy(b => b.Author).ToDictionary(g => g.Key, g => g.Select(b => b.Name).ToList());
        Console.WriteLine(string.Join("\n", bookDictionary.Select(kvp => $"{kvp.Key}: {string.Join(", ", kvp.Value)}")));

        // 12.
        Console.WriteLine("\n12.___________\n");
        Console.WriteLine(string.Join(", ", data.OfType<Film>()
            .Where(f => f.Actors.Any(a => a.Name == "Matt Damon"))
            .Where(f => !f.Actors.Any(a => data.OfType<string>().Contains(a.Name)))
            .Select(f => f.Name)));
    }
}