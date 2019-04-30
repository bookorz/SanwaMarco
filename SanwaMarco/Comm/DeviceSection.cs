using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanwaMarco.Comm
{
    public class DeviceSection : ConfigurationSection
    {
        private DeviceSection()
        {
        }

        [ConfigurationProperty("Code", DefaultValue = "9527")]
        public string Code
        {
            get { return this["Code"].ToString(); }
            set { this["Code"] = value; }
        }

        [ConfigurationProperty("Device")]
        public DeviceElement Device
        {
            get { return (DeviceElement)this["Device"]; }
            set { this["Device"] = value; }
        }

    }

}
