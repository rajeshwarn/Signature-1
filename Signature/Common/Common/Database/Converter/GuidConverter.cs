using System;
using Common.Database.Interface;

namespace Common.Database.Converter
{
    public class GuidConverter : ITypeConverter
    {
        public object Convert(object ValueToConvert)
        {
            return Convert(ValueToConvert, Guid.Empty);
        }

        public object Convert(object ValueToConvert, object defaultValue)
        {
            if (ValueToConvert == null || ValueToConvert == DBNull.Value)
                return defaultValue;

            return Guid.Parse(ValueToConvert.ToString());
        }

    }
}
