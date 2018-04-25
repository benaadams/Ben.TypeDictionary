# Ben.TypeDictionary


Accessing all values via `Type` key, with `Items` count in the Dictionary:
```
                  Method | Items |          Mean | Mean per Item | Op/s per Item | Scaled |
------------------------ |------ |--------------:|--------------:|--------------:|-------:|
 Dictionary<Type,TValue> |     1 |     21.447 ns |     21.447 ns |  46,625,969.0 |   1.00 |
  TypeDictionary<TValue> |     1 |      4.016 ns |      4.016 ns | 248,998,793.3 |   0.19 |
                         |       |               |               |               |        |
 Dictionary<Type,TValue> |     2 |     41.736 ns |     20.868 ns |  47,919,976.8 |   1.00 |
  TypeDictionary<TValue> |     2 |      8.007 ns |      4.004 ns | 249,776,709.5 |   0.19 |
                         |       |               |               |               |        |
 Dictionary<Type,TValue> |     3 |     62.205 ns |     20.735 ns |  48,227,723.8 |   1.00 |
  TypeDictionary<TValue> |     3 |     11.288 ns |      3.763 ns | 265,777,999.8 |   0.18 |
                         |       |               |               |               |        |
 Dictionary<Type,TValue> |     4 |     79.518 ns |     19.879 ns |  50,303,250.3 |   1.00 |
  TypeDictionary<TValue> |     4 |     16.135 ns |      4.034 ns | 247,914,056.0 |   0.20 |
                         |       |               |               |               |        |
 Dictionary<Type,TValue> |     6 |    117.227 ns |    19.5379 ns |  51,182,607.9 |   1.00 |
  TypeDictionary<TValue> |     6 |     27.608 ns |     4.6013 ns | 217,330,166.7 |   0.24 |
                         |       |               |               |               |        |
 Dictionary<Type,TValue> |     8 |    158.937 ns |    19.8671 ns |  50,334,438.1 |   1.00 |
  TypeDictionary<TValue> |     8 |     41.634 ns |     5.2043 ns | 192,148,661.2 |   0.26 |
                         |       |               |               |               |        |
 Dictionary<Type,TValue> |    16 |    338.240 ns |    21.1400 ns |  47,303,651.6 |   1.00 |
  TypeDictionary<TValue> |    16 |    117.981 ns |     7.3738 ns | 135,614,654.8 |   0.35 |
                         |       |               |               |               |        |
 Dictionary<Type,TValue> |    24 |    507.128 ns |    21.1303 ns |  47,325,304.4 |   1.00 |
  TypeDictionary<TValue> |    24 |    234.056 ns |     9.7523 ns | 102,539,516.2 |   0.46 |
                         |       |               |               |               |        |
 Dictionary<Type,TValue> |    32 |    685.034 ns |    21.4073 ns |  46,713,042.7 |   1.00 |
  TypeDictionary<TValue> |    32 |    380.553 ns |    11.8923 ns |  84,088,172.8 |   0.56 |
                         |       |               |               |               |        |
 Dictionary<Type,TValue> |    40 |    871.863 ns |    21.7966 ns |  45,878,785.6 |   1.00 |
  TypeDictionary<TValue> |    40 |    548.862 ns |    13.7216 ns |  72,878,016.6 |   0.63 |
                         |       |               |               |               |        |
 Dictionary<Type,TValue> |    64 |  1,358.290 ns |    21.2233 ns |  47,118,060.1 |   1.00 |
  TypeDictionary<TValue> |    64 |    861.528 ns |    13.4614 ns |  74,286,629.3 |   0.63 |
                         |       |               |               |               |        |
 Dictionary<Type,TValue> |   128 |  2,961.686 ns |    23.1382 ns |  43,218,621.9 |   1.00 |
  TypeDictionary<TValue> |   128 |  1,738.284 ns |    13.5803 ns |  73,635,839.8 |   0.59 |
                         |       |               |               |               |        |
 Dictionary<Type,TValue> |   256 |  5,694.685 ns |    22.2449 ns |  44,954,199.3 |   1.00 |
  TypeDictionary<TValue> |   256 |  3,768.346 ns |    14.7201 ns |  67,934,322.8 |   0.66 |
                         |       |               |               |               |        |
 Dictionary<Type,TValue> |  1024 | 23,323.023 ns |    22.7764 ns |  43,905,113.8 |   1.00 |
  TypeDictionary<TValue> |  1024 | 16,331.561 ns |    15.9488 ns |  62,700,679.4 |   0.70 |

```