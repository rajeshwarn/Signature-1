using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configuration
{
    public class ComponentSection : ConfigurationSection, IComponentConfig
    {
        [ConfigurationProperty("type", IsKey = true, IsRequired = true, Options = ConfigurationPropertyOptions.IsRequired)]
        public string Type
        {
            get
            {
                return this["type"] as string;
            }
            set
            {
                this["type"] = value;
            }
        }

        [ConfigurationProperty("properties")]
        public NameValueConfigurationCollection ComponentProperties
        {
            get
            {
                return this["properties"] as NameValueConfigurationCollection;
            }
            set
            {
                this["properties"] = value;
            }
        }

        public ComponentSection()
        {
            this.ComponentProperties = new NameValueConfigurationCollection();
            this.Type = string.Empty;
        }
    }
}
