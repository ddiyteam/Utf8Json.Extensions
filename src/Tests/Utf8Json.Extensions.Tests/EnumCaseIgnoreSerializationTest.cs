using AutoFixture;
using System;
using System.Collections.Generic;
using Tests.Models;
using Utf8Json.Extensions.Resolvers;
using Utf8Json.Resolvers;
using Xunit;

namespace Utf8Json.Extensions.Tests
{
    public class EnumCaseIgnoreSerializationTest
    {
        public static IEnumerable<object[]> SimpleObjectTestData()
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

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.Default),
                "{\"Id\":\"" + testObj.Id + "\",\"Name\":\"" + testObj.Name + "\",\"ObjectType\":\"LevelOne\",\"ObjectTypeDict\":{\"LevelZero\":\"LevelZero\",\"LevelTwo\":\"LevelTwo\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.DefaultCamelCase, StandardResolver.CamelCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"objectType\":\"levelOne\",\"objectTypeDict\":{\"levelZero\":\"levelZero\",\"levelTwo\":\"levelTwo\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.DefaultSnakeCase, StandardResolver.SnakeCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"object_type\":\"level_one\",\"object_type_dict\":{\"level_zero\":\"level_zero\",\"level_two\":\"level_two\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.ValueDictionaryName, StandardResolver.Default),
                "{\"Id\":\"" + testObj.Id + "\",\"Name\":\"" + testObj.Name + "\",\"ObjectType\":1,\"ObjectTypeDict\":{\"LevelZero\":0,\"LevelTwo\":2}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.ValueDictionaryNameCamelCase, StandardResolver.CamelCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"objectType\":1,\"objectTypeDict\":{\"levelZero\":0,\"levelTwo\":2}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.ValueDictionaryNameSnakeCase, StandardResolver.SnakeCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"object_type\":1,\"object_type_dict\":{\"level_zero\":0,\"level_two\":2}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.UnderlyingValue, StandardResolver.Default),
                "{\"Id\":\"" + testObj.Id + "\",\"Name\":\"" + testObj.Name + "\",\"ObjectType\":1,\"ObjectTypeDict\":{\"0\":0,\"2\":2}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.UnderlyingValue, StandardResolver.CamelCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"objectType\":1,\"objectTypeDict\":{\"0\":0,\"2\":2}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.UnderlyingValue, StandardResolver.SnakeCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"object_type\":1,\"object_type_dict\":{\"0\":0,\"2\":2}}"
            };
        }

        [Theory]
        [MemberData(nameof(SimpleObjectTestData))]
        public void SerializeObjectTest(SimpleObject inputObj, IJsonFormatterResolver utf8jsonResolver, string expected)
        {
            //arrange
            JsonSerializer.SetDefaultResolver(utf8jsonResolver);

            //act
            var utf8jsonResult = JsonSerializer.ToJsonString(inputObj);

            //assert
            Assert.Equal(expected, utf8jsonResult);
        }
    }
}
