using log4net;
using SanwaMarco.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// GUI 指令處理
/// </summary>
namespace SanwaMarco
{
    public class StateObject
    {
        // Client  socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();


        //public void ClearBuffer()
        //{
        //    buffer = new byte[BufferSize];
        //}
    }

    public class GUICmdCtrl : ICommMessage
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(GUICmdCtrl));
        Random ran = new Random();
        ICommMessage _EventReport;
        // Thread signal.  
        public ManualResetEvent allDone = new ManualResetEvent(false);
        public Socket replayer;

        public GUICmdCtrl()
        {
            _EventReport = this;
            Marco._EventReport = this;
        }
        public void Start()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartListening));
        }

        public GUICmdCtrl(ICommMessage EventReport)
        {
            _EventReport = EventReport;
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartListening));
        }

        public void StartListening(object msg)
        {
            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  

            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 9527);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(1);
                _EventReport.On_Connection_Connecting();
                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    //Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                logger.Error(e.Message + "\n" + e.StackTrace);
            }

            //Console.WriteLine("\nPress ENTER to continue...");
            //Console.Read();

        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            _EventReport.On_Connection_Connected(handler);
            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }
        string S = "";
        public void ReadCallback(IAsyncResult ar)
        {
            try
            {
                String content = String.Empty;

                // Retrieve the state object and the handler socket  
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;

                // Read data from the client socket.   

                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There  might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(
                        state.buffer, 0, bytesRead));

                    // Check for end-of-file tag. If it is not there, read   
                    // more data.  
                    content = S + state.sb.ToString();//資料不完全時，先存在S，之後在與接續的指令合併
                    if (content.IndexOf("\r") > -1 || content.IndexOf(Convert.ToChar(3)) != -1)
                    {
                        // All the data has been read from the   
                        // client. Display it on the console.  
                        //Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        //    content.Length, content);
                        string data = "";
                        data = content.Substring(0,content.LastIndexOf("\r"));
                        S = content.Substring(content.LastIndexOf("\r") + 1);
                        state.sb.Clear();
                        _EventReport.On_Connection_Message(handler, data);
                        //state.ClearBuffer();
                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                    }
                    else
                    {
                        // Not all data received. Get more.  
                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                    }
                }
            }
            catch (Exception e)
            {
                _EventReport.On_Connection_Disconnected();
            }
        }

        public void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            try
            {
                handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
            }
            catch (Exception e)
            {
                _EventReport.On_Connection_Disconnected();
            }

        }

        private void SendCallback( IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                //Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void cmdProcess(Socket handler, string msg)
        {
            try
            {
                replayer = handler;
                Dictionary<string, string> argMap = new Dictionary<string, string>();
                argMap.Add("msg", msg);
                #region 訊息解析
                string returnMsg = "";
                string orgMsg = msg.Substring(6, msg.Length - 7);//去除前六碼, 與最後的;
                argMap.Add("orgMsg", orgMsg);

                //分解訊息
                string[] rcvMsgAry = msg.Substring(0, msg.Length - 1).Split('/');//去最後一碼;
                //取得命令
                string cmd = rcvMsgAry[0];
                argMap.Add("cmd", cmd);
                //取得引數
                string[] rcvArgs = new string[rcvMsgAry.Length - 1];//0 是命令，不是參數，所以長度少1
                Array.Copy(rcvMsgAry, 1, rcvArgs, 0, rcvArgs.Length);
                //分解命令: 取得 address
                string address = cmd.Substring(1, 1);
                argMap.Add("address", address);
                //分解命令: 取得 命令類別
                string cmd_type = cmd.Substring(2, 3);
                argMap.Add("cmd_type", cmd_type);
                //分解命令: 取得 功能名稱
                int lastIdx = cmd.LastIndexOf(":") > 5 ? cmd.LastIndexOf(":") : cmd.Length;
                string func_name = cmd.Substring(6, lastIdx - 6).Replace(";","");// 前六碼為固定前修飾詞, 例如 $1CMD: $2MCR:
                //宣告回傳參數與錯誤原因
                string[] param = null;
                string factor = "";
                #endregion

                #region 處理INF 的 ACK
                if (cmd_type.Equals("ACK"))
                {
                    logger.Info("Receive:" + msg);
                    return;//收到前台ACK 暫時不做事情
                }
                #endregion

                #region 檢查指令格式與目前狀態是否能執行
                checkCmd(msg, ref factor, ref param);
                switch (factor)
                {
                    case "MSG":
                        returnMsg = "$" + address + "NAK:MSG|" + orgMsg;
                        Send(handler, returnMsg + ";\r");
                        return;
                    case "ACK":
                        returnMsg = "$" + address + "ACK:" + orgMsg;
                        Send(handler, returnMsg + ";\r");
                        break;
                    case "CAN":
                    default:
                        //returnMsg = "$" + address + "CAN:" + orgMsg + "|" + factor + "/Place";
                        returnMsg = "$" + address + "CAN:" + orgMsg + "|" + factor ;
                        Send(handler, returnMsg + ";\r");
                        return;
                }
                #endregion

                #region 執行工作
                //Thread.Sleep(50);20191114 取消 SLEEP for IO REFRESH 效能
                returnMsg = "$" + address;
                switch (func_name)
                {
                    #region Robot 指令
                    case "ROBOT_CARRY":
                        argMap.Add("@src", rcvArgs[0]);//src : P101, SHELF1, ... 等等; 讓Marco 自行處理可辨識的名稱
                        argMap.Add("@dest", rcvArgs[1]);//dest : P101, SHELF1, ... 等等; 讓Marco 自行處理可辨識的名稱
                        Marco.RunMarco(func_name, argMap);
                        break;
                    case "ROBOT_PRESENCE":
                        argMap.Add("@position", rcvArgs[0]);//position : ARM1,ARM2 ... 等等; 讓Marco 自行處理可辨識的名稱
                        Marco.RunMarco(func_name, argMap);
                        break;
                    #endregion
                    default://不須特別做參數處理的 function, 傳入之參數會依序放在 @arg1...@argn
                        for (int i = 0; i < rcvArgs.Length; i++)
                        {
                            argMap.Add("@arg" + (i + 1), rcvArgs[i]);
                        }
                        Marco.RunMarco(func_name, argMap);
                        break;
                }
                #endregion
            }
            catch (Exception e)
            {
                logger.Error(e.Message + " " + e.StackTrace);
            }
        }

        private void checkCmd(string msg, ref string factor, ref string[] args)
        {
            //factor =  "Factor2";//取消指令時會給各式對應原因
            //factor = "MSG";//訊息格式或參數錯誤
            factor = "ACK";
        }

        public void On_Connection_Message(Socket handler, string Msg )
        {
            string[] MsgAry = ((string)Msg).Split(new string[] { "\r" }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string msg in MsgAry)
            {
                //Thread.Sleep(50);20191114 取消 SLEEP for IO REFRESH 效能
                cmdProcess(handler, msg);
            }
        }


        public void On_Connection_Connecting()
        {
            Console.WriteLine("On_Connection_Connecting");
        }

        public void On_Connection_Connected(Socket handler)
        {
            Console.WriteLine("On_Connection_Connected");
        }

        public void On_Connection_Disconnected()
        {
            Console.WriteLine("On_Connection_Disconnected");
        }

        public void On_Connection_Error(string Msg)
        {
            Console.WriteLine("On_Connection_Error");
        }

        public void On_Marco_Finish(object obj)
        {
            string returnMsg = "";
            JobUtil job = (JobUtil) obj;
            #region 回傳訊息
            if (job.jobResult.Equals(""))
            {
                //INF
                //returnMsg = msg.Replace("MCR", "INF").Replace("GET", "INF").Replace("SET", "INF").Replace(";", "");
                returnMsg = "$" + job.localVarMap["address"] + "INF:" + job.localVarMap["orgMsg"];
                if (!job.jobData.Equals(""))
                {
                    returnMsg = returnMsg + "/" + job.jobData;
                }
            }
            else
            {
                //ABS
                //returnMsg = job.localVarMap["msg"].Replace("MCR", "ABS").Replace("GET", "ABS").Replace(";", "") + "|ERROR/" + job.result  + "/Place";
                returnMsg = job.localVarMap["msg"].Replace("MCR:", "ABS:").Replace("GET:", "ABS:").Replace(";", "") + "|ERROR/" + job.jobResult ;
            }
            Send(replayer, returnMsg + ";\r");//send INF or ABS
            #endregion
        }
        public void On_Marco_Print(string msg)
        {
            Send(replayer, msg + ";\r");//send INF or ABS
        }

    }
}
