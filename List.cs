using CustomCollections.Interfaces;

namespace CustomCollections
{
    public class List : IList
    {
        private object[] _items;
        private int _count;

        public List()
        {
            _items = new object[4];
            _count = 0;
        }

        public int Count => _count;

        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));
                _items[index] = value;
            }
        }

        public void Add(object item)
        {
            if (_count == _items.Length)
                Resize();
            _items[_count++] = item;
        }

        public void Clear()
        {
            _items = new object[4];
            _count = 0;
        }

        public bool Contains(object item)
        {
            for (int i = 0; i < _count; i++)
                if (_items[i].Equals(item)) return true;
            return false;
        }

        public int IndexOf(object item)
        {
            for (int i = 0; i < _count; i++)
                if (_items[i].Equals(item)) return i;
            return -1;
        }

        public void Insert(int index, object item)
        {
            if (index < 0 || index > _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (_count == _items.Length)
                Resize();

            for (int i = _count; i > index; i--)
                _items[i] = _items[i - 1];

            _items[index] = item;
            _count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            for (int i = index; i < _count - 1; i++)
                _items[i] = _items[i + 1];

            _items[--_count] = null;
        }

        public void Remove(object item)
        {
            int index = IndexOf(item);
            if (index != -1)
                RemoveAt(index);
        }

        public void Reverse()
        {
            for (int i = 0; i < _count / 2; i++)
            {
                object temp = _items[i];
                _items[i] = _items[_count - i - 1];
                _items[_count - i - 1] = temp;
            }
        }

        public object[] ToArray()
        {
            object[] result = new object[_count];
            for (int i = 0; i < _count; i++)
                result[i] = _items[i];
            return result;
        }

        private void Resize()
        {
            object[] newArray = new object[_items.Length * 2];
            for (int i = 0; i < _items.Length; i++)
                newArray[i] = _items[i];
            _items = newArray;
        }
    }
}
