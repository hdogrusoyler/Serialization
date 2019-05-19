using Core.Object;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Core.Serialize.Xml
{
    public class XmlSeri<T> : IXmlSerial<T>
        where T : class, IObject, new()
    {
        public void XmlSerialize(string filename, T ai)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, ai);
            writer.Close();
        }

        public T XmlDeSerialize(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            FileStream fs = new FileStream(filename, FileMode.Open);
            //StreamReader reader = new StreamReader(filename);
            T ai;
            ai = (T)serializer.Deserialize(fs);
            //fs.Close();
            return ai;
        }
    }
}
