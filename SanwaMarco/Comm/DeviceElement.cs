using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanwaMarco.Comm
{
    public class DeviceElement : ConfigurationElement
    {
        [ConfigurationProperty("Id", DefaultValue = 2)]
        public int Id
        {
            get { return (int)this["Id"]; }
            set { this["Id"] = value; }
        }

        [ConfigurationProperty("Name", DefaultValue = "Yao")]
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }
    }
}
