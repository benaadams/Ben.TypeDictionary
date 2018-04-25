# Ben.TypeDictionary


Accessing value via `Type` key, with `Items` count in the Dictionary:
```
                  Method | Items | Mean per Item | Op/s per Item | Scaled |
------------------------ |------ |--------------:|--------------:|-------:|
 Dictionary<Type,TValue> |     1 |     22.263 ns |  44,917,152.9 |   1.00 |
  TypeDictionary<TValue> |     1 |      3.749 ns | 266,763,619.9 |   0.17 |
                         |       |               |               |        |
 Dictionary<Type,TValue> |     2 |     20.909 ns |  47,826,624.3 |   1.00 |
  TypeDictionary<TValue> |     2 |      3.746 ns | 266,983,850.6 |   0.18 |
                         |       |               |               |        |
 Dictionary<Type,TValue> |     3 |     20.209 ns |  49,482,575.8 |   1.00 |
  TypeDictionary<TValue> |     3 |      3.743 ns | 267,148,815.1 |   0.19 |
                         |       |               |               |        |
 Dictionary<Type,TValue> |     4 |    19.8104 ns |  50,478,605.9 |   1.00 |
  TypeDictionary<TValue> |     4 |     4.5568 ns | 219,452,112.8 |   0.23 |
                         |       |               |               |        |
 Dictionary<Type,TValue> |     6 |    20.3109 ns |  49,234,750.9 |   1.00 |
  TypeDictionary<TValue> |     6 |     5.4173 ns | 184,593,799.1 |   0.27 |
                         |       |               |               |        |
 Dictionary<Type,TValue> |     8 |    19.8627 ns |  50,345,601.1 |   1.00 |
  TypeDictionary<TValue> |     8 |     6.0230 ns | 166,030,629.2 |   0.30 |
                         |       |               |               |        |
 Dictionary<Type,TValue> |    16 |    21.2779 ns |  46,997,088.8 |   1.00 |
  TypeDictionary<TValue> |    16 |     8.3958 ns | 119,106,709.9 |   0.39 |
                         |       |               |               |        |
 Dictionary<Type,TValue> |    24 |    21.2639 ns |  47,027,981.8 |   1.00 |
  TypeDictionary<TValue> |    24 |    11.5368 ns |  86,679,300.0 |   0.54 |
                         |       |               |               |        |
 Dictionary<Type,TValue> |    32 |    21.3640 ns |  46,807,635.9 |   1.00 |
  TypeDictionary<TValue> |    32 |    12.2169 ns |  81,853,874.1 |   0.57 |
                         |       |               |               |        |
 Dictionary<Type,TValue> |    40 |    22.1265 ns |  45,194,673.9 |   1.00 |
  TypeDictionary<TValue> |    40 |    13.7770 ns |  72,584,911.2 |   0.62 |
                         |       |               |               |        |
 Dictionary<Type,TValue> |    64 |    21.0193 ns |  47,575,431.3 |   1.00 |
  TypeDictionary<TValue> |    64 |    14.1461 ns |  70,690,842.4 |   0.67 |
                         |       |               |               |        |
 Dictionary<Type,TValue> |   128 |    23.1219 ns |  43,249,123.8 |   1.00 |
  TypeDictionary<TValue> |   128 |    14.9139 ns |  67,051,633.8 |   0.65 |
                         |       |               |               |        |
 Dictionary<Type,TValue> |   256 |    22.2625 ns |  44,918,570.5 |   1.00 |
  TypeDictionary<TValue> |   256 |    15.9900 ns |  62,538,893.7 |   0.72 |
```