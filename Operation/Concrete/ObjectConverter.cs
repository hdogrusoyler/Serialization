using Object;
using Operation.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Operation.Concrete
{
    public class ObjectConverter : IObjectConverter
    {
        public List<AddressInfoCsv> ObjConvertXmlToCsv(AddressInfo aicXml)
        {
            List<AddressInfoCsv> aicl = new List<AddressInfoCsv>();

            var c = aicXml.City;

            foreach (AddressInfoCity aic in c)
            {
                var CityName = aic.name;
                var CityCode = aic.code;
                var d = aic.District;

                foreach (AddressInfoCityDistrict aicd in d)
                {
                    var DistrictName = aicd.name;
                    var z = aicd.Zip;

                    foreach (AddressInfoCityDistrictZip aicdz in z)
                    {
                        var ZipCode = aicdz.code;

                        AddressInfoCsv aicv = new AddressInfoCsv();
                        aicv.CityName = CityName;
                        aicv.CityCode = CityCode;
                        aicv.DistrictName = DistrictName;
                        aicv.ZipCode = ZipCode;

                        aicl.Add(aicv);
                    }
                }
            }

            return aicl;
        }

        public AddressInfo ObjConvertCsvToXml(List<AddressInfoCsv> aiCsvList)
        {
            AddressInfo ai = new AddressInfo();

            foreach (AddressInfoCsv aic in aiCsvList)
            {
                //ai.City?.Where(i => i.name == aic.CityName && i.code == aic.CityCode) == null
                if (ai.City == null || Array.Exists(ai.City, i => i.name == aic.CityName && i.code == aic.CityCode) == false)
                {
                    List<AddressInfoCity> lc = new List<AddressInfoCity>();
                    if(ai.City != null)
                    {
                        lc.AddRange(ai.City.ToList());
                    }
                    AddressInfoCity c = new AddressInfoCity();
                    c.name = aic.CityName;
                    c.code = aic.CityCode;
                    c.District = new AddressInfoCityDistrict[] { };

                    List<AddressInfoCityDistrict> ld = new List<AddressInfoCityDistrict>();
                    if (c.District != null)
                    {
                        ld.AddRange(c.District.ToList());
                    }
                    AddressInfoCityDistrict d = new AddressInfoCityDistrict();
                    d.name = aic.DistrictName;
                    d.Zip = new AddressInfoCityDistrictZip[] { };

                    List<AddressInfoCityDistrictZip> lz = new List<AddressInfoCityDistrictZip>();
                    if(d.Zip != null)
                    {
                        lz.AddRange(d.Zip.ToList());
                    }
                    AddressInfoCityDistrictZip z = new AddressInfoCityDistrictZip();
                    z.code = aic.CityCode;

                    lz.Add(z);
                    d.Zip = lz.ToArray();
                    
                    ld.Add(d);
                    c.District = ld.ToArray();

                    lc.Add(c);
                    ai.City = lc.ToArray();
                    
                }
                else
                {
                    foreach(AddressInfoCity aicv in ai.City)
                    {
                        if(aicv.name == aic.CityName && aicv.code == aic.CityCode)
                        {
                            if (aicv.District == null || Array.Exists(aicv.District, l => l.name == aic.DistrictName) == false)
                            {
                                List<AddressInfoCityDistrict> ld = new List<AddressInfoCityDistrict>();
                                if (aicv.District != null)
                                {
                                    ld.AddRange(aicv.District.ToList());
                                }
                                AddressInfoCityDistrict d = new AddressInfoCityDistrict();
                                d.name = aic.DistrictName;
                                d.Zip = new AddressInfoCityDistrictZip[] { };

                                List<AddressInfoCityDistrictZip> lz = new List<AddressInfoCityDistrictZip>();
                                if (d.Zip != null)
                                {
                                    lz.AddRange(d.Zip.ToList());
                                }
                                AddressInfoCityDistrictZip z = new AddressInfoCityDistrictZip();
                                z.code = aic.ZipCode;

                                lz.Add(z);
                                d.Zip = lz.ToArray();

                                ld.Add(d);
                                aicv.District = ld.ToArray();
                            }
                            else
                            {
                                foreach (AddressInfoCityDistrict aicdv in aicv.District)
                                {
                                    if (aicdv.name == aic.DistrictName)
                                    {
                                        if (aicdv.Zip == null || Array.Exists(aicdv.Zip, m => m.code == aic.ZipCode) == false)
                                        {
                                            List<AddressInfoCityDistrictZip> lz = new List<AddressInfoCityDistrictZip>();
                                            if (aicdv.Zip != null)
                                            {
                                                lz.AddRange(aicdv.Zip.ToList());
                                            }
                                            AddressInfoCityDistrictZip z = new AddressInfoCityDistrictZip { };
                                            z.code = aic.ZipCode;
                                            lz.Add(z);

                                            aicdv.Zip = lz.ToArray();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                
            }

            return ai;
        }
    }
}
