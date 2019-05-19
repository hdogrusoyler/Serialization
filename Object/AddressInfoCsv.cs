using Core.Object;
using System;
using System.Collections.Generic;
using System.Text;

namespace Object
{
    public class AddressInfoCsv : IObject
    {
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public string DistrictName { get; set; }
        public string ZipCode { get; set; }
    }
}
