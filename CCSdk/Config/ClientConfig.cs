


using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSdk
{
  /// <summary>
  /// 客户端配置类
  /// </summary>
  public class ClientConfig
  {
    public string Url { get; set; }

    public string AppKey { get; set; }

    public string AppSecret { get; set; }

    public long ClientId { get; set; }


    public ClientConfig()
    {
      Url = Convert.ToString(((NameValueCollection)ConfigurationManager.GetSection("ClientList/Client"))["Url"]);
      AppKey = Convert.ToString(((NameValueCollection)ConfigurationManager.GetSection("ClientList/Client"))["AppKey"]);
      AppSecret =
          Convert.ToString(
              ((NameValueCollection)ConfigurationManager.GetSection("ClientList/Client"))["AppSecret"]);
      ClientId =
          long.Parse(((NameValueCollection)ConfigurationManager.GetSection("ClientList/Client2"))["ClientId"]);
    }
  }
}
