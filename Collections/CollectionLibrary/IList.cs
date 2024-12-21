namespace CustomCollections.Interfaces
{
    public interface IList : ICollection
    {
        object this[int index] { get; set; }
        void Add(object item);
        void Insert(int index, object item);
        void RemoveAt(int index);
        void Remove(object item);
        int IndexOf(object item);
        void Reverse();
    }
}