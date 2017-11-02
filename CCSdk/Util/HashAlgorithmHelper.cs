using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Web.WebSockets;

namespace CCSdk.Util
{
  public class HashAlgorithmHelper
  {
   
    public static string Md5(string text)
    {
      var algorithm = HashAlgorithm.Create("MD5");
      
      if (algorithm == null)
        throw new ArgumentException("无法识别的加密方式", nameof(text));

      var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
      var password = BitConverter.ToString(hashByteArray).Replace("-", "");

      return password;
    }

    public static string Md52(string text)
    {
      var algorithm = MD5.Create();

      if (algorithm == null)
        throw new ArgumentException("无法识别的加密方式", nameof(text));

      var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
      var password = BitConverter.ToString(hashByteArray).Replace("-", "");

      return password;
    }

    public static string Md53(string text)
    {
      var algorithm =  new MD5CryptoServiceProvider();

      if (algorithm == null)
        throw new ArgumentException("无法识别的加密方式", nameof(text));

      var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
      var password = BitConverter.ToString(hashByteArray).Replace("-", "");

      return password;
    }

    public static string Md54(string text)
    {
      //var password = FormsAuthentication.HashPasswordForStoringInConfigFile(text, "MD5");

      //if (algorithm == null)
      //  throw new ArgumentException("无法识别的加密方式", nameof(text));

      //var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
      //var password = BitConverter.ToString(hashByteArray).Replace("-", "");

      //return password;

      return string.Empty;
    }


    public static string Sha1(string text)
    {
      byte[] bytes = Encoding.UTF8.GetBytes(text);

      var sha1 = SHA1.Create();
      byte[] hashBytes = sha1.ComputeHash(bytes);

      return Sha1(hashBytes);
    }

    public static string Sha1(byte[] bytes)
    {
      var sb = new StringBuilder();
      foreach (byte b in bytes)
      {
        var hex = b.ToString("x2");
        sb.Append(hex);
      }

      return sb.ToString();
    }

  }
}
