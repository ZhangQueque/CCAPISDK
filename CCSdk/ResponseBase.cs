using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CCSdk
{
  /// <summary>
  /// 消息响应类基类
  /// </summary>
  public abstract class ResponseBase
  {
    /// <summary>
    /// 请求的Url
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 设置或获取HttpStatusCode
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public HttpStatusCode StatusCode { set; get; }
    /// <summary>
    /// 设置或获取请求信息
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public IRequest Request { set; get; }
    /// <summary>
    /// 设置或获取请求内容
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    public string RequestContent { set; get; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// 错误码
    /// </summary>
    public int ErrorCode { get; set; }

    /// <summary>
    /// 异常消息
    /// </summary>
    public  Exception ExceptionMessage { get; set; }

    /// <summary>
    /// 响应结果是否错误
    /// </summary>
    public bool HaveError
    {
      get { return Convert.ToInt32(ErrorCode) != 0; }
    }
    /// <summary>
    /// 设置或获取响应头
    /// </summary>
    public HttpResponseHeaders Headers { set; get; }

    /// <summary>
    /// 响应消息体
    /// </summary>
    public string ResponseBody { get; internal set; }

    public override string ToString()
    {
      var headers = Headers.ToString();
      return $"{this.GetType().Name}" +
             $"{System.Environment.NewLine}" +
             $"{headers}" +
             $"{System.Environment.NewLine}" +
             $"{Request}:{StatusCode}" +
             $"{System.Environment.NewLine}" +
             $"Request:{RequestContent}" +
             $"{System.Environment.NewLine}" +
             $"Response:{ResponseBody}";
    }

  }
}
