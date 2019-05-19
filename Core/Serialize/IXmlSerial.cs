using Core.Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Serialize
{
    public interface IXmlSerial<T>
        where T : class, IObject, new()
    {
        void XmlSerialize(string filename, T ai);
        T XmlDeSerialize(string filename);
    }
}
