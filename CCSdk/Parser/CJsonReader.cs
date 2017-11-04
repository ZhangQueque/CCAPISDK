/*----------------------------------------------------------------
  Copyright (C) 2017 - CXC

  文件名：AcJsonReader.cs
  文件功能描述： JSON响应通用读取器


  创建标识：CXC @ 2017-06-12

----------------------------------------------------------------*/



using System;
using System.Collections;
using System.Collections.Generic;

namespace CCSdk.Parser
{
    /// <summary>
    /// JSON响应通用读取器.
    /// </summary>
    public class CJsonReader : IReader
    {
        private IDictionary json;

        public CJsonReader(IDictionary json)
        {
            this.json = json;
        }

        public bool HasReturnField(object name)
        {
            return json.Contains(name);
        }

        public object GetPrimitiveObject(object name)
        {
            return json[name];
        }

        public object GetReferenceObject(object name, Type type, ConvertDelegate convert)
        {
            IDictionary dict = json[name] as IDictionary;
            if (dict != null && dict.Count > 0)
            {
                return convert(new CJsonReader(dict), type);
            }
            else
            {
                return null;
            }
        }

        public IList GetListObjects(string listName, string itemName, Type type, ConvertDelegate convert)
        {
            IList listObjs = null;

            IDictionary jsonMap = json[listName] as IDictionary;
            if (jsonMap != null && jsonMap.Count > 0)
            {
                IList jsonList = jsonMap[itemName] as IList;
                if (jsonList != null && jsonList.Count > 0)
                {
                    Type listType = typeof(List<>).MakeGenericType(new Type[] { type });
                    listObjs = Activator.CreateInstance(listType) as IList;
                    foreach (object item in jsonList)
                    {
                        if (typeof(IDictionary).IsAssignableFrom(item.GetType())) // object
                        {
                            IDictionary subMap = item as IDictionary;
                            object subObj = convert(new CJsonReader(subMap), type);
                            if (subObj != null)
                            {
                                listObjs.Add(subObj);
                            }
                        }
                        else if (typeof(IList).IsAssignableFrom(item.GetType())) // list or array
                        {
                            // TODO not support yet
                        }
                        else // string, bool, long, double, null, other
                        {
                            listObjs.Add(item);
                        }
                    }
                }
            }

            return listObjs;
        }
    }
}
