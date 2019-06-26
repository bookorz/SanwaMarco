using log4net;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using SanwaMarco.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SanwaMarco
{
    public class API
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(API));
        public Dictionary<string, string> varMap;
        public Dictionary<string, DeviceNetCtrl> deviceNetCtrlMap = new Dictionary<string, DeviceNetCtrl>();
        private string method_name;
        private Boolean failReturn;
        private string errCode;

        public API()
        {

        }

        public API(string method_name, Boolean failReturn, string errCode, Dictionary<string, string> varMap)
        {
            this.method_name = method_name;
            this.failReturn = failReturn;
            this.errCode = errCode;
            this.varMap = varMap;
        }

        #region 泓格 I7565DNM 控制
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
            byte DesMACID = dnm.getDesMACID();
            byte Contype = dnm.ConType();
            //Add Device Into Firmware
            result = I7565DNM_DotNET.I7565DNM.I7565DNM_AddDevice(ActiveBoardNo, DesMACID, 1000);//Explicit_EPR : [輸入] 預期的包速率。 （通常是 2500）。//參考範例給的值是1000
            if (result != 0)
                return result;
            // Configure Connection
            result = I7565DNM_DotNET.I7565DNM.I7565DNM_AddIOConnection(ActiveBoardNo, DesMACID, Contype, dnm.DeviceInputLen, dnm.DeviceOutputLen, dnm.EPR);
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
        public Byte[] KUMA_GETIO(string portNo, string desMACID)
        {
            UInt16 IOLen = 0;
            Byte[] IODATA = new Byte[512];
            byte DesMACID = Byte.Parse(desMACID);
            uint ret = I7565DNM_DotNET.I7565DNM.I7565DNM_ReadInputData(7, DesMACID, (Byte)I7565DNM_DotNET.I7565DNM.ConType.ConType_Poll, ref IOLen, IODATA);
            return IODATA;
        }
        //泓格 I7565DNM 讀取模組IO
        public uint I7565DNM_GETIO(string portNo, string ioNo)
        {
            int boardNo;
            int io;
            try
            {
                if (ioNo.Length == 3)
                {
                    boardNo = int.Parse(ioNo.Substring(0, 1));
                    io = int.Parse(ioNo.Substring(1));
                }
                else
                {
                    boardNo = int.Parse(ioNo.Substring(0, 2));
                    io = int.Parse(ioNo.Substring(2));
                }
                io = io + 1;//IO定義表從0開始
                DeviceNetCtrl dnm = deviceNetCtrlMap[boardNo.ToString()];
                UInt16 IOLen = 0;
                Byte[] IODATA = new Byte[512];
                byte cPort = Byte.Parse(portNo);//covert port id to byte
                byte DesMACID = dnm.getDesMACID();//get macid
                UInt32 Ret = I7565DNM_DotNET.I7565DNM.I7565DNM_ReadInputData(cPort, DesMACID, dnm.ConType(), ref IOLen, IODATA);
                uint temp = io > 8 ? IODATA[1] : IODATA[0];
                int bit = io > 8 ? io - 8 - 1 : io - 1;

                return (temp >> bit) & 1;//向右移動N-1位後, 與 00000001 做 AND 運算
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return 999999;
            }
        }
        //泓格 I7565DNM 設定模組IO
        public uint I7565DNM_SETIO(string portNo, string ioNo, uint value)
        {
            try
            {
                int boardNo;
                int io;
                if (ioNo.Length == 3)
                {
                    boardNo = int.Parse(ioNo.Substring(0, 1));
                    io = int.Parse(ioNo.Substring(1));
                }
                else
                {
                    boardNo = int.Parse(ioNo.Substring(0, 2));
                    io = int.Parse(ioNo.Substring(2));
                }
                io = io + 1;//IO定義表從0開始
                DeviceNetCtrl dnm = deviceNetCtrlMap[boardNo.ToString()];//boardNo: 0~63
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
                uint temp = io > 8 ? IODATA[1] : IODATA[0];
                if (io > 8)
                {
                    IODATA[1] = (byte)(value == 0 ? IODATA[1] & ~BIT(io - 8) : IODATA[1] | BIT(io - 8));//0: clear , 1:set
                }
                else
                {
                    IODATA[0] = (byte)(value == 0 ? IODATA[0] & ~BIT(io) : IODATA[0] | BIT(io));//0: clear , 1:set
                }
                //Step 3 置換ODATA
                if (dnm.DeviceOutputLen == 2)
                    ODATA = IODATA;//此模組都是OUTPUT
                else
                    ODATA[0] = IODATA[1];//只有1 byte , ODATA 需去掉 input 的部分
                                         //Step4 將ODATA寫入模組
                Ret = I7565DNM_DotNET.I7565DNM.I7565DNM_WriteOutputData(cPort, DesMACID, dnm.ConType(), dnm.DeviceOutputLen, ODATA);
                return Ret;
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return 999999;
            }
        }
        /// <summary>
        /// 泓格 I7565DNM 設備初始化
        /// </summary>
        /// <param name="portNo">Comport No</param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public uint I7565DNM_INIT(string portNo,string fileName)
        {
            // Add Device Into Firmware
            // Configure Connection
            // Start Device
            uint ret = 999999999;
            if (fileName == null || fileName.Trim().Equals(""))
                fileName = "mini.xls";
            string sConfigPath = System.Environment.CurrentDirectory + "\\" + fileName;
            try
            {
                byte ActiveBoardNo = Byte.Parse(portNo);//covert port id to byte
                ret = I7565DNM_ACTIVE(portNo);
                if ((ret == I7565DNM_DotNET.I7565DNM.DNMXS_NoError) || (ret == I7565DNM_DotNET.I7565DNM.I7565DNM_PortInUse))
                //10026 找不到 COM PORT
                //if (true)//測試用
                {
                    ret = I7565DNM_CLEAR_CONFIG(portNo);
                    //ret = 0;//測試用
                    if (ret != 0)
                        return ret;//Clear FAIL
                    #region 讀取 EXCEL 設定檔
                    try
                    {
                        HSSFWorkbook hssfwb;
                        deviceNetCtrlMap.Clear();
                        using (FileStream file = new FileStream(sConfigPath, FileMode.Open, FileAccess.Read))
                        {
                            hssfwb = new HSSFWorkbook(file);
                        }

                        Sheet sheet = hssfwb.GetSheet("Sheet1");
                        Row hRow = sheet.GetRow(0);//header
                        for (int ci = 1; ci <= hRow.LastCellNum - 1; ci++)//column index, 0 是MASTER 不處理
                        {
                            string moduleName = "";
                            Cell cell = hRow.GetCell(ci);
                            cell.SetCellType(NPOI.SS.UserModel.CellType.STRING);
                            moduleName = cell.StringCellValue;
                            //logger.Info(string.Format("Row {2} Column {0} = {1}", ci, cell.StringCellValue, 0));
                            string[] ioNames = new string[16];
                            for (int r = 1; r <= sheet.LastRowNum; r++)//row index
                            {
                                Row dRow = sheet.GetRow(r);//io define
                                cell = dRow.GetCell(ci);
                                cell.SetCellType(NPOI.SS.UserModel.CellType.STRING);
                                ioNames[r - 1] = cell.StringCellValue;
                                logger.Info(string.Format("Row {2} Column {0} = {1}", ci, cell.StringCellValue, r));
                            }
                            //logger.Info("--------------------------------");
                            DeviceNetCtrl dnm = new DeviceNetCtrl(moduleName, ci.ToString(), ioNames);//MACID 從1開始
                            deviceNetCtrlMap.Add(ci.ToString(),dnm);
                        }
                    }
                    catch (Exception e)
                    {
                        logger.Error(e.StackTrace);
                    }
                    #endregion
                    #region I7565DNM_ADD_DEVICE & I7565DNM_START
                    foreach (KeyValuePair<string, DeviceNetCtrl> foo in deviceNetCtrlMap)
                    {
                        ret = I7565DNM_ADD_DEVICE(portNo, foo.Value);
                        if (ret != 0)
                            return ret;//Add device FAIL
                    }
                    ret = I7565DNM_START(portNo);
                    if (ret != 0)
                        return ret;//START device FAIL
                    #endregion
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message + "\n" + e.StackTrace);
            }
            return ret;
        }
        #endregion

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
            logger.Info("No." + device.Name + "-Thread[" + t.ManagedThreadId + " ]:" + t.ThreadState + "");
            device.processState = DeviceController.PROCESS_STATE_PROCESS;
            device.sendCommand(command);
            Thread.Sleep(10000);
            device.processState = DeviceController.PROCESS_STATE_IDLE;
        }

        public string Run()
        {
            String result = "";
            switch (method_name){
                case "ATEL_ROBOT_MOTION_CMD":
                    result = ATEL_ROBOT_MOTION_CMD();
                    break;
                case "ATEL_ROBOT_SET_CMD":
                    result = ATEL_ROBOT_SET_CMD();
                    break;
                case "ATEL_ROBOT_GET_CMD":
                    result = ATEL_ROBOT_GET_CMD();
                    break;
                default:
                    result = method_name + " not support";
                    break;
            }
            return result;
        }
        #region ATEL Robot 控制
        private string ATEL_ROBOT_MOTION_CMD()
        {
            string result = "ATEL_ROBOT_MOTION_CMD ERROR";
            string device = "";
            string cmd = "";
            DeviceController deviceCtrl = null;
            try
            {
                if (!getAtlCmd(ref result, ref device, ref cmd, ref deviceCtrl))
                {
                    return result;
                }
                SpinWait.SpinUntil(() => deviceCtrl.processState == DeviceController.PROCESS_STATE_IDLE, 1000);
                if (deviceCtrl.processState == DeviceController.PROCESS_STATE_IDLE)
                {
                    deviceCtrl.processState = DeviceController.PROCESS_STATE_PROCESS;
                    deviceCtrl.sendCommand(cmd);
                    //等待60秒
                    SpinWait.SpinUntil(() => (deviceCtrl.processState == DeviceController.PROCESS_STATE_IDLE) || (deviceCtrl.processState == DeviceController.PROCESS_STATE_ERROR), 60000);
                    result = deviceCtrl.errorCode;
                }
                else
                {
                    result = device + " is busy.";
                }
                return result;
            }
            catch (Exception e)
            {
                return e.StackTrace;
            }
        }

        private bool getAtlCmd(ref string result, ref string device, ref string cmd, ref DeviceController deviceCtrl)
        {
            try
            {
                varMap.TryGetValue("@device", out device);
                if (device == null)
                {
                    result = "device not define";
                    return false;
                }
                varMap.TryGetValue("@cmd", out cmd);
                if (cmd == null)
                {
                    result = "cmd not define";
                    return false;
                }
                Marco.deviceMap.TryGetValue(device, out object obj);
                if (obj == null)
                {
                    result = "Device:" + device + " not exist.";
                    return false;
                }
                deviceCtrl = (DeviceController)obj;
                return true;
            }
            catch (Exception e)
            {
                result = e.StackTrace;
                return false;
            }
        }
        private string ATEL_ROBOT_GET_CMD()
        {
            string result = "ATEL_ROBOT_GET_CMD ERROR";
            string device = "";
            string cmd = "";
            DeviceController deviceCtrl = null;
            try
            {
                if (!getAtlCmd(ref result, ref device, ref cmd, ref deviceCtrl))
                {
                    return result;
                }
                deviceCtrl.sendCommand(cmd);
                result = deviceCtrl.errorCode;
                return result;
                return result;
            }
            catch (Exception e)
            {
                return e.StackTrace;
            }
        }
        private string ATEL_ROBOT_SET_CMD()
        {
            string result = "ATEL_ROBOT_SET_CMD ERROR";
            string device = "";
            string cmd = "";
            DeviceController deviceCtrl = null;
            try
            {
                if (!getAtlCmd(ref result, ref device, ref cmd, ref deviceCtrl))
                {
                    return result;
                }
                deviceCtrl.sendCommand(cmd);
                result = deviceCtrl.errorCode;
                return result;
            }
            catch (Exception e)
            {
                return e.StackTrace;
            }
        }
        #endregion
    }
}
