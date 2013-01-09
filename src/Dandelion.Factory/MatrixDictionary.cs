using System;
using System.Collections;
using System.Collections.Generic;

namespace Dandelion.Factory
{
    internal class MatrixDictionary<T, T1, T2> : IDictionary<T, IDictionary<T1, ICollection<T2>>>
    {
        private readonly IDictionary<T, IDictionary<T1, ICollection<T2>>> _dictionary = new Dictionary<T, IDictionary<T1, ICollection<T2>>>();
        public IEnumerator<KeyValuePair<T, IDictionary<T1, ICollection<T2>>>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<T, IDictionary<T1, ICollection<T2>>> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<T, IDictionary<T1, ICollection<T2>>> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<T, IDictionary<T1, ICollection<T2>>>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<T, IDictionary<T1, ICollection<T2>>> item)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool ContainsKey(T key)
        {
            throw new NotImplementedException();
        }

        public void Add(T key, IDictionary<T1, ICollection<T2>> value)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(T key, out IDictionary<T1, ICollection<T2>> value)
        {
            throw new NotImplementedException();
        }

        public IDictionary<T1, ICollection<T2>> this[T key]
        {
            get
            {
                if (!_dictionary.ContainsKey(key)) return new Dictionary<T1, ICollection<T2>>();
                return _dictionary[key];
            }
            set { throw new NotImplementedException(); }
        }
        public void Add(T key, T1 key2, T2 value)
        {
            this[key, key2].Add(value);
        }
        public ICollection<T2> this[T key, T1 key2]
        {
            get
            {
                if (!_dictionary.ContainsKey(key)) _dictionary[key] = new Dictionary<T1, ICollection<T2>>();
                if (!_dictionary[key].ContainsKey(key2)) _dictionary[key][key2] = new List<T2>();
                return _dictionary[key][key2];
            }
        }
        public ICollection<T> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public ICollection<IDictionary<T1, ICollection<T2>>> Values
        {
            get { throw new NotImplementedException(); }
        }
    }
}