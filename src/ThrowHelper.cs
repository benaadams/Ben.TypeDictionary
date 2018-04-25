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

        private static KeyNotFoundException GetKeyNotFoundException(Type key)
        {
            return new KeyNotFoundException($"The given key '{key.ToString()}' was not present in the dictionary.");
        }
    }
}
