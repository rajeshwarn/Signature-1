using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Database.Interface
{
    public interface ITypeConverter
    {
        object Convert(object ValueToConvert);
        object Convert(object ValueToConvert, object nullDefaultValue);
    }
}
