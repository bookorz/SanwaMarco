﻿using log4net;
using SanwaMarco.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SanwaMarco.Controller
{
    public class DeviceController : IConnectionReport
    {
        public const string PROCESS_STATE_INIT = "INIT";
        public const string PROCESS_STATE_IDLE = "IDLE";
        public const string PROCESS_STATE_PROCESS = "PROCESS";
        public const string PROCESS_STATE_ERROR = "ERROR";
        public const string PROCESS_STATE_UNKNOWN = "UNKNOWN";

        private static readonly ILog logger = LogManager.GetLogger(typeof(DeviceController));
        IConnection conn;
        public DeviceConfig _Config;
        public string Name { get; set; }
        public string Status = "Disconnected";
        public string processState = PROCESS_STATE_INIT;

        private bool _IsConnected { get; set; }
        public int TrxNo = 1;
        bool WaitingForSync = false;
        string ReturnForSync = "";
        string ReturnTypeForSync = "";

        public DeviceController(DeviceConfig Config)
        {
            _Config = Config;

            switch (Config.ConnectionType)
            {
                case "Socket":
                    //conn = new SocketClient(Config, this);
                    conn = new TcpCommClient(Config, this);
                    break;
                case "ComPort":
                    conn = new ComPortClient(Config, this);
                    break;

            }

            this.Name = _Config.DeviceName;
            this.Status = "";
            this._IsConnected = false;
        }

        public bool sendCommand(string msg)
        {
            bool result = false;
            try
            {
                if (!Status.Equals("Connected"))
                {
                    logger.Error(Name + " is not Connected.");
                    return false;
                }
                if (_Config.Vendor.ToUpper().Equals("SANWA"))
                    msg = msg + "\r";
                conn.Send(msg);
                result = true;
            }
            catch (Exception e)
            {
                logger.Error(Name + " sendCommand error:" + msg + "\n" + e.Message + "\n" + e.StackTrace);
            }
            return result;
        }
        public bool start()
        {
            bool result = false;
            try
            {
                conn.Start();
                this._IsConnected = true;
                result = true;
            }
            catch (Exception e)
            {
                logger.Error("連線失敗:" + e.Message + " " + e.StackTrace);
            }
            return result;
        }

        void IConnectionReport.On_Connection_Connected(object Msg)
        {
            this._IsConnected = true;
            this.Status = "Connected";
            this.processState = DeviceController.PROCESS_STATE_INIT;
        }

        void IConnectionReport.On_Connection_Connecting(string Msg)
        {
            this._IsConnected = false;
            this.Status = "Connecting";
        }

        void IConnectionReport.On_Connection_Disconnected(string Msg)
        {
            this._IsConnected = false;
            this.Status = "Disconnected";
            this.processState = DeviceController.PROCESS_STATE_UNKNOWN;
        }

        void IConnectionReport.On_Connection_Error(string Msg)
        {
            this._IsConnected = false;
            this.processState = DeviceController.PROCESS_STATE_ERROR;
        }

        void IConnectionReport.On_Connection_Message(object Msg)
        {
            Console.WriteLine(_Config.DeviceName + " Receive: " + Msg);
        }
    }

}