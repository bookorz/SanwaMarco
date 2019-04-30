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

        public DeviceConfig _Config;
        void IConnectionReport.On_Connection_Connected(object Msg)
        {
            Console.WriteLine("連線成功阿~~~~~~");
        }

        void IConnectionReport.On_Connection_Connecting(string Msg)
        {
            Console.WriteLine("連線中阿~~~~~~");
        }

        void IConnectionReport.On_Connection_Disconnected(string Msg)
        {
            throw new NotImplementedException();
        }

        void IConnectionReport.On_Connection_Error(string Msg)
        {
            Console.WriteLine("連線失敗阿~~~~~~");
        }

        void IConnectionReport.On_Connection_Message(object Msg)
        {
            throw new NotImplementedException();
        }
    }
}
