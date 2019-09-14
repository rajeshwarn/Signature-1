using System;
using Common.Database.Interface;

namespace Common.Database.Converter
{
    public class StringConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            return Convert(ValueToConvert, string.Empty);
        }

        public object Convert(object ValueToConvert, object defaultValue)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return defaultValue;

            return ValueToConvert.ToString();
        }
    }
}
