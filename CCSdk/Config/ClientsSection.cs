using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace CCSdk.Config
{
  /// <summary>
  /// 配置文件中的配置节
  /// </summary>
  public class ClientsSection : ConfigurationSection
  {
    [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = true)]
    public bool Enable
    {
      get
      {
        return (bool)base["enabled"];
      }
      set
      {
        base["enabled"] = value;
      }
    }

    [ConfigurationProperty("default", IsDefaultCollection = false)]
    public ClientElement Default
    {
      get
      {
        return (ClientElement)base["default"];
      }
      set
      {
        base["default"] = value;
      }
    }

    [ConfigurationProperty("client", IsDefaultCollection = false)]
    [ConfigurationCollection(typeof(ClientElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap, RemoveItemName = "remove")]
    public Clients Client
    {
      get
      {
        return (Clients)base["client"];
      }
      set
      {
        base["client"] = value;
      }
    }

    [ConfigurationProperty("ConfigValues", IsDefaultCollection = false)]
    public NameValueConfigurationCollection ConfigValues
    {
      get
      {
        return (NameValueConfigurationCollection)base["ConfigValues"];
      }
      set
      {
        base["ConfigValues"] = value;
      }
    }
  }

  public class Clients : ConfigurationElementCollection
  {
    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((ClientElement)element).Code;
    }

    protected override ConfigurationElement CreateNewElement()
    {
      return new ClientElement();
    }

    public ClientElement this[int i]
    {
      get
      {
        return (ClientElement)base.BaseGet(i);
      }
    }

    public new ClientElement this[string key]
    {
      get
      {
        return (ClientElement)base.BaseGet(key);
      }
    }
  }

  public class ClientElement : ConfigurationElement
  {
    [ConfigurationProperty("code", IsRequired = true, IsKey = true)]
    public string Code
    {
      get
      {
        return (string)base["code"];
      }
      set
      {
        base["code"] = value;
      }
    }

    [ConfigurationProperty("name", IsRequired = true)]
    public string Name
    {
      get
      {
        return (string)base["name"];
      }
      set
      {
        base["name"] = value;
      }
    }

    [ConfigurationProperty("url", IsRequired = true)]
    public string Url
    {
      get
      {
        return (string)base["url"];
      }
      set
      {
        base["url"] = value;
      }
    }

    [ConfigurationProperty("appKey", IsRequired = false)]
    public string AppKey
    {
      get
      {
        return (string)base["appKey"];
      }
      set
      {
        base["appKey"] = value;
      }
    }

    [ConfigurationProperty("version", IsRequired = false)]
    public string Version
    {
      get
      {
        return (string)base["version"];
      }
      set
      {
        base["version"] = value;
      }
    }

    [ConfigurationProperty("format", IsRequired = false)]
    public string Format
    {
      get
      {
        return (string)base["format"];
      }
      set
      {
        base["format"] = value;
      }
    }

    [ConfigurationProperty("signMethod", IsRequired = false)]
    public string SignMethod
    {
      get
      {
        return (string)base["signMethod"];
      }
      set
      {
        base["signMethod"] = value;
      }
    }

    [ConfigurationProperty("closed", IsRequired = false)]
    public bool Closed
    {
      get
      {
        return (bool)base["closed"];
      }
      set
      {
        base["closed"] = value;
      }
    }

    [ConfigurationProperty("desc", IsRequired = false)]
    public string Desc
    {
      get
      {
        return (string)base["desc"];
      }
      set
      {
        base["desc"] = value;
      }
    }
  }
}
