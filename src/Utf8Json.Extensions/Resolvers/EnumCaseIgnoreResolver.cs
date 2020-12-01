using System;
using System.Reflection;
using Utf8Json.Formatters;
using Utf8Json.Extensions.Resolvers.Internal;
using Utf8Json.Extensions.Formatters;

namespace Utf8Json.Extensions.Resolvers
{
    public static class EnumCaseIgnoreResolver
    {
        /// <summary>Serialize enum as Name, dictionary key enum as Name. Origin case for names</summary>
        public static readonly IJsonFormatterResolver Default = EnumClassNameDictionatyNameOriginCase.Instance;

        /// <summary>Serialize enum as Name, dictionary key enum as Name. CamelCase for names</summary>
        public static readonly IJsonFormatterResolver DefaultCamelCase = EnumClassNameDictionatyNameCamelCase.Instance;

        /// <summary>Serialize enum as Name, dictionary key enum as Name. Snake case for names</summary>
        public static readonly IJsonFormatterResolver DefaultSnakeCase = EnumClassNameDictionatyNameSnakeCase.Instance;
        
        /// <summary>Serialize enum as Value, dictionary key enum as Name. Origin case for names</summary>
        public static readonly IJsonFormatterResolver ValueDictionaryName = EnumClassValueDictionatyNameOriginCase.Instance;

        /// <summary>Serialize enum as Value, dictionary key enum as Name. CamelCase for names</summary>
        public static readonly IJsonFormatterResolver ValueDictionaryNameCamelCase = EnumClassValueDictionatyNameCamelCase.Instance;

        /// <summary>Serialize enum as Value, dictionary key enum as Name. Snake case for names</summary>
        public static readonly IJsonFormatterResolver ValueDictionaryNameSnakeCase = EnumClassValueDictionatyNameSnakeCase.Instance;

        /// <summary>Serialize enum as Value, dictionary properties enum as Value.</summary>
        public static readonly IJsonFormatterResolver UnderlyingValue = EnumClassValueDictionatyValue.Instance;
    }
}

namespace Utf8Json.Extensions.Resolvers.Internal
{
    internal sealed class EnumClassValueDictionatyNameOriginCase : IJsonFormatterResolver
    {
        public static readonly IJsonFormatterResolver Instance = new EnumClassValueDictionatyNameOriginCase();

        EnumClassValueDictionatyNameOriginCase()
        {
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var ti = typeof(T).GetTypeInfo();

                if (IsNullable(ti))
                {
                    // build underlying type and use wrapped formatter.
                    ti = ti.GenericTypeArguments[0].GetTypeInfo();
                    if (!ti.IsEnum)
                    {
                        return;
                    }

                    var innerFormatter = Instance.GetFormatterDynamic(ti.AsType());
                    if (innerFormatter == null)
                    {
                        return;
                    }
                    formatter = (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(ti.AsType()), new object[] { innerFormatter });
                    return;
                }
                else if (typeof(T).IsEnum)
                {
                    formatter = (IJsonFormatter<T>)(object)new EnumCaseIgnoreFormatter<T>(false, true);
                }
            }

            static bool IsNullable(TypeInfo type)
            {
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>);
            }
        }
    }

    internal sealed class EnumClassValueDictionatyNameCamelCase : IJsonFormatterResolver
    {
        public static readonly IJsonFormatterResolver Instance = new EnumClassValueDictionatyNameCamelCase();

        EnumClassValueDictionatyNameCamelCase()
        {
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var ti = typeof(T).GetTypeInfo();

                if (IsNullable(ti))
                {
                    // build underlying type and use wrapped formatter.
                    ti = ti.GenericTypeArguments[0].GetTypeInfo();
                    if (!ti.IsEnum)
                    {
                        return;
                    }

                    var innerFormatter = Instance.GetFormatterDynamic(ti.AsType());
                    if (innerFormatter == null)
                    {
                        return;
                    }
                    formatter = (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(ti.AsType()), new object[] { innerFormatter });
                    return;
                }
                else if (typeof(T).IsEnum)
                {
                    formatter = (IJsonFormatter<T>)(object)new EnumCaseIgnoreFormatter<T>(false, true, 1);
                }
            }

            static bool IsNullable(TypeInfo type)
            {
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>);
            }
        }
    }

    internal sealed class EnumClassValueDictionatyNameSnakeCase : IJsonFormatterResolver
    {
        public static readonly IJsonFormatterResolver Instance = new EnumClassValueDictionatyNameSnakeCase();

        EnumClassValueDictionatyNameSnakeCase()
        {
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var ti = typeof(T).GetTypeInfo();

                if (IsNullable(ti))
                {
                    // build underlying type and use wrapped formatter.
                    ti = ti.GenericTypeArguments[0].GetTypeInfo();
                    if (!ti.IsEnum)
                    {
                        return;
                    }

                    var innerFormatter = Instance.GetFormatterDynamic(ti.AsType());
                    if (innerFormatter == null)
                    {
                        return;
                    }
                    formatter = (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(ti.AsType()), new object[] { innerFormatter });
                    return;
                }
                else if (typeof(T).IsEnum)
                {
                    formatter = (IJsonFormatter<T>)(object)new EnumCaseIgnoreFormatter<T>(false, true, 2);
                }
            }

            static bool IsNullable(TypeInfo type)
            {
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>);
            }
        }
    }


    internal sealed class EnumClassNameDictionatyNameOriginCase : IJsonFormatterResolver
    {
        public static readonly IJsonFormatterResolver Instance = new EnumClassNameDictionatyNameOriginCase();

        EnumClassNameDictionatyNameOriginCase()
        {
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var ti = typeof(T).GetTypeInfo();

                if (IsNullable(ti))
                {
                    // build underlying type and use wrapped formatter.
                    ti = ti.GenericTypeArguments[0].GetTypeInfo();
                    if (!ti.IsEnum)
                    {
                        return;
                    }

                    var innerFormatter = Instance.GetFormatterDynamic(ti.AsType());
                    if (innerFormatter == null)
                    {
                        return;
                    }
                    formatter = (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(ti.AsType()), new object[] { innerFormatter });
                    return;
                }
                else if (typeof(T).IsEnum)
                {
                    formatter = (IJsonFormatter<T>)(object)new EnumCaseIgnoreFormatter<T>(true, true);
                }
            }

            static bool IsNullable(TypeInfo type)
            {
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>);
            }
        }
    }

    internal sealed class EnumClassNameDictionatyNameCamelCase : IJsonFormatterResolver
    {
        public static readonly IJsonFormatterResolver Instance = new EnumClassNameDictionatyNameCamelCase();

        EnumClassNameDictionatyNameCamelCase()
        {
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var ti = typeof(T).GetTypeInfo();

                if (IsNullable(ti))
                {
                    // build underlying type and use wrapped formatter.
                    ti = ti.GenericTypeArguments[0].GetTypeInfo();
                    if (!ti.IsEnum)
                    {
                        return;
                    }

                    var innerFormatter = Instance.GetFormatterDynamic(ti.AsType());
                    if (innerFormatter == null)
                    {
                        return;
                    }
                    formatter = (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(ti.AsType()), new object[] { innerFormatter });
                    return;
                }
                else if (typeof(T).IsEnum)
                {
                    formatter = (IJsonFormatter<T>)(object)new EnumCaseIgnoreFormatter<T>(true, true, 1);
                }
            }

            static bool IsNullable(TypeInfo type)
            {
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>);
            }
        }
    }

    internal sealed class EnumClassNameDictionatyNameSnakeCase : IJsonFormatterResolver
    {
        public static readonly IJsonFormatterResolver Instance = new EnumClassNameDictionatyNameSnakeCase();

        EnumClassNameDictionatyNameSnakeCase()
        {
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var ti = typeof(T).GetTypeInfo();

                if (IsNullable(ti))
                {
                    // build underlying type and use wrapped formatter.
                    ti = ti.GenericTypeArguments[0].GetTypeInfo();
                    if (!ti.IsEnum)
                    {
                        return;
                    }

                    var innerFormatter = Instance.GetFormatterDynamic(ti.AsType());
                    if (innerFormatter == null)
                    {
                        return;
                    }
                    formatter = (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(ti.AsType()), new object[] { innerFormatter });
                    return;
                }
                else if (typeof(T).IsEnum)
                {
                    formatter = (IJsonFormatter<T>)(object)new EnumCaseIgnoreFormatter<T>(true, true, 2);
                }
            }

            static bool IsNullable(TypeInfo type)
            {
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>);
            }
        }
    }


    internal sealed class EnumClassValueDictionatyValue : IJsonFormatterResolver
    {
        public static readonly IJsonFormatterResolver Instance = new EnumClassValueDictionatyValue();

        EnumClassValueDictionatyValue()
        {
        }

        public IJsonFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IJsonFormatter<T> formatter;

            static FormatterCache()
            {
                var ti = typeof(T).GetTypeInfo();

                if (IsNullable(ti))
                {
                    // build underlying type and use wrapped formatter.
                    ti = ti.GenericTypeArguments[0].GetTypeInfo();
                    if (!ti.IsEnum)
                    {
                        return;
                    }

                    var innerFormatter = Instance.GetFormatterDynamic(ti.AsType());
                    if (innerFormatter == null)
                    {
                        return;
                    }
                    formatter = (IJsonFormatter<T>)Activator.CreateInstance(typeof(StaticNullableFormatter<>).MakeGenericType(ti.AsType()), new object[] { innerFormatter });
                    return;
                }
                else if (typeof(T).IsEnum)
                {
                    formatter = (IJsonFormatter<T>)(object)new EnumCaseIgnoreFormatter<T>(false, false);
                }
            }

            static bool IsNullable(TypeInfo type)
            {
                return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(System.Nullable<>);
            }
        }
    }

}