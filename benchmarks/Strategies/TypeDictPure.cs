using System;
using System.Collections.Generic;

namespace Ben.Collections.TypeDictionary.Benchmarks
{
    public class TypeDictPure<TValue>
    {
        private Dictionary<TypeKey, TValue> _dictionary = new Dictionary<TypeKey, TValue>();

        public TValue this[Type key]
        {
            get
            {
                return _dictionary[key];
            }

            set
            {
                _dictionary[key] = value;
            }
        }

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
