using SanwaMarco.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SanwaMarco
{
    public class API
    {
        public Dictionary<string, string> varMap { get => varMap; set => varMap = value; }

        public API(Dictionary<string, string> varMap)
        {
            this.varMap = varMap;
        }

        public API()
        {

        }
        //泓格 I7565DNM 設備激活
        public uint I7565DNM_ACTIVE(string portNo)
        {
            byte ActiveBoardNo = Byte.Parse(portNo);//covert port id to byte
            return I7565DNM_DotNET.I7565DNM.I7565DNM_ActiveModule(ActiveBoardNo);
        }
        //泓格 I7565DNM 設備關閉:此函數是用於停止和關閉 USB 驅動程序。這種方法必須在退出應用程序之前調用一次。
        public uint I7565DNM_CLOSE(string portNo)
        {
            byte ActiveBoardNo = Byte.Parse(portNo);//covert port id to byte
            return I7565DNM_DotNET.I7565DNM.I7565DNM_CloseModule(ActiveBoardNo);
        }
        //泓格 I7565DNM 設備清除設定
        public uint I7565DNM_CLEAR_CONFIG(string portNo)
        {
            byte ActiveBoardNo = Byte.Parse(portNo);//covert port id to byte
            return I7565DNM_DotNET.I7565DNM.I7565DNM_ClearAllConfig(ActiveBoardNo);
        }
        //泓格 I7565DNM 設備新增模組與設定連線
        public uint I7565DNM_ADD_DEVICE(string portNo, DeviceNetCtrl dnm)
        {
            byte ActiveBoardNo = Byte.Parse(portNo);//covert port id to byte
            uint result;
            //Add Device Into Firmware
            result = I7565DNM_DotNET.I7565DNM.I7565DNM_AddDevice(ActiveBoardNo, dnm.getDesMACID(), 1000);//Explicit_EPR : [輸入] 預期的包速率。 （通常是 2500）。//參考範例給的值是1000
            if (result != 0)
                return result;
            // Configure Connection
            result = I7565DNM_DotNET.I7565DNM.I7565DNM_AddIOConnection(ActiveBoardNo, dnm.getDesMACID(), dnm.ConType(), dnm.DeviceInputLen, dnm.DeviceOutputLen, dnm.EPR);
            return result;
        }
        //泓格 I7565DNM 啟動設備通訊
        public uint I7565DNM_START(string portNo)
        {
            byte ActiveBoardNo = Byte.Parse(portNo);//covert port id to byte
            return I7565DNM_DotNET.I7565DNM.I7565DNM_StartAllDevice(ActiveBoardNo);
        }
        //泓格 I7565DNM 停止設備通訊
        public uint I7565DNM_STOP(string portNo)
        {
            byte ActiveBoardNo = Byte.Parse(portNo);//covert port id to byte
            return I7565DNM_DotNET.I7565DNM.I7565DNM_StopAllDevice(ActiveBoardNo);
        }
        //泓格 I7565DNM 取得模組狀態
        public uint I7565DNM_MODULE_STATUS(string portNo, DeviceNetCtrl dnm)
        {
            byte cPort = Byte.Parse(portNo);//covert port id to byte
            byte DesMACID = dnm.getDesMACID();//get macid
            return I7565DNM_DotNET.I7565DNM.I7565DNM_GetSlaveStatus(cPort, DesMACID);
        }
        //泓格 I7565DNM 讀取模組IO
        public uint I7565DNM_GETIO(string portNo, DeviceNetCtrl dnm, int ioNo)
        {
            UInt16 IOLen = 0;
            Byte[] IODATA = new Byte[512];
            byte cPort = Byte.Parse(portNo);//covert port id to byte
            byte DesMACID = dnm.getDesMACID();//get macid
            UInt32 Ret = I7565DNM_DotNET.I7565DNM.I7565DNM_ReadInputData(cPort, DesMACID, dnm.ConType(), ref IOLen, IODATA);
            uint temp = ioNo > 8 ? IODATA[1] : IODATA[0];
            int bit = ioNo > 8 ? ioNo - 8 - 1 : ioNo - 1;

            return (temp >> bit) & 1;//向右移動N-1位後, 與 00000001 做 AND 運算
        }
        //泓格 I7565DNM 設定模組IO
        public uint I7565DNM_SETIO(string portNo, DeviceNetCtrl dnm, int ioNo, uint value)
        {
            UInt16 IOLen = 0;
            Byte[] IODATA = new Byte[512];
            Byte[] ODATA = new Byte[512];
            byte cPort = Byte.Parse(portNo);//covert port id to byte
            byte DesMACID = dnm.getDesMACID();//get macid
            //Step 1 先取得目前IO
            UInt32 Ret = I7565DNM_DotNET.I7565DNM.I7565DNM_ReadInputData(cPort, DesMACID, dnm.ConType(), ref IOLen, IODATA);
            //Step 2 變更IO data
            //i = i | BIT(x); //set bit   : 與 00000001 做 OR 運算
            //i = i & ~BIT(x);//clear bit : 與 11111110 做 AND 運算
            uint temp = ioNo > 8 ? IODATA[1] : IODATA[0];
            if (ioNo > 8)
            {
                IODATA[1] = (byte)(value == 0 ? IODATA[1] & ~BIT(ioNo - 8) : IODATA[1] | BIT(ioNo - 8));//0: clear , 1:set
            }
            else
            {
                IODATA[0] = (byte)(value == 0 ? IODATA[0] & ~BIT(ioNo) : IODATA[0] | BIT(ioNo));//0: clear , 1:set
            }
            //Step 3 置換ODATA
            if (dnm.DeviceOutputLen == IOLen)
                ODATA = IODATA;//此模組都是OUTPUT
            else
                ODATA[0] = IODATA[1];//只有1 byte , ODATA 需去掉 input 的部分
            //Step4 將ODATA寫入模組
            Ret = I7565DNM_DotNET.I7565DNM.I7565DNM_WriteOutputData(cPort, DesMACID, dnm.ConType(), dnm.DeviceOutputLen, ODATA);
            return Ret;
        }
        //泓格 I7565DNM 設備初始化
        public uint I7565DNM_INIT(string portNo,string fileName)
        {
            // Add Device Into Firmware
            // Configure Connection
            // Start Device
            uint ret = 999999999;
            try
            {
                byte ActiveBoardNo = Byte.Parse(portNo);//covert port id to byte
                ret = I7565DNM_ACTIVE(portNo);
                if ((ret == I7565DNM_DotNET.I7565DNM.DNMXS_NoError) || (ret == I7565DNM_DotNET.I7565DNM.I7565DNM_PortInUse))
                {
                    ret = I7565DNM_CLEAR_CONFIG(portNo);
                    if (ret != 0)
                        return ret;//Clear FAIL
                    //int[] Slaves = new int[] { 0,1,2,3,4,5,6,7,8};
                    //for (int g = 0; g < Slaves.Length - 1; g++)
                    //{
                    //    DeviceNetCtrl dnm = new DeviceNetCtrl();
                    //    I7565DNM_ADD_DEVICE(portNo, dnm);
                    //    if (ret != 0)
                    //        return;//Add device FAIL
                    //}
                    DeviceNetCtrl dnm = new DeviceNetCtrl();
                    I7565DNM_ADD_DEVICE(portNo, dnm);
                    if (ret != 0)
                        return ret;//Add device FAIL
                    ret = I7565DNM_START(portNo);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
            }
            return ret;
        }

        public void I7565DNM_CLOSE()
        {
        //    Ret = I7565DNM_DotNET.I7565DNM.I7565DNM_ActiveModule(ActiveBoardNo)
        //If(Ret = I7565DNM_DotNET.I7565DNM.DNMXS_NoError) Or(Ret = I7565DNM_DotNET.I7565DNM.I7565DNM_PortInUse) Then
        //    I7565DNM_DotNET.I7565DNM.I7565DNM_StopAllDevice(ActiveBoardNo)
        //    For G As Integer = 0 To SlaveID_A.Count - 1
        //        I7565DNM_DotNET.I7565DNM.I7565DNM_RemoveDevice(ActiveBoardNo, SlaveID_A(G)) 'Remove Device
        //        I7565DNM_DotNET.I7565DNM.I7565DNM_RemoveIOConnection(ActiveBoardNo, SlaveID_A(G), I7565DNM_DotNET.I7565DNM.ConType.ConType_Poll) 'Remove IO Connection
        //    Next
        //End If
        //Ret = I7565DNM_DotNET.I7565DNM.I7565DNM_CloseModule(ActiveBoardNo)
        //    // Add Device Into Firmware
        //    // Configure Connection
        //    // Start Device
        //    // 
        }

        public void I7565DNM_GETIO()
        {
            //Access I/O Data
        }
        public void I7565DNM_SETIO()
        {
            //Access I/O Data
        }
        //取得BIT位移, 供byte set or clear 運算使用
        private int BIT(int x)
        {
            int idx = x - 1;
            return 1 << idx;
        }

        public void doWork(DeviceController device)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(sendCommand), device);            
        }

        private void sendCommand(object data)
        {
            Thread t = Thread.CurrentThread;
            DeviceController device = (DeviceController)data;
            string command = "$1SET:RESET";
            Console.WriteLine("No.{0}-Thread[{1}]:{2}", device.Name, t.ManagedThreadId, t.ThreadState);
            device.processState = DeviceController.PROCESS_STATE_PROCESS;
            device.sendCommand(command);
            Thread.Sleep(10000);
            device.processState = DeviceController.PROCESS_STATE_IDLE;
        }

    }
}
