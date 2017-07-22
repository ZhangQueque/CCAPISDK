using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace CCSdk.Util
{
  public class XmlHelper
  {

    public static void SaveToXml(string filePath, object sourceObj, Type type, string xmlRootName)
    {
      if (!string.IsNullOrWhiteSpace(filePath) && sourceObj != null)
      {
        type = type != null ? type : sourceObj.GetType();

        using (StreamWriter writer = new StreamWriter(filePath))
        {
          System.Xml.Serialization.XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?
              new System.Xml.Serialization.XmlSerializer(type) :
              new System.Xml.Serialization.XmlSerializer(type, new XmlRootAttribute(xmlRootName));
          xmlSerializer.Serialize(writer, sourceObj);
        }
      }
    }

    public static object LoadFromXml(string filePath, Type type)
    {
      object result = null;

      if (File.Exists(filePath))
      {
        using (StreamReader reader = new StreamReader(filePath))
        {
          System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
          result = xmlSerializer.Deserialize(reader);
        }
      }

      return result;
    }


    /// <summary>
    ///序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="serialObject"></param>
    /// <returns></returns>
    public static string XmlSerializer<T>(T serialObject) where T : class
    {
      //var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
      var emptyNamepsaces = new XmlSerializerNamespaces();
      var serializer = new System.Xml.Serialization.XmlSerializer(serialObject.GetType());
      var settings = new XmlWriterSettings();
      settings.OmitXmlDeclaration = true;
      settings.Encoding = Encoding.UTF8;
      using (var stream = new StringWriter())
      using (var writer = XmlWriter.Create(stream, settings))
      {
        serializer.Serialize(writer, serialObject, emptyNamepsaces);
        return stream.ToString();
      }
    }

    /// <summary>
    ///反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static T DeserializeObject<T>(string xml) where T : class
    {
      using (var str = new StringReader(xml))
      {
        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        var result = (T)xmlSerializer.Deserialize(str);
        return result;
      }
    }
  }
}

