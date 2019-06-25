using log4net.Config;
using SanwaMarco.Comm;
using SanwaMarco.Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SanwaMarco
{
    public static class Marco
    {
        public static Dictionary<string, string> pubVarMap = new Dictionary<string, string>();
        public static Dictionary<string, Object> deviceMap = new Dictionary<string, Object>();
        public static GUICmdCtrl _EventReport;

        public static void RunMarco(String marcoName, Dictionary<string, string> args)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(RunMarco), new JobUtil(marcoName, args));
        }
        private static void RunMarco(object obj)
        {
            JobUtil util = (JobUtil)obj;
            util.RunMarco();
            while (!util.isFinish)
            {
                SpinWait.SpinUntil(() => false, 1000);
            }
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine(util.result);
            Console.WriteLine("-----------------------------------------");
            _EventReport.On_Marco_Finish(util);
            //GUICmdCtrl.
        }
        public static void ConnDevice()
        {
            //util.Connect();
            //XmlConfigurator.Configure();//Log4N 需要
            var configFile = Directory.GetCurrentDirectory() + "\\" + "Device.config";
            ConfigurationFileMap fileMap = new ConfigurationFileMap(configFile);
            DeviceConfiguration config = ConfigurationManager.OpenMappedMachineConfiguration(fileMap).GetSection("deviceSettingGroup/deviceConfig") as DeviceConfiguration;
            Console.WriteLine();
            deviceMap.Clear();
            foreach (DeviceSettingElement foo in config.DeviceSettings)
            {
                DeviceController dvcCtrl;
                //Console.WriteLine("--------------------------");
                //Console.WriteLine(foo.Name);
                //Console.WriteLine(foo.Enable);
                //Console.WriteLine(foo.Conn_Type);
                //Console.WriteLine(foo.Conn_Address);
                //Console.WriteLine(foo.Conn_Port);
                //Console.WriteLine(foo.Com_Baud_Rate);
                //Console.WriteLine(foo.Com_Data_Bits);
                //Console.WriteLine(foo.Com_Parity_Bit);
                //Console.WriteLine(foo.Com_Stop_Bit);
                //Console.WriteLine("--------------------------");

                if (!foo.Enable.Equals("1"))
                {
                    Console.WriteLine("---Device:" + foo.Name + " is disabled !-------------------");
                    continue;
                }
                DeviceConfig dc = new DeviceConfig();
                dc.DeviceName = foo.Name;
                dc.ConnectionType = foo.Conn_Type;
                dc.Vendor = foo.Vendor;
                if (foo.Conn_Type.Equals("Socket"))
                {
                    dc.IPAdress = foo.Conn_Address;
                    dc.Port = Int32.Parse(foo.Conn_Port);
                    dvcCtrl = new DeviceController(dc);
                    dvcCtrl.start();
                    Marco.deviceMap.Add(foo.Name, dvcCtrl);
                }
                else if (foo.Conn_Type.Equals("ComPort"))
                {
                    dc.PortName = foo.Conn_Address;
                    dc.BaudRate = foo.Com_Baud_Rate;
                    dc.DataBits = foo.Com_Data_Bits;
                    dc.ParityBit = foo.Com_Parity_Bit;
                    dc.StopBit = foo.Com_Stop_Bit;
                    dvcCtrl = new DeviceController(dc);
                    dvcCtrl.start();
                    Marco.deviceMap.Add(foo.Name, dvcCtrl);
                }
            }
            ////設定停用
            //var xmlDoc = new XmlDocument();
            //xmlDoc.Load(configFile);
            //xmlDoc.SelectSingleNode("//deviceSettingGroup/deviceConfig/devices/device[@name='Robot02']").Attributes["enable"].Value = "0";
            //xmlDoc.Save(configFile);
            //ConfigurationManager.RefreshSection("deviceSettingGroup/deviceConfig");
        }
        public static void Print(string msg)
        {
            _EventReport.On_Marco_Print(msg);
        }
        //private static void SendMessage(string device_name, string msg)
        //{
        //    if (!deviceMap.ContainsKey(device_name))
        //    {
        //        Console.WriteLine("Device: " + device_name  + " 不存在");
        //    }
        //    else
        //    {
        //        DeviceController device = (DeviceController)deviceMap[device_name];
        //        util.Send(device, msg);
        //    }
        //}
        //public static void Init(string device_name)
        //{
        //    if (!deviceMap.ContainsKey(device_name))
        //    {
        //        Console.WriteLine("Device: " + device_name + " 不存在");
        //    }
        //    else
        //    {
        //        DeviceController device = (DeviceController)deviceMap[device_name];
        //        device.processState = DeviceController.PROCESS_STATE_IDLE;
        //    }
        //}
    }
}
