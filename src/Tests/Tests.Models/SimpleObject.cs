using System;
using System.Collections.Generic;

namespace Tests.Models
{
    public class SimpleObject
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ObjectType ObjectType { get; set; }

        public Dictionary<ObjectType, ObjectType> ObjectTypeDict { get; set; }
    }

    public enum ObjectType
    {
        LevelZero,
        LevelOne,
        LevelTwo
    }

    public class SimpleObject1
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ObjectType1 ObjectType { get; set; }

        public Dictionary<ObjectType1, ObjectType1> ObjectTypeDict { get; set; }
    }

    public enum ObjectType1
    {
        LevelZero,
        LevelOne,
        LevelTwo
    }
}
