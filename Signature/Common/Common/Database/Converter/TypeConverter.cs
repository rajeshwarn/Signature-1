using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Database.Interface;

namespace Common.Database.Converter
{
    public class TypeConverter
    {
        public static ITypeConverter GetConvertType<T>()
        {
            if (typeof(T) == typeof(Guid))
                return (new GuidConverter());
            if (typeof(T) == typeof(Nullable<Guid>))
                return (new GuidNullConverter());
            if (typeof(T) == typeof(int))
                return (new IntegerConverter());
            if (typeof(T) == typeof(long))
                return (new LongConverter());
            if (typeof(T) == typeof(short))
                return (new ShortConverter());
            if (typeof(T) == typeof(float))
                return (new FloatConverter());
            if (typeof(T) == typeof(double))
                return (new DoubleConverter());
            if (typeof(T) == typeof(decimal))
                return (new DecimalConverter());
            if (typeof(T) == typeof(bool))
                return (new BooleanConverter());
            if (typeof(T) == typeof(char))
                return (new CharConverter());
            if (typeof(T) == typeof(DateTime))
                return (new DateTimeConverter());
            if (typeof(T) == typeof(string))
                return (new StringConverter());
            if (typeof(T).IsEnum)
                return (new EnumConverter());

            return null;
        }

        public static ITypeConverter GetConvertType(Type T)
        {
            if (T == typeof(Guid))
                return (new GuidConverter());
            if (T == typeof(Nullable<Guid>))
                return (new GuidNullConverter());
            if (T == typeof(int))
                return (new IntegerConverter());
            if (T == typeof(long))
                return (new LongConverter());
            if (T == typeof(short))
                return (new ShortConverter());
            if (T == typeof(float))
                return (new FloatConverter());
            if (T == typeof(double))
                return (new DoubleConverter());
            if (T == typeof(decimal))
                return (new DecimalConverter());
            if (T == typeof(bool))
                return (new BooleanConverter());
            if (T == typeof(char))
                return (new CharConverter());
            if (T == typeof(DateTime))
                return (new DateTimeConverter());
            if (T == typeof(string))
                return (new StringConverter());
            if (T.IsEnum)
                return (new EnumConverter());

            return null;
        }

    }
}
