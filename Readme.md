 ##  Utf8Json.Extension 
This extension for library Utf8Json. Utf8Json is fast serialization library with good performance but without some functions (for example case insensitive enums).

EnumCaseIgnore resolver
---
EnumCaseIgnore resolver is sligtly modified and upgraded standart Utf8Json EnumResolver with the same perfomance as original (see benchmarks). It uses same hash function to quickly compare bytes (see FarmHash.cs file, copied from original repository, because it's internal there). 
Can serialize enums values with origin, camelcase, snakecase options.
Can deserialize case insensetive enums values.

Benchmarks
---
[EnumCaseIgnore Serialization benckmark](assets/Utf8Json.Extensions.Benchmark.SerializeEnumCaseIgnoreBenchmark-report-github.md)

[EnumCaseIgnore Deserialization benchmark](assets/Utf8Json.Extensions.Benchmark.DeserializeEnumCaseIgnoreBenchmark-report-github.md)


License
----

MIT



