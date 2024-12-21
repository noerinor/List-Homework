namespace CustomCollections.Interfaces
{
    public interface ICollection
    {
        int Count { get; }
        void Clear();
        bool Contains(object item);
        object[] ToArray();
    }
}
