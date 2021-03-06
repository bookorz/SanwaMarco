﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SanwaMarco.Comm
{
    public class TcpCommClient : IConnection
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(TcpCommClient));
        IConnectionReport ConnReport;
        DeviceConfig Config;

        //先建立一個TcpClient;
        TcpClient tcpClient = new TcpClient();

        public TcpCommClient(DeviceConfig _Config, IConnectionReport _ConnReport)
        {
            Config = _Config;

            ConnReport = _ConnReport;
        }
        public bool Send(object Message)
        {
            try
            {
                NetworkStream ns = tcpClient.GetStream();
                if (ns.CanWrite)
                {
                    byte[] msgByte = Encoding.Default.GetBytes(Message.ToString());
                    ns.Write(msgByte, 0, msgByte.Length);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return false;
            }
            return true;
        }

        public bool SendHexData(object Message)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            try
            {
                if (tcpClient.Connected)
                {
                    tcpClient.Client.Shutdown(SocketShutdown.Both);
                    tcpClient.Client.Disconnect(false);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
            }
            ThreadPool.QueueUserWorkItem(new WaitCallback(ConnectServer));
        }

        private void ConnectServer(object input)
        {
            ConnReport.On_Connection_Connecting("["+ Config.DeviceName + "] Connecting");
            //先建立IPAddress物件,IP為欲連線主機之IP
            
            IPAddress ipa = IPAddress.Parse(Config.IPAdress);

            //建立IPEndPoint
            IPEndPoint ipe = new IPEndPoint(ipa, Config.Port);

            //開始連線
            try
            {

                tcpClient.Connect(ipe);
                if (tcpClient.Connected)
                {
                    ConnReport.On_Connection_Connected("[" + Config.DeviceName + "] Connected");
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Receive));
                }
                else
                {
                    ConnReport.On_Connection_Error(Config.DeviceName + "Error");
                }

            }
            catch (Exception e)
            {
                tcpClient.Close();
                logger.Error("[" + Config.DeviceName + "] 連線失敗" + e.StackTrace);
                ConnReport.On_Connection_Error("[" + Config.DeviceName + "] " + e.Message + "\n" + e.StackTrace);                
            }

        }

        private void Receive(object input)
        {
            string receiveMsg = string.Empty;


            int numberOfBytesRead = 0;
            NetworkStream ns = tcpClient.GetStream();

            try
            {
                while (tcpClient.Connected)
                {

                    if (ns.CanRead)
                    {
                        do
                        {
                            byte[] receiveBytes = new byte[tcpClient.ReceiveBufferSize];
                            numberOfBytesRead = ns.Read(receiveBytes, 0, tcpClient.ReceiveBufferSize);

                            byte[] bytesRead = new byte[numberOfBytesRead];
                            Array.Copy(receiveBytes, bytesRead, numberOfBytesRead);
                            //receiveMsg = Encoding.Default.GetString(receiveBytes, 0, numberOfBytesRead);
                            socketDataArrivalHandler(bytesRead);
                        }
                        while (ns.DataAvailable);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace, e);
                //FormMainUpdate.ShowMessage(e.StackTrace);
            }
        }

        string S = "";

        private void socketDataArrivalHandler(byte[] OrgData)
        {

            string data = "";
            switch (Config.Vendor.ToUpper())
            {
                case "TDK":


                    S += Encoding.Default.GetString(OrgData, 0, OrgData.Length);
                    if (S.LastIndexOf(Convert.ToChar(3)) != -1)
                    {
                        //logger.Debug("s:" + S);
                        data = S.Substring(0, S.LastIndexOf(Convert.ToChar(3)) + 1);
                        //logger.Debug("data:" + data);

                        S = S.Substring(S.LastIndexOf(Convert.ToChar(3)) + 1);
                        //logger.Debug("s:" + S);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ConnReport. On_Connection_Message), data);
                        break;
                    }


                    break;
                case "SANWA":

                    S += Encoding.Default.GetString(OrgData, 0, OrgData.Length);

                    if (S.LastIndexOf("\r") != -1)
                    {
                        //logger.Debug("s:" + S);
                        data = S.Substring(0, S.LastIndexOf("\r"));
                        //logger.Debug("data:" + data);

                        S = S.Substring(S.LastIndexOf("\r") + 1);
                        //logger.Debug("s:" + S);
                        ThreadPool.QueueUserWorkItem(new WaitCallback(ConnReport.On_Connection_Message), data);
                        break;
                    }

                    break;
                default:
                    data = Encoding.Default.GetString(OrgData, 0, OrgData.Length);

                    ThreadPool.QueueUserWorkItem(new WaitCallback(ConnReport.On_Connection_Message), data);

                    break;
            }
        }
        public void WaitForData(bool Enable)
        {
            //throw new NotImplementedException();
        }
    }
}
