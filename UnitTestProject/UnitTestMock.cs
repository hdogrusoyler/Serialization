using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Object;
using Operation.Abstract;
using Serialize.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestMock
    {
        [TestMethod]
        public void TestMethod1()
        {
            var _xmlSerialize = new Mock<IXmlSerialize>();
            _xmlSerialize.Setup(foo => foo.XmlSerialize(It.IsAny<string>(), It.Ref<AddressInfo>.IsAny));
            var _csvSerialize = new Mock<ICsvSerialize>();
            _csvSerialize.Setup(foo => foo.CsvToObjDeSerialize(It.IsAny<string>())).Returns(It.Ref<List<AddressInfoCsv>>.IsAny);
            var _objConvertor = new Mock<IObjectConverter>();
            _objConvertor.Setup(foo => foo.ObjConvertCsvToXml(It.Ref<List<AddressInfoCsv>>.IsAny)).Returns(It.Ref<AddressInfo>.IsAny);

            try
            {
                var a = _csvSerialize.Object.CsvToObjDeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.csv");

                a = a.Where(c => c.CityName == "Antalya").ToList();

                var b = _objConvertor.Object.ObjConvertCsvToXml(a);

                _xmlSerialize.Object.XmlSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\CsvToXml.xml", b);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, " ");
            }

        }

        [TestMethod]
        public void TestMethod2()
        {
            var _csvSerialize = new Mock<ICsvSerialize>();
            _csvSerialize.Setup(foo => foo.CsvToObjDeSerialize(It.IsAny<string>())).Returns(It.Ref<List<AddressInfoCsv>>.IsAny);
            _csvSerialize.Setup(foo => foo.ObjToCsvSerialize(It.IsAny<string>(), It.Ref<List<AddressInfoCsv>>.IsAny));

            try
            {
                var a = _csvSerialize.Object.CsvToObjDeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.csv");

                a = a.OrderBy(d => d.CityName).ThenBy(f => f.DistrictName).ToList();

                _csvSerialize.Object.ObjToCsvSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\CsvToCsv.csv", a);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, " ");
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            var _xmlSerialize = new Mock<IXmlSerialize>();
            _xmlSerialize.Setup(foo => foo.XmlDeSerialize(It.IsAny<string>())).Returns(It.Ref<AddressInfo>.IsAny);
            var _csvSerialize = new Mock<ICsvSerialize>();
            _csvSerialize.Setup(foo => foo.ObjToCsvSerialize(It.IsAny<string>(), It.Ref<List<AddressInfoCsv>>.IsAny));
            var _objConvertor = new Mock<IObjectConverter>();
            _objConvertor.Setup(foo => foo.ObjConvertXmlToCsv(It.Ref<AddressInfo>.IsAny)).Returns(It.Ref<List<AddressInfoCsv>>.IsAny);

            try
            {
                var a = _xmlSerialize.Object.XmlDeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.xml");

                a.City = a.City.Where(c => c.name == "Ankara").ToArray();
                foreach (AddressInfoCity aic in a.City)
                {
                    foreach (AddressInfoCityDistrict aicd in aic.District)
                    {
                        aicd.Zip = aicd.Zip.OrderByDescending(z => z.code).ToArray();
                    }
                }

                var b = _objConvertor.Object.ObjConvertXmlToCsv(a);

                _csvSerialize.Object.ObjToCsvSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\XmlToCsv.csv", b);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, " ");
            }
        }
    }
}
