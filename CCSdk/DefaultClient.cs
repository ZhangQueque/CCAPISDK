
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using CCSdk;
using CCSdk.Logging;
using CCSdk.Parser;
using CCSdk.Util;

namespace CCSdk
{
    /// <summary>
    /// 基于REST的客户端,所有客户端调用基于此类.
    /// </summary>
    public abstract class DefaultClient : ClientBase
    {
  
        internal string format = Constants.FORMAT_JSON;

        internal WebUtils webUtils;
        internal ILog logger;
        internal bool disableParser = false; // 禁用响应结果解释
        internal bool disableTrace = false; // 禁用日志调试功能
        internal bool useSimplifyJson = false; // 是否采用精简化的JSON返回
        internal bool useGzipEncoding = false;  // 是否启用响应GZIP压缩
        internal IDictionary<string, string> systemParameters; // 设置所有请求共享的系统级参数

        #region DefaultTopClient Constructors

        public DefaultClient(string serverUrl, string appKey, string appSecret)
        {
            this.appKey = appKey;
            this.appSecret = appSecret;
            this.serverUrl = serverUrl;
            this.webUtils = new WebUtils();
            this.logger =new ConsoleLogFactory().GetLog("cc");
        }

        public DefaultClient(string serverUrl, string appKey, string appSecret, string format)
            : this(serverUrl, appKey, appSecret)
        {
            this.format = format;
        }

        public DefaultClient()
        {
            this.logger = new ConsoleLogFactory().GetLog("cc");
            this.webUtils = new WebUtils();
        }

        public abstract CDictionary InitParameters(CDictionary parameters);


        public void SetTimeout(int timeout)
        {
            this.webUtils.Timeout = timeout;
        }

        public void SetReadWriteTimeout(int readWriteTimeout)
        {
            this.webUtils.ReadWriteTimeout = readWriteTimeout;
        }

        public void SetDisableParser(bool disableParser)
        {
            this.disableParser = disableParser;
        }

        public void SetDisableTrace(bool disableTrace)
        {
            this.disableTrace = disableTrace;
        }

        public void SetUseSimplifyJson(bool useSimplifyJson)
        {
            this.useSimplifyJson = useSimplifyJson;
        }

        public void SetUseGzipEncoding(bool useGzipEncoding)
        {
            this.useGzipEncoding = useGzipEncoding;
        }

        public void SetIgnoreSSLCheck(bool ignore)
        {
            this.webUtils.IgnoreSSLCheck = ignore;
        }

        public void SetSystemParameters(IDictionary<string, string> systemParameters)
        {
            this.systemParameters = systemParameters;
        }

        #region IACClient Members

        public virtual T Execute<T>(IRequest<T> request, string session) where T : ResponseBase
        {
            return Execute<T>(request, session, DateTime.Now);
        }

        public virtual T Execute<T>(IRequest<T> request, string session, DateTime timestamp) where T : ResponseBase
        {
            return DoExecute<T>(request, session, timestamp);
        }

        #endregion

        private T DoExecute<T>(IRequest<T> request, string session, DateTime timestamp) where T : ResponseBase
        {
            long start = DateTime.Now.Ticks;

            // 提前检查业务参数
            try
            {
                request.Validate();
            }
            catch (CException e)
            {
                return CreateErrorResponse<T>(e.ErrorCode, e.ErrorMsg);
            }

            CDictionary txtParams = new CDictionary(request.GetParameters());
            txtParams.Add(Constants.METHOD, request.GetApiName());
            InitParameters(txtParams);


            // 添加协议级请求参数
            //ACDictionary txtParams = new ACDictionary(request.GetParameters());
            //txtParams.Add(Constants.METHOD, request.GetApiName());
            //txtParams.Add(Constants.VERSION, "1.0");
            ////txtParams.Add(Constants.SIGN_METHOD, Constants.SIGN_METHOD_MD5);
            //txtParams.Add(Constants.APP_KEY, appKey);
            //txtParams.Add(Constants.FORMAT, format);
            //txtParams.Add(Constants.TIMESTAMP, timestamp);
            //txtParams.AddAll(this.systemParameters);


            //// 添加签名参数
            //txtParams.Add(Constants.SIGN, SignHelper.SignACRequest(txtParams, appSecret, Constants.SIGN_METHOD_MD5));

            // 添加头部参数
            if (this.useGzipEncoding)
            {
                request.GetHeaderParameters()[Constants.ACCEPT_ENCODING] = Constants.CONTENT_ENCODING_GZIP;
            }

            string realServerUrl = GetServerUrl(this.serverUrl, request.GetApiName(), session);
            string reqUrl = WebUtils.BuildRequestUrl(realServerUrl, txtParams);
            try
            {
                string body;
                if (request is IUploadRequest<T>) // 是否需要上传文件
                {
                    IUploadRequest<T> uRequest = (IUploadRequest<T>)request;
                    IDictionary<string, FileItem> fileParams = SignHelper.CleanupDictionary(uRequest.GetFileParameters());
                    body = webUtils.DoPost(realServerUrl, txtParams, fileParams, request.GetHeaderParameters());
                }
                else
                {
                    body = webUtils.DoPost(realServerUrl, txtParams, request.GetHeaderParameters());
                }

                // 解释响应结果
                T rsp;
                if (disableParser)
                {
                    rsp = Activator.CreateInstance<T>();
                    rsp.ResponseBody = body;
                }
                else
                {
                    if (Constants.FORMAT_XML.Equals(format))
                    {
                        IParser<T> tp = new XmlParser<T>();
                        rsp = tp.Parse(body);
                    }
                    else
                    {
                        IParser<T> tp;
                        if (useSimplifyJson)
                        {
                            tp = new AcSimplifyJsonParser<T>();
                        }
                        else
                        {
                            tp = new JsonParser<T>();
                        }
                        rsp = tp.Parse(body);
                    }
                }

                // 追踪错误的请求
                if (rsp.HaveError)
                {
                    TimeSpan latency = new TimeSpan(DateTime.Now.Ticks - start);
                    TraceApiError(appKey, request.GetApiName(), serverUrl, txtParams, latency.TotalMilliseconds, rsp.ResponseBody);
                }
                return rsp;
            }
            catch (Exception e)
            {
                TimeSpan latency = new TimeSpan(DateTime.Now.Ticks - start);
                TraceApiError(appKey, request.GetApiName(), serverUrl, txtParams, latency.TotalMilliseconds, e.GetType() + ": " + e.Message);
                throw e;
            }
        }

        #endregion


        internal virtual string GetServerUrl(string serverUrl, string apiName, string session)
        {
            return serverUrl;
        }

        internal virtual string GetSdkVersion()
        {
            return Constants.SDK_VERSION;
        }

        internal T CreateErrorResponse<T>(string errCode, string errMsg) where T : ResponseBase
        {
            T rsp = Activator.CreateInstance<T>();
            rsp.ErrorCode = Convert.ToInt32(errCode);
            rsp.ErrorMessage = errMsg;

            if (Constants.FORMAT_XML.Equals(format))
            {
                XmlDocument root = new XmlDocument();
                XmlElement bodyE = root.CreateElement(Constants.ERROR_RESPONSE);
                XmlElement codeE = root.CreateElement(Constants.ERROR_CODE);
                codeE.InnerText = errCode;
                bodyE.AppendChild(codeE);
                XmlElement msgE = root.CreateElement(Constants.ERROR_MSG);
                msgE.InnerText = errMsg;
                bodyE.AppendChild(msgE);
                root.AppendChild(bodyE);
                rsp.ResponseBody = root.OuterXml;
            }
            else
            {
                IDictionary<string, object> errObj = new Dictionary<string, object>();
                errObj.Add(Constants.ERROR_CODE, errCode);
                errObj.Add(Constants.ERROR_MSG, errMsg);
                IDictionary<string, object> root = new Dictionary<string, object>();
                root.Add(Constants.ERROR_RESPONSE, errObj);

                string body = JsonHelper.SerializeObject(root);
                rsp.ResponseBody = body;
            }
            return rsp;
        }

        internal void TraceApiError(string appKey, string apiName, string url, CDictionary parameters, double latency, string errorMessage)
        {
            if (!disableTrace)
            {
                this.logger.ErrorFormat(appKey, apiName, url, parameters, latency, errorMessage);
            }
        }

        //public T Call<T>(IRequest<T> request) where T : ResponseBase
        //{
        //    return Execute<T>(request, null);
        //}

        //public T Call<T>(IRequest<T> request, DateTime timestamp) where T : ResponseBase
        //{
        //    return Execute<T>(request, null, timestamp);
        //}
    }
}
