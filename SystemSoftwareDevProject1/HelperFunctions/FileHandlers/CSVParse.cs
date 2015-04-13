using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemSoftwareDevProject1.HelperFunctions
{
    class CSVParse
    {

        private static string path = @"Resources/companylist.csv";

        public static DataTable CsvToDataSet()
        {   
            DataTable toReturn = new DataTable();
            toReturn.Columns.Add("SYMBOL", typeof(string));
            toReturn.Columns.Add("NAME",typeof(string));

            using (var file = new StreamReader(path))
            {
                //skip first line
                file.ReadLine();
                while (!file.EndOfStream)
                {
                    string line = file.ReadLine();

                    DataRow rowToAdd = toReturn.NewRow();
                    rowToAdd["SYMBOL"] = line.Split(',')[0].Trim();
                    rowToAdd["NAME"] = line.Split(',')[1].Trim().Replace("\"","");

                    toReturn.Rows.Add(rowToAdd);
                }
            }

            return toReturn;
        }

        /// <summary>
        /// Parse the CSV file included int he project into a list of strings.
        /// </summary>
        public static List<string> CsvToStringList()
        {

            List<string> toReturn = new List<string>();

            using(var file = new StreamReader(path))
            {

                while(!file.EndOfStream)
                {

                    string toAdd = file.ReadLine().Split(',')[0].Trim();

                    if (toAdd != "Symbol")
                    {
                        toReturn.Add(toAdd);
                    }
                }
            }

            return toReturn;
        }
    }
}
