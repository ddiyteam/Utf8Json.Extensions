``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 8.1 (6.3.9600.0)
Intel Xeon CPU E5-2660 v2 2.20GHz, 2 CPU, 16 logical and 16 physical cores
.NET Core SDK=3.1.101
  [Host]     : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT
  DefaultJob : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT


```
|                            Method |      Mean |     Error |    StdDev |    Median | Gen 0 | Gen 1 | Gen 2 |  Allocated |
|---------------------------------- |----------:|----------:|----------:|----------:|------:|------:|------:|-----------:|
|              Utf8Json_EnumDefault |  2.447 ms | 0.2507 ms | 0.7392 ms |  2.332 ms |     - |     - |     - |     375 KB |
|    Utf8Json_EnumCaseIgnoreDefault |  2.973 ms | 0.3274 ms | 0.9603 ms |  3.128 ms |     - |     - |     - |  320.31 KB |
|            NewtonSoft_EnumDefault | 11.929 ms | 1.5886 ms | 4.6839 ms | 10.247 ms |     - |     - |     - | 2015.63 KB |
|            Utf8Json_EnumCamelCase |  2.970 ms | 0.2387 ms | 0.6926 ms |  3.032 ms |     - |     - |     - |     375 KB |
|  Utf8Json_EnumCaseIgnoreCamelCase |  2.795 ms | 0.2350 ms | 0.6819 ms |  2.737 ms |     - |     - |     - |     375 KB |
|          NewtonSoft_EnumCamelCase | 11.808 ms | 1.2062 ms | 3.5187 ms | 10.563 ms |     - |     - |     - | 2015.63 KB |
|            Utf8Json_EnumSnakeCase |  2.878 ms | 0.2675 ms | 0.7847 ms |  2.655 ms |     - |     - |     - |  382.81 KB |
|  Utf8Json_EnumCaseIgnoreSnakeCase |  3.350 ms | 0.0358 ms | 0.0299 ms |  3.348 ms |     - |     - |     - |  382.81 KB |
|          NewtonSoft_EnumSnakeCase | 13.577 ms | 1.8951 ms | 5.5579 ms | 10.889 ms |     - |     - |     - | 2023.44 KB |
|           Utf8Json_EnumUnderlying |  3.238 ms | 0.4585 ms | 1.3520 ms |  3.372 ms |     - |     - |     - |  289.06 KB |
| Utf8Json_EnumCaseIgnoreUnderlying |  3.129 ms | 0.4099 ms | 1.2086 ms |  3.427 ms |     - |     - |     - |  289.06 KB |
|         NewtonSoft_EnumUnderlying | 11.167 ms | 0.7321 ms | 1.9540 ms | 10.961 ms |     - |     - |     - | 2015.63 KB |
