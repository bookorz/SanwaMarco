using FTChipID;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SanwaMarco
{
    public static class Util
    {
        static Dictionary<string, string> usbKeys = new Dictionary<string, string>();
        /// <summary>
        /// USB Keypro 序號與使用者清單
        /// </summary>
        public static void initKeyPro()
        {
            usbKeys.Add("KUBC8EDR","RD Software Team[1]");
            usbKeys.Add("FT0NIDRK", "品保[1]");
            usbKeys.Add("FT0NIB85", "品保[2]");
            usbKeys.Add("FT0NI6XV", "檢證[1]");
            usbKeys.Add("FT0NID8W", "檢證[2]");
            usbKeys.Add("AJ03RTD5", "檢證[3]");
            usbKeys.Add("FT0NI6YV", "檢證[4]");
            usbKeys.Add("FT0NHP7C", "檢證[5]");
            usbKeys.Add("FT0NIBA4", "客服[1]");
            usbKeys.Add("FT0NI6YX", "客服[2]");
            usbKeys.Add("AJ03SVEA", "客服[3]");
            usbKeys.Add("FT0LPGKD", "客服[4]");
            usbKeys.Add("FTWGXC1V", "客服[5]");
            usbKeys.Add("FT0LPFQ6", "客服[6]");

            usbKeys.Add("FT0LPG5H", "未分配 KEY[1]");
            usbKeys.Add("FT0LPGJ5", "未分配 KEY[2]");
            usbKeys.Add("FT0LPFU1", "未分配 KEY[3]");
            usbKeys.Add("FT0LPGKO", "未分配 KEY[4]");
            usbKeys.Add("FT0LPG4C", "未分配 KEY[5]");
            usbKeys.Add("FT0LPGLQ", "未分配 KEY[6]");
            usbKeys.Add("FT0LPGT8", "未分配 KEY[7]");
        }

        public static bool checkKeyPro(ref string msg)
        {
            initKeyPro();
            Boolean result = false;
            int numDevices = 0, LocID = 0, ChipID = 0;
            string SerialBuffer = "".PadRight(50, ' ');
            string Description = "".PadRight(50, ' ');

            try
            {
                FTChipID.ChipID.GetNumDevices(ref numDevices);
                if (numDevices > 0)
                {
                    string owner;
                    for (int i = 0; i < numDevices; i++)
                    {
                        FTChipID.ChipID.GetDeviceSerialNumber(i, ref SerialBuffer, 50);
                        FTChipID.ChipID.GetDeviceDescription(i, ref Description, 50);
                        //20190820 長官要求由加密改為寫死序號
                        //if (!checkKeyProSerialNo(SerialBuffer, Description))
                        //{
                        //    msg = "USB Serial Number : " + SerialBuffer + "認證錯誤!(" + Description + ")";
                        //    return false;
                        //}
                        if (!usbKeys.TryGetValue(SerialBuffer,out owner))
                        {
                            msg = "USB Serial Number : " + SerialBuffer + "未經過認證!! 請通知RD部門.";
                            return false;
                        }
                        else
                        {
                            msg = owner;
                            return true;
                        }
                    }
                }
                else
                {
                    msg = "找不到 USB KEY.";
                    return false;
                }
                result = true;
            }
            catch (FTChipID.ChipIDException ex)
            {
                msg = ex.ToString();

            }
            return result;
        }

        private static bool checkKeyProSerialNo(string serialBuffer, string description)
        {
            string md5 = getMD5("SANWA" + serialBuffer + "SANWA");
            if(description.Equals(md5.Substring(0, 8) + md5.Substring(12, 8)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static string getMD5(string data)
        {
            //MD5 md5 = MD5.Create();//建立一個MD5
            //byte[] source = Encoding.ASCII.GetBytes("abcdefg");//將字串轉為Byte[]
            //byte[] crypto = md5.ComputeHash(source);//進行MD5加密
            //string result = Convert.ToBase64String(crypto);//把加密後的字串從Byte[]轉為字串
            using (var md5Hash = MD5.Create())
            {
                // Byte array representation of source string
                var sourceBytes = Encoding.UTF8.GetBytes(data);

                // Generate hash value(Byte Array) for input data
                var hashBytes = md5Hash.ComputeHash(sourceBytes);

                // Convert hash byte array to string
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                // Output the MD5 hash
                //Console.WriteLine("The MD5 hash of " + source + " is: " + hash);
                return hash;//輸出結果
            }
        }
    }
}
