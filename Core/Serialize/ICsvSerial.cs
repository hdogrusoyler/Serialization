using Core.Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Serialize
{
    public interface ICsvSerial<T>
        where T : class, IObject, new()
    {
        void ObjToCsvSerialize(string filename, List<T> aic);
        List<T> CsvToObjDeSerialize(string filename);
    }
}
