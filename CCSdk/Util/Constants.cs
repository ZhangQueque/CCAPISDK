using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSdk.Util
{
    /// <summary>
    /// 公共常量类.
    /// </summary>
    public sealed class Constants
    {
        public const string CHARSET_UTF8 = "utf-8";

        public const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public const string DATE_TIME_MS_FORMAT = "yyyy-MM-dd HH:mm:ss.fff";

        public const string SIGN_METHOD_MD5 = "md5";

        public const string LOG_SPLIT = "^_^";
        public const string LOG_FILE_NAME = "acsdk.log";

        public const string ACCEPT_ENCODING = "Accept-Encoding";
        public const string CONTENT_ENCODING = "Content-Encoding";
        public const string CONTENT_ENCODING_GZIP = "gzip";

        public const string ERROR_RESPONSE = "error_response";
        public const string ERROR_CODE = "code";
        public const string ERROR_MSG = "msg";

        public const string SDK_VERSION = "ac-sdk-net-20170612";
        public const string SDK_VERSION_CLUSTER = "ac-sdk-net-cluster-20170612";

        public const string APP_KEY = "app_key";
        public const string FORMAT = "format";
        public const string METHOD = "method";
        public const string TIMESTAMP = "timestamp";
        public const string VERSION = "v";
        public const string SIGN = "sign";
        public const string SIGN_METHOD = "sign_method";
        public const string FORMAT_XML = "xml";
        public const string FORMAT_JSON = "json";

        public const string QM_ROOT_TAG_REQ = "request";
        public const string QM_ROOT_TAG_RSP = "response";
        public const string QM_CONTENT_TYPE = "text/xml;charset=utf-8";
    }
}
