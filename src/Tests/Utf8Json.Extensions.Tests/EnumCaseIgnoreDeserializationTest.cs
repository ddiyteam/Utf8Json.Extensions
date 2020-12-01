using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Models;
using Utf8Json.Extensions.Resolvers;
using Utf8Json.Resolvers;
using Xunit;

namespace Utf8Json.Extensions.Tests
{
    public class EnumCaseIgnoreDeserializationTest
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
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.Default),
                "{\"Id\":\"" + testObj.Id + "\",\"Name\":\"" + testObj.Name + "\",\"ObjectType\":\"levelOne\",\"ObjectTypeDict\":{\"levelZero\":\"levelZero\",\"levelTwo\":\"levelTwo\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.Default),
                "{\"Id\":\"" + testObj.Id + "\",\"Name\":\"" + testObj.Name + "\",\"ObjectType\":\"level_one\",\"ObjectTypeDict\":{\"level_zero\":\"level_zero\",\"level_two\":\"level_two\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.Default),
                "{\"Id\":\"" + testObj.Id + "\",\"Name\":\"" + testObj.Name + "\",\"ObjectType\":\"levelONE\",\"ObjectTypeDict\":{\"lEvElZeRo\":\"LEVELzero\",\"leVelTwo\":\"levelTwO\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.Default),
                "{\"Id\":\"" + testObj.Id + "\",\"Name\":\"" + testObj.Name + "\",\"ObjectType\":\"1\",\"ObjectTypeDict\":{\"LevelZero\":\"0\",\"LevelTwo\":\"2\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.Default),
                "{\"Id\":\"" + testObj.Id + "\",\"Name\":\"" + testObj.Name + "\",\"ObjectType\":\"1\",\"ObjectTypeDict\":{\"0\":\"0\",\"2\":\"2\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.CamelCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"objectType\":\"LevelOne\",\"objectTypeDict\":{\"LevelZero\":\"LevelZero\",\"LevelTwo\":\"LevelTwo\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.CamelCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"objectType\":\"level_one\",\"objectTypeDict\":{\"level_zero\":\"level_zero\",\"level_two\":\"level_two\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.CamelCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"objectType\":\"levelOne\",\"objectTypeDict\":{\"levelZero\":\"levelZero\",\"levelTwo\":\"levelTwo\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.CamelCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"objectType\":\"levelONE\",\"objectTypeDict\":{\"lEvElZeRo\":\"LEVELzero\",\"leVelTwo\":\"levelTwO\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.CamelCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"objectType\":\"1\",\"objectTypeDict\":{\"levelZero\":\"0\",\"levelTwo\":\"2\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.CamelCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"objectType\":\"1\",\"objectTypeDict\":{\"0\":\"0\",\"2\":\"2\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.SnakeCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"object_type\":\"LevelOne\",\"object_type_dict\":{\"LevelZero\":\"LevelZero\",\"LevelTwo\":\"LevelTwo\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.SnakeCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"object_type\":\"levelOne\",\"object_type_dict\":{\"level_zero\":\"level_zero\",\"level_two\":\"level_two\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.SnakeCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"object_type\":\"levelONE\",\"object_type_dict\":{\"lEvElZeRo\":\"LEVELzero\",\"leVelTwo\":\"levelTwO\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.SnakeCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"object_type\":\"1\",\"object_type_dict\":{\"levelZero\":\"0\",\"levelTwo\":\"2\"}}"
            };

            yield return new object[]
            {
                testObj,
                CompositeResolver.Create(EnumCaseIgnoreResolver.Default, StandardResolver.SnakeCase),
                "{\"id\":\"" + testObj.Id + "\",\"name\":\"" + testObj.Name + "\",\"object_type\":\"1\",\"object_type_dict\":{\"0\":\"0\",\"2\":\"2\"}}"
            };

        }

        [Theory]
        [MemberData(nameof(SimpleObjectTestData))]
        public void DeserializeObjectTest(SimpleObject expected, IJsonFormatterResolver utf8jsonResolver, string inputStr)
        {
            //arrange
            JsonSerializer.SetDefaultResolver(utf8jsonResolver);

            //act
            var utf8jsonResult = JsonSerializer.Deserialize<SimpleObject>(inputStr);

            //assert
            Assert.Equal(expected.Id, utf8jsonResult.Id);
            Assert.Equal(expected.Name, utf8jsonResult.Name);
            Assert.Equal(expected.ObjectType, utf8jsonResult.ObjectType);
            Assert.True(expected.ObjectTypeDict.Count == utf8jsonResult.ObjectTypeDict.Count && !expected.ObjectTypeDict.Except(utf8jsonResult.ObjectTypeDict).Any());
        }
    }
}
