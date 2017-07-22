
/*----------------------------------------------------------------
   Copyright (C) 2017 - CXC

   文件名：MyConfigHandler.cs
   文件功能描述：创建新webconfig,appconfig节点帮助类.


   创建标识：CXC @ 2017-06-13

----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CCSdk
{
    /// <summary>
    /// 创建新webconfig,appconfig节点帮助类.
    /// </summary>
    public class MyConfigHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            NameValueCollection configs;
            NameValueSectionHandler baseHandler = new NameValueSectionHandler();
            configs = (NameValueCollection)baseHandler.Create(parent, configContext, section);
          
            return configs;
        }
    }
}
