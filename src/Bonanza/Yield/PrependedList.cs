using System.Collections;
using System.Collections.Generic;

namespace Yield
{
    public class PrependedList<T>:IList<T>
    {
        private readonly T _alwaysPrepended;
        private readonly T _breakOn;
        private readonly List<T> _backingList=new List<T>(); 
        public PrependedList(T alwaysPrepended,T breakOn)
        {
            _alwaysPrepended = alwaysPrepended;
            _breakOn = breakOn;
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return _alwaysPrepended;
            foreach (var item in _backingList)
            {
                if(_breakOn.Equals(item)) yield break;
                yield return item;
            }
            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _backingList.Add(item);
        }

        public void Clear()
        {
            _backingList.Clear();
        }

        public bool Contains(T item)
        {
            return _backingList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _backingList.CopyTo(array,arrayIndex);
        }

        public bool Remove(T item)
        {
            throw new System.NotImplementedException();
        }

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }
        public int IndexOf(T item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public T this[int index]
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }
    }
}