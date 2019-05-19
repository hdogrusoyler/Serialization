using Core.Serialize.Csv;
using Object;
using Serialize.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Serialize.Concrete.Csv
{
    public class CsvSerial : CsvSeri<AddressInfoCsv>, ICsvSerialize
    {
        public void CsvSerialize(string filename, List<String[]> aic)
        {
            FileStream fs = File.Create(filename);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                foreach (String[] ai in aic)
                {
                    var a = String.Join(",", ai);
                    writer.WriteLine(a);
                }
            }
            //writer.Close();
        }

        public List<String[]> CsvDeSerialize(string filename)
        {
            List<String[]> aic = new List<String[]>();
            FileStream fs = new FileStream(filename, FileMode.Open);
            using (StreamReader reader = new StreamReader(fs))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    aic.Add(line.Split(','));
                }
            }
            //reader.Close();
            return aic;
        }
    }
}
