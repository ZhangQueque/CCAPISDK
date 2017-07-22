/***************************************************************************************
 * Author:CC
 * 
 *客户端工厂接口.
 * 
 * DateTime:2017-06-27
 * ************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CCSdk
{
  /// <summary>
  /// 客户端工厂接口.
  /// </summary>
  public interface IClientFactory
  {
    IClient Create();
  }
}
