using System;
using System.Collections.Generic;
using System.Text;

namespace Ben.Collections
{
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
}
