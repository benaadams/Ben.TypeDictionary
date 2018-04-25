// Copyright (c) Ben Adams. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Ben.Collections
{
    internal static class ThrowHelper
    {
        internal static void ThrowKeyNotFoundException(Type key)
        {
            throw GetKeyNotFoundException(key);
        }

        internal static void ThrowArgumentOutOfRangeException_Capacity()
        {
            throw GetArgumentOutOfRangeException_Capacity();
        }

        private static ArgumentOutOfRangeException GetArgumentOutOfRangeException_Capacity()
        {
            return new ArgumentOutOfRangeException("capacity is less than 0.");
        }

        private static KeyNotFoundException GetKeyNotFoundException(Type key)
        {
            return new KeyNotFoundException($"The given key '{key.ToString()}' was not present in the dictionary.");
        }
    }
}
