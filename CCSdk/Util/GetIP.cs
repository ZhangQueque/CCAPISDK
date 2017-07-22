using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CCSdk.Util
{
  public class GetIP
  {
    private static string intranetIp;
    /// <summary>
    /// 获取本机的局域网IP。
    /// </summary>
    public static string GetIntranetIp()
    {
      if (intranetIp == null)
      {
        IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ipAddress in ipHostEntry.AddressList)
        {
          if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
          {
            intranetIp = ipAddress.ToString();
            break;
          }
        }
      }
      if (intranetIp == null)
      {
        intranetIp = "127.0.0.1";
      }

      return intranetIp;
    }
  }
}
