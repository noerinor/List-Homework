namespace CustomCollections.Interfaces;

public interface IList<T> : ICollection<T>
{
    T this[int index] { get; set; }
    void Add(T item);
    void Insert(int index, T item);
    void RemoveAt(int index);
    void Remove(T item);
    int IndexOf(T item);
    void Reverse();
}