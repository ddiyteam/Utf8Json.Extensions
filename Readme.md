 ##  Utf8Json.Extension 
This extension for Utf8Json library. Utf8Json is fast serialization library with good performance but without some functions (for example case insensitive enums).

EnumCaseIgnore resolver
---
EnumCaseIgnore resolver is slightly modified and upgraded standard Utf8Json EnumResolver with the the closest to original perfomance (see benchmarks). It uses same hash function to quickly compare bytes (see FarmHash.cs file, copied from original repository, because it's internal there). 
Can serialize enums values with origin, camelcase, snakecase options.
Can deserialize case insensetive enums values.

Benchmarks
---
[EnumCaseIgnore Serialization benckmark](assets/Utf8Json.Extensions.Benchmark.SerializeEnumCaseIgnoreBenchmark-report-github.md)

[EnumCaseIgnore Deserialization benchmark](assets/Utf8Json.Extensions.Benchmark.DeserializeEnumCaseIgnoreBenchmark-report-github.md)

How to use
----
- Install NuGet package

```
Install-Package Utf8Json.Extensions
```

- Create Utf8Json CompositeResolver with EnumCaseIgnoreResolver (override default EnumResolver)

```csharp
var obj = new SimpleObject() { Id = Guid.NewGuid(), Name = "TestObject" };

var resolver = CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.Default);

var result = JsonSerializer.ToJsonString(obj, resolver);
```


License
----

MIT



