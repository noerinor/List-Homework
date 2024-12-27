using CustomCollections.Interfaces;
using System;

namespace CustomCollections.Interfaces
{
    public class ObservableList<T> : List<T>
    {
        public event EventHandler<ItemChangedEventArgs<T>> ItemAdded;
        public event EventHandler<ItemChangedEventArgs<T>> ItemInserted;
        public event EventHandler<ItemChangedEventArgs<T>> ItemRemoved;

        public new void Add(T item)
        {
            base.Add(item);
            ItemAdded?.Invoke(this, new ItemChangedEventArgs<T>(item));
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            ItemInserted?.Invoke(this, new ItemChangedEventArgs<T>(item));
        }

        public new void Remove(T item)
        {
            base.Remove(item);
            ItemRemoved?.Invoke(this, new ItemChangedEventArgs<T>(item));
        }
    }

    public class ItemChangedEventArgs<T> : EventArgs
    {
        public T Item { get; }

        public ItemChangedEventArgs(T item)
        {
            Item = item;
        }
    }
}
