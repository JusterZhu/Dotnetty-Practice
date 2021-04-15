using System;
using System.Collections.Generic;
using System.Text;

namespace TestNetty.Infrastructure.Common.Utils
{
    public class EncryptionUtil
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="hashValue">密文前10位</param>
        /// <param name="nonce">随机字符串</param>
        /// <param name="sign">约定字符</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public static bool Verification(string hashValue,string nonce,string sign,long time) 
        {
            if (string.IsNullOrEmpty(hashValue) || string.IsNullOrEmpty(nonce) || string.IsNullOrEmpty(sign) || time <= 0) return false;

            string resultStr = string.Empty;

            //1 将10位哈希值解码
            var result_hashVal = UnBcrypt(hashValue);

            //2 
            var _nonce = SHA2(nonce);

            //3
            var _sign = Bcrypt(sign);

            //可用于判断该消息是否过期  11位
            var _time = Time(time);

            //4
            resultStr = MD5($"{_nonce}{_sign}");//最终结果的字符串

            resultStr = resultStr.Substring(0, 10);
            if (resultStr.Equals(result_hashVal))
            {
                return true;
            }

            return false;
        }

        private static string[] strs = new string[]
                        {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                          "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
        /// <summary>
        /// 创建随机字符串
        /// 本代码来自开源微信SDK项目：https://github.com/night-king/weixinSDK
        /// </summary>
        /// <returns></returns>
        public static string Nonce()
        {
            Random r = new Random();
            var sb = new StringBuilder();
            var length = strs.Length;
            for (int i = 0; i < 6; i++)
            {
                sb.Append(strs[r.Next(length - 1)]);
            }
            return sb.ToString();
        }

        public static string MD5(string content) { return string.Empty; }

        public static string UnMD5(string content) { return string.Empty; }

        public static string SHA2(string content) { return string.Empty; }

        public static string Bcrypt(string content) { return string.Empty; }

        public static string UnBcrypt(string content) { return string.Empty; }

        public static DateTime Time(long time) { return DateTime.Now; }
    }
}
