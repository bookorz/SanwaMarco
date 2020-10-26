using log4net;
using SanwaMarco.Controller;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SanwaMarco
{
    public class API
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(API));
        public Dictionary<string, string> varMap;
        private string method_name;
        private Boolean autoReturn;
        private string errCode;

        public API()
        {

        }

        public API(string method_name, Boolean autoReturn, string errCode, Dictionary<string, string> varMap)
        {
            this.method_name = method_name;
            this.autoReturn = autoReturn;
            this.errCode = errCode;
            this.varMap = varMap;
        }


        public string Run(ref string returnValue)
        {
            String result = "";
            switch (method_name){
                case "N2_PURGE_SET_CMD":
                    result = N2_PURGE_SET_CMD();
                    break;
                case "N2_PURGE_GET_CMD":
                    result = N2_PURGE_GET_CMD(ref returnValue);
                    break;
                case "ATEL_ROBOT_MOTION_CMD":
                    result = ATEL_ROBOT_MOTION_CMD();
                    break;
                case "ATEL_ROBOT_SET_CMD":
                    result = ATEL_ROBOT_SET_CMD();
                    break;
                case "ATEL_ROBOT_GET_CMD":
                    result = ATEL_ROBOT_GET_CMD();
                    break;
                case "I7565DNM_GETIO":
                    result = I7565DNM_GETIO(ref returnValue);
                    break;
                case "I7565DNM_GETIOS":
                    result = I7565DNM_GETIOS(ref returnValue);
                    break;
                case "I7565DNM_READIO":
                    result = I7565DNM_READIO(ref returnValue);
                    break;
                case "I7565DNM_READIOS":
                    result = I7565DNM_READIOS(ref returnValue);
                    break;
                case "I7565DNM_CHECK_IOS":
                    result = I7565DNM_CHECK_IOS(ref returnValue);
                    break;
                case "I7565DNM_SETIO":
                    result = I7565DNM_SETIO();
                    break;
                case "I7565DNM_SETIOS":
                    result = I7565DNM_SETIOS();
                    break;
                case "I7565DNM_CHECK_SLAVE":
                    result = I7565DNM_CHECK_SLAVE();
                    break;
                case "I7565DNM_REFRESH":
                    result = I7565DNM_REFRESH();
                    break;
                default:
                    result = method_name + " not support";
                    break;
            }
            return result;
        }

        private string I7565DNM_CHECK_SLAVE()
        {
            string result = "I7565DNM_CHECK_SLAVE ERROR";
            string device = "";
            I7565DNM deviceCtrl = null;
            try
            {
                varMap.TryGetValue("@device", out device);
                if (device == null)
                {
                    result = "device not define";
                    return result;
                }
                Marco.deviceMap.TryGetValue(device, out object obj);
                if (obj == null)
                {
                    result = "Device:" + device + " not exist.";
                    return result;
                }
                deviceCtrl = (I7565DNM)obj;
                deviceCtrl.I7565DNM_CHECK_SLAVE();
                result = deviceCtrl.errorCode;
                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return result;
            }
            
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
                    //等待600秒
                    SpinWait.SpinUntil(() => (deviceCtrl.processState == DeviceController.PROCESS_STATE_IDLE) || (deviceCtrl.processState == DeviceController.PROCESS_STATE_ERROR), 600000);
                    result = deviceCtrl.errorCode;
                }
                else
                {
                    result = device + " is " + deviceCtrl.processState + ".";
                }
                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return result;
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
                deviceCtrl.errorCode = "";
                deviceCtrl.sendCommand(cmd);
                result = deviceCtrl.errorCode;
                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return result;
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
                deviceCtrl.errorCode = "";
                deviceCtrl.sendCommand(cmd);
                result = deviceCtrl.errorCode;
                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return result;
            }

            //string result = "ATEL_ROBOT_SET_CMD ERROR";
            //string device = "";
            //string cmd = "";
            //DeviceController deviceCtrl = null;

            //return setDeviceCommand(ref result, ref device, ref cmd, ref deviceCtrl);
            ////try
            ////{
            ////    if (!getAtlCmd(ref result, ref device, ref cmd, ref deviceCtrl))
            ////    {
            ////        return result;
            ////    }
            ////    deviceCtrl.errorCode = "";
            ////    deviceCtrl.sendCommand(cmd);
            ////    result = deviceCtrl.errorCode;
            ////    return result;
            ////}
            ////catch (Exception e)
            ////{
            ////    logger.Error(e.StackTrace);
            ////    return result;
            ////}
        }
        #endregion

        #region 泓格 device net API
        /// <summary>
        /// 更新Input 資料
        /// </summary>
        /// <returns></returns>
        private string I7565DNM_REFRESH()
        {
            string result = "I7565DNM_REFRESH ERROR";
            string device = "";
            I7565DNM deviceCtrl = null;
            try
            {
                varMap.TryGetValue("@device", out device);
                if (device == null)
                {
                    result = "device not define";
                    return result;
                }
                Marco.deviceMap.TryGetValue(device, out object obj);
                if (obj == null)
                {
                    result = "Device:" + device + " not exist.";
                    return result;
                }
                deviceCtrl = (I7565DNM)obj;
                deviceCtrl.I7565DNM_REFRESH();
                result = deviceCtrl.errorCode;
                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return result;
            }
        }
        private string I7565DNM_SETIOS()
        {
            string result = "I7565DNM_SETIOS ERROR";
            string device = "";
            string values = "";
            string ios = "";
            I7565DNM deviceCtrl = null;
            try
            {
                if (!checkI7565DNM(ref result, ref device, ref ios, ref deviceCtrl))
                {
                    return result;
                }
                varMap.TryGetValue("@value", out values);
                if (values == null || values.Equals("undefined"))
                {
                    result = "value not define";
                    return result;
                }
                string[] valueAry = values.Split(';');
                if (valueAry.Length!= 1 & (valueAry.Length != ios.Split(';').Length))
                {
                    result = "value item count error";
                    return result;
                }
                int idx = 0;
                foreach (string io in ios.Split(';'))
                {
                    uint value = valueAry.Length == 1 ? Convert.ToUInt32(valueAry[0]) : Convert.ToUInt32(valueAry[idx]);
                    deviceCtrl.I7565DNM_SETIO(io, value);
                    result = deviceCtrl.errorCode;
                    if (!result.Equals(""))
                    {
                        return result;
                    }
                    idx++;
                }
                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return result;
            }
        }

        private string I7565DNM_SETIO()
        {
            string result = "I7565DNM_SETIO ERROR";
            string device = "";
            string value = "";
            string io = "";
            I7565DNM deviceCtrl = null;
            try
            {
                if (!checkI7565DNM(ref result, ref device, ref io, ref deviceCtrl))
                {
                    return result;
                }
                varMap.TryGetValue("@value", out value);
                if (value == null || value.Equals("undefined"))
                {
                    result = "value not define";
                    return result;
                }
                deviceCtrl.I7565DNM_SETIO(io, Convert.ToUInt32(value));
                result = deviceCtrl.errorCode;
                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return result;
            }
        }
        private string I7565DNM_GETIOS(ref string values)
        {
            string result = "I7565DNM_GETIOS ERROR";
            string device = "";
            string ios = "";
            I7565DNM deviceCtrl = null;
            if (!checkI7565DNM(ref result, ref device, ref ios, ref deviceCtrl))
            {
                return result;
            }
            string[] ioAry = ios.Split(';');
            foreach(string io in ioAry)
            {
                uint s = deviceCtrl.I7565DNM_GETIO(io, true);
                values = values + s.ToString();
                result = deviceCtrl.errorCode;
                if (!result.Equals(""))
                    return result;
            }
            return result;
        }
        private string I7565DNM_READIOS(ref string values)
        {
            string result = "I7565DNM_READIOS ERROR";
            string device = "";
            string ios = "";
            I7565DNM deviceCtrl = null;
            if (!checkI7565DNM(ref result, ref device, ref ios, ref deviceCtrl))
            {
                return result;
            }
            string[] ioAry = ios.Split(';');
            foreach (string io in ioAry)
            {
                uint s = deviceCtrl.I7565DNM_READIO(io);
                values = values + s.ToString();
                result = deviceCtrl.errorCode;
                if (!result.Equals(""))
                    return result;
            }
            return result;
        }
        private string I7565DNM_CHECK_IOS(ref string a_values)
        {
            string result = "I7565DNM_CHECK_IOS ERROR";
            int interval;
            int retry_count;
            string values = "";

            //check interval
            if (!varMap.TryGetValue("@interval", out string s_interval))
            {
                result = "interval not define";
                return result;
            }
            else if (!int.TryParse(s_interval, out interval))
            {
                return "interval format error " + s_interval;
            }
            //check retry_count
            if (!varMap.TryGetValue("@retry_count", out string s_retry_count))
            {
                result = "retry_count not define";
                return result;
            }
            else if (!int.TryParse(s_retry_count, out retry_count))
            {
                return "retry_count format error " + s_retry_count;
            }
            //check values
            if (!varMap.TryGetValue("@values", out values))
            {
                result = "values not define";
                return result;
            }
            for (int i=0; i< retry_count; i++)
            {
                //logger.Info("Total retry_count:" + retry_count + " current count:" + i + " interval:" + interval);
                a_values = "";//每次執行前清空一次
                result = I7565DNM_GETIOS(ref a_values);
                if (!result.Equals(""))
                {
                    return result;//I7565DNM_GETIOS 異常
                }
                if (a_values.Equals(values))
                {
                    result = "";
                    return result;//I7565DNM_CHECK_IOS 成功
                }

                //2020.07.06 加入ems 判斷 Pingchung Start ++
                //檢查是否有外部訊號(EMS)需要立即停止動作
                if (Marco.runMode != Marco.RunMode.SrcScriptRun)    //API直接取用Marco元件不是好事
                {
                    if (varMap.TryGetValue("@ems_input", out string s_ems_io) && varMap.TryGetValue("@ems_values", out string s_ems_values))
                    {
                        //讀取ems_input訊號點位
                        //a_values為回傳值
                        result = I7556DNM_CHECK_EMS(ref a_values);
                        if (!result.Equals(""))
                            return result;

                        //比較回傳值與設定值是否相同
                        if (!a_values.Equals(s_ems_values))
                        {
                            result = "emergency stop " + s_ems_io + " ERROR:" + s_ems_values + " not match " + a_values;

                            //有設定ems_output的情況下，可停止對應的點位
                            if (varMap.TryGetValue("@ems_output", out string s_ems_output) && varMap.TryGetValue("@ems_enabled", out string s_ems_enabled))
                            {
                                //注意
                                //I7565DNM_CHECK_IOS讀取的TKey為@values
                                //II7565DNM_SETIO讀取的TKey為@value

                                varMap["@io"] = s_ems_output;
                                varMap["@value"] = s_ems_enabled;

                                if (!s_ems_output.Contains(";"))
                                {
                                    I7565DNM_SETIO();
                                }
                                else
                                {
                                    I7565DNM_SETIOS();
                                }

                            }
                            return result;
                        }
                    }
                }
                //2020.07.06 加入ems 判斷 Pingchung End ++

                Thread.Sleep(interval);//Fail 時 Sleep by interval 後重試
            }
            varMap.TryGetValue("@io", out string io);
            result = "I7565DNM_CHECK_IOS check "+ io + " ERROR:" + a_values + " not match " + values;//超過指定重試條件後，IO訊號依然未達期望值
            return result;
        }
        private string I7556DNM_CHECK_EMS(ref string a_values)  //讀取EMS Bit 
        { 
            string result = "I7565DNM_CHECK_EMS ERROR";
            string device = "";

            //check iems_io
            if (!varMap.TryGetValue("@ems_input", out string s_ems_io))
            {
                result = "ems_io not define";
                return result;
            }

            //check ems_values
            if (!varMap.TryGetValue("@ems_values", out string s_ems_values))
            {
                result = "ems_values not define";
                return result;
            }
            

            varMap.TryGetValue("@device", out device);
            if (device == null || device.Equals("undefined"))
            {
                result = "device not define";
                return result;
            }

            Marco.deviceMap.TryGetValue(device, out object obj);

            I7565DNM deviceCtrl = (I7565DNM)obj;

            //確認單點或這是多點
            if (!s_ems_io.Contains(";"))
            {
                uint s = deviceCtrl.I7565DNM_GETIO(s_ems_io, true);
                a_values = s.ToString();
                result = deviceCtrl.errorCode;
            }
            else
            {
                string deviceErrorCode = "";
                string[] ioAry = s_ems_io.Split(';');   //檢查點位(Address)
                foreach (string ss in ioAry)
                {
                    uint s = deviceCtrl.I7565DNM_GETIO(ss, true);
                    a_values = a_values + s.ToString();
                    deviceErrorCode = deviceCtrl.errorCode;

                    //硬體讀取值異常
                    if (!deviceErrorCode.Equals("")) result = deviceErrorCode;
                }
            }

            return result;
        }


        private string I7565DNM_GETIO(ref string value)
        {
            string result = "I7565DNM_GETIO ERROR";
            string device = "";
            string io = "";
            I7565DNM deviceCtrl = null;
            try
            {
                if (!checkI7565DNM(ref result, ref device, ref io, ref deviceCtrl))
                {
                    return result;
                }
                uint s = deviceCtrl.I7565DNM_GETIO(io, true);
                value = s.ToString();
                result = deviceCtrl.errorCode;
                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return result;
            }
        }
        private string I7565DNM_READIO(ref string value)
        {
            string result = "I7565DNM_READIO ERROR";
            string device = "";
            string io = "";
            I7565DNM deviceCtrl = null;
            try
            {
                if (!checkI7565DNM(ref result, ref device, ref io, ref deviceCtrl))
                {
                    return result;
                }
                uint s = deviceCtrl.I7565DNM_READIO(io);
                value = s.ToString();
                result = deviceCtrl.errorCode;
                return result;
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                return result;
            }
        }
        
        private bool checkI7565DNM(ref string result, ref string device, ref string io, ref I7565DNM deviceCtrl)
        {
            try
            {
                varMap.TryGetValue("@device", out device);
                if (device == null || device.Equals("undefined"))
                {
                    result = "device not define";
                    return false;
                }
                varMap.TryGetValue("@io", out io);
                if (io == null || io.Equals("undefined"))
                {
                    result = "io not define";
                    return false;
                }
                Marco.deviceMap.TryGetValue(device, out object obj);
                if (obj == null)
                {
                    result = "Device:" + device + " not exist.";
                    return false;
                }
                deviceCtrl = (I7565DNM)obj;
                return true;
            }
            catch (Exception e)
            {
                result = e.StackTrace;
                return false;
            }
        }
        #endregion

        private string N2_PURGE_SET_CMD()
        {
            string result = "N2_PURGE_SET_CMD ERROR";
            string device = "";
            string cmd = "";
            DeviceController deviceCtrl = null;

            return setDeviceCommand(ref result, ref device, ref cmd, ref deviceCtrl);
        }
        
        private string setDeviceCommand(ref string result, ref string device, ref string cmd, ref DeviceController deviceCtrl)
        {
            try
            {
                if (getAtlCmd(ref result, ref device, ref cmd, ref deviceCtrl))
                {
                    DeviceController tempdeviceCtrl = null;
                    tempdeviceCtrl = deviceCtrl;

                    SpinWait.SpinUntil(() => (tempdeviceCtrl.processState == DeviceController.PROCESS_STATE_IDLE) ||
                                                (tempdeviceCtrl.processState == DeviceController.PROCESS_STATE_ERROR), 10000);


                    deviceCtrl.errorCode = "";
                    deviceCtrl.processState = DeviceController.PROCESS_STATE_PROCESS;
                    deviceCtrl.sendCommand(cmd);

                    result = deviceCtrl.errorCode;
                    //if (deviceCtrl.processState == DeviceController.PROCESS_STATE_IDLE)
                    //{
                    //    deviceCtrl.errorCode = "";
                    //    deviceCtrl.processState = DeviceController.PROCESS_STATE_PROCESS;
                    //    deviceCtrl.sendCommand(cmd);

                    //    ////等待600秒
                    //    //SpinWait.SpinUntil(() => (tempdeviceCtrl.processState == DeviceController.PROCESS_STATE_IDLE) 
                    //    //                            || (tempdeviceCtrl.processState == DeviceController.PROCESS_STATE_ERROR), 
                    //    //                            600000);
                    //    //result = deviceCtrl.errorCode;
                    //}
                    //else
                    //{
                    //    result = device + " is " + deviceCtrl.processState + ".";
                    //}

                }

            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
                result = e.StackTrace;
            }

            return result;
        }

        private string N2_PURGE_GET_CMD(ref string returnValue)
        {
            string result = "N2_PURGE_GET_CMD ERROR";
            string device = "";
            string cmd = "";
            DeviceController deviceCtrl = null;

            setDeviceCommand(ref result, ref device, ref cmd, ref deviceCtrl);

            if (result.Equals(""))
            {
                DeviceController tempdeviceCtrl = null;
                tempdeviceCtrl = deviceCtrl;

                SpinWait.SpinUntil(() => (tempdeviceCtrl.processState == DeviceController.PROCESS_STATE_IDLE) 
                                            || (tempdeviceCtrl.processState == DeviceController.PROCESS_STATE_ERROR), 
                                            10000);

                if (deviceCtrl.processState == DeviceController.PROCESS_STATE_IDLE)
                {
                    returnValue = tempdeviceCtrl.FINReturnCode;
                }
                else
                {
                    result = device + " is " + deviceCtrl.processState + ".";
                }

            }

            return result;

        }
    }
}
