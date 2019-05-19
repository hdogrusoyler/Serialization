using Core.Serialize;
using Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serialize.Abstract
{
    public interface ICsvSerialize : ICsvSerial<AddressInfoCsv>
    {
        void CsvSerialize(string filename, List<String[]> aic);
        List<String[]> CsvDeSerialize(string filename);
    }
}
