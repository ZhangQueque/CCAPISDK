using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace CCSdk.Config
{
  /// <summary>
  /// 对应配置文件配置组
  /// </summary>
  public class ClientSdkSectionGroup : ConfigurationSectionGroup
  {
    public ClientsSection SdkClients
    {
      get
      {
        return (ClientsSection)base.Sections["sdkClients"];
      }
    }
  }
}
