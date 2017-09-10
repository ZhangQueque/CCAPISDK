using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AcSDK;
using Newtonsoft.Json;
using CCSdk;

namespace CCSdk
{
  public abstract class ClientBase : IClient
  {
    protected string AppKey = string.Empty;
    protected string Secret = string.Empty;
    protected string Url = string.Empty;

    public static HttpClient _httpClient;

    public ClientBase()
    {
      _httpClient = _httpClient ?? new HttpClient();
    }

    public TResponse Execute<TResponse>(IRequest<TResponse> request) where TResponse : ResponseBase, new()
    {
      var responseContent = "";
      try
      {

        var requestUri = GetRequestUri(request);
        var requestMessage = new HttpRequestMessage(request.GetMethod(), requestUri)
        {
          Content = GetRequestContent(request)
        };
        var responseMessage = _httpClient.SendAsync(requestMessage).Result;
        responseContent = responseMessage.Content.ReadAsStringAsync().Result;
        var result = JsonConvert.DeserializeObject<TResponse>(responseContent);

        DownloadFile(result as IFileDownloadResponse);

        result.Url = requestUri;
        result.ResponseBody = GetRequestBody(request);
        result.StatusCode = responseMessage.StatusCode;
        result.Headers = responseMessage.Headers;
        result.ResponseContent = responseContent;

        return result;
      }
      catch (Exception ex)
      {
        throw new CException($"操作失败,{ex.Message},{responseContent}");
      }
    }


    protected virtual void DownloadFile<TResponse>(TResponse response) where TResponse : IFileDownloadResponse
    {
      if (response == null)
        return;

      response.DownloadFile();
    }

    /// <summary>
    /// 获取请求地址
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public abstract string GetRequestUri(IRequest request);
    /// <summary>
    /// 获取请求发送数据
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public abstract string GetRequestBody(IRequest request);
    /// <summary>
    /// 获取请求发送的HttpContent
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public virtual HttpContent GetRequestContent(IRequest request)
    {
      HttpContent result;
      if (request is IUploadRequest)
      {
        var mfdContent = new MultipartFormDataContent();
        var uploadRequest = request as IUploadRequest;
        if (uploadRequest.File != null && uploadRequest.File.Any())
        {
          foreach (var f in uploadRequest.File)
          {
            if (!File.Exists(f))
              throw new FileNotFoundException($"上传文件未找到,{f}");

            var streamContent = new StreamContent(new MemoryStream(File.ReadAllBytes(f)));
            var mimeType = MimeMapping.GetMimeMapping(f);
            streamContent.Headers.Add("Content-Type", mimeType);
            streamContent.Headers.Add("Content-Disposition", "form-data; name=\"file\"; filename=\"" + Path.GetFileName(f) + "\"");
            mfdContent.Add(streamContent, "file", Path.GetFileName(f));
          }
        }
        var body = GetRequestBody(request);
        if (!string.IsNullOrWhiteSpace(body))
        {
          foreach (var p in body.Split('&'))
          {
            var arr = p.Split('=');
            mfdContent.Add(new StringContent(arr[1]), arr[0]);
          }
        }
        result = mfdContent;
      }
      else
      {
        var body = GetRequestBody(request);
        if (string.IsNullOrWhiteSpace(body))
          return null;
        result = (string.IsNullOrWhiteSpace(MediaType) || request.GetMethod() == HttpMethod.Get)
            ? new StringContent(body)
            : new StringContent(body, Encoding.UTF8, MediaType);
      }

      return result;
    }

    /// <summary>
    /// 创建一个Response
    /// </summary>
    /// <param name="bodyData"></param>
    /// <returns></returns>
    public abstract ResponseBase CreateResponse(IResponse bodyData);
    /// <summary>
    /// 获取签名
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public abstract string GetSignature(IRequest request);


    public abstract string ThirdPartyPlatformName { get; }
    /// <summary>
    /// 获取数据提交的MediaType
    /// </summary>
    public virtual string MediaType => "";

    public abstract string ThirdPartyName { get; }
  }
}
