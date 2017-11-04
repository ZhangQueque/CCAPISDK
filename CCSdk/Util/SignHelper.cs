using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CCSdk.Util
{
    public class SignHelper
    {
        /// <summary>
        /// 给AC请求签名。
        /// </summary>
        /// <param name="parameters">所有字符型的TOP请求参数</param>
        /// <param name="secret">签名密钥</param>
        /// <param name="signMethod">签名方法，可选值：md5, hmac</param>
        /// <returns>签名</returns>
        public static string SignACRequest(IDictionary<string, string> parameters, string secret, string signMethod)
        {
            return SignACRequest(parameters, null, secret, signMethod);
        }

        /// <summary>
        /// 给TOP请求签名。
        /// </summary>
        /// <param name="parameters">所有字符型的TOP请求参数</param>
        /// <param name="body">请求主体内容</param>
        /// <param name="secret">签名密钥</param>
        /// <param name="signMethod">签名方法，可选值：md5, hmac</param>
        /// <returns>签名</returns>
        public static string SignACRequest(IDictionary<string, string> parameters, string body, string secret, string signMethod)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder();
            if (Constants.SIGN_METHOD_MD5.Equals(signMethod))
            {
                query.Append(secret);
            }
            foreach (KeyValuePair<string, string> kv in sortedParams)
            {
                if (!string.IsNullOrEmpty(kv.Key) && !string.IsNullOrEmpty(kv.Value))
                {
                    query.Append(kv.Key).Append(kv.Value);
                }
            }

            // 第三步：把请求主体拼接在参数后面
            if (!string.IsNullOrEmpty(body))
            {
                query.Append(body);
            }

            // 第四步：使用MD5/HMAC加密
            byte[] bytes;
            //if (Constants.SIGN_METHOD_HMAC.Equals(signMethod))
            //{
            //    HMACMD5 hmac = new HMACMD5(Encoding.UTF8.GetBytes(secret));
            //    bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));
            //}
            //else
            //{
            query.Append(secret);
            MD5 md5 = MD5.Create();
            bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));
            //}

            // 第五步：把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("X2"));
            }

            return result.ToString();
        }

        /// <summary>
        /// 清除字典中值为空的项。
        /// </summary>
        /// <param name="dict">待清除的字典</param>
        /// <returns>清除后的字典</returns>
        public static IDictionary<string, T> CleanupDictionary<T>(IDictionary<string, T> dict)
        {
            IDictionary<string, T> newDict = new Dictionary<string, T>(dict.Count);

            foreach (KeyValuePair<string, T> kv in dict)
            {
                if (kv.Value != null)
                {
                    newDict.Add(kv.Key, kv.Value);
                }
            }

            return newDict;
        }

    }
}
