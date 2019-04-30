using SanwaMarco.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanwaMarco
{
    public static class Marco
    {
        public static Dictionary<string, string> pubVarMap = new Dictionary<string, string>();
        public static Util util = new Util();
        public static Dictionary<string, Object> deviceMap = new Dictionary<string, Object>();

        public static string RunMarco(String marcoName)
        {
            return util.RunMarco(marcoName);
        }
        public static void ConnDevice()
        {
            util.Connect();
        }
        public static void SendMessage(string device_name, string msg)
        {
            if (!deviceMap.ContainsKey(device_name))
            {
                Console.WriteLine("Device: " + device_name  + " 不存在");
            }
            else
            {
                DeviceController device = (DeviceController)deviceMap[device_name];
                util.Send(device, msg);
            }
        }
        public static void Init(string device_name)
        {
            if (!deviceMap.ContainsKey(device_name))
            {
                Console.WriteLine("Device: " + device_name + " 不存在");
            }
            else
            {
                DeviceController device = (DeviceController)deviceMap[device_name];
                device.processState = DeviceController.PROCESS_STATE_IDLE;
            }
        }
    }
}
