using log4net;
using SanwaMarco.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanwaMarco
{
    public class MessageReport : IConnectionReport
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(MessageReport));
        public DeviceConfig _Config;
        void IConnectionReport.On_Connection_Connected(object Msg)
        {
            logger.Info(_Config.DeviceName + " 連線成功.");
        }

        void IConnectionReport.On_Connection_Connecting(string Msg)
        {
            logger.Info(_Config.DeviceName + " 連線中.");
        }

        void IConnectionReport.On_Connection_Disconnected(string Msg)
        {
            throw new NotImplementedException();
        }

        void IConnectionReport.On_Connection_Error(string Msg)
        {
            logger.Info(_Config.DeviceName + " 連線失敗.");
        }

        void IConnectionReport.On_Connection_Message(object Msg)
        {
            throw new NotImplementedException();
        }
    }
}
