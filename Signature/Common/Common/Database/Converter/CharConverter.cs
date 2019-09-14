using System;
using Common.Database.Interface;

namespace Common.Database.Converter
{
    public class CharConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            return Convert(ValueToConvert, (char)0x0);
        }

        public object Convert(object ValueToConvert, object defaultValue)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return defaultValue;

            return System.Convert.ToChar(ValueToConvert);
        }
    }
}
