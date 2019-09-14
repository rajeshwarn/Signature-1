using System;
using Common.Database.Interface;

namespace Common.Database.Converter
{
    public class BooleanConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            return Convert(ValueToConvert, false);
        }

        public object Convert(object ValueToConvert, object defaultValue)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return defaultValue;

            if (string.IsNullOrEmpty(ValueToConvert.ToString()))
                return false;
            else if (ValueToConvert.ToString() == "0")
                return false;
            else if (ValueToConvert.ToString() == "1")
                return true;
            else if (ValueToConvert.ToString().ToLower() == "true")
                return true;
            else if (ValueToConvert.ToString().ToLower() == "false")
                return false;

            return System.Convert.ToBoolean(ValueToConvert);
        }
    }
}
