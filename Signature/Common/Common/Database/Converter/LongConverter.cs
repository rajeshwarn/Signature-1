using System;
using Common.Database.Interface;

namespace Common.Database.Converter
{
    public class LongConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            return Convert(ValueToConvert, null);
        }

        public object Convert(object ValueToConvert, object defaultValue)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return defaultValue;

            return System.Convert.ToInt64(ValueToConvert);
        }
    }
}
