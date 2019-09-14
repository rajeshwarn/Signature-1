using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configuration.Element
{
    public class ComponentElement : ConfigurationElement, IComponentConfig
    {
        [ConfigurationProperty("type", IsKey = true, IsRequired = true, Options = ConfigurationPropertyOptions.IsRequired)]
        public string Type
        {
            get { return this["type"] as string; }
            set { this["type"] = value; }
        }

        [ConfigurationProperty("properties")]
        public NameValueConfigurationCollection ComponentProperties
        {
            get { return this["properties"] as NameValueConfigurationCollection; }
            set { this["properties"] = value; }
        }

        public ComponentElement()
        {
            this["type"] = string.Empty;
            this["properties"] = new NameValueConfigurationCollection();
        }
    }
}
