using Core.Object;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Core.Serialize.Csv
{
    public class CsvSeri<T> : ICsvSerial<T>
        where T : class, IObject, new()
    {
        public void ObjToCsvSerialize(string filename, List<T> aic)
        {
            List<String[]> list = new List<string[]>();

            foreach (T AddInf in aic)
            {
                List<String> l = new List<string>();
                PropertyInfo[] properties = typeof(T).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    l.Add(property.GetValue(AddInf).ToString());
                }
                list.Add(l.ToArray());

            }

            FileStream fs = File.Create(filename);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                foreach (String[] i in list)
                {
                    var a = String.Join(",", i);
                    writer.WriteLine(a);
                }
            }
        }

        public List<T> CsvToObjDeSerialize(string filename)
        {
            List<T> AddInfList = new List<T>();

            FileStream fs = new FileStream(filename, FileMode.Open);
            using (StreamReader reader = new StreamReader(fs))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    String[] aic = line.Split(',');

                    T AddInf = new T();
                    int i = 0;
                    PropertyInfo[] properties = typeof(T).GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        property.SetValue(AddInf, aic[i]);
                        ++i;
                    }
                    AddInfList.Add(AddInf);
                }
            }
            return AddInfList;
        }
    }
}
