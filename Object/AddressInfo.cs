using Core.Object;
using System;
using System.Xml.Serialization;

namespace Object
{
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class AddressInfo : IObject
    {
        [XmlElement("City")]
        public AddressInfoCity[] City { get; set; }
    }

    public class AddressInfoCity
    {
        [XmlElement("District")]
        public AddressInfoCityDistrict[] District { get; set; }

        [XmlAttribute()]
        public string name { get; set; }

        [XmlAttribute()]
        public string code { get; set; }
    }

    public class AddressInfoCityDistrict
    {
        [XmlElement("Zip")]
        public AddressInfoCityDistrictZip[] Zip { get; set; }

        [XmlAttribute()]
        public string name { get; set; }
    }

    public class AddressInfoCityDistrictZip
    {
        [XmlAttribute()]
        public string code { get; set; }
    }
}