using Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace Operation.Abstract
{
    public interface IObjectConverter
    {
        List<AddressInfoCsv> ObjConvertXmlToCsv(AddressInfo aicXml);
        AddressInfo ObjConvertCsvToXml(List<AddressInfoCsv> aiCsvList);
    }
}
