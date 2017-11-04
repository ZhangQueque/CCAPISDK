using System;
using System.Runtime.Serialization;

namespace CCSdk
{
  /// <summary>
  /// 客户端异常。
  /// </summary>
  public class CException : Exception
  {
    private readonly string errorCode;
    private readonly string errorMsg;

    public CException()
        : base()
    {
    }

    public CException(string message)
        : base(message)
    {
    }

    protected CException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public CException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public CException(string errorCode, string errorMsg)
        : base(errorCode + ":" + errorMsg)
    {
      this.errorCode = errorCode;
      this.errorMsg = errorMsg;
    }

    public string ErrorCode
    {
      get
      {
        return errorCode;
      }
    }

    public string ErrorMsg
    {
      get
      {
        return errorMsg;
      }
    }
  }
}
