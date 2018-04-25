# Ben.TypeDictionary


Accessing value via `Type` key, with `Items` count in the Dictionary:
```
                     Method | Items |         Mean | Op/s per Item | Scaled |
--------------------------- |------ |-------------:|--------------:|-------:|
 'Dictionary<Type, TValue>' |     1 |    21.016 ns |  47,583,523.2 |   1.00 |
   'TypeDictionary<TValue>' |     1 |     3.681 ns | 271,633,954.9 |   0.18 |
                            |       |              |               |        |
 'Dictionary<Type, TValue>' |     2 |    41.695 ns |  47,966,990.8 |   1.00 |
   'TypeDictionary<TValue>' |     2 |     8.274 ns | 241,708,221.4 |   0.20 |
                            |       |              |               |        |
 'Dictionary<Type, TValue>' |     4 |    79.542 ns |  50,287,931.0 |   1.00 |
   'TypeDictionary<TValue>' |     4 |    16.226 ns | 246,514,281.9 |   0.20 |
                            |       |              |               |        |
 'Dictionary<Type, TValue>' |     8 |   159.000 ns |  50,314,576.1 |   1.00 |
   'TypeDictionary<TValue>' |     8 |    41.708 ns | 191,808,792.3 |   0.26 |
                            |       |              |               |        |
 'Dictionary<Type, TValue>' |    16 |   337.870 ns |  47,355,492.4 |   1.00 |
   'TypeDictionary<TValue>' |    16 |   118.004 ns | 135,588,069.6 |   0.35 |
                            |       |              |               |        |
 'Dictionary<Type, TValue>' |    32 |   678.774 ns |  47,143,846.6 |   1.00 |
   'TypeDictionary<TValue>' |    32 |   378.094 ns |  84,635,054.9 |   0.56 |
                            |       |              |               |        |
 'Dictionary<Type, TValue>' |    64 | 1,361.253 ns |  47,015,519.7 |   1.00 |
   'TypeDictionary<TValue>' |    64 |   857.433 ns |  74,641,364.0 |   0.63 |
                            |       |              |               |        |
 'Dictionary<Type, TValue>' |   128 | 2,929.467 ns |  43,693,952.5 |   1.00 |
   'TypeDictionary<TValue>' |   128 | 1,738.428 ns |  73,629,751.8 |   0.59 |
                            |       |              |               |        |
 'Dictionary<Type, TValue>' |   256 | 5,712.671 ns |  44,812,661.5 |   1.00 |
   'TypeDictionary<TValue>' |   256 | 3,697.099 ns |  69,243,472.2 |   0.65 |
```