using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
//using System.Text.Json;

namespace SmartMES
{
    public class Formatter
    {
        public static string XmlSerialize(object obj)
        {
            if (obj == null) return null;

            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, obj);
                return Encoding.UTF8.GetString(memoryStream.GetBuffer()).ToString();
            }
        }

        public static T XmlDeserialize<T>(string xmlData)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlData)))
            {
                reader.MoveToContent();

                XmlSerializer xmlSerializer = new XmlSerializer(temp);

                obj = (T)xmlSerializer.Deserialize(reader);
                return obj;
            }
        }

        /*
        public static string JsonSerialize(object obj)
        {
            if (obj == null) return null;

            String stringBuilder = JsonSerializer.Serialize(obj);

            return stringBuilder;
        }

        public static T JsonDeserialize<T>(string jsonData)
        {
            T obj = Activator.CreateInstance<T>();

            obj = JsonSerializer.Deserialize<T>(jsonData);
            return obj;
        }
        */
    }
}
