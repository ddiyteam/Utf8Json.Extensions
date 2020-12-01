using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using Tests.Models;
using Utf8Json.Extensions.Resolvers;
using Utf8Json.Resolvers;

namespace Utf8Json.Extensions.Benchmark
{
    [HtmlExporter, MarkdownExporterAttribute.GitHub]
    [MemoryDiagnoser]
    public class SerializeEnumCaseIgnoreBenchmark
    {
        private SimpleObject testObj;

        private readonly IJsonFormatterResolver defaultUtf8DefaultEnumResolver;
        private readonly IJsonFormatterResolver defaultUtf8CaseIgnoreEnumResolver;

        private readonly IJsonFormatterResolver camelcaseUtf8DefaultEnumJsonResolver;
        private readonly IJsonFormatterResolver camelcaseUtf8CaseIgnoreJsonEnumResolver;
        private readonly JsonSerializerSettings camelcaseNewtonsoftResolver;

        private readonly IJsonFormatterResolver snakecaseUtf8DefaultEnumJsonResolver;
        private readonly IJsonFormatterResolver snakecaseUtf8CaseIgnoreJsonEnumResolver;
        private readonly JsonSerializerSettings snakecaseNewtonsoftResolver;

        private readonly IJsonFormatterResolver underlyingUtf8DefaultEnumResolver;
        private readonly IJsonFormatterResolver underlyingUtf8CaseIgnoreJsonEnumResolver;
        private readonly JsonSerializerSettings underlyingNewtonsoftResolver;


        private readonly int opNum = 1000;

        public SerializeEnumCaseIgnoreBenchmark()
        {
            testObj = new SimpleObject()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                ObjectType = ObjectType.LevelOne,
                ObjectTypeDict = new Dictionary<ObjectType, ObjectType>()
                {
                    { ObjectType.LevelZero, ObjectType.LevelZero },
                    { ObjectType.LevelTwo, ObjectType.LevelTwo }
                }
            };

            defaultUtf8DefaultEnumResolver = CompositeResolver.Create(EnumResolver.Default, StandardResolver.Default);
            defaultUtf8CaseIgnoreEnumResolver = CompositeResolver.Create(EnumCaseIgnoreResolver.ValueDictionaryName, StandardResolver.Default);

            camelcaseUtf8DefaultEnumJsonResolver = CompositeResolver.Create(EnumResolver.Default, StandardResolver.CamelCase);
            camelcaseUtf8CaseIgnoreJsonEnumResolver = CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.CamelCase);
            camelcaseNewtonsoftResolver = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };

            snakecaseUtf8DefaultEnumJsonResolver = CompositeResolver.Create(EnumResolver.Default, StandardResolver.SnakeCase);
            snakecaseUtf8CaseIgnoreJsonEnumResolver = CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.SnakeCase);
            snakecaseNewtonsoftResolver = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };

            underlyingUtf8DefaultEnumResolver = CompositeResolver.Create(EnumResolver.UnderlyingValue, StandardResolver.Default);
            underlyingUtf8CaseIgnoreJsonEnumResolver = CompositeResolver.Create(EnumCaseIgnoreResolver.UnderlyingValue, StandardResolver.Default);
            underlyingNewtonsoftResolver = new JsonSerializerSettings();
        }

        [Benchmark]
        public string Utf8Json_EnumDefault()
        {
            var result = string.Empty;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(defaultUtf8DefaultEnumResolver);
                result = JsonSerializer.ToJsonString(testObj);
            }

            return result;
        }

        [Benchmark]
        public string Utf8Json_EnumCaseIgnoreDefault()
        {
            var result = string.Empty;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(defaultUtf8CaseIgnoreEnumResolver);
                result = JsonSerializer.ToJsonString(testObj);
            }
            return result;
        }

        [Benchmark]
        public string NewtonSoft_EnumDefault()
        {
            var result = string.Empty;
            for (var i = 0; i < opNum; i++)
            {
                result = JsonConvert.SerializeObject(testObj);
            }
            return result;
        }


        [Benchmark]
        public string Utf8Json_EnumCamelCase()
        {
            var result = string.Empty;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(camelcaseUtf8DefaultEnumJsonResolver);
                result = JsonSerializer.ToJsonString(testObj);
            }
            return result;
        }

        [Benchmark]
        public string Utf8Json_EnumCaseIgnoreCamelCase()
        {
            var result = string.Empty;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(camelcaseUtf8CaseIgnoreJsonEnumResolver);
                result = JsonSerializer.ToJsonString(testObj);
            }
            return result;
        }

        [Benchmark]
        public string NewtonSoft_EnumCamelCase()
        {
            var result = string.Empty;
            for (var i = 0; i < opNum; i++)
            {
                result = JsonConvert.SerializeObject(testObj, camelcaseNewtonsoftResolver);
            }
            return result;
        }

        [Benchmark]
        public string Utf8Json_EnumSnakeCase()
        {
            var result = string.Empty;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(snakecaseUtf8DefaultEnumJsonResolver);
                result = JsonSerializer.ToJsonString(testObj);
            }
            return result;
        }

        [Benchmark]
        public string Utf8Json_EnumCaseIgnoreSnakeCase()
        {
            var result = string.Empty;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(snakecaseUtf8CaseIgnoreJsonEnumResolver);
                result = JsonSerializer.ToJsonString(testObj);
            }
            return result;
        }

        [Benchmark]
        public string NewtonSoft_EnumSnakeCase()
        {
            var result = string.Empty;
            for (var i = 0; i < opNum; i++)
            {
                result = JsonConvert.SerializeObject(testObj, snakecaseNewtonsoftResolver);
            }
            return result;
        }

        [Benchmark]
        public string Utf8Json_EnumUnderlying()
        {
            var result = string.Empty;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(underlyingUtf8DefaultEnumResolver);
                result = JsonSerializer.ToJsonString(testObj);
            }
            return result;
        }

        [Benchmark]
        public string Utf8Json_EnumCaseIgnoreUnderlying()
        {
            var result = string.Empty;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(underlyingUtf8CaseIgnoreJsonEnumResolver);
                result = JsonSerializer.ToJsonString(testObj);
            }
            return result;
        }

        [Benchmark]
        public string NewtonSoft_EnumUnderlying()
        {
            var result = string.Empty;
            for (var i = 0; i < opNum; i++)
            {
                result = JsonConvert.SerializeObject(testObj, underlyingNewtonsoftResolver);
            }
            return result;
        }
    }
}
