namespace CustomCollections.Interfaces;

public interface ICollection<T> : IEnumerable<T>
{
    int Count { get; }
    void Clear();
    bool Contains(T item);
    T[] ToArray();
}