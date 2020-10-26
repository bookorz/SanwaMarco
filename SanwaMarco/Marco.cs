using log4net.Config;
using SanwaMarco.Comm;
using SanwaMarco.Controller;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SanwaMarco
{
    public static class Marco
    {
        public enum RunMode
        {
            Normal = 0,
            SrcScriptRun,
        }

        
        public enum MachineType
        {
            Normal = 0,     //一般形式(18 Port)
            _26Port,
        }
        
        public static Dictionary<string, string> pubVarMap = new Dictionary<string, string>();
        public static Dictionary<string, Object> deviceMap = new Dictionary<string, Object>();
        public static GUICmdCtrl _EventReport;

        /// <summary>
        /// 接收到$1EVT
        /// </summary>
        public static EventHandler<string> ReceivedEventMessage;

        //20200707 Pingchung 設定Marco運轉模式
        public static RunMode runMode;

        //20200708 Pingchung 設定機台形式(Normal、26Port)
        public static MachineType machineType;

        public static void RunMarco(String marcoName, Dictionary<string, string> args)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(RunMarco), new JobUtil(marcoName, args));
        }
        private static void RunMarco(object obj)
        {
            JobUtil util = (JobUtil)obj;

            //改由外部指定
            util.RunMarco();

            while (!util.isFinish)
            {
                SpinWait.SpinUntil(() => false, 1000);
            }
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

            runMode = RunMode.Normal;
            foreach (DeviceSettingElement foo in config.DeviceSettings)
            {
                IDevice dvcCtrl;

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
                    dvcCtrl.AssignedRecevicedEvent(ReceivedEventMessage);
                    dvcCtrl.start();

                    Marco.deviceMap.Add(foo.Name, dvcCtrl);
                }
                else if (foo.Conn_Type.Equals("ComPort"))
                {
                    if (foo.Conn_Address.Equals(""))
                    {
                        string[] ports = SerialPort.GetPortNames();
                        if (ports.Count() == 0)
                        {
                            dc.PortName = "";
                        }
                        else
                        {
                            dc.PortName = ports[0];
                        }
                    }
                    else
                    {
                        dc.PortName = foo.Conn_Address;
                    }
                    dc.BaudRate = foo.Com_Baud_Rate;
                    dc.DataBits = foo.Com_Data_Bits;
                    dc.ParityBit = foo.Com_Parity_Bit;
                    dc.StopBit = foo.Com_Stop_Bit;
                    dvcCtrl = new DeviceController(dc);
                    dvcCtrl.AssignedRecevicedEvent(ReceivedEventMessage);
                    dvcCtrl.start();
                    Marco.deviceMap.Add(foo.Name, dvcCtrl);
                }
                else if (foo.Conn_Type.Equals("ICPDeviceNet"))
                {
                    if (foo.Conn_Address.Equals(""))
                    {
                        string[] ports = SerialPort.GetPortNames();
                        if (ports.Count() == 0)
                        {
                            dc.PortName = "";
                        }
                        else
                        {
                            dc.PortName = ports[0];
                        }
                    }
                    else
                    {
                        dc.PortName = foo.Conn_Address;
                    }

                    //暫時固定路徑
                    if (machineType == MachineType.Normal)
                    {
                        
                        dc.File = "mini_18port.xls";
                    }
                    else
                    {
                        dc.File = "mini.xls";
                    }
                    //dc.File = foo.File;
                    dvcCtrl = new I7565DNM(dc);
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

        //public static void Test()
        //{
        //    Marco.deviceMap.TryGetValue("N2Purge", out object dvcCtrl);

        //    if (dvcCtrl != null)
        //    {
        //        DeviceController Obj = (DeviceController)dvcCtrl;
        //        Obj.ReceivedEventMessage?.Invoke(null, "$1EVT:PGSTS:15,0");
        //    }

        //}
    }
}
