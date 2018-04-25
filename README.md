# Ben.TypeDictionary


Accessing all values via `Type` key, with `Items` count in the Dictionary:
```
                  Method | Items |         Mean | Mean per Item | Op/s per Item | Scaled |
------------------------ |------ |-------------:|--------------:|--------------:|-------:|
 Dictionary<Type,TValue> |     1 |    21.081 ns |     21.081 ns |  47,435,010.6 |   1.00 |
  TypeDictionary<TValue> |     1 |     3.906 ns |      3.906 ns | 256,004,395.4 |   0.19 |
                         |       |              |               |               |        |
 Dictionary<Type,TValue> |     2 |    42.073 ns |     21.036 ns |  47,536,563.4 |   1.00 |
  TypeDictionary<TValue> |     2 |     7.803 ns |      3.901 ns | 256,317,782.4 |   0.19 |
                         |       |              |               |               |        |
 Dictionary<Type,TValue> |     3 |    62.721 ns |     20.907 ns |  47,831,197.5 |   1.00 |
  TypeDictionary<TValue> |     3 |    12.426 ns |      4.142 ns | 241,437,039.2 |   0.20 |
                         |       |              |               |               |        |
 Dictionary<Type,TValue> |     4 |    79.451 ns |    19.8626 ns |  50,345,750.6 |   1.00 |
  TypeDictionary<TValue> |     4 |    16.232 ns |     4.0581 ns | 246,422,115.0 |   0.20 |
                         |       |              |               |               |        |
 Dictionary<Type,TValue> |     6 |   117.593 ns |    19.5989 ns |  51,023,367.7 |   1.00 |
  TypeDictionary<TValue> |     6 |    27.731 ns |     4.6219 ns | 216,360,500.9 |   0.24 |
                         |       |              |               |               |        |
 Dictionary<Type,TValue> |     8 |   161.165 ns |    20.1456 ns |  49,638,596.5 |   1.00 |
  TypeDictionary<TValue> |     8 |    41.676 ns |     5.2095 ns | 191,956,731.5 |   0.26 |
                         |       |              |               |               |        |
 Dictionary<Type,TValue> |    16 |   353.556 ns |    22.0973 ns |  45,254,488.6 |   1.00 |
  TypeDictionary<TValue> |    16 |   117.982 ns |     7.3739 ns | 135,613,810.7 |   0.33 |
                         |       |              |               |               |        |
 Dictionary<Type,TValue> |    24 |   511.589 ns |    21.3162 ns |  46,912,665.3 |   1.00 |
  TypeDictionary<TValue> |    24 |   263.217 ns |    10.9674 ns |  91,179,622.1 |   0.51 |
                         |       |              |               |               |        |
 Dictionary<Type,TValue> |    32 |   707.041 ns |    22.0950 ns |  45,259,043.5 |   1.00 |
  TypeDictionary<TValue> |    32 |   381.443 ns |    11.9201 ns |  83,892,032.2 |   0.54 |
                         |       |              |               |               |        |
 Dictionary<Type,TValue> |    40 |   856.099 ns |    21.4025 ns |  46,723,582.1 |   1.00 |
  TypeDictionary<TValue> |    40 |   565.885 ns |    14.1471 ns |  70,685,708.6 |   0.66 |
                         |       |              |               |               |        |
 Dictionary<Type,TValue> |    64 | 1,332.218 ns |    20.8159 ns |  48,040,179.7 |   1.00 |
  TypeDictionary<TValue> |    64 |   872.251 ns |    13.6289 ns |  73,373,340.4 |   0.65 |
                         |       |              |               |               |        |
 Dictionary<Type,TValue> |   128 | 2,903.867 ns |    22.6865 ns |  44,079,158.3 |   1.00 |
  TypeDictionary<TValue> |   128 | 1,736.581 ns |    13.5670 ns |  73,708,036.4 |   0.60 |
                         |       |              |               |               |        |
 Dictionary<Type,TValue> |   256 | 5,721.472 ns |    22.3495 ns |  44,743,727.1 |   1.00 |
  TypeDictionary<TValue> |   256 | 3,793.415 ns |    14.8180 ns |  67,485,364.9 |   0.66 |
```