/***************************************************************************************
 * Author:CC
 * 
 *客户端工厂基类.
 * 
 * DateTime:2017-06-27
 * ************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSdk
{
  /// <summary>
  /// 客户端工厂基类.
  /// </summary>
  /// <typeparam name="C">具体客户端类，比如微信、淘宝、饿了么等</typeparam>
  public class ClientFactoryBase<C> : IClientFactory where C : IClient,new()
  {
    public IClient Create()
    {
        return  new C();
    }
  }
}
