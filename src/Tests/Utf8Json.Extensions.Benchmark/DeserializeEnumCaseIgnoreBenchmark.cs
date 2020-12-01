using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Tests.Models;
using Utf8Json.Extensions.Resolvers;
using Utf8Json.Resolvers;

namespace Utf8Json.Extensions.Benchmark
{
    [HtmlExporter, MarkdownExporterAttribute.GitHub]
    [MemoryDiagnoser]
    public class DeserializeEnumCaseIgnoreBenchmark
    {
        private string testStr;
        private string testStrCameCase;
        private string testStrSnakeCase;
        private string testStrUnderlying;

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

        public DeserializeEnumCaseIgnoreBenchmark()
        {
            testStr = "{\"Id\":\"" + Guid.NewGuid() + "\",\"Name\":\"Name\",\"ObjectType\":\"LevelOne\",\"ObjectTypeDict\":{\"LevelZero\":\"LevelZero\",\"LevelTwo\":\"LevelTwo\"}}";
            defaultUtf8DefaultEnumResolver = CompositeResolver.Create(EnumResolver.Default, StandardResolver.Default);
            defaultUtf8CaseIgnoreEnumResolver = CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.Default);


            testStrCameCase = "{\"id\":\"" + Guid.NewGuid() + "\",\"name\":\"Name\",\"objectType\":\"LevelOne\",\"objectTypeDict\":{\"LevelZero\":\"LevelZero\",\"LevelTwo\":\"LevelTwo\"}}";
            camelcaseUtf8DefaultEnumJsonResolver = CompositeResolver.Create(EnumResolver.Default, StandardResolver.CamelCase);
            camelcaseUtf8CaseIgnoreJsonEnumResolver = CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.CamelCase);
            camelcaseNewtonsoftResolver = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };


            testStrSnakeCase = "{\"id\":\"" + Guid.NewGuid() + "\",\"name\":\"Name\",\"object_type\":\"LevelOne\",\"object_type_dict\":{\"LevelZero\":\"LevelZero\",\"LevelTwo\":\"LevelTwo\"}}";
            snakecaseUtf8DefaultEnumJsonResolver = CompositeResolver.Create(EnumResolver.Default, StandardResolver.SnakeCase);
            snakecaseUtf8CaseIgnoreJsonEnumResolver = CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.SnakeCase);
            snakecaseNewtonsoftResolver = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };


            testStrUnderlying = "{\"Id\":\"" + Guid.NewGuid() + "\",\"Name\":\"Name\",\"ObjectType\":\"1\",\"ObjectTypeDict\":{\"0\":\"0\",\"2\":\"2\"}}";
            underlyingUtf8DefaultEnumResolver = CompositeResolver.Create(EnumResolver.UnderlyingValue, StandardResolver.Default);
            underlyingUtf8CaseIgnoreJsonEnumResolver = CompositeResolver.Create(EnumCaseIgnoreResolver.UnderlyingValue, StandardResolver.Default);
            underlyingNewtonsoftResolver = new JsonSerializerSettings();
        }

        [Benchmark]
        public SimpleObject Utf8Json_EnumDefault()
        {
            SimpleObject result = null;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(defaultUtf8DefaultEnumResolver);
                result = JsonSerializer.Deserialize<SimpleObject>(testStr);
            }
            return result;
        }

        [Benchmark]
        public SimpleObject Utf8Json_EnumCaseIgnoreDefault()
        {
            SimpleObject result = null;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(defaultUtf8CaseIgnoreEnumResolver);
                result = JsonSerializer.Deserialize<SimpleObject>(testStr);
            }
            return result;
        }

        [Benchmark]
        public SimpleObject NewtonSoft_EnumDefault()
        {
            SimpleObject result = null;
            for (var i = 0; i < opNum; i++)
            {
                result = JsonConvert.DeserializeObject<SimpleObject>(testStr);
            }
            return result;
        }


        [Benchmark]
        public SimpleObject Utf8Json_EnumCamelCase()
        {
            SimpleObject result = null;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(camelcaseUtf8DefaultEnumJsonResolver);
                result = JsonSerializer.Deserialize<SimpleObject>(testStrCameCase);
            }
            return result;
        }

        [Benchmark]
        public SimpleObject Utf8Json_EnumCaseIgnoreCamelCase()
        {
            SimpleObject result = null;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(camelcaseUtf8CaseIgnoreJsonEnumResolver);
                result = JsonSerializer.Deserialize<SimpleObject>(testStrCameCase);
            }
            return result;
        }

        [Benchmark]
        public SimpleObject NewtonSoft_EnumCamelCase()
        {
            SimpleObject result = null;
            for (var i = 0; i < opNum; i++)
            {
                result = JsonConvert.DeserializeObject<SimpleObject>(testStrCameCase, camelcaseNewtonsoftResolver);
            }
            return result;
        }


        [Benchmark]
        public SimpleObject Utf8Json_EnumSnakeCase()
        {
            SimpleObject result = null;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(snakecaseUtf8DefaultEnumJsonResolver);
                result = JsonSerializer.Deserialize<SimpleObject>(testStrSnakeCase);
            }
            return result;
        }

        [Benchmark]
        public SimpleObject Utf8Json_EnumCaseIgnoreSnakeCase()
        {
            SimpleObject result = null;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(snakecaseUtf8CaseIgnoreJsonEnumResolver);
                result = JsonSerializer.Deserialize<SimpleObject>(testStrSnakeCase);
            }
            return result;
        }

        [Benchmark]
        public SimpleObject NewtonSoft_EnumSnakeCase()
        {
            SimpleObject result = null;
            for (var i = 0; i < opNum; i++)
            {
                result = JsonConvert.DeserializeObject<SimpleObject>(testStrSnakeCase, snakecaseNewtonsoftResolver);
            }
            return result;
        }

        [Benchmark]
        public SimpleObject Utf8Json_EnumUnderlying()
        {
            SimpleObject result = null;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(underlyingUtf8DefaultEnumResolver);
                result = JsonSerializer.Deserialize<SimpleObject>(testStrUnderlying);
            }
            return result;
        }

        [Benchmark]
        public SimpleObject Utf8Json_EnumCaseIgnoreUnderlying()
        {
            SimpleObject result = null;
            for (var i = 0; i < opNum; i++)
            {
                JsonSerializer.SetDefaultResolver(underlyingUtf8CaseIgnoreJsonEnumResolver);
                result = JsonSerializer.Deserialize<SimpleObject>(testStrUnderlying);
            }
            return result;
        }

        [Benchmark]
        public SimpleObject NewtonSoft_EnumUnderlying()
        {
            SimpleObject result = null;
            for (var i = 0; i < opNum; i++)
            {
                result = JsonConvert.DeserializeObject<SimpleObject>(testStrUnderlying, underlyingNewtonsoftResolver);
            }
            return result;
        }
    }
}
