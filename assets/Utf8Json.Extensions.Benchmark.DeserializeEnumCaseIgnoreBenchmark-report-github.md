``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 8.1 (6.3.9600.0)
Intel Xeon CPU E5-2660 v2 2.20GHz, 2 CPU, 16 logical and 16 physical cores
.NET Core SDK=3.1.101
  [Host]     : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT
  DefaultJob : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT


```
|                            Method |      Mean |     Error |    StdDev |    Median |   Gen 0 | Gen 1 | Gen 2 |  Allocated |
|---------------------------------- |----------:|----------:|----------:|----------:|--------:|------:|------:|-----------:|
|              Utf8Json_EnumDefault |  5.243 ms | 0.4739 ms | 1.3899 ms |  4.535 ms |       - |     - |     - |   437.5 KB |
|    Utf8Json_EnumCaseIgnoreDefault |  5.462 ms | 0.4706 ms | 1.3800 ms |  4.741 ms |       - |     - |     - |   437.5 KB |
|            NewtonSoft_EnumDefault | 18.696 ms | 0.5145 ms | 1.4171 ms | 19.054 ms |       - |     - |     - |    3375 KB |
|            Utf8Json_EnumCamelCase |  7.362 ms | 0.0748 ms | 0.0584 ms |  7.350 ms |       - |     - |     - |   437.5 KB |
|  Utf8Json_EnumCaseIgnoreCamelCase |  4.232 ms | 0.1573 ms | 0.4565 ms |  4.334 ms | 31.2500 |     - |     - |   437.5 KB |
|          NewtonSoft_EnumCamelCase | 19.483 ms | 0.3536 ms | 0.9064 ms | 19.320 ms |       - |     - |     - |    3375 KB |
|            Utf8Json_EnumSnakeCase |  4.903 ms | 0.3560 ms | 1.0273 ms |  4.555 ms |       - |     - |     - |   437.5 KB |
|  Utf8Json_EnumCaseIgnoreSnakeCase |  4.551 ms | 0.2106 ms | 0.5657 ms |  4.225 ms |       - |     - |     - |   437.5 KB |
|          NewtonSoft_EnumSnakeCase | 20.957 ms | 1.2691 ms | 3.7020 ms | 18.990 ms |       - |     - |     - |    3375 KB |
|           Utf8Json_EnumUnderlying |  6.858 ms | 0.6073 ms | 1.7715 ms |  5.798 ms |       - |     - |     - |  539.36 KB |
| Utf8Json_EnumCaseIgnoreUnderlying |  6.760 ms | 0.0796 ms | 0.0621 ms |  6.763 ms |       - |     - |     - |  398.44 KB |
|         NewtonSoft_EnumUnderlying | 21.924 ms | 0.4381 ms | 1.0153 ms | 21.672 ms |       - |     - |     - | 3414.06 KB |
