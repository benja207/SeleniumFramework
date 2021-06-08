using System;
using System.IO;
using NUnit.Framework;

namespace mds.Framework
{
    class Data
    {
        static String header;
        static String values;

        public static void GetDataValues()
        {
            string pth = AppContext.BaseDirectory;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            String functionality = TestContext.CurrentContext.Test.ClassName.Split('.')[2];
            var reader = new StreamReader(File.OpenRead(projectPath + @"Data//" + functionality + "//" + TestContext.CurrentContext.Test.Name + ".csv"));
            int row = 0;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (row == 0)
                {
                    header = line;
                }
                else
                {
                    values = line;
                }
                row++;
            }
        }

        public static String DataValue(String data)
        {
            var arrHeaders = header.Split(',');
            int datacolumn = 0;
            bool datafound = false;
            foreach (String value in arrHeaders)
            {
                if (value != data)
                {
                    datacolumn++;
                }
                else
                {
                    datafound = true;
                    break;
                }
            }
            String datavalue = null;
            if (datafound)
            {
                var arrData = values.Split(',');
                datavalue = arrData[datacolumn];
            }

            return datavalue;
        }

    }
}
