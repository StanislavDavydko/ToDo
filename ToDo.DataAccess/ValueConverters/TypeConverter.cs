using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Linq;
using ToDo.DomainModel.Legal.Enums;

namespace ToDo.DataAccess.ValueConverters
{
    public class TypeConverter : ValueConverter<Type, string>
    {
        public static IReadOnlyDictionary<string, Type> TypeCodeToType { get; } =
            new Dictionary<string, Type>
            {
                { "TTODO     ",  Type.ToDo },
                { "TINDEVELOP",  Type.InDevelopment },
                { "TDONE     ",  Type.Done },
            };

        public static IReadOnlyDictionary<Type, string> TypeToTypeCode { get; } =
            TypeCodeToType.ToDictionary(keyValuePair => keyValuePair.Value, keyValuePair => keyValuePair.Key);

        public TypeConverter()
            : base(type => TypeToTypeCode[type],
                   typeCode => TypeCodeToType[typeCode])
        {
        }
    }
}
