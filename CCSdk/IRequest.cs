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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CCSdk.Util;

namespace CCSdk
{
  /// <summary>
  /// 所有请求类需要实现的接口.
  /// </summary>
  public interface IRequest
  {
    /// <summary>
    /// 获取API名称。
    /// </summary>
    string GetApiName();
    /// <summary>
    /// 获取所有的Key-Value形式的文本请求参数字典。
    /// </summary>
    CDictionary GetParameters();
    /// <summary>
    /// 客户端参数检查，减少服务端无效调用。
    /// </summary>
    void Validate();
    /// <summary>
    /// 获取请求提交的方法
    /// </summary>
    /// <returns></returns>
    HttpMethod GetMethod();

    /// <summary>
    /// 获取自定义HTTP请求头参数.
    /// </summary>
    IDictionary<string, string> GetHeaderParameters();
  }

  public interface IRequest<out Response> : IRequest where Response : ResponseBase
  {
  }
}
