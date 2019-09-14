using System;
using Common.Database.Interface;

namespace Common.Database.Converter
{
    public class DateTimeConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            return Convert(ValueToConvert, DateTime.MinValue);
        }

        public object Convert(object ValueToConvert, object defaultValue)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return defaultValue;

            return System.Convert.ToDateTime(ValueToConvert);
        }
    }
}
