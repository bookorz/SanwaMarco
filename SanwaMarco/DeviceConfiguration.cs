using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanwaMarco
{
    public class DeviceConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("devices")]
        [ConfigurationCollection(typeof(DeviceSettingElement), AddItemName = "device")]
        public DeviceSettingElementCollection DeviceSettings => this["devices"] as DeviceSettingElementCollection;
    }

    public class DeviceSettingElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new DeviceSettingElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as DeviceSettingElement).Name;
        }
    }


    public class DeviceSettingElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "", IsRequired = true)]
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

        [ConfigurationProperty("enable", DefaultValue = "1", IsRequired = true)]
        public string Enable
        {
            get
            {
                return this["enable"] as string;
            }
            set
            {
                this["enable"] = value;
            }
        }

        
        [ConfigurationProperty("vendor", DefaultValue = "SANWA", IsRequired = true)]
        public string Vendor
        {
            get
            {
                return this["vendor"] as string;
            }
            set
            {
                this["vendor"] = value;
            }
        }

        [ConfigurationProperty("conn_type", IsRequired = true)]
        public string Conn_Type
        {
            get
            {
                return this["conn_type"] as string;
            }
            set
            {
                this["conn_type"] = value;
            }
        }

        [ConfigurationProperty("conn_address", IsRequired = true)]
        public string Conn_Address
        {
            get
            {
                return this["conn_address"] as string;
            }
            set
            {
                this["conn_address"] = value;
            }

        }

        [ConfigurationProperty("conn_port", IsRequired = false)]
        public string Conn_Port
        {
            get
            {
                return this["conn_port"] as string;
            }
            set
            {
                this["conn_port"] = value;
            }

        }


        [ConfigurationProperty("com_baud_rate", IsRequired = false, DefaultValue = "9600")]
        public int Com_Baud_Rate
        {
            get
            {
                return (int)this["com_baud_rate"];
            }
            set
            {
                this["com_baud_rate"] = value;
            }
        }

        [ConfigurationProperty("com_parity_bit", IsRequired = false, DefaultValue = "None")]
        public string Com_Parity_Bit
        {
            get
            {
                return this["com_parity_bit"] as string;
            }
            set
            {
                this["com_parity_bit"] = value;
            }
        }

        [ConfigurationProperty("com_data_bits", IsRequired = false, DefaultValue = "8")]
        public int Com_Data_Bits
        {
            get
            {
                //return this["com_data_bits"] as string;
                return (int)this["com_data_bits"];
            }
            set
            {
                this["com_data_bits"] = value;
            }
        }

        [ConfigurationProperty("com_stop_bit", IsRequired = false, DefaultValue = "1")]
        public string Com_Stop_Bit
        {
            get
            {
                return this["com_stop_bit"] as string;
            }
            set
            {
                this["com_stop_bit"] = value;
            }
        }

    }
}
