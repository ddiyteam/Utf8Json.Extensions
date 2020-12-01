using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Utf8Json.Extensions.Internal;
using Utf8Json.Formatters;

namespace Utf8Json.Extensions.Formatters
{
    public class EnumCaseIgnoreFormatter<T> : IJsonFormatter<T>, IObjectPropertyNameFormatter<T>
    {
        readonly static TComparer<T> _nameValueMapping;
        readonly static Dictionary<T, string>[] _enumNameMapping = new Dictionary<T, string>[3];
        readonly static Dictionary<string, T> _nameEnumMapping;

        readonly static JsonSerializeAction<T> SerializeByUnderlyingValue;
        readonly static JsonDeserializeFunc<T> DeserializeByUnderlyingValue;

        readonly bool _serializeKeysByName;
        readonly bool _serializePropsByName;
        readonly int _caseType;

        static EnumCaseIgnoreFormatter()
        {
            var names = new List<string>();
            var values = new List<object>();
            var possibleNames = new Dictionary<string, string[]>();

            var type = typeof(T);
            foreach (var item in type.GetFields().Where(fi => fi.FieldType == type))
            {
                var value = item.GetValue(null);
                var name = Enum.GetName(type, value);
                var dataMember = item.GetCustomAttributes(typeof(DataMemberAttribute), true)
                  .OfType<DataMemberAttribute>()
                  .FirstOrDefault();
                var enumMember = item.GetCustomAttributes(typeof(EnumMemberAttribute), true)
                   .OfType<EnumMemberAttribute>()
                   .FirstOrDefault();

                values.Add(value);
                names.Add(
                     (enumMember != null && enumMember.Value != null) 
                        ? enumMember.Value
                        : (dataMember != null && dataMember.Name != null) ? dataMember.Name : name);
            }

            int possibleNamesCount = 0;
            foreach(var name in names)
            {
                var pNames = GetPossibleNames(name);
                possibleNames.Add(name, pNames);
                possibleNamesCount += pNames.Length;
            }
            
            _nameValueMapping = new TComparer<T>(possibleNamesCount);
            _enumNameMapping[0] = new Dictionary<T, string>(names.Count); //origin
            _enumNameMapping[1] = new Dictionary<T, string>(names.Count); //camelcase
            _enumNameMapping[2] = new Dictionary<T, string>(names.Count); //snakecase
            _nameEnumMapping = new Dictionary<string, T>(names.Count);

            for (int i = 0; i < names.Count; i++)
            {
                if(possibleNames.TryGetValue(names[i], out string[] namesVariants))
                {
                    foreach(var nameVariant in namesVariants)
                    {
                        _nameValueMapping.Add(JsonWriter.GetEncodedPropertyNameWithoutQuotation(nameVariant), (T)values[i]);
                    }
                }
                _nameValueMapping.Add(JsonWriter.GetEncodedPropertyNameWithoutQuotation(((int)values[i]).ToString()), (T)values[i]);

                _enumNameMapping[0][(T)values[i]] = names[i]; //origin
                _enumNameMapping[1][(T)values[i]] = MakeFirstSymbolLower(names[i]); //camelcase
                _enumNameMapping[2][(T)values[i]] = ToSnakeCase(names[i]); //snakecase
                
                _nameEnumMapping[names[i].ToLower()] = (T)values[i]; //origin
                _nameEnumMapping[ToSnakeCase(names[i]).ToLower()] = (T)values[i]; //snakecase
            }

            SerializeByUnderlyingValue = (JsonSerializeAction<T>)EnumFormatterHelper.GetSerializeDelegate(typeof(T), out bool isBoxed);
            DeserializeByUnderlyingValue = (JsonDeserializeFunc<T>)EnumFormatterHelper.GetDeserializeDelegate(typeof(T), out isBoxed);
        }

        public EnumCaseIgnoreFormatter(bool serializeKeysByName, bool seializeDictionartyPropsByName, int caseType = 0)
        {
            _serializeKeysByName = serializeKeysByName;
            _serializePropsByName = seializeDictionartyPropsByName;
            _caseType = caseType;
        }

        public EnumCaseIgnoreFormatter()
        {
            _serializeKeysByName = false;
            _serializePropsByName = false;
            _caseType = 0;
        }

        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            if (_serializeKeysByName)
            {
                SerializeKeyByName(ref writer, value, _caseType);
            }
            else
            {
                SerializeByUnderlyingValue(ref writer, value, formatterResolver);
            }
        }

        public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            var token = reader.GetCurrentJsonToken();

            if (token == JsonToken.String)
            {
                var key = reader.ReadStringSegmentUnsafe();
               
                if (!_nameValueMapping.TryGetValue(key, out T value))
                {
                    var str = Encoding.UTF8.GetString(key.Array, key.Offset, key.Count);
                    value = _nameEnumMapping.TryGetValue(str.ToLower(), out T target)
                        ? target
                        : (T)Enum.Parse(typeof(T), str, ignoreCase: true); // Enum.Parse is slowest way
                }
                return value;
            }
            else if (token == JsonToken.Number)
            {
                return DeserializeByUnderlyingValue(ref reader, formatterResolver);
            }

            throw new InvalidOperationException("Can't parse JSON to Enum format.");
        }

        public void SerializeToPropertyName(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            if (_serializePropsByName)
            {
                SerializeKeyByName(ref writer, value, _caseType);
            }
            else
            {
                writer.WriteQuotation();
                SerializeByUnderlyingValue(ref writer, value, formatterResolver);
                writer.WriteQuotation();
            }
        }

        public T DeserializeFromPropertyName(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserialize(ref reader, formatterResolver);
        }

        #region internal

        private static void SerializeKeyByName(ref JsonWriter writer, T value, int caseType)
        {
            if (!_enumNameMapping[caseType].TryGetValue(value, out string name))
            {
                name = value.ToString();
            }
            writer.WriteString(name);
        }

        private static string[] GetPossibleNames(string originalName)
        {
            var result = new HashSet<string> { originalName };

            var lowerVariant = originalName.ToLower();
            if(originalName != lowerVariant)
            {
                result.Add(lowerVariant);
            }

            var upperVariant = originalName.ToUpper();
            if(!result.Contains(upperVariant))
            {
                result.Add(upperVariant);
            }

            var frstSymbolUpperVariant = MakeFirstSymbolUpper(originalName);
            if (!result.Contains(frstSymbolUpperVariant))
            {
                result.Add(frstSymbolUpperVariant);
            }

            var frstSymbolLowerVariant = MakeFirstSymbolLower(originalName);
            if (!result.Contains(frstSymbolLowerVariant))
            {
                result.Add(frstSymbolLowerVariant);
            }

            var nameToLowerFrstSymbolUpper = NameToLowerFirstSymbolUpper(originalName);
            if (!result.Contains(nameToLowerFrstSymbolUpper))
            {
                result.Add(nameToLowerFrstSymbolUpper);
            }

            var toSnakeCase = ToSnakeCase(originalName);
            if (!result.Contains(toSnakeCase))
            {
                result.Add(toSnakeCase);
            }

            return result.ToArray();
        }

        /// <summary>
        /// myProperty -> MyProperty
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string MakeFirstSymbolUpper(string name)
        {
            if (string.IsNullOrEmpty(name) || char.IsUpper(name, 0))
            {
                return name;
            }
            
            var array = name.ToCharArray();
            array[0] = char.ToUpper(array[0]);
            return new string(array);
        }

        /// <summary>
        /// MyProperty -> myProperty
        /// </summary>
        private static string MakeFirstSymbolLower(string name)
        {
            if (string.IsNullOrEmpty(name) || char.IsLower(name, 0))
            {
                return name;
            }

            var array = name.ToCharArray();
            array[0] = char.ToLowerInvariant(array[0]);
            return new string(array);
        }

        /// <summary>
        /// MYPROPERTY -> Myproperty
        /// </summary>
        private static string NameToLowerFirstSymbolUpper(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }

            var array = name.ToLower().ToCharArray();
            array[0] = char.ToUpper(array[0]);
            return new string(array);
        }

        /// <summary>
        /// MyProperty -> my_property
        /// </summary>
        private static string ToSnakeCase(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            var sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];

                if (Char.IsUpper(c))
                {
                    // first
                    if (i == 0)
                    {
                        sb.Append(char.ToLowerInvariant(c));
                    }
                    else if (char.IsUpper(s[i - 1])) // WriteIO => write_io
                    {
                        sb.Append(char.ToLowerInvariant(c));
                    }
                    else
                    {
                        sb.Append("_");
                        sb.Append(char.ToLowerInvariant(c));
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        #endregion
    }
}
