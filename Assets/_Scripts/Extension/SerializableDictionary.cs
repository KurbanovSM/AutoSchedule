using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Core
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : IDictionary<TKey, TValue>,
                                                        ISerializationCallbackReceiver,
                                                        IReadOnlyDictionary<TKey, TValue>
    {
        [SerializeField]
        private List<KeyValuePair> _list = new List<KeyValuePair>();
 
        private Dictionary<TKey, TValue> _dict = new Dictionary<TKey, TValue>();
 
        private Dictionary<TKey, int> _indexByKey = new Dictionary<TKey, int>();
 
        // IDictionary
        public TValue this[TKey key]
        {
            get
            {
                if (!_dict.ContainsKey(key))
                {
                    Debug.LogError("There is no key in serializable dictionary");
                }
 
                return _dict[key];
            }
            set
            {
                _dict[key] = value;
 
                if (_indexByKey.ContainsKey(key))
                {
                    var index = _indexByKey[key];
                    _list[index] = new KeyValuePair(key, value);
                }
                else
                {
                    _list.Add(new KeyValuePair(key, value));
                    _indexByKey.Add(key, _list.Count - 1);
                }
            }
        }
 
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;
 
        public ICollection<TKey> Keys => _dict.Keys;
        public ICollection<TValue> Values => _dict.Values;
 
        public void Add(TKey key, TValue value)
        {
            _dict.Add(key, value);
            _list.Add(new KeyValuePair(key, value));
            _indexByKey.Add(key, _list.Count - 1);
        }
 
        public bool ContainsKey(TKey key)
        {
            return _dict.ContainsKey(key);
        }
 
        public bool Remove(TKey key)
        {
            if (_dict.Remove(key))
            {
                var index = _indexByKey[key];
                _list.RemoveAt(index);
                _indexByKey.Remove(key);
 
                return true;
            }
 
            return false;
        }
 
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dict.TryGetValue(key, out value);
        }
 
        // ICollection
        public int Count => _dict.Count;
        public bool IsReadOnly { get; set; }
 
        public void Add(KeyValuePair<TKey, TValue> pair)
        {
            Add(pair.Key, pair.Value);
        }
 
        public void Clear()
        {
            _dict.Clear();
            _list.Clear();
            _indexByKey.Clear();
        }
 
        public bool Contains(KeyValuePair<TKey, TValue> pair)
        {
            TValue value;
 
            if (_dict.TryGetValue(pair.Key, out value))
            {
                return EqualityComparer<TValue>.Default.Equals(value, pair.Value);
            }
 
            return false;
        }
 
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentException("The array cannot be null.");
            }
 
            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("The starting array index cannot be negative.");
            }
 
            if (array.Length - arrayIndex < _dict.Count)
            {
                throw new ArgumentException("The destination array has fewer elements than the collection.");
            }
 
            foreach (var pair in _dict)
            {
                array[arrayIndex] = pair;
                arrayIndex++;
            }
        }
 
        public bool Remove(KeyValuePair<TKey, TValue> pair)
        {
            TValue value;
 
            if (_dict.TryGetValue(pair.Key, out value))
            {
                var valueMatch = EqualityComparer<TValue>.Default.Equals(value, pair.Value);
 
                if (valueMatch)
                {
                    return Remove(pair.Key);
                }
            }
 
            return false;
        }
 
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dict.GetEnumerator();
        }
 
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }
 
        // Since lists can be serialized natively by unity no custom implementation is needed
        public void OnBeforeSerialize()
        { }
 
        // Fill dictionary with list pairs and flag key-collisions.
        public void OnAfterDeserialize()
        {
            _dict.Clear();
            _indexByKey.Clear();
 
            for (var i = 0; i < _list.Count; i++)
            {
                var key = _list[i].Key;
 
                if (key != null && !ContainsKey(key))
                {
                    _dict.Add(key, _list[i].Value);
                    _indexByKey.Add(key, i);
                }
            }
        }
 
        // Serializable KeyValuePair struct
        [Serializable]
        private struct KeyValuePair
        {
            public TKey Key;
            public TValue Value;
 
            public KeyValuePair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}
