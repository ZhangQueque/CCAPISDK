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

namespace CCSdk
{
  /// <summary>
  /// 所有请求类需要实现的接口.
  /// </summary>
  public interface IRequest
  {
    /// <summary>
    /// 获取请求提交的方法
    /// </summary>
    /// <returns></returns>
    HttpMethod GetMethod();
  }

  public interface IRequest<R> : IRequest where R : ResponseBase
  {
   
  }
}
