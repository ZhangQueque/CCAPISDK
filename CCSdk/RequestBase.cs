using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using CCSdk;
using CCSdk.Util;
using Top.Api.Request;

namespace CCSdk
{
  public abstract class RequestBase<TResponse> : IRequest<TResponse> where TResponse : ResponseBase, new()
  {
    public CDictionary _parameters = null;
    /// <summary>
    /// 获取API名称 
    /// </summary>
    /// <returns></returns>
    public abstract string GetApiName();
    /// <summary>
    /// 获取所有的Key-Value形式的文本请求参数字典。
    /// </summary>
    /// <returns></returns>
    public virtual CDictionary GetParameters()
    {
      if (_parameters != null)
        return _parameters;
      var properties = this.GetType().GetProperties().Where(m => m != null);
      //_parameters = properties.ToDictionary(m => m.Name, n => (object)HttpUtility.UrlEncode(n.GetValue(this, null).ToString()));
      IDictionary<string, string> paras = properties.ToDictionary(m => m.Name, n => Convert.ToString(n.GetValue(this, null)));
      _parameters = _parameters ?? new CDictionary();
      _parameters.AddAll(paras);
      return _parameters;
    }
    /// <summary>
    /// 客户端参数检查，减少服务端无效调用
    /// </summary>
    public abstract void Validate();
    /// <summary>
    /// 获取请求提交的方法
    /// </summary>
    /// <returns></returns>
    public abstract System.Net.Http.HttpMethod GetMethod();

    public IDictionary<string, string> GetHeaderParameters()
    {
      throw new NotImplementedException();
    }
  }
}
