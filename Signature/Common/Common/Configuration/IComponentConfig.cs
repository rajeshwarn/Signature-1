using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configuration
{
    public interface IComponentConfig
    {
        string Type { get; set; }
        NameValueConfigurationCollection ComponentProperties { get; set; }
    }
}
