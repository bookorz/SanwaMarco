﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static I7565DNM_DotNET.I7565DNM;

namespace SanwaMarco.Controller
{
    public class DeviceNetCtrl
    {
        public String ModuleName;
        private string desMACID;//0~63
        public Byte getDesMACID()
        {
            return Byte.Parse(desMACID);
        }
        public Byte ConType()
        {
            return (Byte)I7565DNM_DotNET.I7565DNM.ConType.ConType_Poll;
        }

        public DeviceNetCtrl(string moduleName, string desMACID, string[] ioNames)
        {
            ModuleName = moduleName;
            this.desMACID = desMACID;
            switch (moduleName)
            {
                case "DRT2-ID08":
                    DeviceInputLen = 1;
                    DeviceOutputLen = 0;
                    break;
                case "DRT2-ID16TA":
                    DeviceInputLen = 2;
                    DeviceOutputLen = 0;
                    break;
                case "DRT2-OD16TA":
                    DeviceInputLen = 0;
                    DeviceOutputLen = 2;
                    break;
                case "DRT2-MD16":
                    DeviceInputLen = 1;
                    DeviceOutputLen = 1;
                    break;
                case "EX180-SDN":
                case "EX180-SDN1":
                    DeviceInputLen = 0;
                    DeviceOutputLen = 2;
                    break;
                default:
                    DeviceInputLen = 0;
                    DeviceOutputLen = 0;
                    break;
            }
            iO_01_NAME = ioNames[0];
            iO_02_NAME = ioNames[1];
            iO_03_NAME = ioNames[2];
            iO_04_NAME = ioNames[3];
            iO_05_NAME = ioNames[4];
            iO_06_NAME = ioNames[5];
            iO_07_NAME = ioNames[6];
            iO_08_NAME = ioNames[7];
            iO_09_NAME = ioNames[8];
            iO_10_NAME = ioNames[9];
            iO_11_NAME = ioNames[10];
            iO_12_NAME = ioNames[11];
            iO_13_NAME = ioNames[12];
            iO_14_NAME = ioNames[13];
            iO_15_NAME = ioNames[14];
            iO_16_NAME = ioNames[15];
        }

        public ushort DeviceInputLen = 0;
        public ushort DeviceOutputLen = 0;
        public ushort EPR = 200;//參考範例給的值

        private String iO_01_NAME;
        private String iO_02_NAME;
        private String iO_03_NAME;
        private String iO_04_NAME;
        private String iO_05_NAME;
        private String iO_06_NAME;
        private String iO_07_NAME;
        private String iO_08_NAME;
        private String iO_09_NAME;
        private String iO_10_NAME;
        private String iO_11_NAME;
        private String iO_12_NAME;
        private String iO_13_NAME;
        private String iO_14_NAME;
        private String iO_15_NAME;
        private String iO_16_NAME;

        public string IO_01_NAME { get => iO_01_NAME; set => iO_01_NAME = value; }
        public string IO_02_NAME { get => iO_02_NAME; set => iO_02_NAME = value; }
        public string IO_03_NAME { get => iO_03_NAME; set => iO_03_NAME = value; }
        public string IO_04_NAME { get => iO_04_NAME; set => iO_04_NAME = value; }
        public string IO_05_NAME { get => iO_05_NAME; set => iO_05_NAME = value; }
        public string IO_06_NAME { get => iO_06_NAME; set => iO_06_NAME = value; }
        public string IO_07_NAME { get => iO_07_NAME; set => iO_07_NAME = value; }
        public string IO_08_NAME { get => iO_08_NAME; set => iO_08_NAME = value; }
        public string IO_09_NAME { get => iO_09_NAME; set => iO_09_NAME = value; }
        public string IO_10_NAME { get => iO_10_NAME; set => iO_10_NAME = value; }
        public string IO_11_NAME { get => iO_11_NAME; set => iO_11_NAME = value; }
        public string IO_12_NAME { get => iO_12_NAME; set => iO_12_NAME = value; }
        public string IO_13_NAME { get => iO_13_NAME; set => iO_13_NAME = value; }
        public string IO_14_NAME { get => iO_14_NAME; set => iO_14_NAME = value; }
        public string IO_15_NAME { get => iO_15_NAME; set => iO_15_NAME = value; }
        public string IO_16_NAME { get => iO_16_NAME; set => iO_16_NAME = value; }
    }
}
