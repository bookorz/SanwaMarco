using log4net;
using SanwaMarco.Comm;
using SanwaMarco.CommandConvert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SanwaMarco.Controller
{
    public class DeviceController : IConnectionReport, IDevice
    {
        public const string PROCESS_STATE_NOT_CONNECT = "NOT_CONNECT";
        public const string PROCESS_STATE_CONNECT_ERROR = "CONNECT_ERROR";
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
        public string processState = PROCESS_STATE_NOT_CONNECT;
        public string errorCode = "";
        CommandDecoder _Decoder;

        private bool _IsConnected { get; set; }
        public int TrxNo = 1;
        bool WaitingForSync = false;
        string ReturnForSync = "";
        string ReturnTypeForSync = "";

        public DeviceController(DeviceConfig Config)
        {
            _Config = Config;
            _Decoder = new CommandConvert.CommandDecoder(_Config.Vendor);

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
            errorCode = "";
            bool result = false;
            try
            {
                if (!Status.Equals("Connected"))
                {
                    logger.Error(Name + " is not Connected.");
                    return false;
                }
                if (_Config.Vendor.ToUpper().Equals("SANWA"))
                    msg = msg + "\r"; ;
                logger.Debug(_Config.DeviceName + " Send:" + msg);
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

        void IConnectionReport.On_Connection_Connected(object MsgObj)
        {
            this._IsConnected = true;
            this.Status = "Connected";
            //this.processState = DeviceController.PROCESS_STATE_INIT;
            this.processState = DeviceController.PROCESS_STATE_IDLE;
        }

        void IConnectionReport.On_Connection_Connecting(string MsgObj)
        {
            this._IsConnected = false;
            this.Status = "Connecting";
        }

        void IConnectionReport.On_Connection_Disconnected(string MsgObj)
        {
            this._IsConnected = false;
            this.Status = "Disconnected";
            this.processState = DeviceController.PROCESS_STATE_UNKNOWN;
        }

        void IConnectionReport.On_Connection_Error(string MsgObj)
        {
            this._IsConnected = false;
            //this.processState = DeviceController.PROCESS_STATE_ERROR;
            this.processState = DeviceController.PROCESS_STATE_CONNECT_ERROR; 
        }

        void IConnectionReport.On_Connection_Message(object MsgObj)
        {
            string msg = (string)MsgObj;
            msg = msg.Replace("\r", "");
            logger.Debug(_Config.DeviceName + " Recieve:" + msg);
            string cmdType = msg.Substring(2,3);
            switch (cmdType)
            {
                case "ACK":
                    if (msg.Equals("$1ACK:RESET"))
                        processState = PROCESS_STATE_IDLE;
                    break;
                case "NAK":
                    processState = PROCESS_STATE_ERROR;
                    errorCode = msg.Substring(msg.LastIndexOf(":") + 1, 8);
                    break;
                case "FIN":
                    string returnCode = msg.Substring(msg.LastIndexOf(":") + 1, 8);
                    if (returnCode.Equals("00000000")){
                        processState = PROCESS_STATE_IDLE;
                    }
                    else
                    {
                        processState = PROCESS_STATE_ERROR;
                        errorCode = returnCode;
                    }
                    break;
            }
                
            //List<CommandReturnMessage> ReturnMsgList = _Decoder.GetMessage(Msg);
            //foreach (CommandReturnMessage ReturnMsg in ReturnMsgList)
            //{
            //    Console.WriteLine(ReturnMsg.CommandType + ":" + ReturnMsg);
            //}
        }
    }

}
