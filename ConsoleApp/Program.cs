using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Object;
using Operation.Abstract;
using Operation.Concrete;
using Serialize;
using Serialize.Abstract;
using Serialize.Concrete.Csv;
using Serialize.Concrete.Xml;

namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            p.TestCase1();
            p.TestCase2();
            p.TestCase3();
        }

        public void TestCase1()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<ICsvSerialize, CsvSerial>();
            collection.AddScoped<IXmlSerialize, XmlSerial>();
            collection.AddScoped<IObjectConverter, ObjectConverter>();
            var serviceProvider = collection.BuildServiceProvider();
            var serviceCsv = serviceProvider.GetService<ICsvSerialize>();
            var serviceXml = serviceProvider.GetService<IXmlSerialize>();
            var serviceObjCon = serviceProvider.GetService<IObjectConverter>();

            var a = serviceCsv.CsvToObjDeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.csv");

            a = a.Where(c => c.CityName == "Antalya").ToList();

            var b = serviceObjCon.ObjConvertCsvToXml(a);

            serviceXml.XmlSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\CsvToXml.xml", b);

            serviceProvider.Dispose();
            Console.WriteLine("CsvToXml Done!");
        }

        public void TestCase2()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<ICsvSerialize, CsvSerial>();
            var serviceProvider = collection.BuildServiceProvider();
            var service = serviceProvider.GetService<ICsvSerialize>();
            var a = service.CsvToObjDeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.csv");

            a = a.OrderBy(d => d.CityName).ThenBy(f => f.DistrictName).ToList();

            service.ObjToCsvSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\CsvToCsv.csv", a);

            serviceProvider.Dispose();
            Console.WriteLine("CsvToCsv Done!");
        }

        public void TestCase3()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<ICsvSerialize, CsvSerial>();
            collection.AddScoped<IXmlSerialize, XmlSerial>();
            collection.AddScoped<IObjectConverter, ObjectConverter>();
            var serviceProvider = collection.BuildServiceProvider();
            var serviceCsv = serviceProvider.GetService<ICsvSerialize>();
            var serviceXml = serviceProvider.GetService<IXmlSerialize>();
            var serviceObjCon = serviceProvider.GetService<IObjectConverter>();

            var a = serviceXml.XmlDeSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\sample_data.xml");

            a.City = a.City.Where(c => c.name == "Ankara" ).ToArray();
            foreach (AddressInfoCity aic in a.City)
            {
                foreach(AddressInfoCityDistrict aicd in aic.District)
                {
                    aicd.Zip = aicd.Zip.OrderByDescending(z => z.code).ToArray();
                }
            }

            var b = serviceObjCon.ObjConvertXmlToCsv(a);

            serviceCsv.ObjToCsvSerialize("C:\\Users\\hus\\source\\repos\\c\\p\\HW05\\XmlToCsv.csv", b);

            serviceProvider.Dispose();
            Console.WriteLine("XmlToCsv Done!");
        }
    }
}
