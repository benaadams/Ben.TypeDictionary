using System;
using System.Collections.Generic;
using System.Text;

namespace Ben.Collections
{
    // For avoiding covariance checks in the value array
    internal readonly struct ValueBox<TValue>
    {
        public TValue Value { get; }

        public ValueBox(TValue value)
        {
            Value = value;
        }

        public static implicit operator ValueBox<TValue>(TValue value) => new ValueBox<TValue>(value);
        public static implicit operator TValue(ValueBox<TValue> value) => value.Value;
    }
}
