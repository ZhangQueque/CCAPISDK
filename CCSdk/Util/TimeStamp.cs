using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSdk.Util
{
  public class TimeStamp
  {
    /// <summary>
    /// 获取从1970年1月1日到现在的毫秒总数。
    /// </summary>
    /// <returns>毫秒数</returns>
    public static long GetCurrentTimeMillis()
    {
      return (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    }

    /// <summary>
    /// 获取从1970年1月1日到现在的秒总数。
    /// </summary>
    /// <returns>毫秒数</returns>
    public static long GetCurrentTimeSeconds()
    {
      return (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
    }
  }
}
