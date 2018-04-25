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

        public int Count { get; private set; }

        public TypeDictionary() { }

        public TypeDictionary(int capacity)
        {
            if (capacity < 0)
                ThrowHelper.ThrowArgumentOutOfRangeException_Capacity();

            if (capacity == 0) return;

            if (capacity < DictCutOver)
            {
                _types = new TypeKey[capacity];
                _values = new ValueBox[capacity];
            }
            else
            {
                _dictionary = new Dictionary<TypeKey, TValue>(capacity);
            }
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(Type key, out TValue value)
        {
            var dictionary = _dictionary;
            if (dictionary != null)
            {
                return dictionary.TryGetValue(key, out value);
            }

            if (_types == null)
            {
                value = default;
                return false;
            }
            else
            {
                return TryGetArrayValue(key, out value);
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

        private bool TryGetArrayValue(Type key, out TValue value)
        {
            var types = _types;
            for (var i = 0; i < types.Length; i++)
            {
                if (ReferenceEquals(types[i].Type, key))
                {
                    value = _values[i];
                    return true;
                }
                else if (types[i].Type is null)
                {
                    break;
                }
            }

            value = default;
            return false;
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

        public void Add(Type key, TValue value) => throw new NotImplementedException(); //  => _dictionary.Add(key, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(Type key)
        {
            var dictionary = _dictionary;
            if (dictionary != null)
            {
                return dictionary.ContainsKey(key);
            }

            return (_types == null) ? false : ArrayContainsKey(key);
        }

        private bool ArrayContainsKey(Type key)
        {
            var types = _types;
            for (var i = 0; i < types.Length; i++)
            {
                if (ReferenceEquals(types[i].Type, key))
                {
                    return true;
                }
                else if (types[i].Type is null)
                {
                    break;
                }
            }

            return false;
        }

        public void Clear()
        {
            Count = 0;
            _types = null;
            _values = null;
            _dictionary?.Clear();
        }

        public ICollection<Type> Keys => throw new NotImplementedException();

        public ICollection<TValue> Values => throw new NotImplementedException();

        public bool Remove(Type key) => throw new NotImplementedException();//  => _dictionary.Remove(key);

        Enumerator GetEnumerator() => throw new NotImplementedException();// => new Enumerator(_dictionary.GetEnumerator());

        IEnumerator<KeyValuePair<Type, TValue>> IEnumerable<KeyValuePair<Type, TValue>>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool ICollection<KeyValuePair<Type, TValue>>.IsReadOnly => false;

        void ICollection<KeyValuePair<Type, TValue>>.Add(KeyValuePair<Type, TValue> item) => Add(item.Key, item.Value);

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
