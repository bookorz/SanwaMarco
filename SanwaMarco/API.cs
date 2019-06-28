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


        public string Run(ref string returnValue)
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
                case "I7565DNM_GETIO":
                    result = I7565DNM_GETIO(ref returnValue);
                    break;
                case "I7565DNM_GETIOS":
                    result = I7565DNM_GETIOS(ref returnValue);
                    break;
                case "I7565DNM_CHECK_IOS":
                    result = I7565DNM_CHECK_IOS(ref returnValue);
                    break;
                case "I7565DNM_SETIO":
                    result = I7565DNM_SETIO();
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
        #endregion

        #region 泓格 device net API
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
                if (value == null)
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
                uint s = deviceCtrl.I7565DNM_GETIO(io);
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
                Thread.Sleep(interval);//Fail 時 Sleep by interval 後重試
            }
            result = "I7565DNM_CHECK_IOS ERROR:" + a_values;//超過指定重試條件後，IO訊號依然未達期望值
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
                uint s = deviceCtrl.I7565DNM_GETIO(io);
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
                if (device == null)
                {
                    result = "device not define";
                    return false;
                }
                varMap.TryGetValue("@io", out io);
                if (io == null)
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
    }
}
