using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using CCSdk.Logging;

namespace CCSdk.Config
{
    public class ConfigHelper
    {
    //private static readonly ILog log = LogManager.GetLogger(typeof(ConfigHelper));

    // Hack--Added by xucj @2016-10-26 [转换RateCode]
    public static object GetClientConfig(string code, string name = "top")
    {
      return ClientConfig(code, name);
    }

      private static object ClientConfig(string code, string name)
      {
        if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(name))
        {
          return string.Empty;
        }

        var client = GetClientsSection();
        if (client == null) return null;
        if (!client.Enable)
        {
          return null;
        }
        else
        {
          return client.Client[code];
        }
      }

    public static object ClientConfigValue(string code, string name)
    {
      if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(name))
      {
        return string.Empty;
      }

      var client = GetClientsSection();
      if (client == null) return null;
      if (!client.Enable)
      {
        return null;
      }
      else
      {
        return client.ConfigValues[code];
      }
    }

    private static ClientsSection GetClientsSection()
      {
        ClientSdkSectionGroup clientSdkSectionGroup =
          (ClientSdkSectionGroup)
            (ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).SectionGroups["clientSdk"]);
        //MappingRateSectionGroup mappingRate = (MappingRateSectionGroup)System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~").SectionGroups["mappingRate"];
      ClientsSection client = (ClientsSection) clientSdkSectionGroup?.Sections["clients"];

        return client;
      }
    }
}
