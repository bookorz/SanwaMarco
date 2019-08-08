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
        public static bool checkKeyPro(ref string msg)
        {
            Boolean result = false;
            int numDevices = 0, LocID = 0, ChipID = 0;
            string SerialBuffer = "".PadRight(50, ' ');
            string Description = "".PadRight(50, ' ');

            try
            {
                FTChipID.ChipID.GetNumDevices(ref numDevices);
                if (numDevices > 0)
                {
                    for (int i = 0; i < numDevices; i++)
                    {
                        FTChipID.ChipID.GetDeviceSerialNumber(i, ref SerialBuffer, 50);
                        FTChipID.ChipID.GetDeviceDescription(i, ref Description, 50);
                        if (!checkKeyProSerialNo(SerialBuffer, Description))
                        {
                            msg = "USB Serial Number : " + SerialBuffer + "認證錯誤!(" + Description + ")";
                            return false;
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
