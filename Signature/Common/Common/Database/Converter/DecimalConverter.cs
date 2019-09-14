using System;
using Common.Database.Interface;

namespace Common.Database.Converter
{
    public class DecimalConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            return Convert(ValueToConvert, 0.0m);
        }

        public object Convert(object ValueToConvert, object defaultValue)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return defaultValue;

            return System.Convert.ToDecimal(ValueToConvert);
        }
    }
}
