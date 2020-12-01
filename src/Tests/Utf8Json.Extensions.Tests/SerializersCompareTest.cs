using AutoFixture;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using Tests.Models;
using Utf8Json.Extensions.Resolvers;
using Utf8Json.Resolvers;
using Xunit;

namespace Utf8Json.Extensions.Tests
{
    public class SerializersCompareTest
    {
        public static IEnumerable<object[]> NewtonsoftJsonCompareTestData()
        {
            var fixture = new Fixture();
            var testObj = new SimpleObject()
            {
                Id = Guid.NewGuid(),
                Name = fixture.Create<string>(),
                ObjectType = ObjectType.LevelOne,
                ObjectTypeDict = new Dictionary<ObjectType, ObjectType>()
                {
                    { ObjectType.LevelZero, ObjectType.LevelZero },
                    { ObjectType.LevelTwo, ObjectType.LevelTwo }
                }
            };

            //separate object creation for passing test when newtonsoft cache the type for same contractresolver
            var testObj1 = new SimpleObject1()
            {
                Id = Guid.NewGuid(),
                Name = fixture.Create<string>(),
                ObjectType = ObjectType1.LevelOne,
                ObjectTypeDict = new Dictionary<ObjectType1, ObjectType1>()
                {
                    { ObjectType1.LevelZero, ObjectType1.LevelZero },
                    { ObjectType1.LevelTwo, ObjectType1.LevelTwo }
                }
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.Default),
                new JsonSerializerSettings(){
                    Converters = new List<JsonConverter> { new StringEnumConverter() }
                },
                "{\"Id\":\"" + testObj.Id + "\",\"Name\":\"" + testObj.Name + "\",\"ObjectType\":\"LevelOne\",\"ObjectTypeDict\":{\"LevelZero\":\"LevelZero\",\"LevelTwo\":\"LevelTwo\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.DefaultCamelCase, StandardResolver.CamelCase),
                new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy() { ProcessDictionaryKeys = true }
                    },
                    Converters = new List<JsonConverter>() { new StringEnumConverter() { NamingStrategy = new CamelCaseNamingStrategy() } }
                },
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"objectType\":\"levelOne\",\"objectTypeDict\":{\"levelZero\":\"levelZero\",\"levelTwo\":\"levelTwo\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.DefaultSnakeCase, StandardResolver.SnakeCase),
                new JsonSerializerSettings()
                {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy() { ProcessDictionaryKeys = true}
                    },
                    Converters = new List<JsonConverter>() { new StringEnumConverter() { NamingStrategy = new SnakeCaseNamingStrategy() } }
                },
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"object_type\":\"level_one\",\"object_type_dict\":{\"level_zero\":\"level_zero\",\"level_two\":\"level_two\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.ValueDictionaryName, StandardResolver.Default),
                new JsonSerializerSettings(),
                "{\"Id\":\"" + testObj.Id + "\",\"Name\":\"" + testObj.Name + "\",\"ObjectType\":1,\"ObjectTypeDict\":{\"LevelZero\":0,\"LevelTwo\":2}}"
            };

            yield return new object[]
            {
                testObj1,
                CompositeResolver.Create(EnumCaseIgnoreResolver.ValueDictionaryName, StandardResolver.CamelCase),
                new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy() { ProcessDictionaryKeys = false }
                    }
                },
                "{\"id\":\"" + testObj1.Id + "\",\"name\":\"" + testObj1.Name + "\",\"objectType\":1,\"objectTypeDict\":{\"LevelZero\":0,\"LevelTwo\":2}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.ValueDictionaryNameCamelCase, StandardResolver.CamelCase),
                new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                    {
                        NamingStrategy = new CamelCaseNamingStrategy() { ProcessDictionaryKeys = true }
                    }
                },
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"objectType\":1,\"objectTypeDict\":{\"levelZero\":0,\"levelTwo\":2}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.ValueDictionaryNameSnakeCase, StandardResolver.SnakeCase),
                new JsonSerializerSettings()
                {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy() { ProcessDictionaryKeys = true }
                    }
                },
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"object_type\":1,\"object_type_dict\":{\"level_zero\":0,\"level_two\":2}}"
            };
        }

        [Theory]
        [MemberData(nameof(NewtonsoftJsonCompareTestData))]
        public void NewtonsoftJsonCompareTest(object inputObj, IJsonFormatterResolver utf8jsonResolver, JsonSerializerSettings newtonsoftResolver, string expected)
        {
            //arrange
            JsonSerializer.SetDefaultResolver(utf8jsonResolver);

            //act
            var utf8jsonResult = JsonSerializer.ToJsonString(inputObj);
            var newtonsoftResult = JsonConvert.SerializeObject(inputObj, newtonsoftResolver);

            //assert
            Assert.Equal(expected, utf8jsonResult);
            Assert.Equal(expected, newtonsoftResult);
        }
    }
}
