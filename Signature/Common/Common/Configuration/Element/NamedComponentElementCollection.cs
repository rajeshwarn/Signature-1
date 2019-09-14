using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Configuration.Element
{
    public class NamedComponentElementCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new NamedComponentElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as NamedComponentElement).Name;
        }

        public NamedComponentElementCollection()
            : base()
        {
        }

    }
}
