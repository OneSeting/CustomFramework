using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace LittleHourglass.Commonality.Base
{
    public static class EncryptionHelper
    {
        public static string CreatePasswordSHA256(string password, string saltkey, string passwordFormat = "SHA256")
        {
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA256";
            string saltAndPassword = String.Concat(password, saltkey);

            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(saltAndPassword);
            try
            {
                using (SHA256 sha256 = new SHA256CryptoServiceProvider())
                {
                    byte[] retVal = sha256.ComputeHash(bytValue);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < retVal.Length; i++)
                    {
                        sb.Append(retVal[i].ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetSHA256HashFromString() fail,error:" + ex.Message);
            }
        }
        public static string CreatePasswordMD5(string password, string saltkey, string passwordFormat = "MD5")
        {
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "MD5";
            string saltAndPassword = String.Concat(password, saltkey);

            using (MD5 md5 = MD5.Create())
            {
                // 将字符串转换成字节数组
                byte[] byteOld = Encoding.UTF8.GetBytes(saltAndPassword);
                // 调用加密方法
                byte[] byteNew = md5.ComputeHash(byteOld);
                // 将加密结果转换为字符串
                StringBuilder sb = new StringBuilder();
                foreach (byte b in byteNew)
                {
                    // 将字节转换成16进制表示的字符串，
                    sb.Append(b.ToString("x2"));
                }
                // 返回加密的字符串
                return sb.ToString();
            }
        }

    }
}
