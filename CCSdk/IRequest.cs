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
using System.Text;
using System.Threading.Tasks;

namespace CCSdk
{
  /// <summary>
  /// 所有请求类需要实现的接口.
  /// </summary>
  public interface IRequest
  {

  }

  public interface IRequest<R> : IRequest where R : ResponseBase
  {

  }

}
