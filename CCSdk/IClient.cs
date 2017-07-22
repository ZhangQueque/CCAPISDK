/***************************************************************************************
 * Author:CC
 * 
 *客户端基类.
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
  /// 所有客户端需要实现的接口.
  /// </summary>
  public interface IClient
  {
    /// <summary>
    /// 执行API请求。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <param name="request"></param>
    /// <returns>领域对象</returns>
    TResponse Execute<TResponse>(IRequest<TResponse> request) where TResponse : ResponseBase, new();
    /// <summary>
    /// 获取第三方平台名称
    /// </summary>
    string ThirdPartyName { get; }
    
  }
}
