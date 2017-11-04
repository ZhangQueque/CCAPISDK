
using System;
using System.Collections;
using CCSdk;
using CCSdk.Util;
using CCSdk.Parser;
using FastJSON;

namespace CCSdk.Parser
{
    public class AcSimplifyJsonParser<T> : JsonParser<T> where T :ResponseBase
    {
        public override T Parse(string body)
        {
            T rsp = null;

            IDictionary rootJson = JSON.Parse(body) as IDictionary;
            if (rootJson != null)
            {
                IDictionary data = rootJson;
                if (rootJson.Contains(Constants.ERROR_RESPONSE))
                {
                    data = rootJson[Constants.ERROR_RESPONSE] as IDictionary;
                }

                if (data != null)
                {
                    IReader reader = new SimplifyJsonReader(data);
                    rsp = (T)FromJson(reader, typeof(T));
                }
            }

            if (rsp == null)
            {
                rsp = Activator.CreateInstance<T>();
            }

            if (rsp != null)
            {
                rsp.ResponseBody = body;
            }

            return rsp;
        }
    }
}
