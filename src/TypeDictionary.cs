// Copyright (c) Ben Adams. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Ben.Collections
{
    public class TypeDictionary<TValue> : IDictionary<Type, TValue>
    {
        // Chosen via benchmarking
        private const int DictCutOver = 28;

        // Types are in different array to Values to give higher cache density when searching
        private TypeKey[] _types;
        private ValueBox[] _values;

        private Dictionary<TypeKey, TValue> _dictionary;

        public TValue this[Type key]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var dictionary = _dictionary;
                if (dictionary != null)
                {
                    return dictionary[key];
                }

                if (_types == null)
                {
                    ThrowHelper.ThrowKeyNotFoundException(key);
                }

                return GetValue(key);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                var dictionary = _dictionary;
                if (dictionary != null)
                {
                    dictionary[key] = value;
                    Count = dictionary.Count;
                }
                else
                {
                    SetValue(key, in value);
                }
            }
        }

        private void SetValue(Type key, in TValue value)
        {
            var types = _types;
            if (types != null)
            {
                for (var i = 0; i < types.Length; i++)
                {
                    if (ReferenceEquals(types[i].Type, key))
                    {
                        _values[i] = value;
                        return;
                    }
                    else if (types[i].Type is null)
                    {
                        types[i] = key;
                        _values[i] = value;
                        return;
                    }
                }

                _dictionary = new Dictionary<TypeKey, TValue>(DictCutOver + 1);
                for (var i = 0; i < types.Length; i++)
                {
                    _dictionary[types[i]] = _values[i];
                }
                _dictionary[key] = value;
                _types = null;
                _values = null;
            }
            else
            {
                _types = new TypeKey[DictCutOver];
                _values = new ValueBox[DictCutOver];
                _types[0] = key;
                _values[0] = value;
            }
        }

        private TValue GetValue(Type key)
        {
            var types = _types;
            for (var i = 0; i < types.Length; i++)
            {
                if (ReferenceEquals(types[i].Type, key))
                {
                    return _values[i];
                }
                else if (types[i].Type is null)
                {
                    break;
                }
            }

            ThrowHelper.ThrowKeyNotFoundException(key);
            return default;
        }

        public int Count { get; private set; }

        public ICollection<Type> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => _dictionary.Values;

        public void Add(Type key, TValue value) => _dictionary.Add(key, value);

        public void Clear()
        {
            Count = 0;
            _types = null;
            _values = null;
            _dictionary?.Clear();
        }

        public bool ContainsKey(Type key) => _dictionary.ContainsKey(key);

        public bool Remove(Type key) => _dictionary.Remove(key);

        public bool TryGetValue(Type key, out TValue value) => _dictionary.TryGetValue(key, out value);

        Enumerator GetEnumerator() => new Enumerator(_dictionary.GetEnumerator());

        IEnumerator<KeyValuePair<Type, TValue>> IEnumerable<KeyValuePair<Type, TValue>>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool ICollection<KeyValuePair<Type, TValue>>.IsReadOnly => false;

        void ICollection<KeyValuePair<Type, TValue>>.Add(KeyValuePair<Type, TValue> item) => _dictionary.Add(item.Key, item.Value);

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
                    return new KeyValuePair<Type, TValue>(current.Key, current.Value);
                }
            }

            public bool MoveNext() => _enumerator.MoveNext();

            public void Dispose() { }

            object IEnumerator.Current => Current;
            void IEnumerator.Reset() => throw new NotSupportedException();
        }


        // For avoiding covariance checks in the Type array, 
        // and specializing and devirtualizing the Dictionary comparer based on Key
        internal readonly struct TypeKey : IEquatable<TypeKey>, IEquatable<Type>
        {
            public Type Type { get; }

            public TypeKey(Type type)
            {
                Type = type;
            }

            public bool Equals(TypeKey other) => ReferenceEquals(Type, other.Type);
            public bool Equals(Type type) => ReferenceEquals(Type, type);

            public override bool Equals(object obj)
            {
                if (obj is TypeKey key) return Equals(key);
                else if (obj is Type type) return ReferenceEquals(Type, type);
                return false;
            }

            public override int GetHashCode() => Type.GetHashCode();

            public static implicit operator TypeKey(Type value) => new TypeKey(value);
            public static implicit operator Type(TypeKey value) => value.Type;
        }

        // For avoiding covariance checks in the value array
        internal readonly struct ValueBox
        {
            public TValue Value { get; }

            public ValueBox(TValue value)
            {
                Value = value;
            }

            public static implicit operator ValueBox(TValue value) => new ValueBox(value);
            public static implicit operator TValue(ValueBox value) => value.Value;
        }
    }
}
