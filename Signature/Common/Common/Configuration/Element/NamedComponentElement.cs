using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configuration.Element
{
    public class NamedComponentElement : ComponentElement, INamedComponentConfig
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true, Options = ConfigurationPropertyOptions.IsKey)]
        public string Name
        {
            get
            {
                return this["name"] as string;
            }
            set
            {
                this["name"] = value;
            }
        }

        public NamedComponentElement()
            : base()
        {
            this["name"] = string.Empty;
        }

    }
}
