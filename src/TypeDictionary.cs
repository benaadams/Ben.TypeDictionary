// Copyright (c) Ben Adams. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;

namespace Ben.Collections.TypeDictionary
{
    internal class TypeDictionary<TValue> : IDictionary<Type, TValue>
    {
        private Dictionary<TypeKey, TValue> _dictionary = new Dictionary<TypeKey, TValue>();

        public TValue this[Type key]
        {
            get => _dictionary[new TypeKey(key)];
            set => _dictionary[new TypeKey(key)] = value;
        }

        public int Count => _dictionary.Count;

        public ICollection<Type> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => _dictionary.Values;

        public void Add(Type key, TValue value) => _dictionary.Add(new TypeKey(key), value);

        public void Clear() => _dictionary.Clear();

        public bool ContainsKey(Type key) => _dictionary.ContainsKey(new TypeKey(key));

        public bool Remove(Type key) => _dictionary.Remove(new TypeKey(key));

        public bool TryGetValue(Type key, out TValue value) => _dictionary.TryGetValue(new TypeKey(key), out value);

        Enumerator GetEnumerator() => new Enumerator(_dictionary.GetEnumerator());

        IEnumerator<KeyValuePair<Type, TValue>> IEnumerable<KeyValuePair<Type, TValue>>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool ICollection<KeyValuePair<Type, TValue>>.IsReadOnly => false;

        void ICollection<KeyValuePair<Type, TValue>>.Add(KeyValuePair<Type, TValue> item) => _dictionary.Add(new TypeKey(item.Key), item.Value);

        bool ICollection<KeyValuePair<Type, TValue>>.Contains(KeyValuePair<Type, TValue> item)
        {
            throw new NotImplementedException();
        }

        void ICollection<KeyValuePair<Type, TValue>>.CopyTo(KeyValuePair<Type, TValue>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<Type, TValue>>.Remove(KeyValuePair<Type, TValue> item)
        {
            throw new NotImplementedException();
        }

        public struct Enumerator : IEnumerator<KeyValuePair<Type, TValue>>
        {
            private Dictionary<TypeKey, TValue>.Enumerator _enumerator;

            internal Enumerator(Dictionary<TypeKey, TValue>.Enumerator enumerator)
            {
                _enumerator = enumerator;
            }

            public KeyValuePair<Type, TValue> Current
            {
                get
                {
                    var current = _enumerator.Current;
                    return new KeyValuePair<Type, TValue>(current.Key.Type, current.Value);
                }
            }

            public bool MoveNext() => _enumerator.MoveNext();

            public void Dispose()
            { }

            object IEnumerator.Current => Current;
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }
        }

        internal readonly struct TypeKey : IEquatable<TypeKey>
        {
            public Type Type { get; }

            public TypeKey(Type type)
            {
                Type = type;
            }

            public bool Equals(TypeKey other) => ReferenceEquals(Type, other.Type);

            public override bool Equals(object obj)
            {
                if (obj is TypeKey key) return Equals(key);
                else if (obj is Type type) return ReferenceEquals(Type, type);
                return false;
            }

            public override int GetHashCode() => Type.GetHashCode();
        }
    }
}
